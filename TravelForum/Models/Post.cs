using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace TravelForum.Models
{
  public class Post
  {
    private int _id;
    private string _title;
    private string _name;
    private DateTime _startDate;
    private DateTime _endDate;
    private int _cityId;

    public Post(string title, string name, DateTime startDate, DateTime endDate, int cityId, int id=0)
    {
      _id= id;
      _title = title;
      _name = name;
      _startDate = startDate;
      _endDate = endDate;
      _cityId = cityId;
    }
  }
}
