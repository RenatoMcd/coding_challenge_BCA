using System;
using System.Collections.Generic;
using System.Linq;
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
    public string HighestBidder { get; set; }

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
            throw new Exception("There is already a vehicle with this ID.");
        }
        _vehicleInventory.Add(vehicle);
    }

    public IEnumerable<Vehicle> SearchVehicles(string type = null, string manufaturer = null, string model = null, int? year = null)
    {
        return _vehicleInventory.Where(v =>
            (type == null || v.GetType().Name == type) &&
            (manufaturer == null || v.Manufacturer == manufaturer) &&
            (model == null || v.Model == model) &&
            (year == null || v.Year == year));
    }

    public void StartAuction(int vehicleId)
    {
        var vehicle = _vehicleInventory.FirstOrDefault(v => v.Id == vehicleId);
        
        if (vehicle == null)
        {
            throw new Exception("There is no vehicle with this ID in the inventory.");
        }
        
        if (!_auctions.ContainsKey(vehicleId))
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
        var carAuctionManager = new CarAuctionManagementSystem();

        var v1 = new Hatchback(1, "Volkswagen", "Golf", 2022, 15000, 5);
        var v2 = new Sudan(2, "Honda", "Civic", 2020, 20000, 3);

        carAuctionManager.AddVehicle(v1);
        Console.WriteLine($"Adding Volkwagen Golf to inventory...");
        carAuctionManager.AddVehicle(v2);
        Console.WriteLine($"Adding Honda Civic to inventory...");

        Console.WriteLine($"Checking inventory...");

        foreach (var vehicle in carAuctionManager._vehicleInventory)
        {
            Console.WriteLine($"Vehicle {vehicle.Id}: {vehicle.Manufacturer} {vehicle.Model}, year: {vehicle.Year}, starting bid: {vehicle.StartingBid}");
        }

        Console.WriteLine($"Searching for Volkwagen...");

        var searchResults = carAuctionManager.SearchVehicles(manufaturer: "Volkswagen");

        foreach (var vehicle in searchResults)
        {
            Console.WriteLine($"Vehicle {vehicle.Id}: {vehicle.Manufacturer} {vehicle.Model}, year: {vehicle.Year}, starting bid: {vehicle.StartingBid}");
        }

        carAuctionManager.StartAuction(1);
        Console.WriteLine($"Starting aution for the {carAuctionManager._auctions[1].Vehicle.Manufacturer} {carAuctionManager._auctions[1].Vehicle.Model}");
        
        carAuctionManager.PlaceBid(1, 16000, "Renato");
        Console.WriteLine($"{carAuctionManager._auctions[1].HighestBid} from {carAuctionManager._auctions[1].HighestBidder}");
        
        carAuctionManager.PlaceBid(1, 17000, "Ana");
        Console.WriteLine($"{carAuctionManager._auctions[1].HighestBid} from {carAuctionManager._auctions[1].HighestBidder}");
        
        carAuctionManager.CloseAuction(1);
        Console.WriteLine($"Auction Closed");
    }
}


