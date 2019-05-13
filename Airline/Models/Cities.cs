using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Airline.Models
{
  public class City
  {
    private string _name;
    private int _id;

    public City (string name, int id = 0)
    {
      _name = name;
      _id = id;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }

    public int GetId()
    {
      return _id;
    }

    public List<City> GetAll()
    {
      List<City> allCities = new List<City> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * from cities;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        string cityName = rdr.GetString(0);
        int cityId = rdr.GetInt32(1);
        City newCity = new City(cityName, cityId);
        allCities.Add(newCity);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allCities;
    }
  }
}
