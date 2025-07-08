// client-app/src/components/SearchCountsChart.jsx
import React, { useEffect, useState } from 'react';
import { Line } from 'react-chartjs-2';

export default function SearchCountsChart({ from, to, make, model, bodyType }) {
  const [data, setData] = useState([]);

  useEffect(() => {
    const qs = new URLSearchParams({ from, to });
    if (make)     qs.append('make', make);
    if (model)    qs.append('model', model);
    if (bodyType) qs.append('bodyType', bodyType);

    fetch(`/api/SearchCounts?${qs}`)
      .then(r => r.json())
      .then(setData)
      .catch(console.error);
  }, [from, to, make, model, bodyType]);

  const chartData = {
    labels: data.map(d => d.date.substring(0,10)),
    datasets: [{
      label: 'Searches',
      data: data.map(d => d.count),
      fill: false,
      tension: 0.1
    }]
  };

  return (
    <div>
      <h2> Search Counts</h2>
      <Line data={chartData} />
    </div>
  );
}
