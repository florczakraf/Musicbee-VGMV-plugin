﻿using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

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
        }

        int startingPlayer = 1;
        int player1Needs = 2;
        int player2Needs = 2;

        int startTime = 150000; //ms
        int timePass1 = 4000; //ms
        int timePass2 = 4000; //ms

        Color P1Col = Color.Green;
        Color P2Col = Color.Blue;
        bool showHistory = true;

        //not user changed
        bool shouldCountTime = false;
        int timeP1;
        int timeP2;
        public int P1TimeAtNew;
        public int P2TimeAtNew;
        int Player1Score = 0;
        int Player2Score = 0;
        int player; //starting player
        bool GAMEOVER = false;
        bool shouldLoop = true;

        Font smallerFont;
        Font biggerFont;

        public void VGMV_Load(object sender, EventArgs e) {
            InitTimer();
            smallerFont = new Font(rfont.Families[0], 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            biggerFont  = new Font(rfont.Families[0], 30F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            Font mFont12 = new Font(mfont.Families[0], 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            Font rFont2175 = new Font(rfont.Families[0], 21.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            //riffic
            ScoreP2.Font        = rFont2175;
            ScoreP1.Font        = rFont2175;
            TimerP1.Font        = rFont2175;
            TimerP2.Font        = rFont2175;
            restartButton.Font  = new Font(rfont.Families[0], 15.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            settingsButton.Font = new Font(rfont.Families[0], 15.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            label6.Font         = new Font(rfont.Families[0], 36F,    FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            Start.Font          = new Font(rfont.Families[0], 36F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

            //montserrat
            songName.Font       = new Font(mfont.Families[0], 21.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            listBox2.Font       = new Font(mfont.Families[0], 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

            displayHistory.Font = mFont12;
            label8.Font         = mFont12;
            numericUpDown4.Font = mFont12;
            label5.Font         = mFont12;
            label4.Font         = mFont12;
            numericUpDown2.Font = mFont12;
            numericUpDown1.Font = mFont12;
            button2.Font        = mFont12;
            button1.Font        = mFont12;
            label2.Font         = mFont12;
            label1.Font         = mFont12;
            numericUpDown3.Font = mFont12;
            Secs.Font           = mFont12;
            Mins.Font           = mFont12;

            radioButton2.Font   = new Font(mfont.Families[0], 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            radioButton1.Font   = new Font(mfont.Families[0], 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            listBox1.Font       = new Font(mfont.Families[0], 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));






            showHistory = displayHistory.Checked;
            player = startingPlayer;
            timeP1 = startTime;
            timeP2 = startTime;

            TimerP1.Font = smallerFont;
            TimerP2.Font = smallerFont;


            updateTimers();

            Player1Score = 0;
            Player2Score = 0;

            groupBox1.Hide();

            updateText(ScoreP1, Player1Score.ToString());
            updateText(ScoreP2, Player2Score.ToString());

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

            pictureBox2.Hide();
            label6.Hide();
            if (showHistory) {
                listBox1.Show();
                listBox2.Show();
            }
            else {
                listBox1.Hide();
                listBox2.Hide();
            }
        }

        public void incPoints(int pointGain) {
            if(player == 1) {
                Player1Score += Math.Min(pointGain, player1Needs-(Player1Score % player1Needs));
                if (Player1Score % player1Needs == 0) {
                    TimerP1.Font = smallerFont;
                    TimerP2.Font = biggerFont;

                    player = 2;
                    timeP1 += timePass1;
                }
            }
            else {
                Player2Score += Math.Min(pointGain, player2Needs-(Player2Score % player2Needs));
                if (Player2Score % player2Needs == 0) {
                    TimerP2.Font = smallerFont;
                    TimerP1.Font = biggerFont;

                    player = 1;
                    timeP2 += timePass2;
                }
            }
            updateText(ScoreP1, Player1Score.ToString());
            updateText(ScoreP2, Player2Score.ToString());
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
            if (player == 1) {
                ScoreP1.ForeColor = P1Col;
                ScoreP2.ForeColor = Color.Black;
            }
            else {
                ScoreP1.ForeColor = Color.Black;
                ScoreP2.ForeColor = P2Col;
            }
        }

        private Timer timer1;
        public void InitTimer() {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 50; // in miliseconds
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            int A = mApi.Player_GetPosition(); //song playlength in ms
            if(A <= 700) {
                A = 0;
            }
            if (!GAMEOVER && shouldCountTime && mApi.Player_GetPlayState() == Plugin.PlayState.Playing) { //if time should move AND song playing,

                if (player == 1) { //tick
                    timeP1 = P1TimeAtNew - A;
                }
                else {
                    timeP2 = P2TimeAtNew - A;
                }
                if(timeP1 <= -1000 || timeP2 <= -1000) {
                    GAMEOVER = true;
                    timeP1 = Math.Max(timeP1, 0);
                    timeP2 = Math.Max(timeP2, 0);
                    showSong(true);
                    pictureBox2.Show();
                    label6.Show();
                    if (timeP1 <= 0) {
                        label6.Text = "Player 1 Lost";
                    }
                    else {
                        label6.Text = "Player 2 Lost";
                    }
                }
            }

            updateTimers();
        }

        public void showSong(bool showBoxes) {
            if (showBoxes) {
                songName.Show();
                pictureBox1.Show();
                shouldCountTime = false;
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
            //mApi.NowPlayingList_PlayLibraryShuffled(); //shuffles ALL songs
            mApi.Player_SetShuffle(false);
            mApi.Player_SetShuffle(true); // shuffles current list

            //mApi.Playlist_QueryFilesEx();

        }

        public void start() {
            //string startingSong = mApi.NowPlayingList_GetListFileUrl(0); //gets current song
            //mApi.NowPlayingList_QueueLast(startingSong); //plays the first song last in the queue

            if (shouldLoop) {
                mApi.Player_SetRepeat(Plugin.RepeatMode.All); //loop playlist
            }
            else {
                mApi.Player_SetRepeat(Plugin.RepeatMode.None); //dont loop playlist
            }

            shuffleList();

            mApi.Player_PlayNextTrack(); //play next track (random not first song)

            shuffleList(); //now first song is randomly in there


            //shuffleList();

            GAMEOVER = false;

            Player1Score = 0;
            Player2Score = 0;
            incPoints(0); // to update text

            listBox1.Items.Clear();
            listBox2.Items.Clear();

            startTime = (int)(Mins.Value * 60 + Secs.Value) * 1000;
            timePass1 = (int)(numericUpDown3.Value * 1000);
            timePass2 = (int)(numericUpDown4.Value * 1000);

            player = startingPlayer;

            //update fonts
            TimerP1.ForeColor = P1Col;
            TimerP2.ForeColor = P2Col;

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
            label6.Hide();
            pictureBox2.Hide();
            Start.Hide();

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
            if (player == 1) {



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
                    mApi.Player_PlayNextTrack();
                    shouldCountTime = true;
                    addSong(0);

                    handleNextSong();
                }
                else if (e.KeyCode == Keys.Space || e.KeyCode == Keys.M) {
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
            showHistory = displayHistory.Checked;
            if (showHistory) {
                listBox1.Show();
                listBox2.Show();
            }
            else {
                listBox1.Hide();
                listBox2.Hide();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            player = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {
            player = 2;
        }

        private void button1_Click(object sender, EventArgs e) {
            if (colorDialog1.ShowDialog() == DialogResult.OK) {
                button1.ForeColor = colorDialog1.Color;
                P1Col = colorDialog1.Color;
                updateColors();
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            if (colorDialog2.ShowDialog() == DialogResult.OK) {
                button2.ForeColor = colorDialog2.Color;
                P2Col = colorDialog2.Color;
                updateColors();
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
            player1Needs = (int)numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e) {
            player2Needs = (int)numericUpDown2.Value;
        }

        private void Mins_ValueChanged(object sender, EventArgs e) {
            startTime = (int)(Mins.Value * 60 + Secs.Value) * 1000;
        }

        private void Secs_ValueChanged(object sender, EventArgs e) {
            startTime = (int)(Mins.Value * 60 + Secs.Value) * 1000;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e) {
            timePass1 = (int)(numericUpDown3.Value * 1000);
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e) {
            timePass2 = (int)(numericUpDown4.Value * 1000);
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e) {
            shouldLoop = checkBox1.Checked;
        }
    }
}
