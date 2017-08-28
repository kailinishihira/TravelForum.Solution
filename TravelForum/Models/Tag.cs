using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace TravelForum.Models
{
  public class Tag
  {
    private int _id;
    private string _name;

    public Tag(string name, int id=0)
    {
      _id= id;
      _name = name;
    }
  }
}
