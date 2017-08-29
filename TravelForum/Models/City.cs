using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace TravelForum.Models
{
  public class City
  {
    private int _id;
    private string _name;
    private int _countryId;

    public City(string name, int countryId=0, int id=0)
    {
      _id= id;
      _name = name;
      _countryId = countryId;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public int GetCountryId()
    {
      return _countryId;
    }

    public static void DeleteAll()
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
      cmd.CommandText = @"SELECT * FROM cities;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int cityId = rdr.GetInt32(0);
        string cityName = rdr.GetString(1);
        int countryId = rdr.GetInt32(2);
        City newCity = new City(cityName, countryId, cityId);
        allCities.Add(newCity);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCities;
    }

    public override bool Equals(System.Object otherCity)
    {
      if(!(otherCity is City))
      {
        return false;
      }
      else
      {
        City newCity = (City) otherCity;
        bool idEquality = (this.GetId() == newCity.GetId());
        bool nameEquality = (this.GetName() == newCity.GetName());
        bool countryIdEquality = (this.GetCountryId() == newCity.GetCountryId());
        return (idEquality && nameEquality && countryIdEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cities (name, country_id) VALUES (@name, @countryId);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter countryId = new MySqlParameter();
      countryId.ParameterName = "@countryId";
      countryId.Value = this._countryId;
      cmd.Parameters.Add(countryId);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static City Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cities WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int cityId = 0;
      string name = "";
      int countryId = 0;

      while (rdr.Read())
      {
        cityId = rdr.GetInt32(0);
        name = rdr.GetString(1);
        countryId = rdr.GetInt32(2);
      }
      City foundCity = new City(name, countryId, cityId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundCity;
    }

    public List<Post> GetPosts()
    {
      List<Post> allPosts = new List<Post> ();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM posts WHERE city_id = @cityId;";

      MySqlParameter cityIdParam = new MySqlParameter();
      cityIdParam.ParameterName = "@cityId";
      cityIdParam.Value = this._id;
      cmd.Parameters.Add(cityIdParam);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int postId = rdr.GetInt32(0);
        string title = rdr.GetString(1);
        string name = rdr.GetString(2);
        DateTime startDate = rdr.GetDateTime(3);
        DateTime endDate = rdr.GetDateTime(4);
        string text = rdr.GetString(5);
        int cityId = rdr.GetInt32(6);
        int countryId = rdr.GetInt32(7);
        int regionId = rdr.GetInt32(8);
        Post newPost = new Post(title, name, startDate, endDate, text, cityId, countryId, regionId, postId);
        allPosts.Add(newPost);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allPosts;
    }
  }
}
