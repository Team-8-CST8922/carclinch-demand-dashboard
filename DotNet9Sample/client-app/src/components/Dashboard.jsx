// client-app/src/components/Dashboard.jsx
import React from 'react';
import SearchCountsChart        from './SearchCountsChart';
import AnnualSearchCountsChart  from './AnnualSearchCountsChart';
import SalesCountsChart         from './SalesCountsChart';
import AverageDurationChart     from './AverageDurationChart';
import MakeModelDurationChart   from './MakeModelDurationChart';

import '../App.css';

export default function Dashboard() {
  return (
    <div style={{ padding: '1rem 2rem' }}>
      {/* HEADER */}
      <div className="dashboard-header">
        <h1>Car Clinch Insights Dashboard</h1>
      </div>

      {/* First row: 4 panels */}
      <div className="chart-grid">
        <div className="chart-container">
          <SearchCountsChart />
        </div>

        <div className="chart-container">
          <AnnualSearchCountsChart />
        </div>

        <div className="chart-container">
          <SalesCountsChart />
        </div>

        <div className="chart-container">
          <AverageDurationChart />
        </div>

        {/* Second row: full-width inventory turnover */}
        <div className="chart-container inventory-chart">
          <MakeModelDurationChart />
        </div>
      </div>
    </div>
  );
}
