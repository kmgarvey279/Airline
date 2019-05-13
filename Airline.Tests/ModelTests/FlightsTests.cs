using Microsoft.VisualStudio.TestTools.UnitTesting;
using Airline.Models;
using System.Collections.Generic;
using System;
namespace Airline.Tests
{
  [TestClass]
  public class FlightTest
  {
    [TestMethod]
    public void FlightConstructor_CreatesNewFlight_Flight()
    {
      Flight newFlight = new Flight("", "", 0);
      Assert.AreEqual(typeof(Flight), newFlight.GetType());
    }
    [TestMethod]
    public void GetCode_ReturnsFlightCode_String()
    {
      string code = "ABC";
      Flight newFlight = new Flight(code, "", 0);
      string result = newFlight.GetCode();
      Assert.AreEqual(code, result);
    }
    [TestMethod]
    public void GetCity_ReturnsFlightCity_String()
    {
      string city = "Seattle";
      Flight newFlight = new Flight("", city, 0);
      string result = newFlight.GetCity();
      Assert.AreEqual(city, result);
    }
    [TestMethod]
    public void GetId_ReturnsFlightId_Int()
    {
      int id = 1;
      Flight newFlight = new Flight("", "", id);
      int result = newFlight.GetId();
      Assert.AreEqual(id, result);
    }
  }
}
