import React, { useEffect, useState } from 'react';
import { Line } from 'react-chartjs-2';
import 'chart.js/auto';

export default function SeasonalCountsChart({ from, to, make, model, bodyType }) {
  const [data, setData] = useState([]);

  useEffect(() => {
    const qs = new URLSearchParams({ from, to });
    if (make)     qs.append('make', make);
    if (model)    qs.append('model', model);
    if (bodyType) qs.append('bodyType', bodyType);

    fetch(`/api/SeasonalCounts?${qs}`)
      .then(r => r.json())
      .then(setData)
      .catch(console.error);
  }, [from, to, make, model, bodyType]);

  // month labels: Jan, Feb, ...
  const labels = data.map(d =>
    new Date(Date.UTC(0, d.month - 1)).toLocaleString('default', { month: 'short' })
  );

  const chartData = {
    labels,
    datasets: [{
      label: 'Count',
      data: data.map(d => d.count),
      fill: false,
      tension: 0.1
    }]
  };

  return (
    <div>
      <h2> Seasonal Trends</h2>
      <Line data={chartData} />
    </div>
  );
}
