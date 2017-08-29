using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TravelForum.Models;


namespace TravelForum.Tests
{
  [TestClass]
  public class TagTest : IDisposable
  {
    public TagTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=travel_forum_test;";
    }
    public void Dispose()
    {
      Tag.DeleteAll();
    }
    [TestMethod]
    public void Equals_ReturnsTrueForSameTag_Tag()
    {
      Tag newTag = new Tag("name");
      Tag newTag2 = new Tag("name");

      Assert.AreEqual(newTag, newTag2);
    }
    [TestMethod]
    public void Save_SavesNewTagToDatabase_Tag()
    {
      Tag newTag = new Tag("name");
      newTag.Save();

      var expected = newTag;
      var actual = Tag.GetAll()[0];

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Save_AssignsIdToTagObject_Id()
    {
      Tag newTag = new Tag("name");
      newTag.Save();

      int expected = newTag.GetId();
      int actual = Tag.GetAll()[0].GetId();

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Delete_DeletesTagObject_Tag()
    {
      Tag newTag1 = new Tag("name1");
      newTag1.Save();
      Tag newTag2 = new Tag("name2");
      newTag2.Save();

      newTag2.Delete();
      var expected = new List<Tag>{newTag1};
      var actual = Tag.GetAll();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Find_FindTagObject_Tag()
    {
      Tag newTag1 = new Tag("name1");
      newTag1.Save();
      Tag newTag2 = new Tag("name2");
      newTag2.Save();
      int id = newTag1.GetId();

      var expected = newTag1;
      var actual = Tag.Find(id);

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Update_UpdatesTagObject_Tag()
    {
      Tag newTag1 = new Tag("name1");
      newTag1.Save();
      int id = newTag1.GetId();
      newTag1.Update("name2");

      var expected = new Tag("name2", id);
      var actual = Tag.GetAll()[0];

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AddPost_AddsPostToATag_Post()
    {
      Post newPost = new Post("title", "name", default(DateTime), default(DateTime), "text", 1,1,1);
      newPost.Save();

      Tag newTag = new Tag("tagName");
      newTag.Save();
      newTag.AddPost(newPost.GetId());

      int expected = newPost.GetId();
      int actual = newTag.GetPosts()[0].GetId();

      Assert.AreEqual(expected, actual);

    }
  }
}
