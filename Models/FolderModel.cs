using System.Text.Json.Serialization;

public class Folder {
    public int Id { get; set; }
    public string? Name { get; set; }
    public int ParentFolderId { get; set; }
    [JsonIgnore]
    public Folder? ParentFolder { get; set; }
    public int UserId { get; set; }
    public User? Owner { get; set; }
}