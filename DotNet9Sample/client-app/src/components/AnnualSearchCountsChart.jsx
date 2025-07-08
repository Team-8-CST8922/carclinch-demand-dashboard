// client-app/src/components/AnnualSearchCountsChart.jsx
import React, { useEffect, useState } from 'react';
import { Line } from 'react-chartjs-2';

export default function AnnualSearchCountsChart() {
  const [data, setData] = useState([]);

  useEffect(() => {
    fetch('/api/SearchCounts/annual')
      .then(r => r.json())
      .then(setData)
      .catch(console.error);
  }, []);

  const labels = data.map(d =>
    `${d.year}-${String(d.month).padStart(2, '0')}`
  );
  const counts = data.map(d => d.count);

  return (
    <div>
      <h2>SUV Archivals in 2024</h2>
      <Line
        data={{
          labels,
          datasets: [{
            label: '2024 SUVs',
            data: counts,
            fill: false,
            tension: 0.1,
          }]
        }}
      />
    </div>
  );
}
