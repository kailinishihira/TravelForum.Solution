using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TravelForum.Models;


namespace TravelForum.Tests
{
  [TestClass]
  public class CountryTest : IDisposable
  {
    public CountryTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=travel_forum_test;";
    }

    public void Dispose()
    {
      Country.DeleteAll();
      Region.DeleteAll();
      City.DeleteAll();
      Post.DeleteAll();
    }

    [TestMethod]
    public void GetAll_CountriesEmptyAtFirst_0()
    {
      int result = Country.GetAll().Count;
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueForSameCountry_True()
    {
      Country countryOne = new Country("Iceland");
      Country countryTwo = new Country("Iceland");
      Assert.AreEqual(countryOne, countryTwo);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToCountry_Id()
    {
      Country testCountry = new Country("China");
      testCountry.Save();
      Country savedCountry = Country.GetAll()[0];
      int result = savedCountry.GetId();
      int testId = testCountry.GetId();
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Save_SavesCountryToDatabase_CountryList()
    {
      Country testCountry = new Country("Mejico");
      testCountry.Save();
      List<Country> result = Country.GetAll();
      List<Country> testList = new List<Country>{testCountry};
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetAll_ListAllCountries_CountryList()
    {
      Country newCountry1 = new Country("Canadia");
      newCountry1.Save();
      Country newCountry2 = new Country("Rusha");
      newCountry2.Save();
      List<Country> allCountries = Country.GetAll();
      List<Country> expectedList = new List<Country>{newCountry1, newCountry2};
      CollectionAssert.AreEqual(allCountries, expectedList);
    }

    [TestMethod]
    public void Find_FindsCountryInDatabase_Country()
    {
      Country testCountry = new Country("Bruhzil");
      testCountry.Save();
      Country foundCountry = Country.Find(testCountry.GetId());
      Assert.AreEqual(testCountry, foundCountry);
    }

    [TestMethod]
    public void GetCities_RetrievesAllCitiesInCountry_CityList()
    {
      Country testCountry = new Country("Japan");
      testCountry.Save();
      City firstCity = new City("Tokyo", testCountry.GetId());
      firstCity.Save();
      City secondCity = new City("Yokohama", testCountry.GetId());
      secondCity.Save();
      List<City> testCityList = new List<City> {firstCity, secondCity};
      List<City> resultCityList = testCountry.GetCities();
      CollectionAssert.AreEqual(testCityList, resultCityList);
    }

    [TestMethod]
    public void GetPosts_RetrievesAllPostsForCountry_PostList()
    {
      Country testCountry = new Country("Japan");
      testCountry.Save();
      Post firstPost = new Post("Best foods in Tokyo", "Anonymous", new DateTime (2016, 10, 20), new DateTime (2016, 10, 28), "Sushi, ramen", 0, testCountry.GetId());
      firstPost.Save();
      Post secondPost = new Post("Best shopping in Tokyo", "Mary", new DateTime(2016, 12, 05), new DateTime (2016, 12, 20), "Harajuku", 0, testCountry.GetId());
      secondPost.Save();
      List<Post> testPostList = new List<Post> {firstPost, secondPost};
      List<Post> resultPostList = testCountry.GetPosts();
      CollectionAssert.AreEqual(testPostList, resultPostList);
    }

  }
}
