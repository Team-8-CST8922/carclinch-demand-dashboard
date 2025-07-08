// client-app/src/components/Dashboard.jsx
import React, { useState } from 'react';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';

import SearchCountsChart from './SearchCountsChart';
import SalesCountsChart  from './SalesCountsChart';
import SeasonalCountsChart from './SeasonalCountsChart';

import '../App.css';

export default function Dashboard() {
  // date/filter state
  const [from, setFrom]     = useState(new Date(new Date().getFullYear(), 0, 1));
  const [to,   setTo]       = useState(new Date());
  const [make,     setMake]     = useState('');
  const [model,    setModel]    = useState('');
  const [bodyType, setBodyType] = useState('');
  // view toggle: 'detail' = by-date charts, 'seasonal' = stub seasonal view
  const [view, setView]     = useState('detail');

  // helper: format Date → YYYY-MM-DD
  const fmt = d => d.toISOString().slice(0,10);

  return (
    <div style={{ padding: '1rem 2rem' }}>
      {/* 0) Big styled header */}
      <div className="dashboard-header">
        <h1> Car Insights Dashboard</h1>
      </div>

      {/* 1) FILTER BAR */}
      <div className="filter-bar">
        <DatePicker selected={from} onChange={setFrom} />
        <DatePicker selected={to}   onChange={setTo} />
        <input
          type="text"
          placeholder="Make"
          value={make}
          onChange={e => setMake(e.target.value)}
        />
        <input
          type="text"
          placeholder="Model"
          value={model}
          onChange={e => setModel(e.target.value)}
        />
        <input
          type="text"
          placeholder="BodyType"
          value={bodyType}
          onChange={e => setBodyType(e.target.value)}
        />
        <select value={view} onChange={e => setView(e.target.value)}>
          <option value="detail">By Date</option>
          <option value="seasonal">Seasonal Trends</option>
        </select>
      </div>

      {/* 2) CONDITIONAL VIEWS */}
      {view === 'detail' ? (
        <div className="chart-grid">
          <div className="chart-container">
            <SearchCountsChart
              from={fmt(from)} to={fmt(to)}
              make={make} model={model} bodyType={bodyType}
            />
          </div>
          <div className="chart-container">
            <SalesCountsChart
              from={fmt(from)} to={fmt(to)}
              make={make} model={model} bodyType={bodyType}
            />
          </div>
        </div>
      ) : (
        <div className="chart-container" style={{ marginTop: '2rem' }}>
          <SeasonalCountsChart    
           from={fmt(from)} to={fmt(to)}  
           make={make} model={model} bodyType={bodyType} 
          />
        </div>
      )}
    </div>
  );
}
