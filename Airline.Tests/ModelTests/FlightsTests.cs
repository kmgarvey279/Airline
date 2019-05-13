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

    [TestMethod]
    public void FlightConstructor_CreatesNewFlight_Flight()
    {
      Flight newFlight = new Flight("", 1, 0);
      Assert.AreEqual(typeof(Flight), newFlight.GetType());
    }
    [TestMethod]
    public void GetCode_ReturnsFlightCode_String()
    {
      string code = "ABC";
      Flight newFlight = new Flight(code, 1, 0);
      string result = newFlight.GetCode();
      Assert.AreEqual(code, result);
    }
    [TestMethod]
    public void SetCode_ChangesFlightCode_True()
    {
      string code = "ABC";
      Flight newFlight = new Flight(code, 1, 0);
      string newCode = "EFG";
      newFlight.SetCode(newCode);
      string result = newFlight.GetCode();
      Assert.AreEqual(newCode, result);
    }
    [TestMethod]
    public void GetCity_ReturnsFlightCity_String()
    {
      int city = 1;
      Flight newFlight = new Flight("", city, 0);
      int result = newFlight.GetCity();
      Assert.AreEqual(city, result);
    }
    [TestMethod]
    public void SetCity_ChangesFlightCity_True()
    {
      Flight newFlight = new Flight("", 1, 0);
      int newCity = 1;
      newFlight.SetCity(newCity);
      int result = newFlight.GetCity();
      Assert.AreEqual(newCity, result);
    }

    [TestMethod]
    public void GetId_ReturnsFlightId_Int()
    {
      int id = 1;
      Flight newFlight = new Flight("test", 1, id);
      int result = newFlight.GetId();
      Assert.AreEqual(id, result);
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
      Flight newFlight = new Flight("", 1, 1);
      newFlight.Save();
      List<Flight> result = Flight.GetAll();
      Console.WriteLine(result.Count);
      List<Flight> testList = new List<Flight>{newFlight};
      Console.WriteLine(testList.Count);
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_City()
    {
      Flight flight1 = new Flight("", 1, 1);
      Flight flight2 = new Flight("", 1, 1);
      Assert.AreEqual(flight1, flight2);
    }
  }
}
