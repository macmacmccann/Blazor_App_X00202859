

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightTests
{
    internal class BlazorUITests
    {
        private IPlaywright playwright;
        private IBrowser browser;
        private IPage page;


        [Test]
        public async Task BlazorTest()
        {
            // start
            using var playwright = await Playwright.CreateAsync();

            // launch browser
            await using var browser = await playwright.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions
                {
                    Headless = false // show the browser window
                });

            // open a new tab
            var page = await browser.NewPageAsync();

            // go the url main page
            await page.GotoAsync("https://localhost:7058");

            // simple assertion, page is loaded by readnih a selector 
            await page.WaitForSelectorAsync("body");

            var title = await page.TitleAsync();
            Assert.IsNotNull(title);
        }

        [Test]
        public async Task SearchButton_Works()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new() { Headless = false });
            var page = await browser.NewPageAsync();

            await page.GotoAsync("https://localhost:7058/searchspecies");

            await page.ClickAsync("input.search");
            await page.FillAsync("input.search", "Tardigrade");
            await page.ClickAsync("button:has-text(\"Search\")");

            var items = page.Locator("li");

            var count = await items.CountAsync();
            Assert.Greater(count, 0);
        }

        [Test]
        public async Task EmptySearch_Does_Not_Give_Fake_Values()

        {

            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new() { Headless = false });
            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://localhost:7058/searchspecies");

            await page.FillAsync("input.search", "Tardigrade");
            await page.ClickAsync("button:has-text(\"Search\")");

            var filteredCount = await page.Locator("li").CountAsync();
            await page.ClickAsync("input.search");
            await page.FillAsync("input.search", "");
            await page.ClickAsync("button:has-text(\"Search\")");

            var allCount = await page.Locator("li").CountAsync();

            Assert.AreEqual(allCount, filteredCount);
        }

        [Test]
        public async Task ClickingSpecies_NavigatesToDetailPage()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(
                new()
                {
                    Headless = false
                }
                );
            var page = await browser.NewPageAsync();

            await page.GotoAsync("https://localhost:7058/bykingdom");

            var firstLink = page.Locator("li.list a").First;
            await firstLink.ClickAsync();

            await page.WaitForURLAsync("**/rankdescription/**");

            Assert.IsTrue(page.Url.Contains("rankdescription"));
        }

        [Test]
        public async Task LoadMore_AddsMoreItems()
        {

            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(
                new()
                {
                    Headless = false
                }
                );
            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://localhost:7058/byfamily");

            var before = await page.Locator("li").CountAsync();

            await page.ClickAsync("button.api:has-text(\"Retrieve more sample data from the API to gain more accuracy\")");

            var after = await page.Locator("li").CountAsync();

            Assert.AreNotEqual(after, before);
        }

  
    }
}

