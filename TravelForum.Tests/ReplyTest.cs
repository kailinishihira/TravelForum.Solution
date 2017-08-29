using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TravelForum.Models;


namespace TravelForum.Tests
{
  [TestClass]
  public class ReplyTest : IDisposable
  {
    public ReplyTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=travel_forum_test;";
    }

    public void Dispose()
    {
     Reply.DeleteAll();
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Reply()
    {
      //Arrange, Act
      Reply firstReply = new Reply("Great Post", "I really liked your post about your travels", 1);
      Reply secondReply = new Reply("Great Post", "I really liked your post about your travels", 1);

      //Assert
      Assert.AreEqual(firstReply, secondReply);
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Reply.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SavesToDatabase_ReplyList()
    {
      //Arrange
      Reply testReply = new Reply("Great Post", "I really liked your post about your travels", 1);

      //Act
      testReply.Save();
      List<Reply> result = Reply.GetAll();
      List<Reply> testList = new List<Reply>{testReply};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Find_FindsReplyInDatabase_Reply()
    {
     //Arrange
     Reply testReply = new Reply("Great Post", "I really liked your post about your travels", 1);
     testReply.Save();

     //Act
     Reply foundReply = Reply.Find(testReply.GetId());

     //Assert
     Assert.AreEqual(testReply, foundReply);
    }

    [TestMethod]
    public void Update_UpdatesReplyNameInDatabase_Reply()
    {
      Reply testReply = new Reply("Great Post", "I really liked your post about your travels", 1);
      testReply.Save();
      int id = testReply.GetId();

      string newName = "I kind of liked your post";
      string newText = "Here is some text about how I liked your post";

      testReply.Update(newName, newText);

      Reply expected = new Reply(newName, newText, 1, id);


      Reply actual = Reply.GetAll()[0];
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Delete_DeletesReplyFromDatabase_ReplyList()
    {
      Reply testReply = new Reply("Great Post", "I really liked your post about your travels", 1);
      testReply.Save();

      Reply testReply2 = new Reply("I kind of liked your post", "Here is some text about how I liked your post", 1);
      testReply2.Save();

      //Act
      testReply.Delete();
      List<Reply> resultReplys = Reply.GetAll();
      List<Reply> testReplyList = new List<Reply> {testReply2};

      //Assert
      CollectionAssert.AreEqual(testReplyList, resultReplys);
    }

    [TestMethod]
    public void AddPost_AddsPostToJoinTable_PostList()
    {
      //Arrange
      Reply testReply = new Reply("Great Post", "I really liked your post about your travels", 1);
      testReply.Save();

      Post testPost1 = new Post("Title", "name", default(DateTime), default(DateTime), "It was not fun");
      testPost1.Save();
      Post testPost2 = new Post("Title1", "name1", default(DateTime), default(DateTime), "It was super fun");
      testPost2.Save();

      //Act
      testBand.AddPostToJoinTable(testPost1);
      testBand.AddPostToJoinTable(testPost2);

      List<Post> result = testBand.GetPosts();
      List<Post> testList = new List<Post>{testPost1, testPost2};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetPosts_ReturnsAllPostBands_PostList()
    {
      //Arrange
      Reply testReply = new Reply("Great Post", "I really liked your post about your travels", 1);
      testReply.Save();

      Post testPost1 = new Post("Title", "name", default(DateTime), default(DateTime), "It was not fun");
      testPost1.Save();
      Post testPost2 = new Post("Title1", "name1", default(DateTime), default(DateTime), "It was super fun");
      testPost2.Save();

      //Act
      testBand.AddPostToJoinTable(testPost1);
      testBand.AddPostToJoinTable(testPost2);

      List<Post> result = testBand.GetPosts();
      List<Post> testList = new List<Post>{testPost1, testPost2};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
  }
}
