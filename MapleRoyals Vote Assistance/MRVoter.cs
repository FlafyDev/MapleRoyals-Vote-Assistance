
using HtmlAgilityPack;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using System.Drawing;
using AngleSharp.Css;

namespace MapleRoyalsVoteAssistance
{
    class MRVoter
    {
        enum VoteStatus
        {
            CanVote,
            AlreadyVoted,
            NotExists,
            Error
        }
        private class VoteInfo
        {
            public VoteStatus voteStatus;
            public DateTime TimeLeft;
            public int MinutesLeft;
            public string Message;
            public string Link;
            public VoteInfo(string[] message)
            {
                Message = message[0];
                Link = message[1];
                switch (Message)
                {
                    case string a when a.Contains("You have already voted"):
                        voteStatus = MRVoter.VoteStatus.AlreadyVoted;
                        string[] messageSplit = Message.Split(' ');
                        int minutesToVote = int.Parse(messageSplit[Array.FindIndex(messageSplit, element => element == "minutes") - 1]) + 2;
                        TimeLeft = DateTime.Now.AddMinutes(minutesToVote);
                        TimeLeft = TimeLeft.AddSeconds(-TimeLeft.Second);
                        MinutesLeft = minutesToVote;

                        break;
                    case string b when b.Contains("There is no character"):
                        voteStatus = MRVoter.VoteStatus.NotExists;
                        TimeLeft = DateTime.Now;
                        MinutesLeft = 0;
                        
                        break;
                    case string c when c.Contains("Please click here to vote"):
                        voteStatus = MRVoter.VoteStatus.CanVote;

                        break;
                    default:
                        voteStatus = MRVoter.VoteStatus.Error;
                        TimeLeft = DateTime.Now;
                        MinutesLeft = 0;

                        break;
                }
            }
        }
        public string Username;
        public HttpClient client = new HttpClient();
        public string BaseUrl = "https://mapleroyals.com/";
        public bool GtopVoting = false;

        public delegate void ThreadEvent(object sender, Thread data);
        public delegate void DateEvent(object sender, DateTime datetime);

        public event ThreadEvent VotingThreadChangedEvent;
        public Thread VotingListenerThread
        {
            get { return _votingListenerThread; }
            set
            {
                if (_votingListenerThread != value)
                {
                    _votingListenerThread = value;
                    VotingThreadChangedEvent(this, _votingListenerThread);
                }
            }
        }
        private Thread _votingListenerThread = null;

        public event DateEvent VotingThreadTimeLeftEvent;

        private bool _stopVotingListener = false;
        private string _gtopVotingPageBackground = "https://maplestorym.onl/wp-content/gallery/desktop/wallpaper-landscape-1.jpg";
        private Size _gtopVotingPageSize = new Size(500, 459);

        public bool ValidUsername(string username)
        {
            return 12 >= username.Length && username.Length >= 4 && !Regex.IsMatch(username.ToLower(), @"[^a-z]");
        }

        public bool ChangeUsername(string username)
        {
            bool verified = ValidUsername(username) && VerifyUsername(username);
            if (verified)
            {
                Username = username;
                StopVotingThread();
                StartVotingThread();
            }

            return verified;
        }
        
        private bool VerifyUsername(string username = null)
        {
            VoteStatus voteStatus = new VoteInfo(GetVoteMessage(username)).voteStatus;
            return voteStatus == VoteStatus.CanVote || voteStatus == VoteStatus.AlreadyVoted;
        }

        private string[] GetVoteMessage(string username = null)
        {
            if (username == null)
            {
                username = Username;
            }

            try
            {
                using (WebClient client = new WebClient())
                {
                    var data = new NameValueCollection();
                    data["name"] = username;
                    data["gtop"] = "Vote";
                    string response = Encoding.UTF8.GetString(
                        client.UploadValues(BaseUrl + "?page=vote", data));

                    var doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(response);

                    string message = doc.GetElementbyId("main").InnerText;
                    string link;
                    try
                    {
                        link = doc.GetElementbyId("main").Descendants("a").First().GetAttributeValue("href", null);
                    } catch
                    {
                        Console.WriteLine("Couldn't find link.");
                        link = null;
                    }

                    return new string[2] { message, link };
                }
            } catch (Exception e)
            {
                Console.WriteLine($"Couldn't connect to {BaseUrl}, {e}");
                return new string[2] { null, null };
            }
        }
        
