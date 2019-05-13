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
      // City.ClearAll()
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
      List<City> result = newCity.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }
  }
}
