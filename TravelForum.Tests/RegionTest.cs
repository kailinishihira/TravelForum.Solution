using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TravelForum.Models;


namespace TravelForum.Tests
{
  [TestClass]
  public class RegionTest : IDisposable
  {
    public RegionTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=travel_forum_test;";
    }

    public void Dispose()
    {
      Region.DeleteAll();
      Country.DeleteAll();
      Post.DeleteAll();
    }

    [TestMethod]
    public void GetAll_RegionsEmptyAtFirst_0()
    {
      int result = Region.GetAll().Count;
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueForSameRegion_True()
    {
      Region regionOne = new Region("Evrop");
      Region regionTwo = new Region("Evrop");
      Assert.AreEqual(regionOne, regionTwo);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToRegion_Id()
    {
      Region testRegion = new Region("East Aza");
      testRegion.Save();
      Region savedRegion = Region.GetAll()[0];
      int result = savedRegion.GetId();
      int testId = testRegion.GetId();
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Save_SavesRegionToDatabase_RegionList()
    {
      Region testRegion = new Region("South Murrica");
      testRegion.Save();
      List<Region> result = Region.GetAll();
      List<Region> testList = new List<Region>{testRegion};
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetAll_ListAllRegions_RegionList()
    {
      Region newRegion1 = new Region("North Murrica");
      newRegion1.Save();
      Region newRegion2 = new Region("Middle Earth");
      newRegion2.Save();
      List<Region> allRegions = Region.GetAll();
      List<Region> expectedList = new List<Region>{newRegion1, newRegion2};
      CollectionAssert.AreEqual(allRegions, expectedList);
    }

    [TestMethod]
    public void Find_FindsRegionInDatabase_Region()
    {
      Region testRegion = new Region("Mononesia");
      testRegion.Save();
      Region foundRegion = Region.Find(testRegion.GetId());
      Assert.AreEqual(testRegion, foundRegion);
    }

    [TestMethod]
    public void GetCountries_RetrievesAllRegionsCountry_CountryList()
    {
      Region testRegion = new Region("East Aza");
      testRegion.Save();
      Country firstCountry = new Country("Nihon", testRegion.GetId());
      firstCountry.Save();
      Country secondCountry = new Country("Korea", testRegion.GetId());
      secondCountry.Save();
      List<Country> testCountryList = new List<Country> {firstCountry, secondCountry};
      List<Country> resultCountryList = testRegion.GetCountries();
      CollectionAssert.AreEqual(testCountryList, resultCountryList);
    }

    [TestMethod]
    public void GetPosts_RetrievesAllPostsForRegion_PostList()
    {
      Region testRegion = new Region("Asia");
      testRegion.Save();
      Post firstPost = new Post("Best foods in Japan", "Anonymous", new DateTime (2016, 10, 20), new DateTime (2016, 10, 28), "Sushi, ramen...", 0, 0, testRegion.GetId());
      firstPost.Save();
      Post secondPost = new Post("Best shopping in Korea", "Mary", new DateTime(2016, 12, 05), new DateTime (2016, 12, 20), "Seoul...", 0, 0, testRegion.GetId());
      secondPost.Save();
      List<Post> testPostList = new List<Post> {firstPost, secondPost};
      List<Post> resultPostList = testRegion.GetPosts();
      CollectionAssert.AreEqual(testPostList, resultPostList);
    }

  }
}
