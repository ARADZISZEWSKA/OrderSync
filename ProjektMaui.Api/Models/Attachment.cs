namespace ProjektMaui.Api.Models
{
    public class Attachment
    {
        public int Id { get; set; }

        public string FileName { get; set; }
        public string Url { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }

}
