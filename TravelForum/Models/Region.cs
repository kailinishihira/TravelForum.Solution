using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace TravelForum.Models
{
  public class Region
  {
    private int _id;
    private string _name;

    public Region(string name, int id=0)
    {
      _id= id;
      _name = name;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM regions;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Region> GetAll()
    {
      List<Region> allRegions = new List<Region> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM regions;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int RegionId = rdr.GetInt32(0);
        string RegionName = rdr.GetString(1);
        Region newRegion = new Region(RegionName, RegionId);
        allRegions.Add(newRegion);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allRegions;
    }

    public override bool Equals(System.Object otherRegion)
    {
      if(!(otherRegion is Region))
      {
        return false;
      }
      else
      {
        Region newRegion = (Region) otherRegion;
        bool idEquality = (this.GetId() == newRegion.GetId());
        bool nameEquality = (this.GetName() == newRegion.GetName());
        return (idEquality && nameEquality);
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
      cmd.CommandText = @"INSERT INTO regions (name) VALUES (@name);";

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

    public static Region Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM regions WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int regionId = 0;
      string name = "";

      while (rdr.Read())
      {
        regionId = rdr.GetInt32(0);
        name = rdr.GetString(1);
      }
      Region foundRegion = new Region(name, regionId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundRegion;
    }

    public List<Country> GetCountries()
    {
      List<Country> allCountries = new List<Country> ();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM countries WHERE region_id = @regionId;";

      MySqlParameter regionIdParam = new MySqlParameter();
      regionIdParam.ParameterName = "@regionId";
      regionIdParam.Value = this._id;
      cmd.Parameters.Add(regionIdParam);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int countryId = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int regionId = rdr.GetInt32(2);
        Country newCountry = new Country(name, regionId, countryId);
        allCountries.Add(newCountry);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCountries;
    }

    public List<Post> GetPosts()
    {
      List<Post> regionPosts = new List<Post>();
      List<Country> allCountries = this.GetCountries();

      foreach(var country in allCountries)
      {
        List<Post> countryPosts = country.GetPosts();
        foreach(var post in countryPosts)
        {
          regionPosts.Add(post);
        }
      }
      return regionPosts;
    }

  }
}