        private void StopVotingThread()
        {
            if (VotingListenerThread != null)
            {
                _stopVotingListener = true;
                VotingListenerThread.Interrupt();
                VotingListenerThread.Join();
                VotingListenerThread = null;
            }
        }

        private void StartVotingThread()
        {
            _stopVotingListener = false;
            VotingListenerThread = new Thread(_ => 
            {
                while (true)
                {
                    VoteInfo voteInfo = new VoteInfo(GetVoteMessage(Username));
                    while (voteInfo.voteStatus != VoteStatus.CanVote)
                    {
                        if (voteInfo.voteStatus == VoteStatus.AlreadyVoted)
                        {
                            VotingThreadTimeLeftEvent(this, voteInfo.TimeLeft);
                        }

                        try { Thread.Sleep(TimeSpan.FromMinutes(
                            Math.Min(Math.Max(voteInfo.MinutesLeft, 10), 120)
                        )); }
                        catch (ThreadInterruptedException) { }

                        if (_stopVotingListener)
                        {
                            _stopVotingListener = false;
                            return;
                        }
                    }

                    GtopVoting = true;
                    try
                    {
                        OpenVotingWeb(voteInfo.Link);
                    } catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    GtopVoting = false;

                    try { Thread.Sleep(TimeSpan.FromMinutes(10)); }
                    catch (ThreadInterruptedException) { }

                    if (_stopVotingListener)
                    {
                        _stopVotingListener = false;
                        return;
                    }
                }
            });
            VotingListenerThread.IsBackground = true;
            VotingListenerThread.Start();
        }

        private void OpenVotingWeb(string link)
        {
            // Create and configure the Web Driver.
            new DriverManager().SetUpDriver(new ChromeConfig());
            var driverOptions = new ChromeOptions();
            var driverService = ChromeDriverService.CreateDefaultService();

            driverOptions.AddArgument($"--window-size={_gtopVotingPageSize.Width},{_gtopVotingPageSize.Height}");
            driverOptions.AddArgument("--window-position=-200000,-200000");
            driverOptions.AddArgument($"--app={link}");
            driverService.HideCommandPromptWindow = true;

            IWebDriver webDriver = new ChromeDriver(driverService, driverOptions);

            // Wait for the Captcha to load.
            try
            {
                webDriver.WaitForElement(By.Id("FunCaptcha"), 30);
            } catch (WebDriverException) { }

            // Make it look nice.
            IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
            js.ExecuteScript($@"
                form = document.getElementById('Myform');
                form.setAttribute('style', 'position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%);');
                document.body.appendChild(form);
                document.body.getElementsByTagName('header')[0].remove();
                document.body.getElementsByTagName('aside')[0].remove();
                document.body.getElementsByTagName('footer')[0].remove();
                document.body.setAttribute('style', 'display: block; background-image: url(""{_gtopVotingPageBackground}"");');"
            );
            
            // Center the window on the primary screen.
            webDriver.Manage().Window.Position = (Point) (SizeDivision(Screen.PrimaryScreen.Bounds.Size - _gtopVotingPageSize, 2));

            // Wait for browser to be closed.
            while (true)
            {
                try
                {
                    Console.WriteLine(webDriver.Title);
                } catch (WebDriverException) { break; }
                Thread.Sleep(1000);
            }

            // Try quiting the browser if something didn't go right.
            try
            {
                webDriver.Quit();
            } catch { }
        }

        private Size SizeDivision(Size size, int divisor)
        {
            return new Size(size.Width / divisor, size.Height / divisor);
        }
    }
}
