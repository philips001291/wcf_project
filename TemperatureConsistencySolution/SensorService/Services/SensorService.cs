using System;
using System.Threading.Tasks;
using SensorService.Contracts;

namespace SensorService.Services
{
    // WCF dekoracije i EF logiku ćemo dodati kasnije.
    public class SensorService : ISensorService
    {
        public Task SubmitReadingAsync(int sensorId, double value, DateTime timestamp)
        {
            // Privremeno: ne radimo ništa, samo bacamo NotImplementedException.
            // Sutra ovde povezujemo EF i upis u bazu.
            throw new NotImplementedException();
        }
    }
}
