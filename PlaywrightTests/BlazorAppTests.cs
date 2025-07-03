using Microsoft.Playwright;

namespace PlaywrightTests;

[TestClass]
public class BlazorAppTests : PageTest
{
    [TestInitialize]
    public async Task Initialize()
    {
        await Page.Context.Tracing.GroupAsync("Navigate to our app");
        await Page.GotoAsync("http://localhost:5000/");
        await Page.Context.Tracing.GroupEndAsync();
    }

    [TestMethod]
    public async Task TestCounter()
    {
        await Page.Context.Tracing.GroupAsync("Navigate to the counter component");
        await Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions() { Name = "Counter" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Heading, new PageGetByRoleOptions() { Name = "Counter" }))
            .ToBeVisibleAsync();
        await Page.Context.Tracing.GroupEndAsync();
        
        await Page.Context.Tracing.GroupAsync("Click counter 10 times");
        var clickMeButton = Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions(){Name = "Click me"});

        for (var i = 1; i <= 10; ++i)
        {
            await clickMeButton.ClickAsync();
        }
        await Page.Context.Tracing.GroupEndAsync();
        
        await Page.Context.Tracing.GroupAsync("Verify message");
        Assert.AreEqual("Current count: 10", await Page.GetByRole(AriaRole.Status).TextContentAsync());
        await Page.Context.Tracing.GroupEndAsync();
    }
}