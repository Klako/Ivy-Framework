import { test, expect } from '@playwright/test';
import fs from 'fs';

test.use({
  ignoreHTTPSErrors: true
});

test.beforeEach(async ({ page }) => {
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

test('interact', async ({ page }) => {
  page.goto("https://localhost:5010/simple");
  await page.waitForEvent('websocket'); // First websocket is app menu
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
  let widgetUpdateBenchmarks : number[] = [];

  await page.exposeFunction('recordWidgetTreeUpdateBenchmark', (parentId: string | null | undefined, benchmark : number) => {
    if (parentId == new URL(ws.url()).searchParams.get('parentId')) {
      widgetUpdateBenchmarks.push(benchmark);
    }
  });

  let interactCounter = page.getByTestId('interactCounter');
  await expect(interactCounter).toBeVisible();

  let textInput = page.getByTestId("textInput");
  let boolInput = page.getByTestId("boolInput");
  let radioInput = page.getByTestId("radioInput");

  let textToWrite = "The quick brown fox jumped over the lazy dog";

  let previousInteractCounter = 0;
  let previousPerformanceTime = performance.now();
  let nextInteractCount = async () => {
    await expect(interactCounter).toHaveAttribute('value', (previousInteractCounter + 1).toString());
    previousInteractCounter++
    payloads.push(bytesRecieved);
    latencies.push(performance.now() - previousPerformanceTime);
    previousPerformanceTime = performance.now();
  }

  for (let i = 1; i < textToWrite.length; i++) {
    await textInput.fill(textToWrite.slice(0, i));
    await nextInteractCount();
  }
  for (let i = 0; i < 50; i++) {
    await boolInput.click();
    await nextInteractCount();
  }
  let currentRadioButton = 1;
  for (let i = 0; i < 50; i++) {
    await radioInput.getByRole("radio").nth(currentRadioButton).click();
    currentRadioButton = (currentRadioButton + 1) % 3;
    await nextInteractCount();
  }

  let dateTime = new Date().toISOString().replace(/:/g, '-');
  if (!fs.existsSync('results')) {
    fs.mkdirSync('results');
  }
  let f = fs.createWriteStream(`results/simple-${dateTime}.csv`);
  f.write("Iteration,Payload,Latency,WidgetUpdate\n");
  for (let [payload, latency, iteration, widgetUpdate] of payloads.map((p, i) => [p, latencies[i], i, widgetUpdateBenchmarks[i]])) {
    f.write(`${iteration+1},${payload},${latency},${widgetUpdate}\n`);
  }
  f.close();
});