using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium.Chrome;

namespace DaLuong
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Action()
        {
            int luong = (int)Num_luong.Value;
            Rectangle workingArea = Screen.GetWorkingArea(this);
            for (int i = 0; i < (luong/2); i++)
            {
                int x = 320*i;
                for (int j = 0; j < 2; j++)
                {
                    int y = 480*j;
                    Thread thread = new Thread((ThreadStart)delegate
                    {
                        ChromeOptions chromeOptions = new ChromeOptions();
                        chromeOptions.BinaryLocation = "C:\\Program Files\\Google\\Chrome\\Application\\Chrome.exe";
                        chromeOptions.AddArgument("--app=https://tranquocdai.com");
                        chromeOptions.AddArgument("--window-size=320,480");
                        chromeOptions.AddArgument("--user-agent=Mozilla/5.0 (iPhone; CPU iPhone OS 14_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.0 Mobile/15E148 Safari/604.1");
                        chromeOptions.AddExcludedArgument("enable-automation");
                        chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
                        chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
                        chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
                        ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
                        chromeDriverService.HideCommandPromptWindow = true;
                        ChromeDriver driver = new ChromeDriver(chromeDriverService, chromeOptions);

                        driver.Manage().Window.Position = new Point(x,y);
                        Thread.Sleep(1000);
                    });
                    thread.IsBackground = true;
                    thread.Start();
                }
            }
        }
        private void BTN_Start_Click(object sender, EventArgs e)
        {
            if (BTN_Start.Text == "START")
            {
                BTN_Start.Text = "STOP";
                Action();
            }
            else
            {
                BTN_Start.Text = "START";
                try
                {
                    Process[] processesChrome = Process.GetProcessesByName("chromedriver");
                    foreach (Process processChrome in processesChrome)
                    {
                        processChrome.Kill();
                    }
                }
                catch { }
            }
        }
    }
}
