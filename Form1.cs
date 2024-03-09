﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Json;

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


        int startingPlayer = 1;
        int player1Needs = 2;
        int player2Needs = 2;

        int startTimeP1 = 150000; //ms
        int startTimeP2 = 150000; //ms
        int startPointsP1 = 0;
        int startPointsP2 = 0;
        int timePass1 = 2000; //ms
        int timePass2 = 2000; //ms

        Color P1Col = Color.Green;
        Color P2Col = Color.Blue;
        bool showHistory = true;

        float AutoPause = 4;

        //not user changed
        bool shouldCountTime = false;
        int timeP1;
        int timeP2;
        public int P1TimeAtNew;
        public int P2TimeAtNew;
        int player; //starting player
        bool GAMEOVER = false;
        public bool shouldLoop = true;
        public bool shouldShuffle = true;
        bool singlePlayer = false;

        Score p1Score = new Score();
        Score p2Score = new Score();

        Font smallerFont;
        Font biggerFont;

        float[] graph = new float[1000];
        float[] peaks = new float[1000];
        float[] fft = new float[4096];

        bool stupidMode = false;
        bool quickRounds = true;
        float quickRoundLength = 2.0f;
        private Pen pen = new Pen(Color.FromArgb(170, 245, 245, 245), 2); // Change color and width as needed
        Pen transPen = new Pen(Color.FromArgb(170, 0, 0, 0), 4);
        //new Pen(Color.FromArgb(170, 100, 100, 255), 3);
        Bitmap Art;
        int ticks = 0;

        MyListBoxItem currentlyHighlightedItem = null;
        public bool havePaused = false;
        List<Point> graphPoints = new List<Point>();
        List<bouncingImage> Dcolons = new List<bouncingImage>();

        public void PostUpdate(string key, string value)
        {
            if (UpdateURLTextBox.Text == "")
            {
                return;
            }

            try
            {
                HttpClient client = new HttpClient();
                JsonObject j = new JsonObject(new KeyValuePair<string, JsonValue>(key, value));
                var content = new StringContent(j.ToString(), Encoding.UTF8, "application/json");
                client.PostAsync(UpdateURLTextBox.Text, content);
            }
            catch (Exception) {}
        }

        public void VGMV_Load(object sender, EventArgs e) {
            InitTimer();
            updateSongSettings();

            smallerFont =                   new Font(rfont.Families[0], 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            biggerFont =                    new Font(rfont.Families[0], 30F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            Font mFont12 =                  new Font(mfont.Families[0], 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            Font rFont2175 =                new Font(rfont.Families[0], 21.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            //riffic
            ScoreP2.Font =                  rFont2175;
            ScoreP1.Font =                  rFont2175;
            TimerP1.Font =                  rFont2175;
            TimerP2.Font =                  rFont2175;
            Player1Name.Font =              rFont2175;
            Player2Name.Font =              rFont2175;
            restartButton.Font =            new Font(rfont.Families[0], 15.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            settingsButton.Font =           new Font(rfont.Families[0], 15.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            LosingPlayerLabel.Font =        new Font(rfont.Families[0], 25.00F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            Start.Font =                    new Font(rfont.Families[0], 36.00F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

            //montserrat
            songName.Font =                 new Font(mfont.Families[0], 20.00F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            listBox2.Font =                 new Font(mfont.Families[0], 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            P2StartsRadioButton.Font =      new Font(mfont.Families[0], 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            P1StartsRadioButton.Font =      new Font(mfont.Families[0], 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            listBox1.Font =                 new Font(mfont.Families[0], 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            DisplayHistoryCheckBox.Font =   mFont12;
            LoopPlaylistCheckBox.Font =     mFont12;
            ShufflePlaylistCheckBox.Font =  mFont12;
            SingePlayerCheckBox.Font =      mFont12;
            P1NameTextBox.Font =            mFont12;
            P2NameTextBox.Font =            mFont12;
            label8.Font =                   mFont12;
            P2IncrementUpDown.Font =        mFont12;
            label5.Font =                   mFont12;
            label4.Font =                   mFont12;
            P2PointsToPassUpDown.Font =     mFont12;
            P1PointsToPassUpDown.Font =     mFont12;
            P2ChangeColorButton.Font =      mFont12;
            P1ChangeColorButton.Font =      mFont12;
            label2.Font =                   mFont12;
            label1.Font =                   mFont12;
            P1IncrementUpDown.Font =        mFont12;
            SecsP1.Font =                     mFont12;
            MinsP1.Font =                     mFont12;
            export.Font =                   mFont12;
            numericUpDown1.Font =           mFont12;
            numericUpDown2.Font =           mFont12;
            label6.Font =                   mFont12;
            label9.Font =                   mFont12;
            checkBox1.Font =                mFont12;
            //Fonts now no longer need to be set in Form1.Designer.cs -- they are set here instead.
            //The sizing of other elements though depends on the DPI scaling of the computer you are editing on??
            //Either we move the size settings also to here which would be kinda ugly ngl gonna lie, or we just keep editing it each PR


            trackBar1_Set();

            pictureBox3.Visible = false;
            pictureBox4.Visible = false;

            UpdateSettings();
            player = startingPlayer;

            timeP1 = startTimeP1;
            timeP2 = startTimeP2;

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
            panel1.Hide();
            TimerP1.Hide();
            TimerP2.Hide();

            listBox1.DrawMode = DrawMode.OwnerDrawVariable;
            listBox1.MeasureItem += listBox1_MeasureItem;
            listBox1.DrawItem += listBox1_DrawItem;

            listBox2.DrawMode = DrawMode.OwnerDrawVariable;
            listBox2.MeasureItem += listBox2_MeasureItem;
            listBox2.DrawItem += listBox2_DrawItem;

            p1Score.reset();
            p1Score._score = startPointsP1;
            p2Score.reset();
            p2Score._score = startPointsP2;

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

            panel1.BackColor = Color.Transparent;
            panel1.Parent = this; // Set the actual parent control
        }



        private void UpdateSettings() {
            if (_settingsManager.LoadSettings()) {
                startingPlayer = _settingsManager.P1Start ? 1 : 2;
                if (startingPlayer == 1) {
                    P1StartsRadioButton.Checked = true;
                } else {
                    P2StartsRadioButton.Checked = true;
                }

                MinsP1.Value = _settingsManager.MinutesP1;
                SecsP1.Value = _settingsManager.SecondsP1;

                MinsP2.Value = _settingsManager.MinutesP2;
                SecsP2.Value = _settingsManager.SecondsP2;

                startTimeP1 = (int)(MinsP1.Value * 60 + SecsP1.Value) * 1000;
                startTimeP2 = (int)(MinsP2.Value * 60 + SecsP2.Value) * 1000;

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

                AutoPause = _settingsManager.AutoPause;
                numericUpDown1.Value = (decimal) AutoPause;
                quickRounds = _settingsManager.QuickRounds;
                checkBox1.Checked = quickRounds;
                quickRoundLength = _settingsManager.QuickRoundLength;
                numericUpDown2.Value = (decimal) quickRoundLength;
                updateColors();

                UpdateURLTextBox.Text = _settingsManager.UpdateURL;
                startPointsFieldP1.Value = _settingsManager.StartPointsP1;
                startPointsP1 = _settingsManager.StartPointsP1;
                startPointsFieldP2.Value = _settingsManager.StartPointsP2;
                startPointsP2 = _settingsManager.StartPointsP2;
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


            p1Score.reset();
            p1Score._score = startPointsP1;
            p2Score.reset();
            p2Score._score = startPointsP2;
            incPoints(0); // to update text
            updateTimers(true);

            listBox1.Items.Clear();
            listBox2.Items.Clear();

            pictureBox3.Visible = false;
            pictureBox4.Visible = false;

            startTimeP1 = (int)(MinsP1.Value * 60 + SecsP1.Value) * 1000;
            startTimeP2 = (int)(MinsP2.Value * 60 + SecsP2.Value) * 1000;
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
            timeP1 = startTimeP1;
            timeP2 = startTimeP2;
            P1TimeAtNew = timeP1;
            P2TimeAtNew = timeP2;
            shouldCountTime = true;
            havePaused = false;

            //show hide stuff
            ScoreP1.Show();
            ScoreP2.Show();
            songName.Hide();
            panel1.Hide();
            
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
            PostUpdate("SongTitle", "");
            PostUpdate("SongAlbum", "");

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
            framesWithAudio = 0;
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
            PostUpdate("P1Score", p1Score._score.ToString());
            PostUpdate("P2Score", p2Score._score.ToString());
        }

        public void updateTimers(bool forceUpdate = false) {
            int P1Min = (int)Math.Ceiling(timeP1 / 1000.0) / 60;
            int P2Min = (int)Math.Ceiling(timeP2 / 1000.0) / 60;
            int P1Sec = (int)Math.Ceiling(timeP1 / 1000.0) % 60;
            int P2Sec = (int)Math.Ceiling(timeP2 / 1000.0) % 60;

            string P1Seconds = P1Sec.ToString();
            string P2Seconds = P2Sec.ToString();

            //add leading 0s
            if (P1Sec < 10) { P1Seconds = "0" + P1Sec.ToString(); }
            if (P2Sec < 10) { P2Seconds = "0" + P2Sec.ToString(); }

            string newP1Time = P1Min.ToString() + ":" + P1Seconds;
            string newP2Time = P2Min.ToString() + ":" + P2Seconds;

            if (TimerP1.Text != newP1Time || forceUpdate)
            {
                updateText(TimerP1, newP1Time);
                PostUpdate("TimerP1", newP1Time);
            }

            if (TimerP2.Text != newP2Time || forceUpdate)
            {
                updateText(TimerP2, newP2Time);
                PostUpdate("TimerP2", newP2Time);
            }
        }

        public void updateColors() {
            TimerP1.ForeColor = P1Col;
            Player1Name.ForeColor = P1Col;

            TimerP2.ForeColor = P2Col;
            Player2Name.ForeColor = P2Col;

            if (player == 1 || singlePlayer) {
                ScoreP1.ForeColor = P1Col;
                ScoreP2.ForeColor = Color.Black;
            }
            else {
                ScoreP1.ForeColor = Color.Black;
                ScoreP2.ForeColor = P2Col;
            }
        }

        public void updateSongSettings() {

            try {
                Plugin.PictureLocations temp1;
                string temp2;
                byte[] img;
                mApi.Library_GetArtworkEx(mApi.NowPlaying_GetFileUrl(), 0, true, out temp1, out temp2, out img);
                Art = (Bitmap)new ImageConverter().ConvertFrom(img);
            }
            catch {
                Art = Properties.Resources.nocover;
            }


            if (stupidMode) {
                Array.Resize(ref graph, mApi.NowPlaying_GetDuration());
                Array.Resize(ref peaks, mApi.NowPlaying_GetDuration());

                mApi.NowPlaying_GetSoundGraphEx(graph, peaks);
            }
            framesWithAudio = 0;
        }




        //how i draw the funny game over screen + spectogram and stuff like that
        //this is the only way i could find to get proper transparency and layering
        //it is much more annoying but allows for much more detail and precision than regular windows forms
        //yippie

        private void panel1_Paint(object sender, PaintEventArgs e) {

            var g = e.Graphics;
            g.Clear(Color.Transparent);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


            //album art
            g.DrawImage(Art, 0, 0, 500, 500);


            //spectograph
            Point[] points = graphPoints.ToArray();
            if (points.Length > 1) {
                g.DrawCurve(transPen, points);
                g.DrawCurve(pen, points);
            }

            //game over D: :agony:
            if (pictureBox2.Visible) {
                Image Dcolon = Properties.Resources.D_colon;
                Image Agony = Properties.Resources.noooo;

                for (int j = 0; j < Dcolons.Count; j++) {
                    //ugly as sin but i cant find out how else to rotate them
                    Image selection = Dcolon;
                    if(Dcolons[j].ID > 0) {
                        selection = Agony;
                    }
                    g.TranslateTransform(Dcolons[j].x + selection.Width / 2, Dcolons[j].y + selection.Height / 2);
                    g.RotateTransform(Dcolons[j].angle);
                    g.TranslateTransform(-Dcolons[j].x - selection.Width / 2, -Dcolons[j].y - selection.Height / 2);

                    g.DrawImageUnscaled(selection, new Point(Dcolons[j].x, Dcolons[j].y));

                    g.TranslateTransform(Dcolons[j].x + selection.Width / 2, Dcolons[j].y + selection.Height / 2);
                    g.RotateTransform(-Dcolons[j].angle);
                    g.TranslateTransform(-Dcolons[j].x - selection.Width / 2, -Dcolons[j].y - selection.Height / 2);

                }
            }

            //alien dance
            if (pictureBox3.Visible || GAMEOVER) {
                g.DrawImage(images[(ticks % 65 * 2) % 65], new Point(500 - images[0].Width, 500 - images[0].Height));

                g.TranslateTransform(images[0].Width, 0);
                g.ScaleTransform(-1, 1);
                g.DrawImage(images[(ticks % 65 * 2) % 65], new Point(0, 500 - images[0].Height));
                g.ScaleTransform(-1, 1);
                g.TranslateTransform(-images[0].Width, 0);

            }

        }


        private List<Point> generateFFT() {
            mApi.NowPlaying_GetSpectrumData(fft);

            List<Point> pointList = new List<Point>();

            int scaleFactor = fft.Length / panel1.Width;
            for (int j = 0; j < fft.Length; j += scaleFactor) {
                int x = (int)((double)j * 500 / fft.Length);

                int y = (int)(450 - Math.Min(fft[j] * 1000, 150));

                //double logScaledValue = Math.Log(1 + Math.Abs(fft[j]) * 1000);
                //int y = (int)(450 - 50 * logScaledValue);

                pointList.Add(new Point(x, y));
            }

            return pointList;
        }

        //timer
        public void InitTimer() {
            timer1.Start();
        }
        int framesWithAudio = 0;
        private void timer1_Tick(object sender, EventArgs e) {
            ticks++;

            if (pictureBox2.Visible) {

                if (ticks % 4 == 1) {
                    Dcolons.Add(new bouncingImage());
                }

                for (int j = 0; j < Dcolons.Count; j++) {
                    Dcolons[j].tick(timer1.Interval);
                    if (Dcolons[j].y > 500) {
                        Dcolons.Remove(Dcolons[j]);
                    }
                }
            }
            else if(Dcolons.Count > 0) {
                Dcolons.Clear();
            }

            pictureBox3.Image = images[(ticks % 65 * 2) % 65];
            pictureBox4.Image = images[(ticks % 65 * 2) % 65];

            graphPoints = generateFFT();
            panel1.Invalidate();

            if (quickRounds && framesWithAudio > -1000) {
                float maxFFT = 0;
                for (int i = 0; i < fft.Length; i++) {
                    maxFFT = Math.Max(maxFFT, Math.Abs(fft[i]));
                }

                if (maxFFT > 0) {
                    framesWithAudio++;
                }
                //quickRoundLength in seconds * 1000 = ms
                //every 50ms the timer ticks (approx)
                //10 ticks with audio = 10*50 = 500ms of song
                //to go backwards, 0.5s = 500ms of song / 50ms per tick = 10
                if (maxFFT > 0 && framesWithAudio >= (quickRoundLength*1000)/50) {
                    mApi.Player_PlayPause();
                }
            }
            if (stupidMode) {
                int size = 1204;
                int val = (int)(mApi.Player_GetPosition() - 500F);
                size = (int)(peaks[Math.Max(1, val)] * 1204);
                ClientSize = new Size(size + 100, 668);

            }


            gameOverCheck(false);
            songEndingCheck();
            updateTimers();
        }
        //end timer

        private void songEndingCheck() {
            int currentSongTime = mApi.Player_GetPosition();
            int totalSongTime = mApi.NowPlaying_GetDuration();
            if (totalSongTime - (AutoPause * 1000) < currentSongTime && !havePaused) {
                if (mApi.Player_GetPlayState() == Plugin.PlayState.Playing) {
                    mApi.Player_PlayPause();
                }
                havePaused = true;
            }
        }
        private void gameOverCheck(bool quickEnd) {
            int A = mApi.Player_GetPosition(); //song playlength in ms
            //TODO dont count time if the start of the song is silent (until its playing audio duh)
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
                panel1.Show();
                shouldCountTime = false;
                havePaused = false;
            }

            if (!showBoxes && !GAMEOVER) { //dont update if game is not over, and hide the game 
                panel1.Hide();
                songName.Hide();
                havePaused = false;
                return;
            }
            if (!showBoxes && GAMEOVER) { //hide the D: if the game is over and a new track plays
                pictureBox2.Hide();
                pictureBox5.Hide();
                LosingPlayerLabel.Hide();
            }

            string title = mApi.NowPlaying_GetFileTag(Plugin.MetaDataType.TrackTitle);
            string album = mApi.NowPlaying_GetFileTag(Plugin.MetaDataType.Album);
            updateText(songName, title + "\n" + album);
            PostUpdate("SongTitle", title);
            PostUpdate("SongAlbum", album);
        }

        public void updateText(Label label, string textChange) {
            label.Text = textChange;
            label.TextAlign = ContentAlignment.MiddleCenter;
        }

        public void shuffleList() {
            mApi.Player_SetShuffle(false);
            mApi.Player_SetShuffle(true); // shuffles current list
        }



        #region List Box Section

        public class MyListBoxItem {
            public MyListBoxItem(Color c, string m, string f, Font font) {
                ItemColor = c;
                Message = m;
                FileURL = f;
                Font = font;
            }
            public Color ItemColor { get; set; }
            public string Message { get; set; }
            public string FileURL { get; set; }

            public Font Font { get; set; }
        }

        public void addSong(int value) {
            MyListBoxItem EmptyListItem = new MyListBoxItem(Color.Transparent, "empty line", "", listBox1.Font);
            string album = mApi.NowPlaying_GetFileTag(Plugin.MetaDataType.Album);
            string track = mApi.NowPlaying_GetFileTag(Plugin.MetaDataType.TrackTitle);
            string fileURL = mApi.NowPlayingList_GetListFileUrl(mApi.NowPlayingList_GetCurrentIndex());
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
                listBox1.Items.Add(EmptyListItem);
                listBox1.Items.Add(new MyListBoxItem(toBeAss, finalIn, fileURL, listBox1.Font));
                

                listBox1.TopIndex = listBox1.Items.Count - 1;
                listBox1.Refresh();

                //this.objectListView1.AddObject(new MyListBoxItem(toBeAss, finalIn));
            }
            else {
                listBox2.DrawMode = DrawMode.OwnerDrawVariable;

                listBox2.Items.Add(EmptyListItem);
                listBox2.Items.Add(new MyListBoxItem(toBeAss, finalIn, fileURL, listBox2.Font));

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
                e.Graphics.DrawString(item.Message, item.Font, new SolidBrush(item.ItemColor), e.Bounds);
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
                e.Graphics.DrawString(item.Message, item.Font, new SolidBrush(item.ItemColor), e.Bounds);
            }
            catch { }
        }

        private void listBox1_MouseClick(object Sender, MouseEventArgs e)
        {
            int index = listBox1.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches && (index < 65535)) //Index is meant to return -1 but instead returns 65535 if it can't find something and causes an exception
            {
                MyListBoxItem clickedItem = listBox1.Items[index] as MyListBoxItem;
                if (clickedItem != null)
                {
                    Console.WriteLine(clickedItem.FileURL);
                    mApi.NowPlayingList_PlayNow(clickedItem.FileURL);
                }
            }
        }

        private void listBox2_MouseClick(object Sender, MouseEventArgs e)
        {
            int index = listBox2.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches && (index < 65535)) //Index is meant to return -1 but instead returns 65535 if it can't find something and causes an exception
            {
                MyListBoxItem clickedItem = listBox2.Items[index] as MyListBoxItem;
                if (clickedItem != null)
                {
                    Console.WriteLine(clickedItem.FileURL);
                    mApi.NowPlayingList_PlayNow(clickedItem.FileURL);
                }
            }
        }

        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int index = listBox1.IndexFromPoint(e.Location);
            
            if (index != ListBox.NoMatches && (index < 65535)) //Index is meant to return -1 but instead returns 65535 if it can't find something and causes an exception
            {
                MyListBoxItem item = listBox1.Items[index] as MyListBoxItem;
                if (item != null)
                {
                    listBox1.Cursor = Cursors.Hand;
                    if (item != currentlyHighlightedItem)
                    {
                        foreach (MyListBoxItem listItem in listBox1.Items)
                        {
                            listItem.Font = new Font(listBox1.Font, FontStyle.Bold);
                        }
                        item.Font = new Font(listBox1.Font, FontStyle.Underline | FontStyle.Bold);
                        currentlyHighlightedItem = item;
                        listBox1.Invalidate();
                    }
                }
            } 
            else
            {
                if (currentlyHighlightedItem != null)
                {
                    currentlyHighlightedItem = null;
                    listBox1.Invalidate();
                }
            }
        }

        private void listBox1_MouseLeave(object sender, EventArgs e)
        {
            listBox1.Cursor = Cursors.Default;
            foreach (MyListBoxItem listItem in listBox1.Items)
            {
                listItem.Font = new Font(listBox1.Font, FontStyle.Bold);
            }
            currentlyHighlightedItem = null;
            listBox1.Invalidate();
        }
        private void listBox2_MouseMove(object sender, MouseEventArgs e)
        {
            int index = listBox2.IndexFromPoint(e.Location);

            if (index != ListBox.NoMatches && (index < 65535)) //Index is meant to return -1 but instead returns 65535 if it can't find something and causes an exception
            {
                MyListBoxItem item = listBox2.Items[index] as MyListBoxItem;
                if (item != null)
                {
                    listBox2.Cursor = Cursors.Hand;
                    if (item != currentlyHighlightedItem)
                    {
                        foreach (MyListBoxItem listItem in listBox2.Items)
                        {
                            listItem.Font = new Font(listBox2.Font, FontStyle.Bold);
                        }
                        item.Font = new Font(listBox2.Font, FontStyle.Underline | FontStyle.Bold);
                        currentlyHighlightedItem = item;
                        listBox2.Invalidate();
                    }
                }
            }
            else
            {
                if (currentlyHighlightedItem != null)
                {
                    currentlyHighlightedItem = null;
                    listBox2.Invalidate();
                }
            }
        }

        private void listBox2_MouseLeave(object sender, EventArgs e)
        {
            listBox2.Cursor = Cursors.Default;
            foreach (MyListBoxItem listItem in listBox2.Items)
            {
                listItem.Font = new Font(listBox2.Font, FontStyle.Bold);
            }
            currentlyHighlightedItem = null;
            listBox2.Invalidate();
        }

        #endregion

        public void handleNextSong() {
            framesWithAudio = 0;

            mApi.Player_PlayNextTrack();
            shouldCountTime = true;

            songName.Hide();
            panel1.Hide();

            P1TimeAtNew = timeP1;
            P2TimeAtNew = timeP2;

            PostUpdate("SongTitle", "");
            PostUpdate("SongAlbum", "");
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
                //MessageBox.Show(Dcolons.Count.ToString(), "title", MessageBoxButtons.OK);
            }

            if (!GAMEOVER) {
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.J || e.KeyCode == Keys.A) { //should next song 1 point
                    addSong(1);
                    incPoints(1);

                    handleNextSong();
                }
                else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.L || e.KeyCode == Keys.D) { //should next song 2 point
                    addSong(1);
                    incPoints(1);

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
                    framesWithAudio = -1000; //to note to not keep pausing
                }
                else if (e.KeyCode == Keys.H) { //restart song
                    framesWithAudio = 0;
                    mApi.Player_SetPosition(0);
                    if (mApi.Player_GetPlayState() == Plugin.PlayState.Paused) {
                        mApi.Player_PlayPause();
                    }
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
            int value = (int)(MinsP1.Value * 60 + SecsP1.Value) * 1000;
            startTimeP1 = value;
            timeP1 = value;

            _settingsManager.MinutesP1 = (int)MinsP1.Value;
            _settingsManager.SaveSettings();
        }

        private void Secs_ValueChanged(object sender, EventArgs e) {
            int value = (int)(MinsP1.Value * 60 + SecsP1.Value) * 1000;
            startTimeP1 = value;
            timeP1 = value;

            _settingsManager.SecondsP1 = (int)SecsP1.Value;
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

        private void label6_Click(object sender, EventArgs e) {

        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e) {
            AutoPause = (float) numericUpDown1.Value;
            _settingsManager.AutoPause = AutoPause;
            _settingsManager.SaveSettings();

        }

        private void numericUpDown2_ValueChanged_1(object sender, EventArgs e) {
            quickRoundLength = (float)numericUpDown2.Value;
            _settingsManager.QuickRoundLength = quickRoundLength;
            _settingsManager.SaveSettings();

        }

        private void checkBox1_CheckedChanged_2(object sender, EventArgs e) {
            quickRounds = checkBox1.Checked;
            _settingsManager.QuickRounds = quickRounds;
            _settingsManager.SaveSettings();
        }

        private void WSURLTextBox_TextChanged(object sender, EventArgs e)
        {
            _settingsManager.UpdateURL = UpdateURLTextBox.Text;
            _settingsManager.SaveSettings();
        }

        private void MinsP2_ValueChanged(object sender, EventArgs e)
        {
            int value = (int)(MinsP2.Value * 60 + SecsP2.Value) * 1000;
            startTimeP2 = value;
            timeP2 = value;

            _settingsManager.MinutesP2 = (int)MinsP2.Value;
            _settingsManager.SaveSettings();
        }

        private void SecsP2_ValueChanged(object sender, EventArgs e)
        {
            int value = (int)(MinsP2.Value * 60 + SecsP2.Value) * 1000;
            startTimeP2 = value;
            timeP2 = value;

            _settingsManager.SecondsP2 = (int)SecsP2.Value;
            _settingsManager.SaveSettings();
        }

        private void startPointsFieldP1_ValueChanged(object sender, EventArgs e)
        {
            int value = (int)startPointsFieldP1.Value;
            startPointsP1 = value;
            _settingsManager.StartPointsP1 = value;
            _settingsManager.SaveSettings();
        }

        private void startPointsFieldP2_ValueChanged(object sender, EventArgs e)
        {
            int value = (int)startPointsFieldP2.Value;
            startPointsP2 = value;
            _settingsManager.StartPointsP2 = value;
            _settingsManager.SaveSettings();
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
