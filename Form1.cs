using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;
using System.IO;

namespace MusicBeePlugin {
    public partial class VGMV: Form {
        public Plugin.MusicBeeApiInterface mApi;


        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection mfont = new PrivateFontCollection();
        private PrivateFontCollection rfont = new PrivateFontCollection();

        Font Monfont;
        Font Riffont;

        public VGMV(Plugin.MusicBeeApiInterface pApi) {
            mApi = pApi;
            //--------------------------------------------------------
            byte[] Montserrat = Properties.Resources.Montserrat_VariableFont_wght;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(Montserrat.Length);
            System.Runtime.InteropServices.Marshal.Copy(Montserrat, 0, fontPtr, Montserrat.Length);
            uint dummy = 0;
            mfont.AddMemoryFont(fontPtr, Properties.Resources.Montserrat_VariableFont_wght.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.Montserrat_VariableFont_wght.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            Monfont = new Font(mfont.Families[0], 16.0F);
            //--------------------------------------------------------
            byte[] Riffic = Properties.Resources.RifficFree_Bold;
            IntPtr fontPtr2 = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(Riffic.Length);
            System.Runtime.InteropServices.Marshal.Copy(Riffic, 0, fontPtr2, Riffic.Length);
            uint dummy2 = 0;
            rfont.AddMemoryFont(fontPtr2, Properties.Resources.RifficFree_Bold.Length);
            AddFontMemResourceEx(fontPtr2, (uint)Properties.Resources.RifficFree_Bold.Length, IntPtr.Zero, ref dummy2);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr2);

            Riffont = new Font(rfont.Families[0], 16.0F);
            //--------------------------------------------------------
            this.KeyPreview = true;
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(VGMV_KeyDown);

            LoadImages();

        }
        Image[] images = new Image[66];

        private void LoadImages() {
            Image image = Properties.Resources.alienDanceFast;
            FrameDimension dimension = new FrameDimension(image.FrameDimensionsList[0]);

            for (int i = 0; i < 65; i++) {
                image.SelectActiveFrame(dimension, i);
                images[i] = new Bitmap(image);
            }
            //pictureBox3.Image = images[1];
        }

        private SettingsManager _settingsManager = new SettingsManager();

        int i = 0;

        int startingPlayer = 1;
        int player1Needs = 2;
        int player2Needs = 2;

        int startTime = 150000; //ms
        int timePass1 = 2000; //ms
        int timePass2 = 2000; //ms

        Color P1Col = Color.Green;
        Color P2Col = Color.Blue;
        bool showHistory = true;

        //not user changed
        bool shouldCountTime = false;
        int timeP1;
        int timeP2;
        public int P1TimeAtNew;
        public int P2TimeAtNew;
        int player; //starting player
        bool GAMEOVER = false;
        bool shouldLoop = true;
        bool shouldShuffle = true;
        bool singlePlayer = false;

        Score p1Score = new Score();
        Score p2Score = new Score();

        Font smallerFont;
        Font biggerFont;

