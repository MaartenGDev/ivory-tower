using System;
using System.Collections.Generic;
using System.Linq;
using Integrations.Models;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Integrations
{
    public class ConfluenceService
    {
        private readonly string _sessionToken;
        private readonly string _confluenceDomain;

        public ConfluenceService(IConfiguration configuration)
        {
            _sessionToken = configuration.GetSection("SessionToken").Value;
            _confluenceDomain = configuration.GetSection("ConfluenceDomain").Value;
        }

        public IEnumerable<Contributor> GetContributors()
        {
            using (IWebDriver driver = new ChromeDriver("."))
            {
                driver.Navigate().GoToUrl(new Uri("http://" + _confluenceDomain + "/confluence/login.action"));
                
                // Set the authentication cookie
                driver.Manage().Cookies.DeleteAllCookies();
                driver.Manage().Cookies.AddCookie(new Cookie("JSESSIONID", _sessionToken,
                    _confluenceDomain, "/confluence", null));
                
                // Navigate to the page with contributors table
                driver.Navigate().GoToUrl(new Uri("http://" + _confluenceDomain + "/confluence/display/IQGWIM/Home"));


                return GetContributorOnPage(driver);
            }
        }

        private IEnumerable<Contributor> GetContributorOnPage(ISearchContext driver)
        {
            var summaryTable =
                driver.FindElement(By.CssSelector(@"[data-macro-name=""contributors-summary""]"));

            var contributors = summaryTable.FindElements(By.CssSelector("tr"))
                .Where(row => row.FindElements(By.CssSelector("td")).Count > 0)
                .Select(row =>
                {
                    var columns = row.FindElements(By.CssSelector("td"));

                    return new Contributor
                    {
                        Name = columns[0].Text,
                        EditCount = int.Parse(columns[1].Text),
                        CommentCount = int.Parse(columns[2].Text),
                    };
                }).OrderBy(x => x.EditCount).ToList();

            return contributors;
        }
    }
}