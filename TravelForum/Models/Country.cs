using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace TravelForum.Models
{
  public class Country
  {
    private int _id;
    private string _name;
    private int _regionId;

<<<<<<< HEAD
    public Country(string name, int regionId=0, int id=0)
=======
    public Country(string name, int regionId, int id = 0)
>>>>>>> reply
    {
      _id  = id;
      _name = name;
      _regionId = regionId;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public int GetRegionId()
    {
      return _regionId;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM countries;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Country> GetAll()
    {
      List<Country> allCountries = new List<Country> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM countries;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int countryId = rdr.GetInt32(0);
        string countryName = rdr.GetString(1);
        int regionId = rdr.GetInt32(2);
        Country newCountry = new Country(countryName, regionId, countryId);
        allCountries.Add(newCountry);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCountries;
    }

    public override bool Equals(System.Object otherCountry)
    {
      if(!(otherCountry is Country))
      {
        return false;
      }
      else
      {
        Country newCountry = (Country) otherCountry;
        bool idEquality = (this.GetId() == newCountry.GetId());
        bool nameEquality = (this.GetName() == newCountry.GetName());
        bool regionIdEquality = (this.GetRegionId() == newCountry.GetRegionId());
        return (idEquality && nameEquality && regionIdEquality);
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
      cmd.CommandText = @"INSERT INTO countries (name, region_id) VALUES (@name, @regionId);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter regionId = new MySqlParameter();
      regionId.ParameterName = "@regionId";
      regionId.Value = this._regionId;
      cmd.Parameters.Add(regionId);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Country Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM countries WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int countryId = 0;
      string name = "";
      int regionId = 0;

      while (rdr.Read())
      {
        countryId = rdr.GetInt32(0);
        name = rdr.GetString(1);
        regionId = rdr.GetInt32(2);
      }
      Country foundCountry = new Country(name, regionId, countryId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundCountry;
    }

    public List<City> GetCities()
    {
      List<City> allCities = new List<City> ();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cities WHERE country_id = @countryId;";

      MySqlParameter countryIdParam = new MySqlParameter();
      countryIdParam.ParameterName = "@countryId";
      countryIdParam.Value = this._id;
      cmd.Parameters.Add(countryIdParam);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int cityId = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int countryId = rdr.GetInt32(2);
        City newCity = new City(name, countryId, cityId);
        allCities.Add(newCity);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCities;
    }

    public List<Post> GetPosts()
    {
      List<Post> allPosts = new List<Post> ();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM posts WHERE country_id = @countryId;";

      MySqlParameter countryIdParam = new MySqlParameter();
      countryIdParam.ParameterName = "@countryId";
      countryIdParam.Value = this._id;
      cmd.Parameters.Add(countryIdParam);

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
