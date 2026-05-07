import { test, expect, Page } from '@playwright/test';
import fs from 'fs';

test.use({
  ignoreHTTPSErrors: true
});

test.beforeEach(async ({ page }) => {
  fs.existsSync('results/simple') || fs.mkdirSync('results/simple');
  await page.route('**/*', async (route) => {
    const response = await route.fetch();
    const headers = {
      ...response.headers(),
      'Cross-Origin-Opener-Policy': 'same-origin',
      'Cross-Origin-Embedder-Policy': 'require-corp',
    };
    await route.fulfill({ response, headers });
  });
});

async function beforeBenchmark(page: Page, testname: string) {
  page.goto("https://localhost:5010/simple");
  let ws = await page.waitForEvent('websocket');
  console.log(`WebSocket opened: ${ws.url()}`);
  let bytesRecieved = 0;
  ws.on('framereceived', event => {
    if (event.payload.length > 20) {
      bytesRecieved = event.payload.length;
    }
  });
  let latencies: number[] = [];
  let payloads: number[] = [];
  let widgetUpdates: number[] = [];

  await page.exposeFunction('recordWidgetTreeUpdateBenchmark', (parentId: string | null | undefined, benchmark: number) => {
    widgetUpdates.push(benchmark);
  });

  let interactCounter = page.getByTestId('interactCounter');
  await expect(interactCounter).toBeVisible();

  let previousInteractCounter = 0;
  let previousPerformanceTime = performance.now();
  let beforeInteract = () => {
    previousPerformanceTime = performance.now();
  }
  let afterInteract = async () => {
    await expect(interactCounter).toHaveAttribute('value', (previousInteractCounter + 1).toString());
    previousInteractCounter++
    payloads.push(bytesRecieved);
    latencies.push(performance.now() - previousPerformanceTime);
  }

  let afterBenchmark = () => {
    let f = fs.createWriteStream(`results/simple/${testname}.csv`);
    f.write("Iteration,Payload,Latency,WidgetUpdate\n");
    for (let [payload, latency, iteration, widgetUpdate] of payloads.map((p, i) => [p, latencies[i], i, widgetUpdates[i]])) {
      f.write(`${iteration + 1},${payload},${latency},${widgetUpdate}\n`);
    }
    f.close();
  }


  return {
    beforeInteract,
    afterInteract,
    afterBenchmark
  };
}

test('write_text', async ({ page }) => {
  let {
    beforeInteract,
    afterInteract,
    afterBenchmark
  } = await beforeBenchmark(page, 'write_text');

  let textToWrite = "The quick brown fox jumped over the lazy dog";

  let textInput = page.getByTestId("textInput");

  for (let i = 1; i < textToWrite.length; i++) {
    beforeInteract();
    await textInput.fill(textToWrite.slice(0, i));
    await afterInteract();
  }
  for (let i = 1; i < textToWrite.length; i++) {
    beforeInteract();
    await textInput.fill(textToWrite.slice(0, i));
    await afterInteract();
  }

  afterBenchmark();
});

test('click_checkbox', async ({ page }) => {
  let {
    beforeInteract,
    afterInteract,
    afterBenchmark
  } = await beforeBenchmark(page, 'click_checkbox');

  let boolInput = page.getByTestId("boolInput");

  for (let i = 0; i < 100; i++) {
    beforeInteract();
    await boolInput.click();
    await afterInteract();
  }

  afterBenchmark();
});

test('click_radiobuttons', async ({ page }) => {
  let {
    beforeInteract,
    afterInteract,
    afterBenchmark
  } = await beforeBenchmark(page, 'click_radiobuttons');

  let radioInput = page.getByTestId("radioInput");

  let currentRadioButton = 1;
  for (let i = 0; i < 100; i++) {
    beforeInteract();
    await radioInput.getByRole("radio").nth(currentRadioButton).click();
    await afterInteract();
    currentRadioButton = (currentRadioButton + 1) % 3;
  }

  afterBenchmark();
})