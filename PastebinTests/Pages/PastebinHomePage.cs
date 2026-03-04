using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace PastebinTests.Pages
{
    public class PastebinHomePage
    {
        private static string Url { get; } = "https://pastebin.com/";
        
        private readonly IWebDriver driver;

        public PastebinHomePage(IWebDriver driver) => this.driver = driver ?? throw new ArgumentException(nameof(driver));

        public PastebinHomePage Open()
        {
            driver.Url = Url;
            return this;
        }

        public void CreatePaste(string code, string expiration, string pasteName, string? syntaxHighlighting = null)
        {
            Thread.Sleep(5000);
            
            // Paste code
            var codeArea = driver.FindElement(By.Id("postform-text"));
            codeArea.Clear();
            codeArea.SendKeys(code);

            // Syntax Highlighting 
            if (!string.IsNullOrEmpty(syntaxHighlighting))
            {
                var syntaxDropdown = driver.FindElement(By.Id("select2-postform-format-container"));
                syntaxDropdown.Click();
                
                var syntaxWait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                var searchInput = syntaxWait.Until(d => d.FindElement(By.CssSelector(".select2-search__field")));
                searchInput.SendKeys(syntaxHighlighting);
                
                var syntaxOption = syntaxWait.Until(d => d.FindElement(
                    By.XPath($"//li[contains(@class,'select2-results__option') and contains(text(),'{syntaxHighlighting}')]")));
                syntaxOption.Click();
            }

            // Expiration
            var expirationDropdown = driver.FindElement(By.Id("select2-postform-expiration-container"));
            expirationDropdown.Click();
            
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var expirationOption = wait.Until(d => d.FindElement(
                By.XPath($"//li[contains(@class,'select2-results__option') and contains(text(),'{expiration}')]")));
            expirationOption.Click();

            // Paste name
            var nameInput = driver.FindElement(By.Id("postform-name"));
            nameInput.Clear();
            nameInput.SendKeys(pasteName);

            // Click button create
            var createButton = driver.FindElement(By.XPath("//button[contains(text(),'Create New Paste')]"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", createButton);
            Thread.Sleep(500);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", createButton);
        }
    }
}
