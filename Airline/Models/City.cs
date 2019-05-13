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

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM cities;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<City> GetAll()
    {
      List<City> allCities = new List<City> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * from cities;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int cityId = rdr.GetInt32(0);
        string cityName = rdr.GetString(1);
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

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cities (name) VALUES (@name);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherCity)
    {
      if (!(otherCity is City))
      {
        return false;
      }
      else
      {
        City newCity = (City) otherCity;
        bool idEquality = this.GetId() == newCity.GetId();
        bool nameEquality = this.GetName() == newCity.GetName();
        return (idEquality && nameEquality);
      }
    }

    public static City Find (int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * from cities where id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int cityId = 0;
      string cityName ="";
      while (rdr.Read())
      {
        cityId = rdr.GetInt32(0);
        cityName = rdr.GetString(1);
      }
      City newCity = new City(cityName, cityId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newCity;
    }

    public List<Flight> GetFlights()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT flight_id FROM cities_flights WHERE city_id = @cityId;";
      MySqlParameter cityIdParameter = new MySqlParameter();
      cityIdParameter.ParameterName = "@cityId";
      cityIdParameter.Value = _id;
      cmd.Parameters.Add(cityIdParameter);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<int> flightIds = new List<int> {};
      while(rdr.Read())
      {
        int flightId = rdr.GetInt32(0);
        flightIds.Add(flightId);
      }
      rdr.Dispose();
      List<Flight> flights = new List<Flight> {};
      foreach (int flightId in flightIds)
      {
        var flightQuery = conn.CreateCommand() as MySqlCommand;
        flightQuery.CommandText = @"SELECT * FROM flights WHERE id = @FlightId;";
        MySqlParameter flightIdParameter = new MySqlParameter();
        flightIdParameter.ParameterName = "@FlightId";
        flightIdParameter.Value = flightId;
        flightQuery.Parameters.Add(flightIdParameter);
        var flightQueryRdr = flightQuery.ExecuteReader() as MySqlDataReader;
        while(flightQueryRdr.Read())
        {
          int thisFlightId = flightQueryRdr.GetInt32(0);
          string flightName = flightQueryRdr.GetString(1);
          Flight foundFlight = new Flight(flightName, thisFlightId);
          flights.Add(foundFlight);
        }
        flightQueryRdr.Dispose();
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return flights;
    }

    public void AddFlight(Flight newFlight)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cities_flights (city_id, flight_id) VALUES (@CityId, @FlightId);";
      MySqlParameter flight_id = new MySqlParameter();
      flight_id.ParameterName = "@FlightId";
      flight_id.Value = newFlight.GetId();
      cmd.Parameters.Add(flight_id);
      MySqlParameter city_id = new MySqlParameter();
      city_id.ParameterName = "@CityId";
      city_id.Value = _id;
      cmd.Parameters.Add(city_id);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }



  }
}
