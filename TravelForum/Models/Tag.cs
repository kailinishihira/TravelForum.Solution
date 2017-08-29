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

    public void AddPost(int postId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO posts_tags(post_id, tag_id) VALUES (@postId, @tagId);";

      MySqlParameter postIdParam = new MySqlParameter();
      postIdParam.ParameterName = "@postId";
      postIdParam.Value = postId;
      cmd.Parameters.Add(postIdParam);

      MySqlParameter tagIdParam = new MySqlParameter();
      tagIdParam.ParameterName = "@tagId";
      tagIdParam.Value = _id;
      cmd.Parameters.Add(tagIdParam);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Post> GetPosts()
    {
      List<Post> allPosts = new List<Post>{};

      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT posts.*
      FROM tags
      JOIN posts_tags ON (tags.id = posts_tags.tag_id)
      JOIN posts ON (posts.id = posts_tags.post_id)
      WHERE tags.id = @tagId;";

      MySqlParameter tagIdParam = new MySqlParameter();
      tagIdParam.ParameterName = "@tagId";
      tagIdParam.Value = _id;
      cmd.Parameters.Add(tagIdParam);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string title = rdr.GetString(1);
        string name = rdr.GetString(2);
        DateTime startDate = rdr.GetDateTime(3);
        DateTime endDate = rdr.GetDateTime(4);
        string text = rdr.GetString(5);
        int cityId = rdr.GetInt32(6);
        int countryId = rdr.GetInt32(7);
        int regionId = rdr.GetInt32(8);
        Post newPost = new Post(title, name, startDate, endDate, text, cityId, countryId, regionId, id);
        allPosts.Add(newPost);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allPosts;

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
