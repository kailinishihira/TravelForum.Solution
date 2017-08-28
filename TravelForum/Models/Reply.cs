using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace TravelForum.Models
{
  public class Reply
  {
    private int _id;
    private string _name;
    private string _text;
    private int _postId;

    public Reply(string name, string text, int postId, int id=0)
    {
      _id= id;
      _name = name;
      _text = text;
      _postId = postId;
    }
  }
}
