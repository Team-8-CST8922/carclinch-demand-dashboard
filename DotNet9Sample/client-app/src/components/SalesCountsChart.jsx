import React, { useEffect, useState } from 'react';
import { Bar } from 'react-chartjs-2';

export default function SalesCountsChart() {
  const [data, setData] = useState([]);

  useEffect(() => {
    fetch('/api/SalesCounts')
      .then(r => r.json())
      .then(setData)
      .catch(console.error);
  }, []);

  const labels = data.map(d => `${d.year}-${String(d.month).padStart(2,'0')}`);
  const counts = data.map(d => d.count);

  return (
    <div>
      <h2>Total Archivals Over Time</h2>
      <Bar data={{ labels, datasets:[{ label:'Sales', data:counts, backgroundColor:'rgba(75,192,192,0.5)' }] }} />
    </div>
  );
}
