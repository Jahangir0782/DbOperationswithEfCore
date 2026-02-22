using System.Data.SqlTypes;

namespace DBOperationWihEfCore.Data
{
    public class BookPrice
    {
        public int Id { get; set; }
        public int  BookId { get; set; }
        public int  CurencyId { get; set; }
        public int Amount { get; set; }   

        public Book book { get; set; }
        public CurrencyType CurrencyType { get; set; }

    }
}
