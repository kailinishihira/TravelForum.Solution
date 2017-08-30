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
    private string _text;

    public Post(string title, string name, DateTime startDate, DateTime endDate, string text, int id=0)
    {
      _id= id;
      _title = title;
      _name = name;
      _startDate = startDate;
      _endDate = endDate;
      _text = text;
    }


    public int GetId()
    {
      return _id;
    }
    public string GetTitle()
    {
      return _title;
    }
    public string GetName()
    {
      return _name;
    }
    public DateTime GetStartDate()
    {
      return _startDate;
    }
    public DateTime GetEndDate()
    {
      return _endDate;
    }
    public string GetText()
    {
      return _text;
    }
    // public int GetCityId()
    // {
    //   return _cityId;
    // }
    // public int GetCountryId()
    // {
    //   return _countryId;
    // }
    // public int GetRegionId()
    // {
    //   return _regionId;
    // }

    public override bool Equals(object otherPost)
    {
      if (! (otherPost is Post))
      {
        return false;
      }
      else
      {
        Post newPost = (Post) otherPost;
        bool idEquality = newPost.GetId() == this._id;
        bool titleEquality = newPost.GetTitle() == this._title;
        bool nameEquality = newPost.GetName() == this._name;
        bool startDateEquality = newPost.GetStartDate() == this._startDate;
        bool endDateEquality = newPost.GetEndDate() == this._endDate;
        bool textEquality = newPost.GetText() == this._text;
        return (idEquality && titleEquality && nameEquality && startDateEquality && endDateEquality && textEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetTitle().GetHashCode();
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO posts (title, name, start_date, end_date, text) VALUES (@title, @name, @startDate, @endDate, @text);";

      MySqlParameter titleParam = new MySqlParameter();
      titleParam.ParameterName = "@title";
      titleParam.Value = _title;
      cmd.Parameters.Add(titleParam);

      MySqlParameter nameParam = new MySqlParameter();
      nameParam.ParameterName = "@name";
      nameParam.Value = _name;
      cmd.Parameters.Add(nameParam);

      MySqlParameter startDateParam = new MySqlParameter();
      startDateParam.ParameterName = "@startDate";
      startDateParam.Value = _startDate;
      cmd.Parameters.Add(startDateParam);

      MySqlParameter endDateParam = new MySqlParameter();
      endDateParam.ParameterName = "@endDate";
      endDateParam.Value = _endDate;
      cmd.Parameters.Add(endDateParam);

      MySqlParameter textParam = new MySqlParameter();
      textParam.ParameterName = "@text";
      textParam.Value = _text;
      cmd.Parameters.Add(textParam);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Post> GetAll()
    {
      var allPosts = new List<Post>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM posts;";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string title = rdr.GetString(1);
        string name = rdr.GetString(2);
        DateTime startDate = rdr.GetDateTime(3);
        DateTime endDate = rdr.GetDateTime(4);
        string text = rdr.GetString(5);
        Post newPost = new Post(title, name, startDate, endDate, text, id);
        allPosts.Add(newPost);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allPosts;
    }

    public static Post Find(int searchId)
    {
      var foundPost = default(Post);
      int id = 0;
      string title = "";
      string name = "";
      DateTime startDate = default(DateTime);
      DateTime endDate = default(DateTime);
      string text = "";

      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM posts WHERE id = @id;";

      MySqlParameter idParam = new MySqlParameter();
      idParam.ParameterName = "@id";
      idParam.Value = searchId;
      cmd.Parameters.Add(idParam);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        title = rdr.GetString(1);
        name = rdr.GetString(2);
        startDate = rdr.GetDateTime(3);
        endDate = rdr.GetDateTime(4);
        text = rdr.GetString(5);
        foundPost = new Post(title, name, startDate, endDate, text, id);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundPost;
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM posts;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Update(string title, string name, DateTime startDate, DateTime endDate, string text, int cityId, int countryId, int regionId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE posts SET title = @title, name = @name, start_date = @startDate, end_date = @endDate, text = @text, city_id = @cityId, country_id = @countryId, region_id = @regionId WHERE id = @id;";

      MySqlParameter titleParam = new MySqlParameter();
      titleParam.ParameterName = "@title";
      titleParam.Value = title;
      cmd.Parameters.Add(titleParam);

      MySqlParameter nameParam = new MySqlParameter();
      nameParam.ParameterName = "@name";
      nameParam.Value = name;
      cmd.Parameters.Add(nameParam);

      MySqlParameter startDateParam = new MySqlParameter();
      startDateParam.ParameterName = "@startDate";
      startDateParam.Value = startDate;
      cmd.Parameters.Add(startDateParam);

      MySqlParameter endDateParam = new MySqlParameter();
      endDateParam.ParameterName = "@endDate";
      endDateParam.Value = endDate;
      cmd.Parameters.Add(endDateParam);

      MySqlParameter textParam = new MySqlParameter();
      textParam.ParameterName = "@text";
      textParam.Value = text;
      cmd.Parameters.Add(textParam);

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

    public void AddTag(int tagId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO posts_tags (post_id, tag_id) VALUES (@postId, @tagId);";

      MySqlParameter postIdParam = new MySqlParameter();
      postIdParam.ParameterName = "@postId";
      postIdParam.Value = _id;
      cmd.Parameters.Add(postIdParam);

      MySqlParameter tagIdParam = new MySqlParameter();
      tagIdParam.ParameterName = "@tagId";
      tagIdParam.Value = tagId;
      cmd.Parameters.Add(tagIdParam);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Tag> GetTags()
    {
      List<Tag> allTags = new List<Tag>{};

      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT tags.*
      FROM posts
      JOIN posts_tags ON (posts.id = posts_tags.post_id)
      JOIN tags ON (tags.id = posts_tags.tag_id)
      WHERE posts.id = @postId;";

      MySqlParameter postIdParam = new MySqlParameter();
      postIdParam.ParameterName = "@postId";
      postIdParam.Value = _id;
      cmd.Parameters.Add(postIdParam);

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

    public void AddCity(int cityId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cities_posts(city_id, post_id) VALUES (@cityId, @postId);";

      MySqlParameter cityIdParam = new MySqlParameter();
      cityIdParam.ParameterName = "@cityId";
      cityIdParam.Value = cityId;
      cmd.Parameters.Add(cityIdParam);

      MySqlParameter postIdParam = new MySqlParameter();
      postIdParam.ParameterName = "@postId";
      postIdParam.Value = this._id;
      cmd.Parameters.Add(postIdParam);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<City> GetCities()
    {
      List<City> cities = new List<City>{};

      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT cities.* FROM posts JOIN cities_posts ON (posts.id = cities_posts.post_id) JOIN cities ON (cities.id = cities_posts.city_id) WHERE posts.id = @postId;";

      MySqlParameter postIdParam = new MySqlParameter();
      postIdParam.ParameterName = "@postId";
      postIdParam.Value = _id;
      cmd.Parameters.Add(postIdParam);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int countryId = rdr.GetInt32(2);

        City newCity = new City(name, countryId, id);
        cities.Add(newCity);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return cities;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM posts WHERE id = @id;";

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
