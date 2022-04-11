namespace TestProject1.Web.Models
{
    public class DataItem
    {
        public int AlbumId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }

        public DataItem()
        {

        }

        public DataItem(int albumId, int id, string title, string url, string thumbnailUrl)
        {
            AlbumId = albumId;
            Id = id;
            Title = title;
            Url = url;
            ThumbnailUrl = thumbnailUrl;
        }
    }
}
