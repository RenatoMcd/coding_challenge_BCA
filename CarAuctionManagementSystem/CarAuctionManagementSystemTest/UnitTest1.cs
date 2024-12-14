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
        public void TestSearchVehicle_Success()
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

        [TestMethod]
        public void TestAuctionStart_Success()
        {
            var v1 = new Hatchback(1, "Volkswagen", "Golf", 2022, 15000, 5);
            _test.AddVehicle(v1);
            _test.StartAuction(1);

            Assert.IsTrue(_test._auctions[1].IsActive);

            var v2 = new Hatchback(2, "Volkswagen", "Polo", 2024, 25000, 5);
            _test.AddVehicle(v2);
            _test.StartAuction(2);

            Assert.IsTrue(_test._auctions[1].IsActive);
            Assert.IsTrue(_test._auctions[2].IsActive);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAuctionStart_FailNoVehicle()
        {
            _test.StartAuction(1);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAuctionStart_FailAlreadyStarted()
        {
            var v1 = new Hatchback(1, "Volkswagen", "Golf", 2022, 15000, 5);
            _test.AddVehicle(v1);
            _test.StartAuction(1);
            _test.StartAuction(1);
        }

        [TestMethod]
        public void TestAuctionClose_Success()
        {
            var v1 = new Hatchback(1, "Volkswagen", "Golf", 2022, 15000, 5);
            _test.AddVehicle(v1);
            _test.StartAuction(1);
            _test.CloseAuction(1);

            Assert.IsFalse(_test._auctions[1].IsActive);

            var v2 = new Hatchback(2, "Volkswagen", "Polo", 2024, 25000, 5);
            _test.AddVehicle(v2);

            _test.StartAuction(1);
            _test.StartAuction(2);

            _test.CloseAuction(2);
            Assert.IsTrue(_test._auctions[1].IsActive);
            Assert.IsFalse(_test._auctions[2].IsActive);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAuctionClose_FailNoAuctionStarted()
        {
            _test.CloseAuction(1);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAuctionClose_FailAlreadyClosed()
        {
            var v1 = new Hatchback(1, "Volkswagen", "Golf", 2022, 15000, 5);
            _test.AddVehicle(v1);
            _test.StartAuction(1);
            _test.CloseAuction(1);
            _test.CloseAuction(1);
        }

        [TestMethod]
        public void TestPlaceBid_Success()
        {
            var v1 = new Hatchback(1, "Volkswagen", "Golf", 2022, 15000, 5);
            _test.AddVehicle(v1);
            _test.StartAuction(1);

            _test.PlaceBid(1, 15100, "Renato");

            Assert.AreEqual(15100, _test._auctions[1].HighestBid);
            Assert.AreEqual("Renato", _test._auctions[1].HighestBidder);

            _test.PlaceBid(1, 15200, "Ana");

            Assert.AreEqual(15200, _test._auctions[1].HighestBid);
            Assert.AreEqual("Ana", _test._auctions[1].HighestBidder);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestPlaceBid_FailLowBid()
        {
            var v1 = new Hatchback(1, "Volkswagen", "Golf", 2022, 15000, 5);
            _test.AddVehicle(v1);
            _test.StartAuction(1);

            _test.PlaceBid(1, 14000, "Renato");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestPlaceBid_FailNoVehicleInAuction()
        {
            var v1 = new Hatchback(1, "Volkswagen", "Golf", 2022, 15000, 5);
            _test.AddVehicle(v1);
            _test.PlaceBid(2, 15100, "Renato");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestPlaceBid_FailNoAuctionStarted()
        {
            var v1 = new Hatchback(1, "Volkswagen", "Golf", 2022, 15000, 5);
            _test.AddVehicle(v1);
            _test.PlaceBid(1, 15100, "Renato");
        }
    }
}
