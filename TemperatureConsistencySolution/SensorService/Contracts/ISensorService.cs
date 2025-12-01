using System;
using System.ServiceModel;

namespace SensorService.Contracts
{
    [ServiceContract]
    public interface ISensorService
    {
        [OperationContract]
        void SubmitReading(int sensorId, double value, DateTime timestamp);
    }
}
