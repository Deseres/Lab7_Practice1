using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
public class FileCarStore
{
    public void StoreCars(string path, IEnumerable<Car> cars)
    {
        File.Delete(path);
        foreach (var car in cars)
        {
            File.AppendAllLines("car.txt",
                [$" {car.Brand} ; {car.Model} ; {car.TankCapacity} ; {car.FuelConsumption} ; {car.FuelLevel} ; {car.Odometer} "]);
        }
    }
    public IList<Car> RestoreCars(string path)
    {
        var result = new List<Car>();
        var fileLines = File.ReadAllLines(path);

        foreach (var line in fileLines)
        {
            var carRawData = line.Split(';');
            
            var carType = typeof(Car);
            var fuelLevelField = carType.GetField("_fuelLevel", BindingFlags.NonPublic | BindingFlags.Instance);
            var odometerField = carType.GetField("_odometer", BindingFlags.NonPublic | BindingFlags.Instance);

            
            var car = new Car(carRawData[0], carRawData[1], int.Parse(carRawData[2]), double.Parse(carRawData[3]));
            fuelLevelField.SetValue(car, double.Parse(carRawData[4]));
            odometerField.SetValue(car, double.Parse(carRawData[5]));

            result.Add(car);
        }
        return result;
    }
}

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
        Brand = brand;
        Model = model;
        TankCapacity = tankCapacity;
        FuelConsumption = fuelConsumption;
        CarsMade++;
    }
    public void AddFuel(double amount)
    {
        if (amount + _fuelLevel > TankCapacity)
            _fuelLevel = TankCapacity;
        else
            _fuelLevel += amount;
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



