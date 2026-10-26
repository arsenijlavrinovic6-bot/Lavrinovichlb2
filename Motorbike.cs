using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Lavrinovich
{
    [Table("Table_Motorbike")]
    public class Motorbike
    {
        [Key, Column("ID")]
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public int Power { get; set; }
        public int Mileage { get; set; }
        public string Image { get; set; }
    }
}