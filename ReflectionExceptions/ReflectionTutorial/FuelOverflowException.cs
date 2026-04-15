using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionTutorial;

internal class FuelOverflowException : Exception
{
    public double AmountAdded { get; } 
    public double CurrentFuelLevel { get; }
    public double ExcessAmount { get; }
    public FuelOverflowException(double amountAdded, double currentFuelLevel, double tankCapacity) 
        : base($"Cannot add {amountAdded}L of fuel to a tank that already contains {currentFuelLevel}L. " + 
               $"This would exceed the capacity of {tankCapacity} by {amountAdded + currentFuelLevel - tankCapacity}L.")
    {
        AmountAdded = amountAdded;
        CurrentFuelLevel = currentFuelLevel;
        ExcessAmount = amountAdded + currentFuelLevel - tankCapacity;
    }
}