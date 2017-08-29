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

      Dictionary<string, object> model = new Dictionary<string, object> {};
      model.Add("posts", allPosts);
      model.Add("regions", allRegions);
      model.Add("countries", allCountries);
      model.Add("cities", allCities);

      return View(model);
    }



  }
}
