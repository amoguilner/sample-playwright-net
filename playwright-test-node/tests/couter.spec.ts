import { test, expect } from '@playwright/test';

test.beforeEach(async ({ page }) => {
    await test.step("Navigate to Blazor app", async () => {
        await page.goto('http://localhost:5000/');
    });
});

test('should show expected message when clicked few times', async ({page}) => {
    await test.step('Navigate to the counter component', async () => {
       await page.getByRole('link', { name: 'Counter' }).click(); 
       await expect(page.getByRole('heading', { name: 'Counter' })).toBeVisible();
    });
    
    const messageArea = page.getByRole('status');
    
    await test.step('Verify initial message', async () => {
        let text = await messageArea.innerText();
        
        await expect(text).toBe('Current count: 0');
    });
    
    await test.step('Click counter 10 times', async () => {
        const clickMeButton = page.getByRole('button', { name: 'Click me' });
        
        for(let i = 1; i <= 10; ++i) {
            await clickMeButton.click();
        }
    });
    
    await test.step('Verify message', async () => {
        let text = await messageArea.innerText();
        
        await expect(text).toBe('Current count: 10');
    });
    
});