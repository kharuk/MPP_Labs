using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ValidationLib.Attributes;

namespace Model
{
    [Table("Books")]
    public class Book
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("Name")]
        [RequiredValue]
        [MinimumStringLength(3)]
        public string Name { get; set; }

        [RangeOfInteger(0, 1000000)]
        [Column("Price")]
        public int Price { get; set; }

        [RequiredValue]
        [MinimumStringLength(3)]
        [Column("Author")]
        public string Author { get; set; }

        [RangeOfInteger(2, 3000)]
        [Column("CountOfPages")]
        public int CountOfPages { get; set; }
    }
}
