public class Document {
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content  { get; set; }
    public ContentType? ContentType { get; set; }
    public DateTime CreatedDate { get; set; }
    public int UserId { get; set; }
    public User? Owner { get; set; }
}