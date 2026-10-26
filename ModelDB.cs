using System.Data.Entity;
namespace Lavrinovich
{
    public class ModelDB : DbContext
    {
        public ModelDB() : base("name=MotorbikeDB") { }
        public DbSet<Motorbike> Table_Motorbike { get; set; }
    }
}