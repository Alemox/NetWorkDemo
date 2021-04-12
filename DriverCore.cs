using System;
using System.Collections.Generic;

namespace NetWorkDemo
{
    public class DriverCore
    {
        private OpenQA.Selenium.Chrome.ChromeDriver m_driver;
        public OpenQA.Selenium.Chrome.ChromeDriver Driver { get => m_driver; }
        private OpenQA.Selenium.DevTools.DevToolsSession m_session;
        private OpenQA.Selenium.DevTools.V89.DevToolsSessionDomains m_domains;

        public List<string> Result;
        private object m_lock;

        public DriverCore()
        {
            Result = new List<string>();
            m_lock = new object();
            var driverService = OpenQA.Selenium.Chrome.ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            var option = new OpenQA.Selenium.Chrome.ChromeOptions();
            m_driver = new OpenQA.Selenium.Chrome.ChromeDriver(driverService, option);
            m_session = m_driver.GetDevToolsSession();
            m_domains = m_session.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V89.DevToolsSessionDomains>();
            m_domains.Network.Enable(new OpenQA.Selenium.DevTools.V89.Network.EnableCommandSettings()).GetAwaiter().GetResult();
            m_domains.Network.ResponseReceived += Network_ResponseReceived;
        }

        private void Network_ResponseReceived(object sender, OpenQA.Selenium.DevTools.V89.Network.ResponseReceivedEventArgs e)
        {
            lock (m_lock)
            {
                Result.Add($"{e.Response.MimeType}, {e.Response.Url}");
            }
        }
    }
}
