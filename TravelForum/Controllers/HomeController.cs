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


  }
}
