using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PastebinTests.Pages
{
    public class PasteResultPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        // Locators
        private By CodeContent => By.CssSelector("ol.de1");
        private By SyntaxHighlightingLabel => By.CssSelector("a.btn.-small.h_800");
        private By RawCodeContent => By.CssSelector("textarea.textarea");

        public PasteResultPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        public string GetPageTitle()
        {
            return _driver.Title;
        }

        public string GetCodeContent()
        {
            var codeElement = _wait.Until(d => d.FindElement(CodeContent));
            return codeElement.Text;
        }

        public string GetSyntaxHighlighting()
        {
            var syntaxLabel = _wait.Until(d => d.FindElement(SyntaxHighlightingLabel));
            return syntaxLabel.Text;
        }

        public bool IsSyntaxHighlightedAs(string expectedSyntax)
        {
            try
            {
                var syntaxLabel = _wait.Until(d => d.FindElement(SyntaxHighlightingLabel));
                return syntaxLabel.Text.Contains(expectedSyntax, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        public bool TitleContains(string expectedTitle)
        {
            try
            {
                _wait.Until(d => d.Title.Contains(expectedTitle, StringComparison.OrdinalIgnoreCase));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}
