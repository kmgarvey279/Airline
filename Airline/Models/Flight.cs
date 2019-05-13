using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Airline.Models
{
  public class Flight
  {
    private string _code;
    private int _city;
    private int _id;

    public Flight(string code, int city, int id = 0)
    {
      _code = code;
      _city = city;
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

    public int GetCity()
    {
      return _city;
    }
    public void SetCity(int newCity)
    {
      _city = newCity;
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
        int flightCity = rdr.GetInt32(2);
        Flight newFlight = new Flight (flightCode, flightCity, flightId);
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
      cmd.CommandText = @"INSERT INTO flights (code, city) VALUES (@code, @city);";
      MySqlParameter code = new MySqlParameter();
      code.ParameterName = "@code";
      code.Value = this._code;
      cmd.Parameters.Add(code);
      MySqlParameter city = new MySqlParameter();
      city.ParameterName = "@city";
      city.Value = this._city;
      cmd.Parameters.Add(city);
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
        bool codeEquality = this.GetId() == newFlight.GetId();
        bool cityEquality = this.GetCity() == newFlight.GetCity();
        bool idEquality = this.GetId() == newFlight.GetId();
        return (codeEquality && cityEquality && idEquality);
      }
    }
  }
}
