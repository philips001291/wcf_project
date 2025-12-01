using System;
using System.Linq;
using SensorData.Data;
using SensorData.Models;
using SensorClient.Simulation;

namespace SensorClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pokušavam da kreiram bazu i obezbedim senzore...");

            try
            {
                EnsureSensorsExist();   // 1) Ubacuje 10 senzora ako ih nema

                Console.WriteLine("Senzori su spremni. Pokrećem simulaciju merenja...");

                StartSimulation();      // 2) Pokreće simulaciju za svih 10 senzora

                Console.WriteLine("Simulacija je pokrenuta. Pritisni Enter za izlaz.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Greška: " + ex.Message);
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Proverava da li postoje senzori u bazi.
        /// Ako ne postoje, kreira 10 senzora: Sensor1 ... Sensor10.
        /// </summary>
        private static void EnsureSensorsExist()
        {
            using (var context = new SensorDbContext())
            {
                if (context.Sensors.Any())
                {
                    Console.WriteLine("Senzori već postoje u bazi.");
                    return;
                }

                for (int i = 1; i <= 10; i++)
                {
                    var sensor = new Sensor
                    {
                        Name = $"Sensor{i}"
                    };

                    context.Sensors.Add(sensor);
                }

                context.SaveChanges();
                Console.WriteLine("Upisano 10 senzora u bazu.");
            }
        }

        /// <summary>
        /// Za svakog senzora iz baze kreira i startuje njegov SensorSimulator.
        /// Svaki simulator u pozadini upisuje TemperatureReading vrednosti.
        /// </summary>
        private static void StartSimulation()
        {
            using (var context = new SensorDbContext())
            {
                var sensors = context.Sensors
                                     .OrderBy(s => s.Id)
                                     .ToList();

                foreach (var sensor in sensors)
                {
                    var simulator = new SensorSimulator(sensor.Id, sensor.Name);
                    simulator.Start();
                }

                Console.WriteLine($"Pokrenuto je {sensors.Count} senzora.");
            }
        }
    }
}
