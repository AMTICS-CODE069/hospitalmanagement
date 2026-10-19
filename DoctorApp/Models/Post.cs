using System.Text.Json.Serialization;

namespace DoctorApp.Models;

public class Post
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = "";
}
