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

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }

    public override bool Equals(object otherTag)
    {
      if (! (otherTag is Tag))
      {
        return false;
      }
      else
      {
        Tag newTag = (Tag) otherTag;
        bool idEquality = newTag.GetId() == this._id;
        bool nameEquality = newTag.GetName() == this._name;
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO tags (name) VALUES (@name);";

      MySqlParameter nameParam = new MySqlParameter();
      nameParam.ParameterName = "@name";
      nameParam.Value = _name;
      cmd.Parameters.Add(nameParam);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Tag> GetAll()
    {
      List<Tag> allTags = new List<Tag> {};

      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM tags;";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Tag newTag = new Tag(name, id);
        allTags.Add(newTag);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allTags;
    }

    public static Tag Find(int searchId)
    {
      var foundTag = default(Tag);
      int id = 0;
      string name = "";

      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM tags WHERE id = @id;";

      MySqlParameter idParam = new MySqlParameter();
      idParam.ParameterName = "@id";
      idParam.Value = searchId;
      cmd.Parameters.Add(idParam);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
        foundTag = new Tag(name, id);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundTag;
    }

    public void Update(string name)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE tags SET name = @name WHERE id = @id;";

      MySqlParameter nameParam = new MySqlParameter();
      nameParam.ParameterName = "@name";
      nameParam.Value = name;
      cmd.Parameters.Add(nameParam);

      MySqlParameter idParam = new MySqlParameter();
      idParam.ParameterName = "@id";
      idParam.Value = _id;
      cmd.Parameters.Add(idParam);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM tags;";

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM tags WHERE id = @id;";

      MySqlParameter idParam = new MySqlParameter();
      idParam.ParameterName = "@id";
      idParam.Value = _id;
      cmd.Parameters.Add(idParam);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


  }
}
