using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Airline.Models
{
  public class Flight
  {
    private string _code;
    private string _city;
    private int _id;

    public Flight(string code, string city, int id = 0)
    {
      _code = code;
      _city = city;
      _id = id;
    }

    public string GetCode()
    {
      return _code;
    }

    public string GetCity()
    {
      return _city;
    }


    public int GetId()
    {
      return _id;
    }
  }

}
