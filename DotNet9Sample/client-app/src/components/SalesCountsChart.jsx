// client-app/src/components/SalesCountsChart.jsx
import React, { useEffect, useState } from 'react';
import { Bar } from 'react-chartjs-2';

export default function SalesCountsChart({ from, to, make, model, bodyType }) {
  const [data, setData] = useState([]);

  useEffect(() => {
    const qs = new URLSearchParams({ from, to });
    if (make)     qs.append('make', make);
    if (model)    qs.append('model', model);
    if (bodyType) qs.append('bodyType', bodyType);

    fetch(`/api/SalesCounts?${qs}`)
      .then(r => r.json())
      .then(setData)
      .catch(console.error);
  }, [from, to, make, model, bodyType]);

  const chartData = {
    labels: data.map(d => d.date.substring(0,10)),
    datasets: [{
      label: 'Sales',
      data: data.map(d => d.count),
      backgroundColor: 'rgba(75, 192, 192, 0.5)'
    }]
  };

  return (
    <div>
      <h2> Sales Counts</h2>
      <Bar data={chartData} />
    </div>
  );
}
