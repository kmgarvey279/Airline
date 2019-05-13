using Microsoft.VisualStudio.TestTools.UnitTesting;
using Airline.Models;
using System.Collections.Generic;
using System;

namespace Airline.Tests
{
  [TestClass]
  public class CityTest : IDisposable
  {
    public void Dispose()
    {
      City.ClearAll();
    }

    public CityTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=airline_test;";
    }

    [TestMethod]
    public void CityConstructor_CreatesNewInstanceOfCity_City()
    {
      City newCity = new City("test");
      Assert.AreEqual(typeof(City), newCity.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsCityName_String()
    {
      string testName = "test";
      City newCity = new City(testName);
      Assert.AreEqual(testName, newCity.GetName());
    }

    [TestMethod]
    public void SetName_ChangesCityName_True()
    {
      City newCity = new City("name1");
      string name2 = "name2";
      newCity.SetName(name2);
      Assert.AreEqual(newCity.GetName(), name2);
    }

    [TestMethod]
    public void GetId_ReturnsCityId_Int()
    {
    City newCity = new City("test");
    Assert.AreEqual(newCity.GetId(), 0);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyCityList_CityList()
    {
      List<City> newList = new List<City> { };
      City newCity = new City("test");
      List<City> result = City.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectCityFromDatabase_City()
    {
      City newCity = new City("test");
      newCity.Save();
      City foundCity = City.Find(newCity.GetId());
      Assert.AreEqual(newCity, foundCity);

    }
    [TestMethod]
    public void AddFlight_AddsFlightToCity_CityList()
    {
      City testCity = new City("test");
      testCity.Save();
      Flight testFlight = new Flight("test", 1);
      testFlight.Save();
      testCity.AddFlight(testFlight);
      List<Flight> result = testCity.GetFlights();
      Console.WriteLine(result[0]);
      List<Flight> testList = new List<Flight>{testFlight};
      Console.WriteLine(testList[0]);
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetFlights_ReturnsAllFlightsToCity_CityList()
    {
      City testCity = new City("test");
      testCity.Save();
      Flight testFlight1 = new Flight ("test", 1);
      testFlight1.Save();
      Flight testFlight2 = new Flight ("test", 2);
      testFlight2.Save();
      testCity.AddFlight(testFlight1);
      List<Flight> result = testCity.GetFlights();
      Console.WriteLine(result[0]);
      List<Flight> testList = new List<Flight>{testFlight1};
      Console.WriteLine(testList[0]);
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_SavesToDatabase_CityList()
    {
      City newCity = new City("test");
      newCity.Save();
      List<City> result = City.GetAll();
      List<City> testList = new List<City>{newCity};
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      City testCity = new City("test");
      testCity.Save();
      City savedCity = City.GetAll()[0];
      int result = savedCity.GetId();
      int testId = testCity.GetId();
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_City()
    {
      City firstCity = new City("name");
      City secondCity = new City("name");
      Assert.AreEqual(firstCity, secondCity);
    }

  }
}
