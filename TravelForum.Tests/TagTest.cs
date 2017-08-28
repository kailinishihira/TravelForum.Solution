using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TravelForum.Models;


namespace TravelForum.Tests
{
  [TestClass]
  public class TagTest
  {
    public TagTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=travel_forum_test;";
    }
  }
}
