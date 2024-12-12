using System;
using System.Security.Cryptography.X509Certificates;

public class Vehicle
{
	public int Id { get; set; }
	public string Manufacturer { get; set; }
	public string Model { get; set; }
	public int Year { get; set; }
	public decimal StartingBid { get; set; }

	public Vehicle(int id, string manufacturer, string model, int year, decimal startingBid)
	{
		Id = id;
		Manufacturer = manufacturer;
		Model = model;
		Year = year;
		StartingBid = startingBid;
	}
}

public class Hatchback : Vehicle
{
	public int NumberOfDoors { get; set; }

	public Hatchback(int id, string manufacturer, string model, int year, decimal startingBid ,int numberOfDoors)
		: base(id, manufacturer, model, year, startingBid) 
	{
		NumberOfDoors = numberOfDoors;
	}
}

public class Sudan : Vehicle
{
    public int NumberOfDoors { get; set; }

    public Sudan(int id, string manufacturer, string model, int year, decimal startingBid, int numberOfDoors)
        : base(id, manufacturer, model, year, startingBid)
    {
        NumberOfDoors = numberOfDoors;
    }
}

public class SUV : Vehicle
{
    public int NumberOfSeats { get; set; }

    public SUV(int id, string manufacturer, string model, int year, decimal startingBid, int numberOfSeats)
        : base(id, manufacturer, model, year, startingBid)
    {
        NumberOfSeats = numberOfSeats;
    }
}

public class Truck : Vehicle
{
    public int LoadCapacity { get; set; }

    public Truck(int id, string manufacturer, string model, int year, decimal startingBid, int loadCapacity)
        : base(id, manufacturer, model, year, startingBid)
    {
        LoadCapacity = loadCapacity;
    }
}
