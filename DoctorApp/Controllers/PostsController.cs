using System.Text.Json;
using DoctorApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoctorApp.Controllers;

public class PostsController : Controller
{
    private readonly IHttpClientFactory _factory;

    public PostsController(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<IActionResult> Index()
    {
        var client = _factory.CreateClient();
        var json = await client.GetStringAsync("https://jsonplaceholder.typicode.com/posts");
        var posts = JsonSerializer.Deserialize<List<Post>>(json) ?? new List<Post>();
        return View(posts);
    }
}
