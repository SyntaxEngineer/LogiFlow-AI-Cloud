import { useState, useEffect } from 'react';

import './Dashboard.css';


function Dashboard() {
  const [logs, setLogs] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchLogs = async () => {
      try {
        
        const response = await fetch('/api/GetShipmentLogs');
        const data = await response.json();
        setLogs(data);
        setLoading(false);
      } catch (error) {
        console.error("Error fetching logs:", error);
        setLoading(false);
      }
    };

    fetchLogs();
    const interval = setInterval(fetchLogs, 5000);
    return () => clearInterval(interval);
  }, []);
  

  return (
    <div className="container">
      <h2>Live Monitoring Dashboard</h2>
      
      <div className="stats-panel">
        <div className="card error">
          <h3>High Risk</h3>
          <p>{logs.filter(l => l.Sentiment === "Negative").length}</p>
        </div>
        <div className="card success">
          <h3>Safe Shipments</h3>
          <p>{logs.filter(l => l.Sentiment !== "Negative").length}</p>
        </div>
      </div>

      {loading ? <p>Loading...</p> : (
        <table>
          <thead>
            <tr>
              <th>City</th>
              <th>Truck ID</th>
              <th>Status</th>
              <th>AI Sentiment</th>
              <th>Driver Notes</th>
            </tr>
          </thead>
          <tbody>
            {logs.map((log) => (
            //   <tr key={log.RowKey} className={log.Sentiment === "Negative" ? "row-danger" : "row-ok"}>
            <tr key={log.RowKey} className={
                log.Sentiment === "Negative" ? "row-danger" : 
                log.Sentiment === "Positive" ? "row-success" : "row-neutral"
            }>
                <td>{log.PartitionKey}</td>
                <td>{log.RowKey}</td>
                <td>{log.Status}</td>
                <td><span className={`badge ${log.Sentiment}`}>{log.Sentiment}</span></td>
                <td>{log.Notes}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}

export default Dashboard;