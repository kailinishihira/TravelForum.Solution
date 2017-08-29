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

    public Country(string name, int regionId, int id = 0)
    {
      _id  = id;
      _name = name;
      _regionId = regionId;
    }
  }
}
