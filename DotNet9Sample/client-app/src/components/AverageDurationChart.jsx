import React, { useEffect, useState } from 'react';

export default function AverageDurationChart() {
  const [avg, setAvg] = useState(null);

  useEffect(() => {
    fetch('/api/Duration/average')
      .then(r => r.json())
      .then(data => setAvg(data.averageDaysInSystem))
      .catch(console.error);
  }, []);

  return (
    <div>
      <h2>Avg. Time to Archival</h2>
      {avg !== null 
        ? <div style={{ fontSize: '2rem' }}>{avg.toFixed(1)} days</div>
        : <p>Loading…</p>}
    </div>
  );
}
