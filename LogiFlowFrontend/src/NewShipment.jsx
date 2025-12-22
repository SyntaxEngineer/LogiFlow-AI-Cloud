import { useState } from 'react';
import { useNavigate } from 'react-router-dom'; 
import './NewShipment.css';


function NewShipment() {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({ city: 'Delhi', truckId: '', notes: '' });
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsSubmitting(true);

    try {
      await fetch('/api/PostShipment', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          Id: formData.truckId,
          City: formData.city,
          Notes: formData.notes
        })
      });
      
      alert("Shipment Sent!");
      navigate("/"); 
      
    } catch (error) {
      console.error(error);
      alert("Failed to send.");
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className="container">
      <h2>Enter New Shipment</h2>
      <div className="card form-card">
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <input name="truckId" placeholder="Truck ID" value={formData.truckId} onChange={handleInputChange} required />
            <select name="city" value={formData.city} onChange={handleInputChange}>
              <option>Delhi</option>
              <option>Mumbai</option>
              <option>Bangalore</option>
              <option>Chennai</option>
              <option>Kolkata</option>
            </select>
          </div>
          <textarea name="notes" placeholder="Notes..." value={formData.notes} onChange={handleInputChange} required />
          <button type="submit" disabled={isSubmitting}>
            {isSubmitting ? 'Sending...' : 'Submit Entry'}
          </button>
        </form>
      </div>
    </div>
  );
}

export default NewShipment;