// client-app/src/components/MakeModelDurationChart.jsx

import React, { useEffect, useState } from 'react';
import { Bar } from 'react-chartjs-2';

export default function MakeModelDurationChart() {
  const [data, setData] = useState([]);

  useEffect(() => {
    fetch('/api/MakeModelDuration')
      .then(res => res.json())
      .then(setData)
      .catch(console.error);
  }, []);

  if (!data.length) {
    return (
      <div className="inventory-chart">
        <p>Loading Inventory Turnover…</p>
      </div>
    );
  }

  // Prepare labels and values for every make/model
  const labels = data.map(d => `${d.make} ${d.model}`);
  const values = data.map(d => d.averageDaysInSystem);

  // Compute canvas height: at least 300px, else 14px per bar
  const canvasHeight = Math.max(300, labels.length * 14);

  return (
    <div className="inventory-chart">
      <h2>Inventory Turnover by Make/Model</h2>
      <div className="inventory-scroll">
        <Bar
          data={{
            labels,
            datasets: [
              {
                label: 'Avg Days In System',
                data: values,
                backgroundColor: 'rgba(30,144,255,0.6)',
                borderColor: 'rgba(30,144,255,1)',
                borderWidth: 1,
                barThickness: 12
              }
            ]
          }}
          options={{
            indexAxis: 'y',
            maintainAspectRatio: false,
            scales: {
              x: { beginAtZero: true },
              y: {
                ticks: {
                  autoSkip: true,
                  maxTicksLimit: 20,
                  font: { size: 10 },
                  padding: 4
                }
              }
            },
            plugins: { legend: { display: false } }
          }}
          height={canvasHeight}
        />
      </div>
    </div>
  );
}
