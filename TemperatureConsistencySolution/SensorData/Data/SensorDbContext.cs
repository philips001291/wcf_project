using System.Data.Entity;
using SensorData.Models;

namespace SensorData.Data
{
    public class SensorDbContext : DbContext
    {
        // Uses the connection string defined as "SensorDbContext" in App.config
        public SensorDbContext()
            : base("name=SensorDbContext")
        {
        }

        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<TemperatureReading> TemperatureReadings { get; set; }
    }
}
