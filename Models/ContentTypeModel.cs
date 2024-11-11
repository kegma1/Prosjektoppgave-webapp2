public enum FileType {
    PlainText,
    Image,
}
public class ContentType {
    public int Id { get; set; }
    public FileType Type { get; set; }
}