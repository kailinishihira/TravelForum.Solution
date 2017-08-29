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

    public City(string name, int countryId, int id=0)
    {
      _id= id;
      _name = name;
      _countryId = countryId;
    }
  }
}
