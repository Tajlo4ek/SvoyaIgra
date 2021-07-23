namespace SvoyaIgra.Forms
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.mediaTimer = new System.Windows.Forms.Timer(this.components);
            this.videoPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.imagePlayer = new System.Windows.Forms.PictureBox();
            this.pbRoundData = new System.Windows.Forms.PictureBox();
            this.moveTimer = new System.Windows.Forms.Timer(this.components);
            this.rtbChat = new System.Windows.Forms.RichTextBox();
            this.tlpPlayers = new System.Windows.Forms.TableLayoutPanel();
            this.adminPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnSP = new System.Windows.Forms.Button();
            this.btnAns = new System.Windows.Forms.Button();
            this.btnSkip = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.tlpAns = new System.Windows.Forms.TableLayoutPanel();
            this.btnAnsFalse = new System.Windows.Forms.Button();
            this.btnAnsTrue = new System.Windows.Forms.Button();
            this.pbAnswer = new System.Windows.Forms.PictureBox();
            this.pbAdminSay = new System.Windows.Forms.PictureBox();
            this.pbAdminImage = new System.Windows.Forms.PictureBox();
            this.pbAdminName = new System.Windows.Forms.PictureBox();
            this.pbProgressBar = new System.Windows.Forms.PictureBox();
            this.userConfigMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.kickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configMoneyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setChoiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbConfigUser = new System.Windows.Forms.GroupBox();
            this.tbConfigMoney = new System.Windows.Forms.TextBox();
            this.btnConfigCancel = new System.Windows.Forms.Button();
            this.btnConfigSet = new System.Windows.Forms.Button();
            this.gbAuction = new System.Windows.Forms.GroupBox();
            this.tbAuctionRate = new System.Windows.Forms.TextBox();
            this.btnAuctionSet = new System.Windows.Forms.Button();
            this.btnAuctionAllIn = new System.Windows.Forms.Button();
            this.btnAuctionPass = new System.Windows.Forms.Button();
            this.trBarAuctionRate = new System.Windows.Forms.TrackBar();
            this.gbFinalAns = new System.Windows.Forms.GroupBox();
            this.btnAnsFinal = new System.Windows.Forms.Button();
            this.rtbAns = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.videoPlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagePlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRoundData)).BeginInit();
            this.adminPanel.SuspendLayout();
            this.tlpAns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAnswer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdminSay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdminImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdminName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbProgressBar)).BeginInit();
            this.userConfigMenu.SuspendLayout();
            this.gbConfigUser.SuspendLayout();
            this.gbAuction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trBarAuctionRate)).BeginInit();
            this.gbFinalAns.SuspendLayout();
            this.SuspendLayout();
            // 
            // mediaTimer
            // 
            this.mediaTimer.Tick += new System.EventHandler(this.MediaTimer_Tick);
            // 
            // videoPlayer
            // 
            this.videoPlayer.Enabled = true;
            this.videoPlayer.Location = new System.Drawing.Point(150, 25);
            this.videoPlayer.Name = "videoPlayer";
            this.videoPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("videoPlayer.OcxState")));
            this.videoPlayer.Size = new System.Drawing.Size(500, 300);
            this.videoPlayer.TabIndex = 2;
            // 
            // imagePlayer
            // 
            this.imagePlayer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.imagePlayer.BackgroundImage = global::SvoyaIgra.Properties.Resources.Background;
            this.imagePlayer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imagePlayer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imagePlayer.Location = new System.Drawing.Point(150, 25);
            this.imagePlayer.Margin = new System.Windows.Forms.Padding(0);
            this.imagePlayer.Name = "imagePlayer";
            this.imagePlayer.Size = new System.Drawing.Size(500, 300);
            this.imagePlayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imagePlayer.TabIndex = 3;
            this.imagePlayer.TabStop = false;
            // 
            // pbRoundData
            // 
            this.pbRoundData.BackgroundImage = global::SvoyaIgra.Properties.Resources.Background;
            this.pbRoundData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbRoundData.InitialImage = global::SvoyaIgra.Properties.Resources.Background;
            this.pbRoundData.Location = new System.Drawing.Point(150, 25);
            this.pbRoundData.Name = "pbRoundData";
            this.pbRoundData.Size = new System.Drawing.Size(500, 300);
            this.pbRoundData.TabIndex = 4;
            this.pbRoundData.TabStop = false;
            this.pbRoundData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PbRoundData_MouseDown);
            this.pbRoundData.MouseLeave += new System.EventHandler(this.PbRoundData_MouseLeave);
            this.pbRoundData.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PbRoundData_MouseMove);
            // 
            // rtbChat
            // 
            this.rtbChat.Location = new System.Drawing.Point(656, 25);
            this.rtbChat.Name = "rtbChat";
            this.rtbChat.ReadOnly = true;
            this.rtbChat.Size = new System.Drawing.Size(116, 300);
            this.rtbChat.TabIndex = 5;
            this.rtbChat.TabStop = false;
            this.rtbChat.Text = "";
            // 
            // tlpPlayers
            // 
            this.tlpPlayers.ColumnCount = 1;
            this.tlpPlayers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPlayers.Location = new System.Drawing.Point(150, 328);
            this.tlpPlayers.Name = "tlpPlayers";
            this.tlpPlayers.RowCount = 3;
            this.tlpPlayers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPlayers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpPlayers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpPlayers.Size = new System.Drawing.Size(500, 121);
            this.tlpPlayers.TabIndex = 6;
            // 
            // adminPanel
            // 
            this.adminPanel.ColumnCount = 1;
            this.adminPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.adminPanel.Controls.Add(this.btnSP, 0, 0);
            this.adminPanel.Controls.Add(this.btnAns, 0, 1);
            this.adminPanel.Controls.Add(this.btnSkip, 0, 2);
            this.adminPanel.Controls.Add(this.btnExit, 0, 3);
            this.adminPanel.Location = new System.Drawing.Point(656, 328);
            this.adminPanel.Name = "adminPanel";
            this.adminPanel.RowCount = 4;
            this.adminPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.adminPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.adminPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.adminPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.adminPanel.Size = new System.Drawing.Size(116, 121);
            this.adminPanel.TabIndex = 7;
            this.adminPanel.TabStop = true;
            // 
            // btnSP
            // 
            this.btnSP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSP.Location = new System.Drawing.Point(3, 3);
            this.btnSP.Name = "btnSP";
            this.btnSP.Size = new System.Drawing.Size(110, 24);
            this.btnSP.TabIndex = 0;
            this.btnSP.TabStop = false;
            this.btnSP.Text = "старт";
            this.btnSP.UseVisualStyleBackColor = true;
            this.btnSP.Click += new System.EventHandler(this.BtnSP_Click);
            // 
            // btnAns
            // 
            this.btnAns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAns.Location = new System.Drawing.Point(3, 33);
            this.btnAns.Name = "btnAns";
            this.btnAns.Size = new System.Drawing.Size(110, 24);
            this.btnAns.TabIndex = 8;
            this.btnAns.Text = "Ответить";
            this.btnAns.UseVisualStyleBackColor = true;
            this.btnAns.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnAns_MouseDown);
            // 
            // btnSkip
            // 
            this.btnSkip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSkip.Enabled = false;
            this.btnSkip.Location = new System.Drawing.Point(3, 63);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(110, 24);
            this.btnSkip.TabIndex = 9;
            this.btnSkip.TabStop = false;
            this.btnSkip.Text = "пропуcтить";
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Click += new System.EventHandler(this.BtnSkip_Click);
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Enabled = false;
            this.btnExit.Location = new System.Drawing.Point(3, 93);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(110, 25);
            this.btnExit.TabIndex = 10;
            this.btnExit.TabStop = false;
            this.btnExit.Text = "выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // tlpAns
            // 
            this.tlpAns.ColumnCount = 1;
            this.tlpAns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAns.Controls.Add(this.btnAnsFalse, 0, 1);
            this.tlpAns.Controls.Add(this.btnAnsTrue, 0, 0);
            this.tlpAns.Location = new System.Drawing.Point(12, 250);
            this.tlpAns.Name = "tlpAns";
            this.tlpAns.RowCount = 2;
            this.tlpAns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAns.Size = new System.Drawing.Size(132, 72);
            this.tlpAns.TabIndex = 7;
            this.tlpAns.Visible = false;
            // 
            // btnAnsFalse
            // 
            this.btnAnsFalse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAnsFalse.Location = new System.Drawing.Point(3, 39);
            this.btnAnsFalse.Name = "btnAnsFalse";
            this.btnAnsFalse.Size = new System.Drawing.Size(126, 30);
            this.btnAnsFalse.TabIndex = 11;
            this.btnAnsFalse.TabStop = false;
            this.btnAnsFalse.Text = "Не верно";
            this.btnAnsFalse.UseVisualStyleBackColor = true;
            this.btnAnsFalse.Click += new System.EventHandler(this.BtnAnsFalse_Click);
            // 
            // btnAnsTrue
            // 
            this.btnAnsTrue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAnsTrue.Location = new System.Drawing.Point(3, 3);
            this.btnAnsTrue.Name = "btnAnsTrue";
            this.btnAnsTrue.Size = new System.Drawing.Size(126, 30);
            this.btnAnsTrue.TabIndex = 0;
            this.btnAnsTrue.TabStop = false;
            this.btnAnsTrue.Text = "Верно";
            this.btnAnsTrue.UseVisualStyleBackColor = true;
            this.btnAnsTrue.Click += new System.EventHandler(this.BtnAnsTrue_Click);
            // 
            // pbAnswer
            // 
            this.pbAnswer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.pbAnswer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbAnswer.InitialImage = global::SvoyaIgra.Properties.Resources.Background;
            this.pbAnswer.Location = new System.Drawing.Point(12, 328);
            this.pbAnswer.Name = "pbAnswer";
            this.pbAnswer.Size = new System.Drawing.Size(132, 121);
            this.pbAnswer.TabIndex = 12;
            this.pbAnswer.TabStop = false;
            this.pbAnswer.Visible = false;
            // 
            // pbAdminSay
            // 
            this.pbAdminSay.BackColor = System.Drawing.Color.Blue;
            this.pbAdminSay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbAdminSay.Location = new System.Drawing.Point(12, 196);
            this.pbAdminSay.Name = "pbAdminSay";
            this.pbAdminSay.Size = new System.Drawing.Size(132, 51);
            this.pbAdminSay.TabIndex = 13;
            this.pbAdminSay.TabStop = false;
            // 
            // pbAdminImage
            // 
            this.pbAdminImage.ErrorImage = global::SvoyaIgra.Properties.Resources.NoImg;
            this.pbAdminImage.InitialImage = null;
            this.pbAdminImage.Location = new System.Drawing.Point(12, 25);
            this.pbAdminImage.Name = "pbAdminImage";
            this.pbAdminImage.Size = new System.Drawing.Size(132, 133);
            this.pbAdminImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbAdminImage.TabIndex = 14;
            this.pbAdminImage.TabStop = false;
            // 
            // pbAdminName
            // 
            this.pbAdminName.BackColor = System.Drawing.Color.Transparent;
            this.pbAdminName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbAdminName.Location = new System.Drawing.Point(12, 164);
            this.pbAdminName.Name = "pbAdminName";
            this.pbAdminName.Size = new System.Drawing.Size(132, 26);
            this.pbAdminName.TabIndex = 15;
            this.pbAdminName.TabStop = false;
            // 
            // pbProgressBar
            // 
            this.pbProgressBar.ErrorImage = null;
            this.pbProgressBar.InitialImage = null;
            this.pbProgressBar.Location = new System.Drawing.Point(150, 12);
            this.pbProgressBar.Name = "pbProgressBar";
            this.pbProgressBar.Size = new System.Drawing.Size(500, 10);
            this.pbProgressBar.TabIndex = 17;
            this.pbProgressBar.TabStop = false;
            this.pbProgressBar.Visible = false;
            // 
            // userConfigMenu
            // 
            this.userConfigMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kickToolStripMenuItem,
            this.configMoneyToolStripMenuItem,
            this.setChoiseToolStripMenuItem});
            this.userConfigMenu.Name = "userConfigMenu";
            this.userConfigMenu.Size = new System.Drawing.Size(151, 70);
            // 
            // kickToolStripMenuItem
            // 
            this.kickToolStripMenuItem.Name = "kickToolStripMenuItem";
            this.kickToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.kickToolStripMenuItem.Text = "kick";
            this.kickToolStripMenuItem.Click += new System.EventHandler(this.KickToolStripMenuItem_Click);
            // 
            // configMoneyToolStripMenuItem
            // 
            this.configMoneyToolStripMenuItem.Name = "configMoneyToolStripMenuItem";
            this.configMoneyToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.configMoneyToolStripMenuItem.Text = "Config Money";
            this.configMoneyToolStripMenuItem.Click += new System.EventHandler(this.ConfigMoneyToolStripMenuItem_Click);
            // 
            // setChoiseToolStripMenuItem
            // 
            this.setChoiseToolStripMenuItem.Name = "setChoiseToolStripMenuItem";
            this.setChoiseToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.setChoiseToolStripMenuItem.Text = "Set choice";
            this.setChoiseToolStripMenuItem.Click += new System.EventHandler(this.SetChoiseToolStripMenuItem_Click);
            // 
            // gbConfigUser
            // 
            this.gbConfigUser.Controls.Add(this.tbConfigMoney);
            this.gbConfigUser.Controls.Add(this.btnConfigCancel);
            this.gbConfigUser.Controls.Add(this.btnConfigSet);
            this.gbConfigUser.Location = new System.Drawing.Point(306, 38);
            this.gbConfigUser.Name = "gbConfigUser";
            this.gbConfigUser.Size = new System.Drawing.Size(200, 100);
            this.gbConfigUser.TabIndex = 18;
            this.gbConfigUser.TabStop = false;
            this.gbConfigUser.Text = "gbConfigUser";
            this.gbConfigUser.Visible = false;
            // 
            // tbConfigMoney
            // 
            this.tbConfigMoney.Location = new System.Drawing.Point(6, 35);
            this.tbConfigMoney.Name = "tbConfigMoney";
            this.tbConfigMoney.Size = new System.Drawing.Size(188, 20);
            this.tbConfigMoney.TabIndex = 2;
            // 
            // btnConfigCancel
            // 
            this.btnConfigCancel.Location = new System.Drawing.Point(119, 71);
            this.btnConfigCancel.Name = "btnConfigCancel";
            this.btnConfigCancel.Size = new System.Drawing.Size(75, 23);
            this.btnConfigCancel.TabIndex = 1;
            this.btnConfigCancel.Text = "Cancel";
            this.btnConfigCancel.UseVisualStyleBackColor = true;
            this.btnConfigCancel.Click += new System.EventHandler(this.BtnConfigCancel_Click);
            // 
            // btnConfigSet
            // 
            this.btnConfigSet.Location = new System.Drawing.Point(6, 71);
            this.btnConfigSet.Name = "btnConfigSet";
            this.btnConfigSet.Size = new System.Drawing.Size(75, 23);
            this.btnConfigSet.TabIndex = 0;
            this.btnConfigSet.Text = "Set";
            this.btnConfigSet.UseVisualStyleBackColor = true;
            this.btnConfigSet.Click += new System.EventHandler(this.BtnConfigSet_Click);
            // 
            // gbAuction
            // 
            this.gbAuction.Controls.Add(this.tbAuctionRate);
            this.gbAuction.Controls.Add(this.btnAuctionSet);
            this.gbAuction.Controls.Add(this.btnAuctionAllIn);
            this.gbAuction.Controls.Add(this.btnAuctionPass);
            this.gbAuction.Controls.Add(this.trBarAuctionRate);
            this.gbAuction.Location = new System.Drawing.Point(306, 144);
            this.gbAuction.Name = "gbAuction";
            this.gbAuction.Size = new System.Drawing.Size(200, 100);
            this.gbAuction.TabIndex = 19;
            this.gbAuction.TabStop = false;
            this.gbAuction.Text = "Аукцион";
            this.gbAuction.Visible = false;
            // 
            // tbAuctionRate
            // 
            this.tbAuctionRate.Enabled = false;
            this.tbAuctionRate.Location = new System.Drawing.Point(6, 45);
            this.tbAuctionRate.Name = "tbAuctionRate";
            this.tbAuctionRate.Size = new System.Drawing.Size(187, 20);
            this.tbAuctionRate.TabIndex = 4;
            this.tbAuctionRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnAuctionSet
            // 
            this.btnAuctionSet.Location = new System.Drawing.Point(135, 71);
            this.btnAuctionSet.Name = "btnAuctionSet";
            this.btnAuctionSet.Size = new System.Drawing.Size(58, 23);
            this.btnAuctionSet.TabIndex = 3;
            this.btnAuctionSet.Text = "ставка";
            this.btnAuctionSet.UseVisualStyleBackColor = true;
            this.btnAuctionSet.Click += new System.EventHandler(this.BtnAuctionSet_Click);
            // 
            // btnAuctionAllIn
            // 
            this.btnAuctionAllIn.Location = new System.Drawing.Point(71, 71);
            this.btnAuctionAllIn.Name = "btnAuctionAllIn";
            this.btnAuctionAllIn.Size = new System.Drawing.Size(58, 23);
            this.btnAuctionAllIn.TabIndex = 2;
            this.btnAuctionAllIn.Text = "ва-банк";
            this.btnAuctionAllIn.UseVisualStyleBackColor = true;
            this.btnAuctionAllIn.Click += new System.EventHandler(this.BtnAuctionAllIn_Click);
            // 
            // btnAuctionPass
            // 
            this.btnAuctionPass.Location = new System.Drawing.Point(6, 71);
            this.btnAuctionPass.Name = "btnAuctionPass";
            this.btnAuctionPass.Size = new System.Drawing.Size(59, 23);
            this.btnAuctionPass.TabIndex = 1;
            this.btnAuctionPass.Text = "пас";
            this.btnAuctionPass.UseVisualStyleBackColor = true;
            this.btnAuctionPass.Click += new System.EventHandler(this.BtnAuctionPass_Click);
            // 
            // trBarAuctionRate
            // 
            this.trBarAuctionRate.AutoSize = false;
            this.trBarAuctionRate.LargeChange = 50;
            this.trBarAuctionRate.Location = new System.Drawing.Point(6, 20);
            this.trBarAuctionRate.Maximum = 50;
            this.trBarAuctionRate.Name = "trBarAuctionRate";
            this.trBarAuctionRate.Size = new System.Drawing.Size(188, 26);
            this.trBarAuctionRate.SmallChange = 50;
            this.trBarAuctionRate.TabIndex = 0;
            this.trBarAuctionRate.TickFrequency = 100;
            this.trBarAuctionRate.ValueChanged += new System.EventHandler(this.TrBarRate_ValueChanged);
            // 
            // gbFinalAns
            // 
            this.gbFinalAns.Controls.Add(this.btnAnsFinal);
            this.gbFinalAns.Controls.Add(this.rtbAns);
            this.gbFinalAns.Location = new System.Drawing.Point(150, 328);
            this.gbFinalAns.Name = "gbFinalAns";
            this.gbFinalAns.Size = new System.Drawing.Size(500, 121);
            this.gbFinalAns.TabIndex = 20;
            this.gbFinalAns.TabStop = false;
            this.gbFinalAns.Text = "Введите ответ";
            this.gbFinalAns.Visible = false;
            // 
            // btnAnsFinal
            // 
            this.btnAnsFinal.Location = new System.Drawing.Point(419, 19);
            this.btnAnsFinal.Name = "btnAnsFinal";
            this.btnAnsFinal.Size = new System.Drawing.Size(75, 96);
            this.btnAnsFinal.TabIndex = 1;
            this.btnAnsFinal.Text = "ответить";
            this.btnAnsFinal.UseVisualStyleBackColor = true;
            this.btnAnsFinal.Click += new System.EventHandler(this.BtnAnsFinal_Click);
            // 
            // rtbAns
            // 
            this.rtbAns.Location = new System.Drawing.Point(6, 19);
            this.rtbAns.Name = "rtbAns";
            this.rtbAns.Size = new System.Drawing.Size(410, 96);
            this.rtbAns.TabIndex = 0;
            this.rtbAns.Text = "";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.gbFinalAns);
            this.Controls.Add(this.gbAuction);
            this.Controls.Add(this.pbAdminName);
            this.Controls.Add(this.pbAdminImage);
            this.Controls.Add(this.gbConfigUser);
            this.Controls.Add(this.pbProgressBar);
            this.Controls.Add(this.pbAdminSay);
            this.Controls.Add(this.pbAnswer);
            this.Controls.Add(this.tlpAns);
            this.Controls.Add(this.adminPanel);
            this.Controls.Add(this.tlpPlayers);
            this.Controls.Add(this.rtbChat);
            this.Controls.Add(this.pbRoundData);
            this.Controls.Add(this.imagePlayer);
            this.Controls.Add(this.videoPlayer);
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jeopardy";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameForm_FormClosed);
            this.SizeChanged += new System.EventHandler(this.GameForm_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.videoPlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagePlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRoundData)).EndInit();
            this.adminPanel.ResumeLayout(false);
            this.tlpAns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAnswer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdminSay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdminImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdminName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbProgressBar)).EndInit();
            this.userConfigMenu.ResumeLayout(false);
            this.gbConfigUser.ResumeLayout(false);
            this.gbConfigUser.PerformLayout();
            this.gbAuction.ResumeLayout(false);
            this.gbAuction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trBarAuctionRate)).EndInit();
            this.gbFinalAns.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer mediaTimer;
        private AxWMPLib.AxWindowsMediaPlayer videoPlayer;
        private System.Windows.Forms.PictureBox imagePlayer;
        private System.Windows.Forms.PictureBox pbRoundData;
        private System.Windows.Forms.Timer moveTimer;
        private System.Windows.Forms.RichTextBox rtbChat;
        private System.Windows.Forms.TableLayoutPanel tlpPlayers;
        private System.Windows.Forms.TableLayoutPanel adminPanel;
        private System.Windows.Forms.Button btnSP;
        private System.Windows.Forms.Button btnAns;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TableLayoutPanel tlpAns;
        private System.Windows.Forms.Button btnAnsFalse;
        private System.Windows.Forms.Button btnAnsTrue;
        private System.Windows.Forms.PictureBox pbAnswer;
        private System.Windows.Forms.PictureBox pbAdminSay;
        private System.Windows.Forms.PictureBox pbAdminImage;
        private System.Windows.Forms.PictureBox pbAdminName;
        private System.Windows.Forms.PictureBox pbProgressBar;
        private System.Windows.Forms.ContextMenuStrip userConfigMenu;
        private System.Windows.Forms.ToolStripMenuItem kickToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbConfigUser;
        private System.Windows.Forms.TextBox tbConfigMoney;
        private System.Windows.Forms.Button btnConfigCancel;
        private System.Windows.Forms.Button btnConfigSet;
        private System.Windows.Forms.ToolStripMenuItem configMoneyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setChoiseToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbAuction;
        private System.Windows.Forms.TrackBar trBarAuctionRate;
        private System.Windows.Forms.TextBox tbAuctionRate;
        private System.Windows.Forms.Button btnAuctionSet;
        private System.Windows.Forms.Button btnAuctionAllIn;
        private System.Windows.Forms.Button btnAuctionPass;
        private System.Windows.Forms.GroupBox gbFinalAns;
        private System.Windows.Forms.Button btnAnsFinal;
        private System.Windows.Forms.RichTextBox rtbAns;
    }
}