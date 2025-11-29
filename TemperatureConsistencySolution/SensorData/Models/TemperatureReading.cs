using System;

namespace SensorData.Models
{
    public class TemperatureReading
    {
        public int Id { get; set; }             // Primarni ključ merenja
        public int SensorId { get; set; }       // Veza ka senzoru
        public double Value { get; set; }       // Izmerena temperatura
        public DateTime Timestamp { get; set; } // Vreme merenja
    }
}
