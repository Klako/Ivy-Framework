import { test, expect } from '@playwright/test';
import fs from 'fs';

test.use({
  ignoreHTTPSErrors: true
});

test('normal', async ({ page, }) => {
  page.goto("https://localhost:5010/list");
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

  let interactCounter = page.getByTestId('interactCounter');
  await expect(interactCounter).toBeVisible();

  let useKeys = page.getByTestId("useKeys");
  let mixTypes = page.getByTestId("mixTypes");
  let listSize = page.getByTestId("listSize");
  let filterInput = page.getByTestId("filterText");

  let previousInteractCounter = 0;
  let previousPerformanceTime = performance.now();
  let nextInteractCount = async () => {
    await expect(interactCounter).toHaveAttribute('value', (previousInteractCounter + 1).toString());
    previousInteractCounter++
    payloads.push(bytesRecieved);
    latencies.push(performance.now() - previousPerformanceTime);
    previousPerformanceTime = performance.now();
  }

  let filters = [
    "1",
    "2",
    "3",
    "4",
    "5",
    "6",
    "7",
    "8",
    "9",
  ];

  for (let filter of filters) {
    await filterInput.fill(filter);
    await nextInteractCount();
  }

  let dateTime = new Date().toISOString().replace(/:/g, '-');
  if (!fs.existsSync('results')) {
    fs.mkdirSync('results');
  }
  let f = fs.createWriteStream(`results/list-normal-${dateTime}.csv`);
  f.write("Iteration,Payload,Latency\n");
  for (let [payload, latency, iteration] of payloads.map((p, i) => [p, latencies[i], i])) {
    f.write(`${iteration+1},${payload},${latency}\n`);
  }
  f.close();
});