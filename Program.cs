using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2.Responses;

namespace Gmail_login
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                new Program().Run().Wait();

               /* IWebDriver driver = new FirefoxDriver(Environment.CurrentDirectory);

                driver.Navigate().GoToUrl("https://mail.google.com/");
                System.Threading.Thread.Sleep(2000);
                driver.Manage().Window.Maximize();

                driver.FindElement(By.Id("identifierId")).SendKeys("");
                System.Threading.Thread.Sleep(2000);

                //driver.FindElement(By.XPath("//span[text() = 'Επόμενο']")).Click();
                driver.FindElement(By.Id("identifierNext")).Click();
                System.Threading.Thread.Sleep(2000);*/


            }
            catch (AggregateException e)
            {
                Console.WriteLine("not found");
                foreach (var i in e.InnerExceptions)
                {
                    Console.WriteLine("ERROR: " + i.Message);
                }
            }

            //Close the browser
            //driver.Close();
        }
        private async Task Run()
        {
            UserCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    new[] { GmailService.Scope.GmailSettingsBasic },
                    "user", CancellationToken.None, new FileDataStore("Gmail_login"));
            }

            // Create the service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Gmail login",
            });

            //await credential.RevokeTokenAsync(CancellationToken.None);

            // Reauthorize the user. A browser should be opened, and the user should enter his or her credential again.
            await GoogleWebAuthorizationBroker.ReauthorizeAsync(credential, CancellationToken.None);

            // The request should succeed now.
            //await Open_Gmail(service);

        }

       /* public Task Open_Gmail(GmailService service)
        {
            IWebDriver driver = new FirefoxDriver(Environment.CurrentDirectory);

            driver.Navigate().GoToUrl("https://mail.google.com/");
            System.Threading.Thread.Sleep(2000);
            driver.Manage().Window.Maximize();

            driver.FindElement(By.Id("identifierId")).SendKeys("");
            System.Threading.Thread.Sleep(2000);

            //driver.FindElement(By.XPath("//span[text() = 'Επόμενο']")).Click();
            driver.FindElement(By.Id("identifierNext")).Click();
            System.Threading.Thread.Sleep(2000);
            return Task.CompletedTask;
        }*/

    }
}