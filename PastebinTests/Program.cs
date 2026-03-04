using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PastebinTests.Pages;

// Task #1
Console.WriteLine("=== Task #1 ===");
IWebDriver driver1 = new ChromeDriver();

try
{
    var homePage = new PastebinHomePage(driver1);
    
    homePage.Open().CreatePaste(
        code: "Hello from WebDriver",
        expiration: "10 Minutes",
        pasteName: "helloweb"
    );

    Console.WriteLine("Task #1 completed");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    driver1.Quit();
}

// Task #2
Console.WriteLine("\n=== Task #2 ===");
IWebDriver driver2 = new ChromeDriver();

try
{
    var homePage = new PastebinHomePage(driver2);
    
    string bashCode = @"git config --global user.name  ""New Sheriff in Town""
git reset $(git commit-tree HEAD^{tree} -m ""Legacy code"")
git push origin master --force";

    homePage.Open().CreatePaste(
        code: bashCode,
        expiration: "10 Minutes",
        pasteName: "how to gain dominance among developers",
        syntaxHighlighting: "Bash"
    );

    Console.WriteLine("Task #2 completed");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    driver2.Quit();
}
