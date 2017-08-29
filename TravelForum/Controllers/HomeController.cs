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
      return View();
    }

    [HttpGet("/post/{postId}/region/{regionId}/country/{countryId}/city/{cityId}")]
    public ActionResult PostDetails(int postId, int regionId, int countryId, int cityId)
    {
      var model = new Dictionary<string, object> {};
      Post post = Post.Find(postId);
      List<Reply> replyList = Reply.GetRepliesByPostId(postId);
      Region region = Region.Find(regionId);
      Country country = Country.Find(countryId);
      City city = City.Find(cityId);
      model.Add("post", post);
      model.Add("replyList", replyList);
      model.Add("region", region);
      model.Add("country", country);
      model.Add("city", city);

      return View(model);
    }

    [HttpPost("/post/{postId}/region/{regionId}/country/{countryId}/city/{cityId}")]
    public ActionResult PostThePostDetails(int postId, int regionId, int countryId, int cityId)
    {

      string replyName = Request.Form["reply-name"];
      string replyText = Request.Form["reply-text"];
      Reply newReply = new Reply(replyName, replyText, postId);
      newReply.Save();

      var model = new Dictionary<string, object> {};
      Post post = Post.Find(postId);
      List<Reply> replyList = Reply.GetRepliesByPostId(postId);
      Region region = Region.Find(regionId);
      Country country = Country.Find(countryId);
      City city = City.Find(cityId);
      model.Add("post", post);
      model.Add("replyList", replyList);
      model.Add("region", region);
      model.Add("country", country);
      model.Add("city", city);

      return View("PostDetails", model);
    }

  }
}
