import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom'
import Dashboard from './Dashboard'
import NewShipment from './NewShipment'

import './App.css'



function App() {
  return (
    <Router>
      <div className="app-shell">
        {/* Navigation Bar */}
        <nav className="navbar">
          <h1>🚛 LogiFlow AI</h1>
          <div className="nav-links">
            <Link to="/" className="nav-item">Dashboard</Link>
            <Link to="/new" className="nav-item">New Entry</Link>
          </div>
        </nav>

        {/* Page Content loads here */}
        <div className="content">
          <Routes>
            <Route path="/" element={<Dashboard />} />
            <Route path="/new" element={<NewShipment />} />
          </Routes>
        </div>
      </div>
    </Router>
  );
}

export default App
