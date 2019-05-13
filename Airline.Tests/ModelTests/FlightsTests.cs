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
    public void GetCode_ReturnsFlightCode_String()
    {
      string code = "ABC";
      Flight newFlight = new Flight(code, "", 0);
      string result = newFlight.GetCode();
      Assert.AreEqual(code, result);
    }
  }
}
