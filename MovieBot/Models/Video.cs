namespace MovieBot.Models
{
    public class Video
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Code { get; set; }
        public string Length { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public string Rating { get; set; }
        public int Votes { get; set; }
        public int Views { get; set; }
    }
}