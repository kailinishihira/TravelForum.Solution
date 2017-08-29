using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TravelForum.Models;


namespace TravelForum.Tests
{
  [TestClass]
  public class CityTest : IDisposable
  {
    public CityTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=travel_forum_test;";
    }

    public void Dispose()
    {
      Country.DeleteAll();
      City.DeleteAll();
      Post.DeleteAll();
    }

    [TestMethod]
    public void GetAll_CitiesEmptyAtFirst_0()
    {
      int result = Country.GetAll().Count;
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueForSameCity_True()
    {
      City cityOne = new City("Seattle");
      City cityTwo = new City("Seattle");
      Assert.AreEqual(cityOne, cityTwo);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToCity_Id()
    {
      City testCity = new City("Honolulu");
      testCity.Save();
      City savedCity = City.GetAll()[0];
      int result = savedCity.GetId();
      int testId = testCity.GetId();
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Save_SavesCityToDatabase_CityList()
    {
      City testCity = new City("Mexico City");
      testCity.Save();
      List<City> result = City.GetAll();
      List<City> testList = new List<City>{testCity};
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetAll_ListAllCities_CityList()
    {
      City newCity1 = new City("Vancouver");
      newCity1.Save();
      City newCity2 = new City("Moscow");
      newCity2.Save();
      List<City> allCities = City.GetAll();
      List<City> expectedList = new List<City>{newCity1, newCity2};
      CollectionAssert.AreEqual(allCities, expectedList);
    }

    [TestMethod]
    public void Find_FindsCityInDatabase_City()
    {
      City testCity = new City("Las Vegas");
      testCity.Save();
      City foundCity = City.Find(testCity.GetId());
      Assert.AreEqual(testCity, foundCity);
    }

    [TestMethod]
    public void GetPosts_RetrievesAllPostsForCity_PostList()
    {
      City testCity = new City("Tokyo");
      testCity.Save();
      Post firstPost = new Post("Best foods in Tokyo", "Anonymous", new DateTime (2016, 10, 20), new DateTime (2016, 10, 28), "Sushi, ramen", testCity.GetId());
      firstPost.Save();
      Post secondPost = new Post("Best shopping in Tokyo", "Mary", new DateTime(2016, 12, 05), new DateTime (2016, 12, 20), "Harajuku", testCity.GetId());
      secondPost.Save();
      List<Post> testPostList = new List<Post> {firstPost, secondPost};
      List<Post> resultPostList = testCity.GetPosts();
      CollectionAssert.AreEqual(testPostList, resultPostList);
    }

  }
}