        public void VGMV_Load(object sender, EventArgs e) {
            InitTimer();

            smallerFont = new Font(rfont.Families[0], 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            biggerFont = new Font(rfont.Families[0], 30F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            Font mFont12 = new Font(mfont.Families[0], 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            Font rFont2175 = new Font(rfont.Families[0], 21.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            //riffic
            ScoreP2.Font = rFont2175;
            ScoreP1.Font = rFont2175;
            TimerP1.Font = rFont2175;
            TimerP2.Font = rFont2175;
            Player1Name.Font = rFont2175;
            Player2Name.Font = rFont2175;
            restartButton.Font = new Font(rfont.Families[0], 15.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            settingsButton.Font = new Font(rfont.Families[0], 15.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            LosingPlayerLabel.Font = new Font(rfont.Families[0], 25.00F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            Start.Font = new Font(rfont.Families[0], 36.00F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

            //montserrat
            songName.Font = new Font(mfont.Families[0], 20.00F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            listBox2.Font = new Font(mfont.Families[0], 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

            DisplayHistoryCheckBox.Font = mFont12;
            label8.Font = mFont12;
            P2IncrementUpDown.Font = mFont12;
            label5.Font = mFont12;
            label4.Font = mFont12;
            P2PointsToPassUpDown.Font = mFont12;
            P1PointsToPassUpDown.Font = mFont12;
            P2ChangeColorButton.Font = mFont12;
            P1ChangeColorButton.Font = mFont12;
            label2.Font = mFont12;
            label1.Font = mFont12;
            P1IncrementUpDown.Font = mFont12;
            Secs.Font = mFont12;
            Mins.Font = mFont12;
            export.Font = mFont12;

            P2StartsRadioButton.Font = new Font(mfont.Families[0], 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            P1StartsRadioButton.Font = new Font(mfont.Families[0], 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            listBox1.Font = new Font(mfont.Families[0], 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));



            trackBar1_Set();

            pictureBox3.Visible = false;
            pictureBox4.Visible = false;

            UpdateSettings();
            player = startingPlayer;

            timeP1 = startTime;
            timeP2 = startTime;

            TimerP1.Font = smallerFont;
            TimerP2.Font = smallerFont;
            Player1Name.Font = smallerFont;
            Player2Name.Font = smallerFont;


            updateTimers();

            groupBox1.Hide();
            updateText(ScoreP1, p1Score._score.ToString());
            updateText(ScoreP2, p2Score._score.ToString());

            ScoreP1.Hide();
            ScoreP2.Hide();
            pictureBox1.Hide();
            TimerP1.Hide();
            TimerP2.Hide();

            listBox1.DrawMode = DrawMode.OwnerDrawVariable;
            listBox1.MeasureItem += listBox1_MeasureItem;
            listBox1.DrawItem += listBox1_DrawItem;

            listBox2.DrawMode = DrawMode.OwnerDrawVariable;
            listBox2.MeasureItem += listBox2_MeasureItem;
            listBox2.DrawItem += listBox2_DrawItem;

            p1Score.reset();
            p2Score.reset();

            pictureBox2.Hide();
            pictureBox5.Hide();
            LosingPlayerLabel.Hide();
            Player1Name.Hide();
            Player2Name.Hide();

            if (showHistory) {
                listBox1.Show();
                listBox2.Show();
            }
            else {
                listBox1.Hide();
                listBox2.Hide();
            }


        }



        private void UpdateSettings() {
            if (_settingsManager.LoadSettings()) {
                startingPlayer = _settingsManager.P1Start ? 1 : 2;
                if (startingPlayer == 1) {
                    P1StartsRadioButton.Checked = true;
                } else {
                    P2StartsRadioButton.Checked = true;
                }

                Mins.Value = _settingsManager.Minutes;
                Secs.Value = _settingsManager.Seconds;

                startTime = (int)(Mins.Value * 60 + Secs.Value) * 1000;

                updateText(Player1Name, _settingsManager.P1Name);
                P1NameTextBox.Text = _settingsManager.P1Name;

                P1Col = _settingsManager.P1Color;
                P1ChangeColorButton.ForeColor = P1Col;
                colorDialog1.Color = P1Col;

                player1Needs = _settingsManager.P1PointsToPass;
                P1PointsToPassUpDown.Value = player1Needs;

                timePass1 = (int)_settingsManager.P1TimeIncrement * 1000;
                P1IncrementUpDown.Value = (decimal)_settingsManager.P1TimeIncrement;

                updateText(Player2Name, _settingsManager.P2Name);
                P2NameTextBox.Text = _settingsManager.P2Name;

                P2Col = _settingsManager.P2Color;
                P2ChangeColorButton.ForeColor = P2Col;
                colorDialog2.Color = P2Col;

                player2Needs = _settingsManager.P2PointsToPass;
                P2PointsToPassUpDown.Value = player2Needs;

                timePass2 = (int)_settingsManager.P2TimeIncrement * 1000;
                P2IncrementUpDown.Value = (decimal)_settingsManager.P2TimeIncrement;

                showHistory = _settingsManager.DisplayHistory;
                DisplayHistoryCheckBox.Checked = showHistory;


                shouldLoop = _settingsManager.LoopPlaylist;
                LoopPlaylistCheckBox.Checked = shouldLoop;

                shouldShuffle = _settingsManager.ShufflePlaylist;
                ShufflePlaylistCheckBox.Checked = shouldShuffle;

                singlePlayer = _settingsManager.SinglePlayer;
                SingePlayerCheckBox.Checked = singlePlayer;


                if (showHistory) {
                    listBox1.Show();
                    listBox2.Show();
                }
                else {
                    listBox1.Hide();
                    listBox2.Hide();
                }

                updateColors();
            }
        }

        public void start() {
            //string startingSong = mApi.NowPlayingList_GetListFileUrl(0); //gets current song
            //mApi.NowPlayingList_QueueLast(startingSong); //plays the first song last in the queue
            //TODO:
            //make loop and repeat automatically update if the setting is changed in MusicBee
            if (shouldLoop) {
                mApi.Player_SetRepeat(Plugin.RepeatMode.All); //loop playlist
            }
            else {
                mApi.Player_SetRepeat(Plugin.RepeatMode.None); //dont loop playlist
            }
            if (shouldShuffle) {
                shuffleList();

                mApi.Player_PlayNextTrack(); //play next track (random not first song)

                shuffleList(); //now first song is randomly in there
            }
            else {
                mApi.Player_SetShuffle(false);
                mApi.Player_SetPosition(0);
            }


            //shuffleList();

            GAMEOVER = false;

            p1Score.reset();
            p2Score.reset();

            incPoints(0); // to update text

            p1Score.reset();
            p2Score.reset();

            listBox1.Items.Clear();
            listBox2.Items.Clear();

            pictureBox3.Visible = false;
            pictureBox4.Visible = false;

            startTime = (int)(Mins.Value * 60 + Secs.Value) * 1000;
            timePass1 = (int)(P1IncrementUpDown.Value * 1000);
            timePass2 = (int)(P2IncrementUpDown.Value * 1000);

            player = startingPlayer;

            //update fonts
            TimerP1.ForeColor = P1Col;
            TimerP2.ForeColor = P2Col;
            Player1Name.ForeColor = P1Col;
            Player2Name.ForeColor = P2Col;

            if (player == 2) {
                TimerP1.Font = smallerFont;
                TimerP2.Font = biggerFont;
            }
            else {
                TimerP2.Font = smallerFont;
                TimerP1.Font = biggerFont;
            }

            //set times
            timeP1 = startTime;
            timeP2 = startTime;
            P1TimeAtNew = timeP1;
            P2TimeAtNew = timeP2;
            shouldCountTime = true;

            //show hide stuff
            ScoreP1.Show();
            ScoreP2.Show();
            songName.Hide();
            pictureBox1.Hide();
            updateColors();
            updateTimers();
            TimerP1.Show();
            TimerP2.Show();
            groupBox1.Hide();
            LosingPlayerLabel.Hide();
            pictureBox2.Hide();
            pictureBox5.Hide();
            Start.Hide();
            Player1Name.Show();
            Player2Name.Show();


            if (showHistory) {
                listBox1.Show();
                listBox2.Show();
            }
            else {
                listBox1.Hide();
                listBox2.Hide();
            }


            if (mApi.Player_GetPlayState() == Plugin.PlayState.Paused) {
                mApi.Player_PlayPause(); //unpause if needed
            }

            if (singlePlayer) {
                listBox2.Hide();
                ScoreP2.Hide();
                Player2Name.Hide();
                TimerP2.Hide();
            }
            else {
                if (showHistory) { listBox2.Show(); }
                ScoreP2.Show();
                Player2Name.Show();
                TimerP2.Show();
            }
        }

        public void incPoints(int pointGain) {
            if (player == 1 || singlePlayer) {
                p1Score.intPoints(pointGain, player1Needs, singlePlayer);
                if (p1Score._score % player1Needs == 0 && pointGain > 0) {
                    TimerP1.Font = smallerFont;
                    TimerP2.Font = biggerFont;
                    player = 2;
                    timeP1 += timePass1;
                }

            }
            else {
                p2Score.intPoints(pointGain, player2Needs, singlePlayer);
                if (p2Score._score % player2Needs == 0 && pointGain > 0) {
                    TimerP2.Font = smallerFont;
                    TimerP1.Font = biggerFont;
                    player = 1;
                    timeP2 += timePass2;
                }
            }
            if (singlePlayer) {
                TimerP1.Font = biggerFont;
            }
            updateText(ScoreP1, p1Score._score.ToString() + "\n(" + (p1Score._score % player1Needs).ToString() + "/" + player1Needs.ToString() + ")");
            updateText(ScoreP2, p2Score._score.ToString() + "\n(" + (p2Score._score % player2Needs).ToString() + "/" + player2Needs.ToString() + ")");
            updateTimers();
            updateColors();

        }

        public void updateTimers() {
            int P1Min = (int)Math.Ceiling(timeP1 / 1000.0) / 60;
            int P2Min = (int)Math.Ceiling(timeP2 / 1000.0) / 60;
            int P1Sec = (int)Math.Ceiling(timeP1 / 1000.0) % 60;
            int P2Sec = (int)Math.Ceiling(timeP2 / 1000.0) % 60;

            string P1Seconds = P1Sec.ToString();
            string P2Seconds = P2Sec.ToString();

            //add leading 0s
            if (P1Sec < 10) { P1Seconds = "0" + P1Sec.ToString(); }
            if (P2Sec < 10) { P2Seconds = "0" + P2Sec.ToString(); }
            updateText(TimerP1, P1Min.ToString() + ":" + P1Seconds);
            updateText(TimerP2, P2Min.ToString() + ":" + P2Seconds);
        }

        public void updateColors() {
            if (player == 1 || singlePlayer) {
                ScoreP1.ForeColor = P1Col;
                ScoreP2.ForeColor = Color.Black;
            }
            else {
                ScoreP1.ForeColor = Color.Black;
                ScoreP2.ForeColor = P2Col;
            }
        }


        //timer
        private Timer timer1;
        public void InitTimer() {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 50; // in miliseconds
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e) {

            i = i % 65;
            pictureBox3.Image = images[(i * 2) % 65];
            pictureBox4.Image = images[(i * 2) % 65];
            i++;



            gameOverCheck(false);
            updateTimers();
        }
        //end timer

        private void gameOverCheck(bool quickEnd) {
            int A = mApi.Player_GetPosition(); //song playlength in ms
            if (A <= 700) {
                A = 0;
            }

            if ((!GAMEOVER && shouldCountTime && mApi.Player_GetPlayState() == Plugin.PlayState.Playing) || quickEnd) { //if time should move AND song playing,

                if (player == 1 || singlePlayer) { //tick
                    timeP1 = P1TimeAtNew - A;
                }
                else {
                    timeP2 = P2TimeAtNew - A;
                }
                if (timeP1 <= -1000 || timeP2 <= -1000 || quickEnd) {
                    GAMEOVER = true;
                    timeP1 = Math.Max(timeP1, 0);
                    timeP2 = Math.Max(timeP2, 0);
                    showSong(true);
                    pictureBox2.Show();
                    pictureBox5.Show();
                    LosingPlayerLabel.Show();
                    if (timeP1 <= 0) {
                        LosingPlayerLabel.Text = Player1Name.Text + " Lost";
                    }
                    else {
                        LosingPlayerLabel.Text = Player2Name.Text + " Lost";
                    }
                    int sumP1 = p1Score._zeroPoint + p1Score._onePoint + p1Score._twoPoint;
                    int sumP2 = p2Score._zeroPoint + p2Score._onePoint + p2Score._twoPoint;
                    updateText(ScoreP1, p1Score._score + "\n" + p1Score._twoPoint + "/" + p1Score._onePoint + "/" + p1Score._zeroPoint + " - " + sumP1);
                    updateText(ScoreP2, p2Score._score + "\n" + p2Score._twoPoint + "/" + p2Score._onePoint + "/" + p2Score._zeroPoint + " - " + sumP2);

                }
            }
        }


        public void showSong(bool showBoxes) {
            if (showBoxes) {
                songName.Show();
                pictureBox1.Show();
                shouldCountTime = false;
            }

            if (!showBoxes && !GAMEOVER) { //dont update if game is not over, and hide the game 
                pictureBox1.Hide();
                songName.Hide();
                return;
            }
            if (!showBoxes && GAMEOVER) { //hide the D: if the game is over and a new track plays
                pictureBox2.Hide();
                pictureBox5.Hide();
                LosingPlayerLabel.Hide();
            }

            try {

                Plugin.PictureLocations temp1;
                string temp2;
                byte[] img;
                mApi.Library_GetArtworkEx(mApi.NowPlaying_GetFileUrl(), 0, true, out temp1, out temp2, out img);
                Bitmap Art = (Bitmap)new ImageConverter().ConvertFrom(img);

                pictureBox1.Image = Art;
            }
            catch {
                pictureBox1.Image = Properties.Resources.nocover;
            }

            updateText(songName, mApi.NowPlaying_GetFileTag(Plugin.MetaDataType.TrackTitle) + "\n" + mApi.NowPlaying_GetFileTag(Plugin.MetaDataType.Album));

        }

        public void updateText(Label label, string textChange) {
            label.Text = textChange;
            label.TextAlign = ContentAlignment.MiddleCenter;
        }

        public void shuffleList() {
            mApi.Player_SetShuffle(false);
            mApi.Player_SetShuffle(true); // shuffles current list
        }




        public class MyListBoxItem {
            public MyListBoxItem(Color c, string m) {
                ItemColor = c;
                Message = m;
            }
            public Color ItemColor { get; set; }
            public string Message { get; set; }
        }

        public void addSong(int value) {
            string album = mApi.NowPlaying_GetFileTag(Plugin.MetaDataType.Album);
            string track = mApi.NowPlaying_GetFileTag(Plugin.MetaDataType.TrackTitle);
            string finalIn = track + "\n" + album + "\n";

            Color toBeAss = Color.Tomato;
            if (value == 1) {
                toBeAss = Color.DarkOrange;
            }
            else if (value == 2) {
                toBeAss = Color.Green;
            }
            if (!showHistory) {

            }
            if (player == 1 || singlePlayer) {
                listBox1.DrawMode = DrawMode.OwnerDrawVariable;

                listBox1.Items.Add(new MyListBoxItem(Color.Transparent, "empty line"));
                listBox1.Items.Add(new MyListBoxItem(toBeAss, finalIn));

                listBox1.TopIndex = listBox1.Items.Count - 1;
                listBox1.Refresh();

                //this.objectListView1.AddObject(new MyListBoxItem(toBeAss, finalIn));
            }
            else {
                listBox2.DrawMode = DrawMode.OwnerDrawVariable;

                listBox2.Items.Add(new MyListBoxItem(Color.Transparent, "empty line"));
                listBox2.Items.Add(new MyListBoxItem(toBeAss, finalIn));

                listBox2.TopIndex = listBox2.Items.Count - 1;
                listBox2.Refresh();

            }

        }
        private void listBox1_DrawItem(object sender, DrawItemEventArgs e) {
            try {
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    e = new DrawItemEventArgs(e.Graphics,
                                              e.Font,
                                              e.Bounds,
                                              e.Index,
                                              e.State ^ DrawItemState.Selected,
                                              e.ForeColor,
                                              Color.Transparent);//Choose the color
                MyListBoxItem item = listBox1.Items[e.Index] as MyListBoxItem;
                e.DrawBackground();
                e.DrawFocusRectangle();
                e.Graphics.DrawString(item.Message, e.Font, new SolidBrush(item.ItemColor), e.Bounds);
            }
            catch { }
        }

        private void listBox2_DrawItem(object sender, DrawItemEventArgs e) {
            try {
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    e = new DrawItemEventArgs(e.Graphics,
                                              e.Font,
                                              e.Bounds,
                                              e.Index,
                                              e.State ^ DrawItemState.Selected,
                                              e.ForeColor,
                                              Color.Transparent);//Choose the color
                MyListBoxItem item = listBox2.Items[e.Index] as MyListBoxItem;
                e.DrawBackground();
                e.DrawFocusRectangle();
                e.Graphics.DrawString(item.Message, e.Font, new SolidBrush(item.ItemColor), e.Bounds);
            }
            catch { }
        }

        public void handleNextSong() {

            mApi.Player_PlayNextTrack();
            shouldCountTime = true;

            songName.Hide();
            pictureBox1.Hide();

            P1TimeAtNew = timeP1;
            P2TimeAtNew = timeP2;
        }

        public void VGMV_KeyDown(object sender, KeyEventArgs e) {
            //Typing in text box while settings is open will press the keys so better to have disabled until closed again
            if (groupBox1.Visible) {
                e.Handled = false;
                return;
            }

            if (e.KeyCode == Keys.P) {
                pictureBox3.Visible = !pictureBox3.Visible;
                pictureBox4.Visible = !pictureBox4.Visible;

            }

            if (!GAMEOVER) {
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.J || e.KeyCode == Keys.A) { //should next song 1 point
                    addSong(1);
                    incPoints(1);

                    handleNextSong();
                }
                else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.L || e.KeyCode == Keys.D) { //should next song 2 point
                    addSong(2);
                    incPoints(2);

                    handleNextSong();
                }
                else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.I || e.KeyCode == Keys.W) { //pause song, then display track name and art
                    showSong(true);
                }
                else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.K || e.KeyCode == Keys.S) { //skip song
                    addSong(0);
                    incPoints(0);

                    handleNextSong();
                }
                else if (e.KeyCode == Keys.Space || e.KeyCode == Keys.M) {
                    mApi.Player_PlayPause();
                }
                else if (e.KeyCode == Keys.H) {
                    mApi.Player_SetPosition(0);
                    if (mApi.Player_GetPlayState() == Plugin.PlayState.Paused) {
                        mApi.Player_PlayPause();
                    }
                }
                else if (e.KeyCode == Keys.T) {
                    gameOverCheck(true);
                }
                else if (e.Control && e.Shift && e.KeyCode == Keys.R) {
                    //reset settings (with pop-up?)
                    _settingsManager.SetDefaultSettings();
                    UpdateSettings();
                }
            }
            else {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.K || e.KeyCode == Keys.S) { //skip song
                    mApi.Player_PlayNextTrack();
                }
                else if(e.KeyCode == Keys.Space || e.KeyCode == Keys.M) {
                    mApi.Player_PlayPause();
                }
            }


            e.Handled = false;
        }



        //boxes update below

        private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e) {
            try {
                MyListBoxItem item = listBox1.Items[e.Index] as MyListBoxItem;
                e.ItemHeight = (int)e.Graphics.MeasureString(item.Message, listBox1.Font, listBox1.Width, new StringFormat(StringFormatFlags.MeasureTrailingSpaces)).Height;
            }
            catch { e.ItemHeight = 10; }
        }
        private void listBox2_MeasureItem(object sender, MeasureItemEventArgs e) {
            try {
                MyListBoxItem item = listBox2.Items[e.Index] as MyListBoxItem;
                e.ItemHeight = (int)e.Graphics.MeasureString(item.Message, listBox2.Font, listBox2.Width, new StringFormat(StringFormatFlags.MeasureTrailingSpaces)).Height;
            }
            catch { e.ItemHeight = 10; }
        }
        public void Start_Click_1(object sender, EventArgs e) {
            start();
        }

        private void restartButton_Click(object sender, EventArgs e) {
            start();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            showHistory = DisplayHistoryCheckBox.Checked;
            if (showHistory) {
                listBox1.Show();
                listBox2.Show();
            }
            else {
                listBox1.Hide();
                listBox2.Hide();
            }
            _settingsManager.DisplayHistory = showHistory;
            _settingsManager.SaveSettings();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            startingPlayer = 1;
            _settingsManager.P1Start = true;
            _settingsManager.SaveSettings();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {
            startingPlayer = 2;
            _settingsManager.P1Start = false;
            _settingsManager.SaveSettings();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (colorDialog1.ShowDialog() == DialogResult.OK) {
                P1ChangeColorButton.ForeColor = colorDialog1.Color;
                P1Col = colorDialog1.Color;
                updateColors();
                _settingsManager.P1Color = P1Col;
                _settingsManager.SaveSettings();
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            if (colorDialog2.ShowDialog() == DialogResult.OK) {
                P2ChangeColorButton.ForeColor = colorDialog2.Color;
                P2Col = colorDialog2.Color;
                updateColors();
                _settingsManager.P2Color = P2Col;
                _settingsManager.SaveSettings();
            }
        }

        private void settingsButton_Click(object sender, EventArgs e) {
            if (groupBox1.Visible) { 
                groupBox1.Hide();
            }
            else {
                groupBox1.Show();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            player1Needs = (int)P1PointsToPassUpDown.Value;
            _settingsManager.P1PointsToPass = player1Needs;
            _settingsManager.SaveSettings();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e) {
            player2Needs = (int)P2PointsToPassUpDown.Value;
            _settingsManager.P2PointsToPass = player2Needs;
            _settingsManager.SaveSettings();
        }

        private void Mins_ValueChanged(object sender, EventArgs e) {
            startTime = (int)(Mins.Value * 60 + Secs.Value) * 1000;
            _settingsManager.Minutes = (int)Mins.Value;
            _settingsManager.SaveSettings();
        }

        private void Secs_ValueChanged(object sender, EventArgs e) {
            startTime = (int)(Mins.Value * 60 + Secs.Value) * 1000;
            _settingsManager.Seconds = (int)Secs.Value;
            _settingsManager.SaveSettings();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e) {
            timePass1 = (int)(P1IncrementUpDown.Value * 1000);
            _settingsManager.P1TimeIncrement = (float) P1IncrementUpDown.Value;
            _settingsManager.SaveSettings();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e) {
            timePass2 = (int)(P2IncrementUpDown.Value * 1000);
            _settingsManager.P2TimeIncrement = (float) P2IncrementUpDown.Value;
            _settingsManager.SaveSettings();
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e) {
            shouldLoop = LoopPlaylistCheckBox.Checked;
            _settingsManager.LoopPlaylist = shouldLoop;
            _settingsManager.SaveSettings();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) {
            shouldShuffle = ShufflePlaylistCheckBox.Checked;
            _settingsManager.ShufflePlaylist = shouldShuffle;
            _settingsManager.SaveSettings();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e) {
            singlePlayer = SingePlayerCheckBox.Checked;
            if (singlePlayer) {
                listBox2.Hide();
                ScoreP2.Hide();
                TimerP2.Hide();
            }
            else {
                if (showHistory) { listBox2.Show(); }
                ScoreP2.Show();
                TimerP2.Show();
            }
            _settingsManager.SinglePlayer = singlePlayer;
            _settingsManager.SaveSettings();
        }

        private void editMakeFile(string path, string text) {
            string name = path + "\\VGMV " + DateTime.Now.ToString().Replace("/", "-").Replace(":", "-") + ".csv";

            Clipboard.SetText(name);

            if (!File.Exists(name)) { // If file does not exists
                File.Create(name).Close(); // Create file
                using (StreamWriter sw = File.AppendText(name)) {
                    sw.WriteLine(text); // Write text to .txt file
                }
            }
        }

        private void export_Click(object sender, EventArgs e) {

            if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel) {
                return;
            }

            string output = "Player,Game,Track,Points\n";
            output += getExportFromListbox(listBox1, 1);
            
            if (!singlePlayer) {
                output += "\n----------\n";
                output += getExportFromListbox(listBox2, 2);
            }
            editMakeFile(folderBrowserDialog1.SelectedPath, output.TrimEnd('\r', '\n'));
        }

        private string getExportFromListbox(ListBox listBox, int player) {
            string output = "";
            foreach (MyListBoxItem item in listBox.Items) {
                if (item.Message != "empty line") {
                    //string finalIn = track + "\n" + album + "\n";

                    string track = item.Message.Split('\n')[0];
                    string album = item.Message.Split('\n')[1];
                    string score = item.ItemColor.Equals(Color.Green) ? "2" : item.ItemColor.Equals(Color.DarkOrange) ? "1" : "0";

                    string format1 = String.Format("Player {3},\"{1}\",\"{0}\",\"{2}\"\n", track, album, score, player);
                    output += format1;
                }
            }

            return output.TrimEnd('\r', '\n'); //remove ending newline
        }

        private string getExportFromListboxFancy(ListBox listBox) {
            string output = "";
            int maxLength = 0;
            int maxAlbum = 0;

            foreach (MyListBoxItem item in listBox.Items) {
                if (item.Message != "empty line") {
                    maxLength = Math.Max(maxLength, item.Message.Split('\n')[0].Length);
                    maxAlbum  = Math.Max(maxAlbum,  item.Message.Split('\n')[1].Length);
                }
            }
            foreach (MyListBoxItem item in listBox.Items) {
                if (item.Message != "empty line") {
                    //string finalIn = track + "\n" + album + "\n";

                    string track = item.Message.Split('\n')[0];
                    string album = item.Message.Split('\n')[1];
                    string score = item.ItemColor.Equals(Color.Green) ? "2" : item.ItemColor.Equals(Color.DarkOrange) ? "1" : "0";

                    string format1 = String.Format("{{0,-{0}}} {{1,-{1}}} ({{2}}) \n", maxLength + 2, maxAlbum + 2);
                    output += String.Format(format1, track, album, score);
                }
            }

            return output.TrimEnd('\r', '\n'); //remove ending newline
        }

        private void trackBar1_Scroll(object sender, EventArgs e) {
            mApi.Player_SetVolume((float) trackBar1.Value / 100);
        }
        public void trackBar1_Set() {
            trackBar1.Value = (int) (mApi.Player_GetVolume() * 100);
        }

        private void P1NameTextBox_TextChanged(object sender, EventArgs e)
        {
            updateText(Player1Name, P1NameTextBox.Text);
            _settingsManager.P1Name = P1NameTextBox.Text;
            _settingsManager.SaveSettings();
        }

        private void P2NameTextBox_TextChanged(object sender, EventArgs e)
        {
            updateText(Player2Name, P2NameTextBox.Text);
            _settingsManager.P2Name = P2NameTextBox.Text;
            _settingsManager.SaveSettings();
        }

        private void groupBox1_Enter(object sender, EventArgs e) {

        }

    }



    public class Score {
        public int _score { get; set; }
        public int _twoPoint { get; set; }
        public int _onePoint { get; set; }
        public int _zeroPoint { get; set; }

        public void reset() {
            _score = 0;
            _twoPoint = 0;
            _onePoint = 0;
            _zeroPoint = 0;
        }

        public void intPoints(int points, int required, bool singlePlayer) {
            if (singlePlayer) {
                _score += points;
            }
            else {
                _score += Math.Min(points, required - (_score % required));
            }

            switch (points){
                case 0:
                    _zeroPoint++;
                    break;
                case 1:
                    _onePoint++;
                    break;
                case 2:
                    _twoPoint++;
                    break;
                default:
                    break;
            }
            return;
        }
    }
}
