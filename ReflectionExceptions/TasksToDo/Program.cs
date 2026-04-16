using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using TasksToDo;
public class FileCarStore
{
    public void StoreCars(string path, IEnumerable<Car> cars)
    {
        if (File.Exists(path)) File.Delete(path);

        var carType = typeof(Car);
        var fuelLevelField = carType.GetField("_fuelLevel", BindingFlags.NonPublic | BindingFlags.Instance);
        var odometerField = carType.GetField("_odometer", BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var car in cars)
        {

            File.AppendAllLines(path,
                [$"{car.Brand};{car.Model};{car.TankCapacity};{car.FuelConsumption};{fuelLevelField.GetValue(car)};{odometerField.GetValue(car)}"]);
        }
    }
    public IList<Car> RestoreCars(string path)
    {
        var result = new List<Car>();
        var fileLines = File.ReadAllLines(path);

        var carType = typeof(Car);
        var fuelLevelField = carType.GetField("_fuelLevel", BindingFlags.NonPublic | BindingFlags.Instance);
        var odometerField = carType.GetField("_odometer", BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var line in fileLines)
        {
            var carRawData = line.Split(';');
            
            var car = new Car(carRawData[0], carRawData[1], int.Parse(carRawData[2]), double.Parse(carRawData[3]));
            fuelLevelField.SetValue(car, double.Parse(carRawData[4]));
            odometerField.SetValue(car, double.Parse(carRawData[5]));

            result.Add(car);
        }
        Car.CarsMade = 0;
        return result;
    }
}




