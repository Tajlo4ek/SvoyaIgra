using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SvoyaIgra.Forms
{
    public partial class GameForm : Form
    {
        private readonly Utils.Controllers.SizeController sizeController;
        private readonly Utils.Controllers.ImageController imageController;

        public Action<int, int> OnMouseMoveAction;
        public Action<int, int> OnMouseClickAction;

        public delegate bool StartStopDelegate(bool forse);
        public StartStopDelegate StartStopAction;

        private readonly Action onEndAction;
        public Action OnSkipAction;
        public Action<string> OnUserClick;
        public Action OnAnswerClick;
        public Action<bool> OnCheckUserAns;
        public Action<string> OnKickUser;
        public Action<string> OnConfigUserMoney;
        public Action<string, int> OnAcceptUserMoney;
        public Action OnCloseAction;
        public Action<string> SetChoiceUser;
        public Action<int> AuctionMove;
        public Action<string> FinalAnsClick;

        private bool canChoise;

        private bool isGamePaused;
        private bool isGameStarted;

        private readonly bool isServer;

        private class UserInfo
        {
            public UserInfo(string token, PictureBox image, PictureBox pbName, PictureBox pbMoney)
            {
                this.Token = token;
                this.PbImage = image;
                this.PbMoney = pbMoney;
                this.PbName = pbName;
                this.nameController = new Utils.Controllers.TextController(pbName, Color.Black);
                this.moneyController = new Utils.Controllers.TextController(pbMoney, Color.Black);
            }

            public readonly string Token;

            public PictureBox PbImage { get; private set; }

            public PictureBox PbName { get; private set; }

            public PictureBox PbMoney { get; private set; }

            private readonly Utils.Controllers.TextController nameController;

            private readonly Utils.Controllers.TextController moneyController;

            public void SetName(string name)
            {
                nameController.SetText(name);
            }

            public void SetMoney(string money)
            {
                moneyController.SetText(money);
            }

            public void Dispose()
            {
                nameController.Dispose();
                moneyController.Dispose();
                PbImage?.Dispose();
                PbMoney?.Dispose();
                PbName?.Dispose();
            }

            public void Resize()
            {
                nameController.Resize();
                moneyController.Resize();
            }

        }

        private readonly Utils.Controllers.TextController adminNameController;
        private readonly Utils.Controllers.TextController adminSayController;
        private readonly Utils.Controllers.TextController answerTextController;

        private readonly Utils.Controllers.ProgressBarController progressBarAnswerController;

        private readonly List<UserInfo> users;
        private string selectedUserToken = "";

        private readonly object locker;

        private int time;
        private bool toEnd;
        private bool isWaitAnswer;

        private bool isStartQuestion;

        public GameForm(int sizeX, int sizeY, Action onEnd, Utils.Controllers.ImageController.OnResizeDelegate onResize, bool isServer)
        {
            this.Size = new Size(sizeX, sizeY);

            this.DoubleBuffered = true;

            InitializeComponent();

            sizeController = new Utils.Controllers.SizeController(this.Size);

            sizeController.AddControl(imagePlayer);
            sizeController.AddControl(videoPlayer);
            sizeController.AddControl(pbRoundData);
            sizeController.AddControl(rtbChat);
            sizeController.AddControl(tlpPlayers);
            sizeController.AddControl(adminPanel);
            sizeController.AddControl(pbProgressBar);
            sizeController.AddControl(pbAdminImage);

            sizeController.AddControl(tlpAns);
            sizeController.AddControl(pbAnswer);
            sizeController.AddControl(pbAdminSay);
            sizeController.AddControl(pbAdminName);

            sizeController.AddControl(gbFinalAns);
            sizeController.AddControl(rtbAns);
            sizeController.AddControl(btnAnsFinal);

            adminNameController = new Utils.Controllers.TextController(pbAdminName, Color.Black);
            answerTextController = new Utils.Controllers.TextController(pbAnswer, Utils.Controllers.GameController.MainColor);
            adminSayController = new Utils.Controllers.TextController(pbAdminSay, Utils.Controllers.GameController.MainColor);

            progressBarAnswerController = new Utils.Controllers.ProgressBarController(pbProgressBar, Color.Blue);

            videoPlayer.PlayStateChange += VideoPlayer_PlayStateChange;

            imagePlayer.Visible = false;
            videoPlayer.Visible = false;
            pbRoundData.Visible = true;

            imageController = new Utils.Controllers.ImageController(pbRoundData, moveTimer, onEnd, onResize);

            onEndAction += onEnd;

            this.WindowState = FormWindowState.Normal;

            btnSP.Enabled = isServer;
            tlpAns.Visible = false;
            pbAnswer.Visible = false;
            btnSkip.Enabled = false;
            btnAns.Enabled = false;

            users = new List<UserInfo>();
            canChoise = false;

            isGamePaused = true;
            this.isServer = isServer;

            mediaTimer.Interval = 50;

            trBarAuctionRate.SmallChange = Utils.Controllers.GameController.AuctionStep;
            trBarAuctionRate.LargeChange = Utils.Controllers.GameController.AuctionStep;
            trBarAuctionRate.TickFrequency = Utils.Controllers.GameController.AuctionStep;

            locker = new object();
        }

        public void SetAdminData(string name)
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    adminNameController.SetText(name);
                }
            }));
        }

        public void SetAdminImage(string path)
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    pbAdminImage.ImageLocation = path;
                    pbAdminImage.LoadAsync();
                }
            }));
        }

        private void VideoPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == 8)
            {
                FinishMedia();
            }
        }

        public void AddUserData(string name, string money, string token)
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    lock (users)
                    {
                        var userCount = users.Count;

                        if (userCount != 0)
                        {
                            tlpPlayers.ColumnCount++;
                            tlpPlayers.ColumnStyles.Add(new ColumnStyle());

                            foreach (ColumnStyle style in tlpPlayers.ColumnStyles)
                            {
                                style.SizeType = SizeType.Percent;
                                style.Width = (float)100 / tlpPlayers.ColumnCount;
                            }
                        }

                        var pbImage = new PictureBox
                        {
                            Name = token,
                            Dock = DockStyle.Fill,
                            SizeMode = PictureBoxSizeMode.Zoom,
                            ErrorImage = Properties.Resources.NoImg,
                        };
                        pbImage.MouseClick += OnUserPictureClick;

                        var pbName = new PictureBox
                        {
                            Name = token + "_name",
                            Dock = DockStyle.Fill,
                        };

                        var pbMoney = new PictureBox
                        {
                            Name = token + "_money",
                            Dock = DockStyle.Fill,
                        };

                        tlpPlayers.Controls.Add(pbImage, userCount, 0);
                        tlpPlayers.Controls.Add(pbName, userCount, 1);
                        tlpPlayers.Controls.Add(pbMoney, userCount, 2);


                        var user = new UserInfo(token, pbImage, pbName, pbMoney);
                        user.SetName(name);
                        user.SetMoney(money);

                        users.Add(user);
                        users.ForEach((x) => x.Resize());
                    }
                }
            }));
        }

        public void AddUserImage(string token, string url)
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    lock (users)
                    {
                        foreach (var user in users)
                        {
                            if (user.Token.Equals(token))
                            {
                                user.PbImage.LoadAsync(url);
                            }
                        }
                    }
                }
            }));
        }

        public void PlayMedia(string path, int time, bool isQuestion = false)
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    imageController.Stop();
                    imagePlayer.Visible = false;
                    videoPlayer.Visible = true;
                    pbRoundData.Visible = false;

                    if (!isQuestion)
                    {
                        pbProgressBar.Visible = false;
                        isStartQuestion = false;
                    }

                    if (!isServer)
                    {
                        if (!isStartQuestion)
                        {
                            btnAns.Enabled = true;
                        }
                        isStartQuestion = true;
                    }

                    videoPlayer.URL = path;

                    isStartQuestion = true;

                    if (time != -1)
                    {
                        mediaTimer.Start();
                        toEnd = false;
                        this.time = time * 1000;
                    }
                    else
                    {
                        toEnd = true;
                    }
                }
            }));

        }

        public void ShowImage(string path, int time, bool isQuestion = false)
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    imageController.Stop();
                    imagePlayer.Visible = true;
                    videoPlayer.Visible = false;
                    pbRoundData.Visible = false;

                    if (!isQuestion)
                    {
                        pbProgressBar.Visible = false;
                        isStartQuestion = false;
                    }

                    if (!isServer)
                    {
                        if (!isStartQuestion)
                        {
                            btnAns.Enabled = true;
                        }
                        isStartQuestion = true;
                    }


                    imagePlayer.Image = imagePlayer.InitialImage;
                    imagePlayer.LoadAsync(path);

                    this.time = time * 1000;
                    mediaTimer.Start();
                }
            }));
        }

        public void ShowMoveToUpImage(Image img)
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    imagePlayer.Visible = false;
                    videoPlayer.Visible = false;
                    pbRoundData.Visible = true;
                    imageController.ShowMoveToUpImage(img);
                }
            }));
        }

        public void ShowImageBackground(Image img, float time, bool isQuestion = false)
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    imagePlayer.Visible = false;
                    videoPlayer.Visible = false;
                    pbRoundData.Visible = true;

                    if (!isQuestion)
                    {
                        pbProgressBar.Visible = false;
                        isStartQuestion = false;
                    }
                    else if (!isServer)
                    {
                        if (!isStartQuestion)
                        {
                            btnAns.Enabled = true;
                        }
                        isStartQuestion = true;
                    }

                    imageController.ShowImg(img, time);
                }
            }));
        }

        public Size GetViewSize()
        {
            return pbRoundData.Size;
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            videoPlayer.close();
            OnCloseAction();
        }

        private void MediaTimer_Tick(object sender, EventArgs e)
        {
            if (!toEnd)
            {
                if (time < 0)
                {
                    FinishMedia();
                }
                else
                {
                    time -= mediaTimer.Interval;
                    if (isWaitAnswer)
                    {
                        progressBarAnswerController.SetValue(time);
                    }
                }
            }

        }

        public void FinishMedia(bool callBack = true)
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    toEnd = false;
                    videoPlayer.Ctlcontrols.stop();
                    videoPlayer.URL = "";
                    mediaTimer.Stop();

                    isWaitAnswer = false;
                    pbProgressBar.Visible = false;
                    gbFinalAns.Visible = false;

                    gbAuction.Visible = false;

                    if (callBack)
                    {
                        onEndAction();
                    }
                }

            }));
        }

        public void Pause()
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    mediaTimer.Stop();
                    videoPlayer.Ctlcontrols.pause();
                    imageController.Pause();
                }
            }));
        }

        public void Start()
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    if (toEnd || time >= 0)
                    {
                        mediaTimer.Start();
                    }

                    if (videoPlayer.URL != "")
                    {
                        videoPlayer.Ctlcontrols.play();
                    }

                    imageController.Start();
                }
            }));
        }

        private void GameForm_SizeChanged(object sender, EventArgs e)
        {
            sizeController.ResizeAll(this.Size);

            imageController.Resize();
            answerTextController.Resize();

            adminNameController.Resize();
            adminSayController.Resize();
            answerTextController.Resize();

            users.ForEach((user) => user.Resize());
        }

        private void PbRoundData_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseClickAction(e.X, e.Y);
        }

        private void PbRoundData_MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMoveAction(e.X, e.Y);
        }

        private void PbRoundData_MouseLeave(object sender, EventArgs e)
        {
            OnMouseMoveAction(-1, -1);
        }

        public void AddToChat(string message)
        {
            rtbChat.BeginInvoke(new Action(() =>
            {
                rtbChat.AppendText(message);
                rtbChat.AppendText("\r\n");
                rtbChat.ScrollToCaret();
            }));
        }

        private void BtnSP_Click(object sender, EventArgs e)
        {
            isGamePaused = StartStopAction(false);
            if (!isGameStarted && isGamePaused)
            {
                var result = MessageBox.Show("Не все готовы. Начать?", "", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    isGamePaused = StartStopAction(true);
                    isGameStarted = true;
                    btnSkip.Enabled = isServer;
                }
            }
            else
            {
                isGameStarted = true;
                btnSkip.Enabled = isServer;
            }
            btnSP.Text = isGamePaused ? "старт" : "пауза";
        }

        private void OnUserPictureClick(object sender, MouseEventArgs e)
        {
            var userPb = (PictureBox)sender;
            var userToken = userPb.Name;
            gbConfigUser.Visible = false;

            if (e.Button == MouseButtons.Right)
            {
                if (isServer)
                {
                    selectedUserToken = userToken;
                    userConfigMenu.Show(userPb, e.Location);
                }
            }
            else
            {
                selectedUserToken = "";
            }

            if (!canChoise)
                return;

            OnUserClick(userToken);
        }

        public void SetCanChoise(bool can)
        {
            canChoise = can;

            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    lock (users)
                    {
                        foreach (var user in users)
                        {
                            user.PbImage.BorderStyle = can ? BorderStyle.FixedSingle : BorderStyle.None;
                        }

                        if (!isServer)
                        {
                            users[0].PbImage.BorderStyle = BorderStyle.None;
                        }
                    }
                }
            }));
        }

        public void WaitAnswer(int time, string text)
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    this.time = time * 1000;
                    isWaitAnswer = true;

                    progressBarAnswerController.SetMaxValue(this.time);
                    progressBarAnswerController.SetValue(0);
                    pbProgressBar.Visible = true;
                    gbAuction.Visible = false;

                    if (isServer)
                    {
                        SetAnswer(text);
                        btnAns.Enabled = false;
                    }

                    mediaTimer.Start();
                }

            }));
        }

        public void SetAnswer(string text)
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    if (isServer)
                    {
                        answerTextController.SetText(text);
                        btnAns.Enabled = false;
                    }
                }
            }));
        }

        private void BtnAnsTrue_Click(object sender, EventArgs e)
        {
            OnCheckUserAns(true);
        }

        private void BtnAnsFalse_Click(object sender, EventArgs e)
        {
            OnCheckUserAns(false);
        }

        public void SetCanAnswer(bool can)
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    if (!isServer)
                    {
                        btnAns.Enabled = can;
                    }
                }
            }));
        }

        public void SetAdminSay(string text)
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    adminSayController.SetText(text);
                }
            }));
        }

        public void EndGame()
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    btnExit.Enabled = true;
                    videoPlayer.Ctlcontrols.stop();
                    videoPlayer.Visible = false;
                    mediaTimer.Stop();
                    imagePlayer.Visible = false;
                    btnSP.Enabled = false;
                    btnSkip.Enabled = false;
                    btnAns.Enabled = false;
                }
            }));
        }

        public void ShowAnsMenu(bool show)
        {
            if (isServer)
            {
                this.BeginInvoke(new Action(() =>
                {
                    lock (locker)
                    {
                        tlpAns.Visible = show;
                        pbAnswer.Visible = show;
                    }
                }));
            }
        }

        public void UpdateMoney(string token, string money)
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    lock (users)
                    {
                        foreach (var user in users)
                        {
                            if (user.Token == token)
                            {
                                user.SetMoney(money);
                            }
                        }
                    }
                }
            }));
        }

        public void ShowMessageBox(string data)
        {
            MessageBox.Show(data);
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Kick(string token)
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    lock (users)
                    {
                        int delUserId;
                        for (delUserId = 0; delUserId < users.Count; delUserId++)
                        {
                            if (users[delUserId].Token == token)
                            {
                                var user = users[delUserId];

                                while (tlpPlayers.Controls.Contains(user.PbImage))
                                {
                                    tlpPlayers.Controls.Remove(user.PbImage);
                                }

                                while (tlpPlayers.Controls.Contains(user.PbName))
                                {
                                    tlpPlayers.Controls.Remove(user.PbName);
                                }

                                while (tlpPlayers.Controls.Contains(user.PbMoney))
                                {
                                    tlpPlayers.Controls.Remove(user.PbMoney);
                                }

                                user.Dispose();
                                break;
                            }
                        }

                        for (int i = delUserId + 1; i < users.Count; i++)
                        {
                            tlpPlayers.SetCellPosition(users[i].PbImage, new TableLayoutPanelCellPosition(i - 1, 0));
                            tlpPlayers.SetCellPosition(users[i].PbName, new TableLayoutPanelCellPosition(i - 1, 1));
                            tlpPlayers.SetCellPosition(users[i].PbMoney, new TableLayoutPanelCellPosition(i - 1, 2));
                        }

                        users.RemoveAt(delUserId);

                        if (tlpPlayers.ColumnCount > 0)
                        {
                            tlpPlayers.ColumnCount--;
                        }

                        users.ForEach((user) => user.Resize());
                    }
                }
            }));
        }

        private void BtnSkip_Click(object sender, EventArgs e)
        {
            OnSkipAction();

            tlpAns.Visible = false;
            pbAnswer.Visible = false;
            gbAuction.Visible = false;
            gbConfigUser.Visible = false;
        }

        private void BtnAns_MouseDown(object sender, MouseEventArgs e)
        {
            OnAnswerClick();
        }

        private void KickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!selectedUserToken.Equals(""))
            {
                OnKickUser(selectedUserToken);
                selectedUserToken = "";
            }
        }

        public void StartConfigUser(string name, int nowMoney)
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    gbConfigUser.Text = name;
                    tbConfigMoney.Text = nowMoney.ToString();
                    gbConfigUser.Visible = true;

                    gbConfigUser.Top = (this.Height - gbConfigUser.Height) / 2;
                    gbConfigUser.Left = (this.Width - gbConfigUser.Width) / 2;
                }
            }));
        }

        private void ConfigMoneyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnConfigUserMoney(selectedUserToken);
        }

        private void BtnConfigSet_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbConfigMoney.Text, out int money))
            {
                OnAcceptUserMoney(selectedUserToken, money);
            }
            gbConfigUser.Visible = false;
            selectedUserToken = "";
        }

        private void BtnConfigCancel_Click(object sender, EventArgs e)
        {
            gbConfigUser.Visible = false;
            selectedUserToken = "";
        }

        private void SetChoiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetChoiceUser(selectedUserToken);
            selectedUserToken = "";
        }

        public void ShowAuction(int minValue, int maxValue, bool canPass, bool canAllIn, bool canSet)
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    trBarAuctionRate.Minimum = (int)Math.Round((float)minValue / Utils.Controllers.GameController.AuctionStep);
                    tbAuctionRate.Text = (trBarAuctionRate.Minimum * Utils.Controllers.GameController.AuctionStep).ToString();

                    if (canAllIn && !canSet)
                    {
                        trBarAuctionRate.Minimum = maxValue;
                        trBarAuctionRate.Maximum = maxValue;
                        tbAuctionRate.Text = trBarAuctionRate.Minimum.ToString();
                    }
                    else
                    {
                        if (minValue < maxValue)
                        {
                            trBarAuctionRate.Maximum = maxValue / Utils.Controllers.GameController.AuctionStep;
                        }
                        else if (minValue == maxValue)
                        {
                            trBarAuctionRate.Minimum = minValue;
                            trBarAuctionRate.Maximum = minValue;
                            tbAuctionRate.Text = trBarAuctionRate.Minimum.ToString();
                        }
                        else
                        {
                            trBarAuctionRate.Maximum = trBarAuctionRate.Minimum;
                        }
                    }


                    trBarAuctionRate.Value = trBarAuctionRate.Minimum;

                    btnAuctionAllIn.Enabled = canAllIn;
                    btnAuctionPass.Enabled = canPass;
                    btnAuctionSet.Enabled = canSet;
                    trBarAuctionRate.Enabled = canSet;


                    gbAuction.Visible = true;
                    gbAuction.Top = (this.Height - gbAuction.Height) / 2;
                    gbAuction.Left = (this.Width - gbAuction.Width) / 2;

                }
            }));
        }

        private void TrBarRate_ValueChanged(object sender, EventArgs e)
        {
            tbAuctionRate.Text = (trBarAuctionRate.Value * Utils.Controllers.GameController.AuctionStep).ToString();
        }

        private void BtnAuctionPass_Click(object sender, EventArgs e)
        {
            gbAuction.Visible = false;
            AuctionMove(0);
        }

        private void BtnAuctionAllIn_Click(object sender, EventArgs e)
        {
            gbAuction.Visible = false;
            AuctionMove(-1);
        }

        private void BtnAuctionSet_Click(object sender, EventArgs e)
        {
            gbAuction.Visible = false;
            AuctionMove(trBarAuctionRate.Value * Utils.Controllers.GameController.AuctionStep);
        }

        private void BtnAnsFinal_Click(object sender, EventArgs e)
        {
            gbFinalAns.Visible = false;
            FinalAnsClick(rtbAns.Text);
        }

        public void ShowFinalAnswer()
        {
            this.BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    btnAns.Enabled = false;
                    gbFinalAns.Visible = true;
                }
            }));
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                e.Handled = true;
                if (btnAns.Enabled)
                {
                    OnAnswerClick();
                }
            }
        }

    }
}

