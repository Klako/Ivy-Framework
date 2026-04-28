import { test, expect } from '@playwright/test';
import fs from 'fs';

test.use({
  ignoreHTTPSErrors: true
});



function average(nums: number[]): number {
  let sum = 0;
  for (let num of nums) {
    sum += num;
  }
  return sum / nums.length;
}

function stdDev(nums: number[]): number {
  let avg = average(nums);
  let sum = 0;
  for (let num of nums) {
    sum += Math.pow(num - avg, 2);
  }
  return Math.sqrt(sum / nums.length);
}

test('test', async ({ page }) => {
  await page.goto('https://localhost:5010/widgets/inputs/select-input');
  let bytesReceived = 0;
  page.on('websocket', ws => {
    console.log(`WebSocket opened: ${ws.url()}`);

    ws.on('framereceived', event => {
      bytesReceived = event.payload.length;
    });
  });
  let latencies: number[] = [];
  let payloads: number[] = [];
  await expect(page.getByText("Basic Usage")).toBeVisible();
  await page.getByRole('tab', { name: 'Sizes' }).click();
  await expect(page.getByText("SelectInput Sizes")).toBeVisible();
  let firstRadioGroup = page.getByRole('radiogroup').nth(0);
  let firstRadioButtons = firstRadioGroup.getByRole('radio');
  for (let i = 0; i < 50; i++) {
    let now = performance.now();
    await firstRadioButtons.nth(1).click();
    await expect(firstRadioButtons.nth(1)).toBeChecked();
    latencies.push(performance.now() - now);
    payloads.push(bytesReceived);
    now = performance.now();
    await firstRadioButtons.nth(2).click();
    await expect(firstRadioButtons.nth(2)).toBeChecked();
    latencies.push(performance.now() - now);
    payloads.push(bytesReceived);
    now = performance.now();
    await firstRadioButtons.nth(3).click();
    await expect(firstRadioButtons.nth(3)).toBeChecked();
    latencies.push(performance.now() - now);
    payloads.push(bytesReceived);
    now = performance.now();
    await firstRadioButtons.nth(0).click();
    await expect(firstRadioButtons.nth(0)).toBeChecked();
    latencies.push(performance.now() - now);
    payloads.push(bytesReceived);
  }
  //console.log(latencies);
  console.log("latency average: " + average(latencies));
  console.log("latency stddev: " + stdDev(latencies));
  //console.log(payloads);
  console.log("payload average: " + average(payloads));
  console.log("payload stddev: " + stdDev(payloads));
  let f = fs.createWriteStream('benchmark.csv', 'ascii');
  f.write("payload, latency\n");
  for (let [payload, latency] of payloads.map((p, i) => [p, latencies[i]])) {
    
  }
});
