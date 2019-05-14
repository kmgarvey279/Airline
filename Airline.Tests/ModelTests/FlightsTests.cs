using Microsoft.VisualStudio.TestTools.UnitTesting;
using Airline.Models;
using System.Collections.Generic;
using System;

namespace Airline.Tests
{
  [TestClass]
  public class FlightTest : IDisposable
  {
    public void Dispose()
    {
      Flight.ClearAll();
    }

    public FlightTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=airline_test;";
    }


    [TestMethod]
    public void FlightConstructor_CreatesNewFlight_Flight()
    {
      Flight newFlight = new Flight("test");
      Assert.AreEqual(typeof(Flight), newFlight.GetType());
    }
    [TestMethod]
    public void GetCode_ReturnsFlightCode_String()
    {
      string code = "ABC";
      Flight newFlight = new Flight(code);
      string result = newFlight.GetCode();
      Assert.AreEqual(code, result);
    }
    [TestMethod]
    public void SetCode_ChangesFlightCode_True()
    {
      string code = "ABC";
      Flight newFlight = new Flight(code);
      string newCode = "EFG";
      newFlight.SetCode(newCode);
      string result = newFlight.GetCode();
      Assert.AreEqual(newCode, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyList_FlightList()
    {
      List<Flight> newList = new List<Flight>{ };
      List<Flight> result = Flight.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }

    // [TestMethod]
    // public void GetAll_ReturnsFlight_FlightList

    [TestMethod]
    public void Save_SavesToDatabase_FlightList()
    {
      Flight newFlight = new Flight("test");
      newFlight.Save();
      List<Flight> result = Flight.GetAll();
      Console.WriteLine(result.Count);
      List<Flight> testList = new List<Flight>{newFlight};
      Console.WriteLine(testList.Count);
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      Flight testFlight = new Flight("test");
      testFlight.Save();
      Flight savedFlight = Flight.GetAll()[0];
      int result = savedFlight.GetId();
      int testId = testFlight.GetId();
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_City()
    {
      Flight flight1 = new Flight("test");
      Flight flight2 = new Flight("test");
      Assert.AreEqual(flight1, flight2);
    }

    [TestMethod]
    public void Find_ReturnsCorrectFlightsFromDatabase_Flight()
    {
      Flight testFlight = new Flight("test");
      testFlight.Save();
      Flight foundFlight = Flight.Find(testFlight.GetId());
      Assert.AreEqual(testFlight, foundFlight);
    }

    [TestMethod]
    public void AddCity_AddsCityToFlight_FlightList()
    {
      Flight testFlight = new Flight("test");
      testFlight.Save();
      City testCity = new City("test");
      testCity.Save();
      testFlight.AddCity(testCity);
      List<City> result = testFlight.GetCities();
      List<City> testList = new List<City>{testCity};
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetCities_ReturnsAllCityFlights_FlightList()
    {
      Flight testFlight = new Flight("test");
      testFlight.Save();
      City testCity1 = new City ("test1");
      testCity1.Save();
      City testCity2 = new City ("test2");
      testCity2.Save();
      testFlight.AddCity(testCity1);
      List<City> result = testFlight.GetCities();
      List<City> testList = new List<City>{testCity1};
      CollectionAssert.AreEqual(testList, result);
    }
  }
}
