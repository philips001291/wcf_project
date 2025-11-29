using System;
using System.Threading.Tasks;

namespace SensorService.Contracts
{
    // Ovde definišemo metode koje klijent (senzori) mogu da pozovu.
    // WCF atribute [ServiceContract] i [OperationContract] ćemo dodati kasnije.
    public interface ISensorService
    {
        Task SubmitReadingAsync(int sensorId, double value, DateTime timestamp);
    }
}
