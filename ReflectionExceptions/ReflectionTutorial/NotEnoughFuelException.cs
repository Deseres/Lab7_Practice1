using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionTutorial;

internal class NotEnoughFuelException : Exception
{
    public double Distance { get; }
    public double  FuelLevel { get; }
    public double DistanceAvailable { get; }

    public NotEnoughFuelException(double distance, double fuelLevel, double fuelConsumption) : base($"Can't drive {distance} with current fuel level:" +
        $"{fuelLevel}, available distance only {fuelLevel / fuelConsumption * 100.0}")
    {
        Distance = distance;
        FuelLevel = fuelLevel;
        DistanceAvailable = fuelLevel / fuelConsumption * 100.0;
    }
}