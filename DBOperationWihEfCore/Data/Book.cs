namespace DBOperationWihEfCore.Data
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NumberOfPage { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
        






    }
}
