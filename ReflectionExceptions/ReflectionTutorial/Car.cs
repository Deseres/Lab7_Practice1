namespace ReflectionTutorial;

public class Car
{
    public static int CarsMade { get; set; }

    public string Brand { get; set; }
    public string Model { get; set; }
    public int TankCapacity { get; set; }
    public double FuelConsumption { get; set; }
    public int FuelLevel => (int)_fuelLevel;
    public int Odometer => (int)_odometer;

    private double _fuelLevel;
    private double _odometer;

    public Car(string brand, string model, int tankCapacity, double fuelConsumption)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(brand, nameof(brand));
        ArgumentException.ThrowIfNullOrWhiteSpace(model, nameof(model));
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(tankCapacity, 0, nameof(tankCapacity));
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(fuelConsumption, 0, nameof(fuelConsumption));

        Brand = brand;
        Model = model;
        TankCapacity = tankCapacity;
        FuelConsumption = fuelConsumption;

        CarsMade++;
    }

    public void AddFuel(double amount)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(amount, 0, nameof(amount));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(amount, TankCapacity, nameof(amount));

        if (amount + _fuelLevel > TankCapacity)
        {
            throw new FuelOverflowException(amount, _fuelLevel, TankCapacity);
        }
        else
        {
            _fuelLevel += amount;
        }
    }

    public void Drive(double distance)
    {
        var fuelNeeded = distance / 100.0 * FuelConsumption;

        if (fuelNeeded > _fuelLevel)
        {
            _odometer += _fuelLevel / FuelConsumption * 100.0;
            _fuelLevel = 0;
        }
        else
        {
            _fuelLevel -= fuelNeeded;
            _odometer += distance;
        }
    }
}