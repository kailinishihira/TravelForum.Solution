using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using TravelForum.Models;

namespace TravelForum.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Post> allPosts = Post.GetAll();
      List<Region> allRegions = Region.GetAll();
      List<Country> allCountries = Country.GetAll();
      List<City> allCities = City.GetAll();
      List<Tag> allTags = Tag.GetAll();

      Dictionary<string, object> model = new Dictionary<string, object> {};
      model.Add("posts", allPosts);
      model.Add("regions", allRegions);
      model.Add("countries", allCountries);
      model.Add("cities", allCities);
      model.Add("tags", allTags);

      return View(model);
    }

    [HttpPost("/posts/by-region")]
    public ActionResult RegionPosts()
    {
      Region thisRegion = Region.Find(Int32.Parse(Request.Form["region-id"]));
      List<Post> regionPosts = thisRegion.GetPosts();
      List<Region> allRegions = Region.GetAll();
      List<Country> allCountries = Country.GetAll();
      List<City> allCities = City.GetAll();
      List<Tag> allTags = Tag.GetAll();

      Dictionary<string, object> model = new Dictionary<string, object> {};
      model.Add("posts", regionPosts);
      model.Add("regions", allRegions);
      model.Add("countries", allCountries);
      model.Add("cities", allCities);
      model.Add("tags", allTags);

      return View("Index", model);
    }

    [HttpPost("/posts/by-country")]
    public ActionResult CountryPosts()
    {
      Country thisCountry = Country.Find(Int32.Parse(Request.Form["country-id"]));
      List<Post> countryPosts = thisCountry.GetPosts();
      List<Region> allRegions = Region.GetAll();
      List<Country> allCountries = Country.GetAll();
      List<City> allCities = City.GetAll();
      List<Tag> allTags = Tag.GetAll();

      Dictionary<string, object> model = new Dictionary<string, object> {};
      model.Add("posts", countryPosts);
      model.Add("regions", allRegions);
      model.Add("countries", allCountries);
      model.Add("cities", allCities);
      model.Add("tags", allTags);

      return View("Index", model);
    }

    [HttpPost("/posts/by-city")]
    public ActionResult CityPosts()
    {
      City thisCity = City.Find(Int32.Parse(Request.Form["city-id"]));
      List<Post> cityPosts = thisCity.GetPosts();
      List<Region> allRegions = Region.GetAll();
      List<Country> allCountries = Country.GetAll();
      List<City> allCities = City.GetAll();
      List<Tag> allTags = Tag.GetAll();

      Dictionary<string, object> model = new Dictionary<string, object> {};
      model.Add("posts", cityPosts);
      model.Add("regions", allRegions);
      model.Add("countries", allCountries);
      model.Add("cities", allCities);
      model.Add("tags", allTags);

      return View("Index", model);
    }

    [HttpPost("/posts/by-tag")]
    public ActionResult TagPosts()
    {
      Tag thisTag = Tag.Find(Int32.Parse(Request.Form["tag-id"]));
      List<Post> tagPosts = thisTag.GetPosts();
      List<Region> allRegions = Region.GetAll();
      List<Country> allCountries = Country.GetAll();
      List<City> allCities = City.GetAll();
      List<Tag> allTags = Tag.GetAll();

      Dictionary<string, object> model = new Dictionary<string, object> {};
      model.Add("posts", tagPosts);
      model.Add("regions", allRegions);
      model.Add("countries", allCountries);
      model.Add("cities", allCities);
      model.Add("tags", allTags);

      return View("Index", model);
    }

    [HttpGet("/post/form")]
    public ActionResult PostForm()
    {
      Dictionary<string, object> model = new Dictionary<string, object> {};

      List<Region> allRegions = Region.GetAll();
      model.Add("regions", allRegions);
      List<Country> allCountries = Country.GetAll();
      model.Add("countries", allCountries);
      List<City> allCities = City.GetAll();
      model.Add("cities", allCities);

      return View(model);
    }

    [HttpPost("/form/summary")]
    public ActionResult PostSummary()
    {
      var model = new Dictionary<string,object>{};
      string title = Request.Form["title"];
      string name = Request.Form["name"];
      DateTime start = DateTime.Parse(Request.Form["start-date"]);
      DateTime end = DateTime.Parse(Request.Form["end-date"]);
      string text = Request.Form["text"];
      int cityId = int.Parse(Request.Form["city"]);
      int countryId = int.Parse(Request.Form["country"]);
      int regionId = int.Parse(Request.Form["region"]);

      var cities = City.Find(cityId);
      var countries = Country.Find(countryId);
      var regions = Region.Find(regionId);
      var newPost = new Post(title, name, start, end, text, cityId, countryId, regionId);


      model.Add("cities", cities);
      model.Add("countries", countries);
      model.Add("regions", regions);
      model.Add("post",  newPost);
      return View(model);
    }

    [HttpPost("/form/summary/create")]
    public ActionResult CreatePost()
    {
      var model = new Dictionary<string,object>{};
      string title = Request.Form["title"];
      string name = Request.Form["name"];
      DateTime start = DateTime.Parse(Request.Form["start-date"]);
      DateTime end = DateTime.Parse(Request.Form["end-date"]);
      string text = Request.Form["text"];
      int cityId = int.Parse(Request.Form["city"]);
      int countryId = int.Parse(Request.Form["country"]);
      int regionId = int.Parse(Request.Form["region"]);

      var cities = City.Find(cityId);
      var countries = Country.Find(countryId);
      var regions = Region.Find(regionId);
      var newPost = new Post(title, name, start, end, text, cityId, countryId, regionId);
      newPost.Save();

      model.Add("cities", cities);
      model.Add("countries", countries);
      model.Add("regions", regions);
      model.Add("post",  newPost);
      return View("PostDetails", model);
    }

    [HttpGet("/update/{postId}")]
    public ActionResult UpdatePost(int postId)
    {
      var model = new Dictionary<string,object>{};
      string title = Request.Form["title"];
      string name = Request.Form["name"];
      DateTime start = DateTime.Parse(Request.Form["start-date"]);
      DateTime end = DateTime.Parse(Request.Form["end-date"]);
      string text = Request.Form["text"];
      int cityId = int.Parse(Request.Form["city"]);
      int countryId = int.Parse(Request.Form["country"]);
      int regionId = int.Parse(Request.Form["region"]);

      var postToUpdate = Post.Find(postId);
      var cities = City.Find(cityId);
      var countries = Country.Find(countryId);
      var regions = Region.Find(regionId);

      postToUpdate.Update(title, name, start, end, text, cityId, countryId, regionId);
      model.Add("cities", cities);
      model.Add("countries", countries);
      model.Add("regions", regions);
      model.Add("post",  postToUpdate);

      return View("PostSummary", model);
    }
  }
}
