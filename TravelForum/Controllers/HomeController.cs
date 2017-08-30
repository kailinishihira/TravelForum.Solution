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
      Region thisRegion = new Region("All");
      List<Post> allPosts = Post.GetAll();
      List<Region> allRegions = Region.GetAll();
      List<Country> allCountries = Country.GetAll();
      List<City> allCities = City.GetAll();
      List<Tag> allTags = Tag.GetAll();

      Dictionary<string, object> model = new Dictionary<string, object> {};
      model.Add("searchby", thisRegion);
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
      model.Add("searchby", thisRegion);
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
      model.Add("searchby", thisCountry);
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
      model.Add("searchby", thisCity);
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
      model.Add("searchby", thisTag);
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
      List<Tag> allTags = Tag.GetAll();
      model.Add("tags", allTags);

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
      int tagId = int.Parse(Request.Form["tag"]);

      City city = City.Find(cityId);
      Country country = Country.Find(countryId);
      Region region = Region.Find(regionId);
      Post newPost = new Post(title, name, start, end, text, cityId, countryId, regionId);
      newPost.Save();
      newPost.AddTag(tagId);
      List<Tag> postTags = newPost.GetTags();

      model.Add("city", city);
      model.Add("country", country);
      model.Add("region", region);
      model.Add("post",  newPost);
      model.Add("tags", postTags);
      model.Add("tagId", tagId);
      return View(model);
    }

    [HttpGet("/post/{postId}/region/{regionId}/country/{countryId}/city/{cityId}/view-post")]
    public ActionResult ViewPost(int postId, int regionId, int countryId, int cityId)
    {
      var model = new Dictionary<string, object> {};
      Post post = Post.Find(postId);
      List<Reply> replyList = Reply.GetRepliesByPostId(postId);
      Region region = Region.Find(regionId);
      Country country = Country.Find(countryId);
      City city = City.Find(cityId);
      List<Tag> allTags = Tag.GetAll();
      List<Tag> getTags = post.GetTags();
      model.Add("post", post);
      model.Add("replyList", replyList);
      model.Add("region", region);
      model.Add("country", country);
      model.Add("city", city);
      model.Add("allTags", allTags);
      model.Add("postTags", getTags);

      return View("PostDetails", model);
    }

    [HttpGet("/update/{postId}")]
    public ActionResult UpdatePost(int postId)
    {
      Post postToUpdate = Post.Find(postId);
      var model = new Dictionary<string,object>{};
      City city = City.Find(postToUpdate.GetCityId());
      Country country = Country.Find(postToUpdate.GetCountryId());
      Region region = Region.Find(postToUpdate.GetRegionId());
      List<City> allCities = City.GetAll();
      List<Country> allCountries = Country.GetAll();
      List<Region> allRegions = Region.GetAll();
      List<Reply> replyList = Reply.GetRepliesByPostId(postId);
      List<Tag> allTags = Tag.GetAll();
      List<Tag> getTags = postToUpdate.GetTags();
      model.Add("city", city);
      model.Add("country", country);
      model.Add("region", region);
      model.Add("allcities", allCities);
      model.Add("allcountries", allCountries);
      model.Add("allregions", allRegions);
      model.Add("post",  postToUpdate);
      model.Add("replyList", replyList);
      model.Add("allTags", allTags);
      model.Add("postTags", getTags);

      return View(model);
    }

    [HttpGet("/update-post/{postId}/region/{regionId}/country/{countryId}/city/{cityId}")]
    public ActionResult UpdatePostFromDetails(int postId, int regionId, int countryId, int cityId)
    {

      var model = new Dictionary<string, object> {};
      Post post = Post.Find(postId);
      Region region = Region.Find(regionId);
      Country country = Country.Find(countryId);
      City city = City.Find(cityId);
      List<City> allCities = City.GetAll();
      List<Country> allCountries = Country.GetAll();
      List<Region> allRegions = Region.GetAll();
      List<Tag> allTags = Tag.GetAll();
      model.Add("startDate", post.GetStartDate());
      model.Add("post", post);
      model.Add("region", region);
      model.Add("country", country);
      model.Add("city", city);
      model.Add("allcities", allCities);
      model.Add("allcountries", allCountries);
      model.Add("allregions", allRegions);
      model.Add("allTags", allTags);

      return View(model);
    }

    [HttpPost("/post/{postId}/region/{regionId}/country/{countryId}/city/{cityId}/updated")]
    public ActionResult FormUpdated(int postId, int regionId, int countryId, int cityId)
    {
      Post postToUpdate = Post.Find(postId);
      string title = Request.Form["title"];
      string name = Request.Form["name"];
      DateTime start = DateTime.Parse(Request.Form["start-date"]);
      DateTime end = DateTime.Parse(Request.Form["end-date"]);
      string text = Request.Form["text"];
      int cityIdForm = int.Parse(Request.Form["city"]);
      int countryIdForm = int.Parse(Request.Form["country"]);
      int regionIdForm = int.Parse(Request.Form["region"]);
      int tagId = int.Parse(Request.Form["tag"]);
      postToUpdate.AddTag(tagId);
      List<Tag> getTags = postToUpdate.GetTags();

      postToUpdate.Update(title, name, start, end, text, cityIdForm, countryIdForm, regionIdForm);

      var model = new Dictionary<string, object> {};
      Post post = Post.Find(postId);
      List<Reply> replyList = Reply.GetRepliesByPostId(postId);
      List<Tag> allTags = Tag.GetAll();
      Region region = Region.Find(regionId);
      Country country = Country.Find(countryId);
      City city = City.Find(cityId);
      model.Add("post", post);
      model.Add("replyList", replyList);
      model.Add("region", Region.Find(post.GetRegionId()));
      model.Add("country", Country.Find(post.GetCountryId()));
      model.Add("city", City.Find(post.GetCityId()));
      model.Add("allTags", allTags);
      model.Add("postTags", getTags);

      return View("PostDetails", model);
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
      List<Tag> allTags = Tag.GetAll();
      List<Tag> getTags = post.GetTags();
      model.Add("allTags", allTags);
      model.Add("post", post);
      model.Add("replyList", replyList);
      model.Add("region", region);
      model.Add("country", country);
      model.Add("city", city);
      model.Add("postTags", getTags);

      return View(model);
    }

    [HttpPost("/post/{postId}/region/{regionId}/country/{countryId}/city/{cityId}/add-tags")]
    public ActionResult PostNewTagsToPostDetail(int postId, int regionId, int countryId, int cityId)
    {
      var model = new Dictionary<string, object> {};
      Post post = Post.Find(postId);
      int tagId = int.Parse(Request.Form["tags"]);
      post.AddTag(tagId);
      List<Tag> getTags = post.GetTags();
      List<Reply> replyList = Reply.GetRepliesByPostId(postId);
      Region region = Region.Find(regionId);
      Country country = Country.Find(countryId);
      City city = City.Find(cityId);
      List<Tag> allTags = Tag.GetAll();
      model.Add("allTags", allTags);
      model.Add("post", post);
      model.Add("replyList", replyList);
      model.Add("region", region);
      model.Add("country", country);
      model.Add("city", city);
      model.Add("postTags", getTags);

      return View("PostDetails", model);
    }

    [HttpGet("/post/{postId}/region/{regionId}/country/{countryId}/city/{cityId}/tag/{tagId}/delete-tag")]
    public ActionResult DeleteTag(int postId, int regionId, int countryId, int cityId, int tagId)
    {
      var model = new Dictionary<string, object> {};
      Post post = Post.Find(postId);
      List<Reply> replyList = Reply.GetRepliesByPostId(postId);
      Region region = Region.Find(regionId);
      Country country = Country.Find(countryId);
      City city = City.Find(cityId);
      List<Tag> allTags = Tag.GetAll();
      List<Tag> getTags = post.GetTags();
      Tag thisTag = Tag.Find(tagId);
      post.DeleteTag(thisTag);
      model.Add("allTags", allTags);
      model.Add("post", post);
      model.Add("replyList", replyList);
      model.Add("region", region);
      model.Add("country", country);
      model.Add("city", city);
      model.Add("postTags", getTags);

      return View("PostDetails", model);

    }




    [HttpPost("/post/{postId}/region/{regionId}/country/{countryId}/city/{cityId}")]
    public ActionResult PostReplyToPostDetails(int postId, int regionId, int countryId, int cityId)
    {

      string replyName = Request.Form["reply-name"];
      string replyText = Request.Form["reply-text"];
      Reply newReply = new Reply(replyName, replyText, postId);
      newReply.Save();

      var model = new Dictionary<string, object> {};
      Post post = Post.Find(postId);
      List<Tag> getTags = post.GetTags();
      List<Reply> replyList = Reply.GetRepliesByPostId(postId);
      Region region = Region.Find(regionId);
      Country country = Country.Find(countryId);
      City city = City.Find(cityId);
      List<Tag> allTags = Tag.GetAll();
      model.Add("allTags", allTags);
      model.Add("post", post);
      model.Add("replyList", replyList);
      model.Add("region", region);
      model.Add("country", country);
      model.Add("city", city);
      model.Add("postTags", getTags);

      return View("PostDetails", model);
    }

    [HttpGet("/update-reply/{postId}/region/{regionId}/country/{countryId}/city/{cityId}/reply/{replyId}")]
    public ActionResult UpdateReplyFromDetails(int postId, int regionId, int countryId, int cityId, int replyId)
    {

      var model = new Dictionary<string, object> {};
      Post post = Post.Find(postId);
      Region region = Region.Find(regionId);
      Country country = Country.Find(countryId);
      City city = City.Find(cityId);
      Reply reply = Reply.Find(replyId);
      List<City> allCities = City.GetAll();
      List<Country> allCountries = Country.GetAll();
      List<Region> allRegions = Region.GetAll();

      model.Add("post", post);
      model.Add("region", region);
      model.Add("country", country);
      model.Add("city", city);
      model.Add("reply", reply);
      model.Add("allcities", allCities);
      model.Add("allcountries", allCountries);
      model.Add("allregions", allRegions);

      return View(model);
    }

    [HttpPost("/post/{postId}/region/{regionId}/country/{countryId}/city/{cityId}/reply/{replyId}/updated")]
    public ActionResult ReplyFormUpdated(int postId, int regionId, int countryId, int cityId, int replyId)
    {
      Reply replyToUpdate = Reply.Find(replyId);
      string name = Request.Form["name"];
      string text = Request.Form["text"];

      replyToUpdate.Update(name, text);


      var model = new Dictionary<string, object> {};
      Post post = Post.Find(postId);
      List<Reply> replyList = Reply.GetRepliesByPostId(postId);
      Region region = Region.Find(regionId);
      Country country = Country.Find(countryId);
      City city = City.Find(cityId);
      model.Add("post", post);
      model.Add("replyList", replyList);
      model.Add("region", Region.Find(post.GetRegionId()));
      model.Add("country", Country.Find(post.GetCountryId()));
      model.Add("city", City.Find(post.GetCityId()));

      return View("PostDetails", model);
    }

    [HttpGet("/delete/{id}")]
    public ActionResult DeletePost(int id)
    {
      Post postToDelete = Post.Find(id);
      postToDelete.Delete();
      return View(postToDelete);
    }

    [HttpGet("/post/{postId}/region/{regionId}/country/{countryId}/city/{cityId}/reply/{replyId}/deleted-reply")]
    public ActionResult DeleteReply(int postId, int regionId, int countryId, int cityId, int replyId)
    {
      Reply replyToDelete = Reply.Find(replyId);
      replyToDelete.Delete();

      var model = new Dictionary<string, object> {};
      Post post = Post.Find(postId);
      Region region = Region.Find(regionId);
      Country country = Country.Find(countryId);
      City city = City.Find(cityId);
      List<Reply> replyList = Reply.GetRepliesByPostId(postId);
      List<City> allCities = City.GetAll();
      List<Country> allCountries = Country.GetAll();
      List<Region> allRegions = Region.GetAll();

      model.Add("post", post);
      model.Add("region", region);
      model.Add("country", country);
      model.Add("city", city);
      model.Add("replyList", replyList);
      model.Add("allcities", allCities);
      model.Add("allcountries", allCountries);
      model.Add("allregions", allRegions);

      return View("PostDetails",model);
    }
  }
}
