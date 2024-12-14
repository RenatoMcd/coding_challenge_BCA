using System;
using System.Collections.Generic;
using System.Linq;
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

public class Auction
{
    public Vehicle Vehicle { get; set; }
    public bool IsActive { get; set; }
    public decimal HighestBid { get; set; }
    public decimal HighestBidder { get; set; }

    public Auction(Vehicle vehicle)
    {
        Vehicle = vehicle;
        IsActive = false;
        HighestBid = vehicle.StartingBid;
    }

    public void StartAuction()
    {
        if (IsActive)
        {
            throw new Exception("Auction is already active.");
        }
        IsActive = true;
    }

    public void CloseAuction()
    {
        if (!IsActive)
        {
            throw new Exception("Auction is not active.");
        }
        IsActive = false;
    }

    public void PlaceBid(decimal bid, string bidder)
    {
        if (!IsActive)
        {
            throw new Exception("Can't place the bid, the auction is not active.");
        }
        if (bid <= HighestBid)
        {
            throw new Exception("Bid must be greatter than the current bid.");
        }

        HighestBid = bid;
        HighestBidder = bidder;
    }
}

public class CarAuctionManagementSystem
{
    public List<Vehicle> _vehicleInventory = new List<Vehicle>();
    public Dictionary<int, Auction> _auctions = new Dictionary<int, Auction>();

    public void AddVehicle(Vehicle vehicle)
    {
        if (_vehicleInventory.Any(v => v.Id == vehicle.Id))
        {
            throw new Exception("There is already a vehicle with this ID.")
        }
        _vehicleInventory.Add(vehicle);
    }

    public IEnumerable<Vehicle> SearchVehicles(string type, string manufaturer, string model, int year)
    {
        return _vehicleInventory.Where(v =>
            v.GetType().Name == type &&
            v.Manufacturer == manufaturer &&
            v.Model == model &&
            v.Year == year);
    }

    public void StartAuction(int vehicleId)
    {
        var vehicle = _vehicleInventory.FirstOrDefault(v => v.Id == vehicleId);
        
        if (vehicle == null)
        {
            throw new Exception("There is no vehicle with this ID in the inventory.");
        }
        
        if (_auctions.ContainsKey(vehicleId))
        {
            throw new Exception("This vehicle is already in an active auction.");
        }
        else
        {
            _auctions[vehicleId] = new Auction(vehicle);
        }

        _auctions[vehicleId].StartAuction();    
    }

    public void CloseAuction(int vehicleID)
    {
        if (!_auctions.ContainsKey(vehicleID) || !_auctions[vehicleID].IsActive)
        {
            throw new Exception("There is no active auction for this vehicle ID");
        }

        _auctions[vehicleID].CloseAuction();
    }

    public void PlaceBid(int vehicleID, decimal bid, string bidder)
    {
        if (!_auctions.ContainsKey(vehicleID) || !_auctions[vehicleID].IsActive)
        {
            throw new Exception("There is no active auction for this vehicle ID");
        }

        _auctions[vehicleID].PlaceBid(bid, bidder);
    }
}


public class Program
{
    public static void Main()
    {
        
    }
}


