using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Airline.Models
{
  public class Flight
  {
    private string _code;
    private int _id;

    public Flight(string code, int id = 0)
    {
      _code = code;
      _id = id;
    }

    public string GetCode()
    {
      return _code;
    }

    public void SetCode(string newCode)
    {
      _code = newCode;
    }

    public int GetId()
    {
      return _id;
    }

    public static List<Flight> GetAll()
    {
      List<Flight> allFlights = new List<Flight>{ };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM flights;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int flightId = rdr.GetInt32(0);
        string flightCode = rdr.GetString(1);
        Flight newFlight = new Flight (flightCode, flightId);
        allFlights.Add(newFlight);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allFlights;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO flights (code) VALUES (@code);";
      MySqlParameter code = new MySqlParameter();
      code.ParameterName = "@code";
      code.Value = this._code;
      cmd.Parameters.Add(code);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM flights;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherFlight)
    {
      if(!(otherFlight is Flight))
      {
        return false;
      }
      else
      {
        Flight newFlight = (Flight) otherFlight;
        bool codeEquality = this.GetCode() == newFlight.GetCode();
        bool idEquality = this.GetId() == newFlight.GetId();
        return (codeEquality && idEquality);
      }
    }

    public static Flight Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText =@"SELECT * FROM flights WHERE id = (@searchId);";
      MySqlParameter searchId =  new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int flightId = 0;
      string flightCode = "";
      while(rdr.Read())
      {
        flightId = rdr.GetInt32(0);
        flightCode = rdr.GetString(1);
      }
      Flight newFlight = new Flight(flightCode, flightId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newFlight;
    }

    public List<City> GetCities()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT city_id FROM cities_flights WHERE flight_id = @flightId;";
      MySqlParameter flightIdParameter = new MySqlParameter();
      flightIdParameter.ParameterName = "@flightId";
      flightIdParameter.Value = _id;
      cmd.Parameters.Add(flightIdParameter);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<int> cityIds = new List<int> {};
      while(rdr.Read())
      {
        int cityId = rdr.GetInt32(0);
        cityIds.Add(cityId);
      }
      rdr.Dispose();
      List<City> cities = new List<City> {};
      foreach (int cityId in cityIds)
      {
        var cityQuery = conn.CreateCommand() as MySqlCommand;
        cityQuery.CommandText = @"SELECT * FROM cities WHERE id = @CityId;";
        MySqlParameter cityIdParameter = new MySqlParameter();
        cityIdParameter.ParameterName = "@CityId";
        cityIdParameter.Value = cityId;
        cityQuery.Parameters.Add(cityIdParameter);
        var cityQueryRdr = cityQuery.ExecuteReader() as MySqlDataReader;
        while(cityQueryRdr.Read())
        {
          int thisCityId = cityQueryRdr.GetInt32(0);
          string cityName = cityQueryRdr.GetString(1);
          City foundCity = new City(cityName, thisCityId);
          cities.Add(foundCity);
        }
        cityQueryRdr.Dispose();
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return cities;
    }

    public void AddCity(City newCity)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cities_flights (city_id, flight_id) VALUES (@CityId, @FlightId);";
      MySqlParameter city_id = new MySqlParameter();
      city_id.ParameterName = "@CityId";
      city_id.Value = newCity.GetId();
      cmd.Parameters.Add(city_id);
      MySqlParameter flight_id = new MySqlParameter();
      flight_id.ParameterName = "@FlightId";
      flight_id.Value = _id;
      cmd.Parameters.Add(flight_id);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

  }
}
