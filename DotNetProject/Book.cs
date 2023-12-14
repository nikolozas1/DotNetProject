using System.ComponentModel.DataAnnotations;


namespace DotNetProject
{
    public class Book
    {
        [Key]
        public string Title { get; set; }
        public string Year_of_Publication { get; set; }
        public string AuthorName { get; set; }
    }
}
