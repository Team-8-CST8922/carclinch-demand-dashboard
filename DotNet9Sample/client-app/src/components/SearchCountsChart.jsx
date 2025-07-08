import React, { useEffect, useState } from 'react';
import { Line } from 'react-chartjs-2';

export default function SearchCountsChart() {
  const [data, setData] = useState([]);

  useEffect(() => {
    fetch('/api/SearchCounts')
      .then(r => r.json())
      .then(setData)
      .catch(console.error);
  }, []);

  const labels = data.map(d => `${d.year}-${String(d.month).padStart(2,'0')}`);
  const counts = data.map(d => d.count);

  return (
    <div>
      <h2>SUV Archivals Over Time</h2>
      <Line data={{ labels, datasets:[{ label: 'SUVs', data: counts, fill:false, tension:0.1 }] }} />
    </div>
  );
}
