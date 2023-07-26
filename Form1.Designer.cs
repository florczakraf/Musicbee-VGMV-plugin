namespace MusicBeePlugin {
    partial class VGMV {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VGMV));
            this.songName = new System.Windows.Forms.Label();
            this.Start = new System.Windows.Forms.Button();
            this.ScoreP2 = new System.Windows.Forms.Label();
            this.ScoreP1 = new System.Windows.Forms.Label();
            this.TimerP1 = new System.Windows.Forms.Label();
            this.TimerP2 = new System.Windows.Forms.Label();
            this.restartButton = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.DisplayHistoryCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.P2NameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.export = new System.Windows.Forms.Button();
            this.SingePlayerCheckBox = new System.Windows.Forms.CheckBox();
            this.ShufflePlaylistCheckBox = new System.Windows.Forms.CheckBox();
            this.P2NameSettingsLabel = new System.Windows.Forms.Label();
            this.P1NameTextBox = new System.Windows.Forms.TextBox();
            this.P1NameSettingsLabel = new System.Windows.Forms.Label();
            this.LoopPlaylistCheckBox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.P2IncrementUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.P2PointsToPassUpDown = new System.Windows.Forms.NumericUpDown();
            this.P1PointsToPassUpDown = new System.Windows.Forms.NumericUpDown();
            this.P2ChangeColorButton = new System.Windows.Forms.Button();
            this.P1ChangeColorButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.P1IncrementUpDown = new System.Windows.Forms.NumericUpDown();
            this.Secs = new System.Windows.Forms.NumericUpDown();
            this.Mins = new System.Windows.Forms.NumericUpDown();
            this.P2StartsRadioButton = new System.Windows.Forms.RadioButton();
            this.P1StartsRadioButton = new System.Windows.Forms.RadioButton();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.settingsButton = new System.Windows.Forms.Button();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.LosingPlayerLabel = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.Player1Name = new System.Windows.Forms.Label();
            this.Player2Name = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.P2IncrementUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.P2PointsToPassUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.P1PointsToPassUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.P1IncrementUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Secs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Mins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // songName
            // 
            this.songName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.songName.AutoEllipsis = true;
            this.songName.BackColor = System.Drawing.Color.Transparent;
            this.songName.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.songName.ForeColor = System.Drawing.Color.Coral;
            this.songName.Location = new System.Drawing.Point(532, 17);
            this.songName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.songName.Name = "songName";
            this.songName.Size = new System.Drawing.Size(1346, 235);
            this.songName.TabIndex = 1;
            this.songName.Text = "VGM Versus";
            this.songName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.songName.UseMnemonic = false;
            // 
            // Start
            // 
            this.Start.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Start.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Start.FlatAppearance.BorderSize = 5;
            this.Start.Location = new System.Drawing.Point(818, 498);
            this.Start.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(778, 308);
            this.Start.TabIndex = 2;
            this.Start.Text = "Click to start";
            this.Start.UseVisualStyleBackColor = false;
            this.Start.Click += new System.EventHandler(this.Start_Click_1);
            // 
            // ScoreP2
            // 
            this.ScoreP2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ScoreP2.BackColor = System.Drawing.Color.Transparent;
            this.ScoreP2.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScoreP2.Location = new System.Drawing.Point(1816, 1040);
            this.ScoreP2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ScoreP2.Name = "ScoreP2";
            this.ScoreP2.Size = new System.Drawing.Size(570, 227);
            this.ScoreP2.TabIndex = 3;
            this.ScoreP2.Text = "Score Player 2";
            this.ScoreP2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ScoreP1
            // 
            this.ScoreP1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ScoreP1.BackColor = System.Drawing.Color.Transparent;
            this.ScoreP1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScoreP1.Location = new System.Drawing.Point(22, 1040);
            this.ScoreP1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ScoreP1.Name = "ScoreP1";
            this.ScoreP1.Size = new System.Drawing.Size(570, 227);
            this.ScoreP1.TabIndex = 5;
            this.ScoreP1.Text = "Score Player 1";
            this.ScoreP1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TimerP1
            // 
            this.TimerP1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TimerP1.BackColor = System.Drawing.Color.Transparent;
            this.TimerP1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimerP1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.TimerP1.Location = new System.Drawing.Point(90, 113);
            this.TimerP1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TimerP1.Name = "TimerP1";
            this.TimerP1.Size = new System.Drawing.Size(356, 98);
            this.TimerP1.TabIndex = 6;
            this.TimerP1.Text = "5:00";
            this.TimerP1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TimerP2
            // 
            this.TimerP2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TimerP2.BackColor = System.Drawing.Color.Transparent;
            this.TimerP2.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimerP2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.TimerP2.Location = new System.Drawing.Point(1962, 113);
            this.TimerP2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TimerP2.Name = "TimerP2";
            this.TimerP2.Size = new System.Drawing.Size(356, 98);
            this.TimerP2.TabIndex = 7;
            this.TimerP2.Text = "5:00";
            this.TimerP2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // restartButton
            // 
            this.restartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restartButton.Location = new System.Drawing.Point(2172, 17);
            this.restartButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(214, 90);
            this.restartButton.TabIndex = 8;
            this.restartButton.Text = "Restart";
            this.restartButton.UseVisualStyleBackColor = true;
            this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
            // 
            // listBox2
            // 
            this.listBox2.BackColor = System.Drawing.Color.Lavender;
            this.listBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 25;
            this.listBox2.Location = new System.Drawing.Point(1790, 271);
            this.listBox2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.listBox2.Name = "listBox2";
            this.listBox2.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBox2.Size = new System.Drawing.Size(594, 817);
            this.listBox2.TabIndex = 11;
            this.listBox2.TabStop = false;
            this.listBox2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox2_DrawItem);
            // 
            // DisplayHistoryCheckBox
            // 
            this.DisplayHistoryCheckBox.AutoSize = true;
            this.DisplayHistoryCheckBox.Checked = true;
            this.DisplayHistoryCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DisplayHistoryCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayHistoryCheckBox.Location = new System.Drawing.Point(608, 325);
            this.DisplayHistoryCheckBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.DisplayHistoryCheckBox.Name = "DisplayHistoryCheckBox";
            this.DisplayHistoryCheckBox.Size = new System.Drawing.Size(276, 41);
            this.DisplayHistoryCheckBox.TabIndex = 12;
            this.DisplayHistoryCheckBox.Text = "Display History";
            this.DisplayHistoryCheckBox.UseVisualStyleBackColor = true;
            this.DisplayHistoryCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox1.Controls.Add(this.P2NameTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Controls.Add(this.export);
            this.groupBox1.Controls.Add(this.SingePlayerCheckBox);
            this.groupBox1.Controls.Add(this.ShufflePlaylistCheckBox);
            this.groupBox1.Controls.Add(this.P2NameSettingsLabel);
            this.groupBox1.Controls.Add(this.P1NameTextBox);
            this.groupBox1.Controls.Add(this.P1NameSettingsLabel);
            this.groupBox1.Controls.Add(this.LoopPlaylistCheckBox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.P2IncrementUpDown);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.P2PointsToPassUpDown);
            this.groupBox1.Controls.Add(this.DisplayHistoryCheckBox);
            this.groupBox1.Controls.Add(this.P1PointsToPassUpDown);
            this.groupBox1.Controls.Add(this.P2ChangeColorButton);
            this.groupBox1.Controls.Add(this.P1ChangeColorButton);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.P1IncrementUpDown);
            this.groupBox1.Controls.Add(this.Secs);
            this.groupBox1.Controls.Add(this.Mins);
            this.groupBox1.Controls.Add(this.P2StartsRadioButton);
            this.groupBox1.Controls.Add(this.P1StartsRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(24, 113);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Size = new System.Drawing.Size(2362, 494);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // P2NameTextBox
            // 
            this.P2NameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.P2NameTextBox.Location = new System.Drawing.Point(2018, 88);
            this.P2NameTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.P2NameTextBox.Multiline = true;
            this.P2NameTextBox.Name = "P2NameTextBox";
            this.P2NameTextBox.Size = new System.Drawing.Size(328, 91);
            this.P2NameTextBox.TabIndex = 26;
            this.P2NameTextBox.Text = "Player 2";
            this.P2NameTextBox.TextChanged += new System.EventHandler(this.P2NameTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(688, 433);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 37);
            this.label3.TabIndex = 23;
            this.label3.Text = "Volume";
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 10;
            this.trackBar1.Location = new System.Drawing.Point(658, 388);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(222, 90);
            this.trackBar1.TabIndex = 22;
            this.trackBar1.TickFrequency = 10;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // export
            // 
            this.export.Location = new System.Drawing.Point(1332, 408);
            this.export.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(324, 60);
            this.export.TabIndex = 21;
            this.export.Text = "Export Tracklist";
            this.export.UseVisualStyleBackColor = true;
            this.export.Click += new System.EventHandler(this.export_Click);
            // 
            // SingePlayerCheckBox
            // 
            this.SingePlayerCheckBox.AutoSize = true;
            this.SingePlayerCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SingePlayerCheckBox.Location = new System.Drawing.Point(998, 410);
            this.SingePlayerCheckBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.SingePlayerCheckBox.Name = "SingePlayerCheckBox";
            this.SingePlayerCheckBox.Size = new System.Drawing.Size(235, 41);
            this.SingePlayerCheckBox.TabIndex = 18;
            this.SingePlayerCheckBox.Text = "Singleplayer";
            this.SingePlayerCheckBox.UseVisualStyleBackColor = true;
            this.SingePlayerCheckBox.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // ShufflePlaylistCheckBox
            // 
            this.ShufflePlaylistCheckBox.AutoSize = true;
            this.ShufflePlaylistCheckBox.Checked = true;
            this.ShufflePlaylistCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShufflePlaylistCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShufflePlaylistCheckBox.Location = new System.Drawing.Point(1346, 325);
            this.ShufflePlaylistCheckBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ShufflePlaylistCheckBox.Name = "ShufflePlaylistCheckBox";
            this.ShufflePlaylistCheckBox.Size = new System.Drawing.Size(273, 41);
            this.ShufflePlaylistCheckBox.TabIndex = 17;
            this.ShufflePlaylistCheckBox.Text = "Shuffle Playlist";
            this.ShufflePlaylistCheckBox.UseVisualStyleBackColor = true;
            this.ShufflePlaylistCheckBox.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // P2NameSettingsLabel
            // 
            this.P2NameSettingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.P2NameSettingsLabel.Location = new System.Drawing.Point(1818, 90);
            this.P2NameSettingsLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.P2NameSettingsLabel.Name = "P2NameSettingsLabel";
            this.P2NameSettingsLabel.Size = new System.Drawing.Size(232, 94);
            this.P2NameSettingsLabel.TabIndex = 27;
            this.P2NameSettingsLabel.Text = "Name:";
            this.P2NameSettingsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // P1NameTextBox
            // 
            this.P1NameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.P1NameTextBox.Location = new System.Drawing.Point(202, 88);
            this.P1NameTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.P1NameTextBox.Multiline = true;
            this.P1NameTextBox.Name = "P1NameTextBox";
            this.P1NameTextBox.Size = new System.Drawing.Size(328, 91);
            this.P1NameTextBox.TabIndex = 25;
            this.P1NameTextBox.Text = "Player 1";
            this.P1NameTextBox.TextChanged += new System.EventHandler(this.P1NameTextBox_TextChanged);
            // 
            // P1NameSettingsLabel
            // 
            this.P1NameSettingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.P1NameSettingsLabel.Location = new System.Drawing.Point(12, 88);
            this.P1NameSettingsLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.P1NameSettingsLabel.Name = "P1NameSettingsLabel";
            this.P1NameSettingsLabel.Size = new System.Drawing.Size(178, 94);
            this.P1NameSettingsLabel.TabIndex = 24;
            this.P1NameSettingsLabel.Text = "Name:";
            this.P1NameSettingsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LoopPlaylistCheckBox
            // 
            this.LoopPlaylistCheckBox.AutoSize = true;
            this.LoopPlaylistCheckBox.Checked = true;
            this.LoopPlaylistCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.LoopPlaylistCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoopPlaylistCheckBox.Location = new System.Drawing.Point(998, 325);
            this.LoopPlaylistCheckBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.LoopPlaylistCheckBox.Name = "LoopPlaylistCheckBox";
            this.LoopPlaylistCheckBox.Size = new System.Drawing.Size(243, 41);
            this.LoopPlaylistCheckBox.TabIndex = 16;
            this.LoopPlaylistCheckBox.Text = "Loop Playlist";
            this.LoopPlaylistCheckBox.UseVisualStyleBackColor = true;
            this.LoopPlaylistCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1194, 179);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(300, 37);
            this.label8.TabIndex = 15;
            this.label8.Text = "Increment on Pass";
            // 
            // P2IncrementUpDown
            // 
            this.P2IncrementUpDown.DecimalPlaces = 2;
            this.P2IncrementUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.P2IncrementUpDown.Location = new System.Drawing.Point(1168, 227);
            this.P2IncrementUpDown.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.P2IncrementUpDown.Name = "P2IncrementUpDown";
            this.P2IncrementUpDown.Size = new System.Drawing.Size(400, 44);
            this.P2IncrementUpDown.TabIndex = 14;
            this.P2IncrementUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.P2IncrementUpDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.P2IncrementUpDown.ValueChanged += new System.EventHandler(this.numericUpDown4_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(2010, 294);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(233, 37);
            this.label5.TabIndex = 13;
            this.label5.Text = "Points to pass";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(100, 294);
            this.label4.Margin = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(233, 37);
            this.label4.TabIndex = 12;
            this.label4.Text = "Points to pass";
            // 
            // P2PointsToPassUpDown
            // 
            this.P2PointsToPassUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.P2PointsToPassUpDown.Location = new System.Drawing.Point(2276, 290);
            this.P2PointsToPassUpDown.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.P2PointsToPassUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.P2PointsToPassUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.P2PointsToPassUpDown.Name = "P2PointsToPassUpDown";
            this.P2PointsToPassUpDown.Size = new System.Drawing.Size(74, 44);
            this.P2PointsToPassUpDown.TabIndex = 11;
            this.P2PointsToPassUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.P2PointsToPassUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.P2PointsToPassUpDown.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // P1PointsToPassUpDown
            // 
            this.P1PointsToPassUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.P1PointsToPassUpDown.Location = new System.Drawing.Point(14, 290);
            this.P1PointsToPassUpDown.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.P1PointsToPassUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.P1PointsToPassUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.P1PointsToPassUpDown.Name = "P1PointsToPassUpDown";
            this.P1PointsToPassUpDown.Size = new System.Drawing.Size(74, 44);
            this.P1PointsToPassUpDown.TabIndex = 10;
            this.P1PointsToPassUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.P1PointsToPassUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.P1PointsToPassUpDown.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // P2ChangeColorButton
            // 
            this.P2ChangeColorButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.P2ChangeColorButton.Location = new System.Drawing.Point(2072, 204);
            this.P2ChangeColorButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.P2ChangeColorButton.Name = "P2ChangeColorButton";
            this.P2ChangeColorButton.Size = new System.Drawing.Size(278, 75);
            this.P2ChangeColorButton.TabIndex = 9;
            this.P2ChangeColorButton.Text = "Change Color";
            this.P2ChangeColorButton.UseVisualStyleBackColor = true;
            this.P2ChangeColorButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // P1ChangeColorButton
            // 
            this.P1ChangeColorButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.P1ChangeColorButton.Location = new System.Drawing.Point(14, 204);
            this.P1ChangeColorButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.P1ChangeColorButton.Name = "P1ChangeColorButton";
            this.P1ChangeColorButton.Size = new System.Drawing.Size(278, 75);
            this.P1ChangeColorButton.TabIndex = 8;
            this.P1ChangeColorButton.Text = "Change Color";
            this.P1ChangeColorButton.UseVisualStyleBackColor = true;
            this.P1ChangeColorButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(724, 179);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(300, 37);
            this.label7.TabIndex = 7;
            this.label7.Text = "Increment on Pass";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1280, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 37);
            this.label2.TabIndex = 6;
            this.label2.Text = "Seconds";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(814, 62);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 37);
            this.label1.TabIndex = 5;
            this.label1.Text = "Minutes";
            // 
            // P1IncrementUpDown
            // 
            this.P1IncrementUpDown.DecimalPlaces = 2;
            this.P1IncrementUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.P1IncrementUpDown.Location = new System.Drawing.Point(704, 227);
            this.P1IncrementUpDown.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.P1IncrementUpDown.Name = "P1IncrementUpDown";
            this.P1IncrementUpDown.Size = new System.Drawing.Size(400, 44);
            this.P1IncrementUpDown.TabIndex = 4;
            this.P1IncrementUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.P1IncrementUpDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.P1IncrementUpDown.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // Secs
            // 
            this.Secs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Secs.Location = new System.Drawing.Point(1170, 112);
            this.Secs.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Secs.Name = "Secs";
            this.Secs.Size = new System.Drawing.Size(400, 44);
            this.Secs.TabIndex = 3;
            this.Secs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Secs.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.Secs.ValueChanged += new System.EventHandler(this.Secs_ValueChanged);
            // 
            // Mins
            // 
            this.Mins.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mins.Location = new System.Drawing.Point(706, 112);
            this.Mins.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.Mins.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Mins.Name = "Mins";
            this.Mins.Size = new System.Drawing.Size(400, 44);
            this.Mins.TabIndex = 2;
            this.Mins.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Mins.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.Mins.ValueChanged += new System.EventHandler(this.Mins_ValueChanged);
            // 
            // P2StartsRadioButton
            // 
            this.P2StartsRadioButton.AutoSize = true;
            this.P2StartsRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.P2StartsRadioButton.Location = new System.Drawing.Point(2006, 25);
            this.P2StartsRadioButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.P2StartsRadioButton.Name = "P2StartsRadioButton";
            this.P2StartsRadioButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.P2StartsRadioButton.Size = new System.Drawing.Size(315, 48);
            this.P2StartsRadioButton.TabIndex = 1;
            this.P2StartsRadioButton.TabStop = true;
            this.P2StartsRadioButton.Text = "Player 2 Starts";
            this.P2StartsRadioButton.UseVisualStyleBackColor = true;
            this.P2StartsRadioButton.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // P1StartsRadioButton
            // 
            this.P1StartsRadioButton.AutoSize = true;
            this.P1StartsRadioButton.Checked = true;
            this.P1StartsRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.P1StartsRadioButton.Location = new System.Drawing.Point(12, 25);
            this.P1StartsRadioButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.P1StartsRadioButton.Name = "P1StartsRadioButton";
            this.P1StartsRadioButton.Size = new System.Drawing.Size(315, 48);
            this.P1StartsRadioButton.TabIndex = 0;
            this.P1StartsRadioButton.TabStop = true;
            this.P1StartsRadioButton.Text = "Player 1 Starts";
            this.P1StartsRadioButton.UseVisualStyleBackColor = true;
            this.P1StartsRadioButton.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // settingsButton
            // 
            this.settingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsButton.Location = new System.Drawing.Point(1950, 17);
            this.settingsButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(210, 90);
            this.settingsButton.TabIndex = 14;
            this.settingsButton.Text = "Settings";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.Lavender;
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 25;
            this.listBox1.Location = new System.Drawing.Point(24, 271);
            this.listBox1.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBox1.Size = new System.Drawing.Size(594, 817);
            this.listBox1.TabIndex = 15;
            this.listBox1.TabStop = false;
            // 
            // LosingPlayerLabel
            // 
            this.LosingPlayerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LosingPlayerLabel.Location = new System.Drawing.Point(940, 994);
            this.LosingPlayerLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.LosingPlayerLabel.Name = "LosingPlayerLabel";
            this.LosingPlayerLabel.Size = new System.Drawing.Size(510, 238);
            this.LosingPlayerLabel.TabIndex = 17;
            this.LosingPlayerLabel.Text = "PLAYER 1 LOST";
            this.LosingPlayerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox5.Image = global::MusicBeePlugin.Properties.Resources.noooo;
            this.pictureBox5.Location = new System.Drawing.Point(1448, 994);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(248, 238);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 20;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Location = new System.Drawing.Point(-6, 412);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(702, 675);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 19;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Visible = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Location = new System.Drawing.Point(1696, 412);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(702, 675);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 18;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::MusicBeePlugin.Properties.Resources.D_colon;
            this.pictureBox2.Location = new System.Drawing.Point(696, 994);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(248, 238);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 16;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(696, 271);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1000, 962);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // Player1Name
            // 
            this.Player1Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.Player1Name.Location = new System.Drawing.Point(16, 212);
            this.Player1Name.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Player1Name.Name = "Player1Name";
            this.Player1Name.Size = new System.Drawing.Size(506, 75);
            this.Player1Name.TabIndex = 21;
            this.Player1Name.Text = "Player1";
            this.Player1Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Player2Name
            // 
            this.Player2Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player2Name.Location = new System.Drawing.Point(1888, 212);
            this.Player2Name.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Player2Name.Name = "Player2Name";
            this.Player2Name.Size = new System.Drawing.Size(506, 75);
            this.Player2Name.TabIndex = 22;
            this.Player2Name.Text = "Player2";
            this.Player2Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VGMV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(2408, 1285);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.songName);
            this.Controls.Add(this.Player1Name);
            this.Controls.Add(this.Player2Name);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.LosingPlayerLabel);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.restartButton);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.TimerP2);
            this.Controls.Add(this.TimerP1);
            this.Controls.Add(this.ScoreP1);
            this.Controls.Add(this.ScoreP2);
            this.Controls.Add(this.Start);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "VGMV";
            this.Text = "VGMV";
            this.Load += new System.EventHandler(this.VGMV_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.P2IncrementUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.P2PointsToPassUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.P1PointsToPassUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.P1IncrementUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Secs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Mins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox P2NameTextBox;
        private System.Windows.Forms.Label P2NameSettingsLabel;
        private System.Windows.Forms.Label Player2Name;
        private System.Windows.Forms.TextBox P1NameTextBox;
        private System.Windows.Forms.Label P1NameSettingsLabel;
        private System.Windows.Forms.Label Player1Name;
        private System.Windows.Forms.Label songName;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Label ScoreP2;
        private System.Windows.Forms.Label ScoreP1;
        private System.Windows.Forms.Label TimerP1;
        private System.Windows.Forms.Label TimerP2;
        private System.Windows.Forms.Button restartButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.CheckBox DisplayHistoryCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton P2StartsRadioButton;
        private System.Windows.Forms.RadioButton P1StartsRadioButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown Secs;
        private System.Windows.Forms.NumericUpDown Mins;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button P2ChangeColorButton;
        private System.Windows.Forms.Button P1ChangeColorButton;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.ColorDialog colorDialog2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown P2PointsToPassUpDown;
        private System.Windows.Forms.NumericUpDown P1PointsToPassUpDown;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown P2IncrementUpDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown P1IncrementUpDown;
        private System.Windows.Forms.Label LosingPlayerLabel;
        private System.Windows.Forms.CheckBox LoopPlaylistCheckBox;
        private System.Windows.Forms.CheckBox ShufflePlaylistCheckBox;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.CheckBox SingePlayerCheckBox;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Button export;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}