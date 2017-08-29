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
    private int _post_id;

    public Reply(string name, string text, int post_id, int id=0)
    {
      _id= id;
      _name = name;
      _text = text;
      _post_id = post_id;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetText()
    {
      return _text;
    }

    public int GetPostId()
    {
      return _post_id;
    }

    public override bool Equals(Object otherReply)
    {
      if (!(otherReply is Reply))
      {
        return false;
      }
      else
      {
        Reply newReply = (Reply) otherReply;

        bool idEquality = (this.GetId() == newReply.GetId());
        bool nameEquality = (this.GetName() == newReply.GetName());
        bool textEquality = (this.GetText() == newReply.GetText());
        bool post_idEquality = (this.GetPostId() == newReply.GetPostId());


        return (idEquality && nameEquality && textEquality && post_idEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public static List<Reply> GetAll()
    {
     List<Reply> replyList = new List<Reply> {};

     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM replies;";

     var rdr = cmd.ExecuteReader() as MySqlDataReader;
     while(rdr.Read())
     {
       int replyId = rdr.GetInt32(0);
       string name = rdr.GetString(1);
       string text = rdr.GetString(2);
       int post_id = rdr.GetInt32(3);

       Reply newReply = new Reply(name, text, post_id, replyId);
       replyList.Add(newReply);
     }
     conn.Close();
     return replyList;
    }

    public void Save()
   {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"INSERT INTO replies (name, text, post_id) VALUES (@name, @text, @post_id);";

     MySqlParameter name = new MySqlParameter();
     name.ParameterName = "@name";
     name.Value = this._name;
     cmd.Parameters.Add(name);

     MySqlParameter text = new MySqlParameter();
     text.ParameterName = "@text";
     text.Value = this._text;
     cmd.Parameters.Add(text);

     MySqlParameter post_id = new MySqlParameter();
     post_id.ParameterName = "@post_id";
     post_id.Value = this._post_id;
     cmd.Parameters.Add(post_id);

     cmd.ExecuteNonQuery();
     _id = (int) cmd.LastInsertedId;
     conn.Close();
   }

   public static Reply Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM replies WHERE id = @replyId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@replyId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int replyId = 0;
      string name = "";
      string text = "";
      int postId = 0;

      while(rdr.Read())
      {
        replyId = rdr.GetInt32(0);
        name = rdr.GetString(1);
        text = rdr.GetString(2);
        postId = rdr.GetInt32(3);

      }
      Reply foundReply = new Reply(name, text, postId, replyId);
      conn.Close();
      return foundReply;
    }

    public void Update(string newName, string newText)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE replies SET name = @newName, text = @newText WHERE id = @thisId;";


      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);

      MySqlParameter text = new MySqlParameter();
      text.ParameterName = "@newText";
      text.Value = newText;
      cmd.Parameters.Add(text);

      cmd.ExecuteNonQuery();
        conn.Close();
        if(conn != null)
        {
          conn.Dispose();
        }
    }

    public static List<Reply> GetRepliesByPostId(int id)
    {
      List<Reply> replyList = new List<Reply> {};

      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM replies WHERE post_id = @postId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@postId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);


      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int replyId = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string text = rdr.GetString(2);
        int post_id = rdr.GetInt32(3);

        Reply newReply = new Reply(name, text, post_id, replyId);
        replyList.Add(newReply);
      }
      conn.Close();
      return replyList;
     }

     public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM replies WHERE id = @thisId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      cmd.ExecuteNonQuery();
      conn.Close();
    }


   public static void DeleteAll()
   {
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"DELETE FROM replies;";
     cmd.ExecuteNonQuery();
     conn.Close();
   }


  }
}
