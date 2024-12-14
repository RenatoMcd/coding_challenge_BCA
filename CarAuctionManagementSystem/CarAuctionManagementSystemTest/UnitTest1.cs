using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarAuctionManagementSystemTest
{
    [TestClass]
    public class CarAuctionManagementSystemTest
    {
        public CarAuctionManagementSystem _test;

        [TestInitialize]
        public void Setup()
        {
            _test = new CarAuctionManagementSystem();
        }

        [TestMethod]
        public void TestAddVehicle_Success()
        {
            var v1 = new Hatchback(1, "Volkswagen", "Golf", 2022, 15000, 5);
            _test.AddVehicle(v1);

            var result = _test._vehicleInventory;

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Hatchback", result.First().ToString());
            Assert.AreEqual(1, result.First().Id);
            Assert.AreEqual("Volkswagen", result.First().Manufacturer);
            Assert.AreEqual("Golf", result.First().Model);
            Assert.AreEqual(2022, result.First().Year);
            Assert.AreEqual(15000, result.First().StartingBid);

            var v2 = new Sudan(2, "Honda", "Civic", 2020, 20000, 3);
            _test.AddVehicle(v2);

            var result2 = _test._vehicleInventory;

            Assert.AreEqual(2, result2.Count());
            Assert.AreEqual("Sudan", result2[1].ToString());
            Assert.AreEqual(2, result2[1].Id);
            Assert.AreEqual("Honda", result2[1].Manufacturer);
            Assert.AreEqual("Civic", result2[1].Model);
            Assert.AreEqual(2020, result2[1].Year);
            Assert.AreEqual(20000, result2[1].StartingBid);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAddVehicle_FailDuplicate()
        {
            var v1 = new Hatchback(1, "Volkswagen", "Golf", 2022, 15000, 5);
            _test.AddVehicle(v1);
            _test.AddVehicle(v1);
        }

        [TestMethod]
        public void TestSearchVehicle()
        {
            var v1 = new Hatchback(1, "Volkswagen", "Golf", 2022, 15000, 5);
            _test.AddVehicle(v1);

            var result = _test.SearchVehicles(type: "Hatchback");

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Hatchback", result.First().ToString());
            Assert.AreEqual(1, result.First().Id);
            Assert.AreEqual("Volkswagen", result.First().Manufacturer);
            Assert.AreEqual("Golf", result.First().Model);
            Assert.AreEqual(2022, result.First().Year);
            Assert.AreEqual(15000, result.First().StartingBid);

            var v2 = new Hatchback(2, "Volkswagen", "Polo", 2024, 25000, 5);
            _test.AddVehicle(v2);

            var result2 = _test.SearchVehicles(type: "Hatchback");

            Assert.AreEqual(2, result2.Count());
            Assert.AreEqual("Hatchback", result2.First().ToString());
            Assert.AreEqual(1, result2.First().Id);
            Assert.AreEqual("Volkswagen", result2.First().Manufacturer);
            Assert.AreEqual("Golf", result2.First().Model);
            Assert.AreEqual(2022, result2.First().Year);
            Assert.AreEqual(15000, result2.First().StartingBid);
            Assert.AreEqual("Hatchback", result2.ElementAt(1).ToString());
            Assert.AreEqual(2, result2.ElementAt(1).Id);
            Assert.AreEqual("Volkswagen", result2.ElementAt(1).Manufacturer);
            Assert.AreEqual("Polo", result2.ElementAt(1).Model);
            Assert.AreEqual(2024, result2.ElementAt(1).Year);
            Assert.AreEqual(25000, result2.ElementAt(1).StartingBid);

            var result3 = _test.SearchVehicles(type: "Sudan");

            Assert.AreEqual(0, result3.Count());
        }
    }
}
