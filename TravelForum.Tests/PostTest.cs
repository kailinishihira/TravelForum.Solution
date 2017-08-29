using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TravelForum.Models;

namespace TravelForum.Tests
{
  [TestClass]
  public class PostTest : IDisposable
  {
    public PostTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=travel_forum_test;";
    }

    public void Dispose()
    {
      Post.DeleteAll();
    }
    [TestMethod]
    public void Equals_ReturnsTrueForSamePost_Post()
    {
      Post newPost = new Post("Title", "name", default(DateTime), default(DateTime), "It was fun");
      Post newPost2 = new Post("Title", "name", default(DateTime), default(DateTime), "It was fun");

      Assert.AreEqual(newPost, newPost2);
    }
    [TestMethod]
    public void Save_SavesNewPostToDatabase_Post()
    {
      Post newPost = new Post("Title", "name", default(DateTime), default(DateTime), "It was fun");
      newPost.Save();

      var expected = newPost;
      var actual = Post.GetAll()[0];

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      Post newPost = new Post("Title", "name", default(DateTime), default(DateTime), "It was fun");
      newPost.Save();

      int expected = newPost.GetId();
      int actual = Post.GetAll()[0].GetId();

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Find_GetsPostById_Post()
    {
      Post newPost = new Post("Title", "name", default(DateTime), default(DateTime), "It was fun");
      newPost.Save();

      var expected = newPost;
      var actual = Post.Find(newPost.GetId());

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Delete_DeletesAPost_List()
    {
      Post newPost = new Post("Title", "name", default(DateTime), default(DateTime), "It was fun");
      newPost.Save();

      Post newPost2 = new Post("Title2", "name2", default(DateTime), default(DateTime), "It was not fun");
      newPost2.Save();

      newPost.Delete();

      var expected = new List<Post>{newPost2};
      var actual = Post.GetAll();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Update_UpdatesPostObject_Post()
    {
      Post newPost = new Post("Title", "name", default(DateTime), default(DateTime), "It was fun");
      newPost.Save();
      int id = newPost.GetId();
      Console.WriteLine("Expected id =============" + id.ToString());

      newPost.Update("Title2", "name2", default(DateTime), default(DateTime), "It was not fun", 2, 2, 2);

      var expected = new Post("Title2", "name2", default(DateTime), default(DateTime), "It was not fun", 2, 2, 2, id);

      Console.WriteLine("Actual id =============" + Post.GetAll()[0].GetId().ToString());
      var actual = Post.GetAll()[0];

      Assert.AreEqual(expected, actual);
    }

    // [TestMethod]
    // public void AddTag_AddsTagToPost_Post()
    // {
    //
    // }
  }
}
