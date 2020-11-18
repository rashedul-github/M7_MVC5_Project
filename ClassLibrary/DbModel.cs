using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class CarType
    {
        public CarType()
        {
            this.Car = new List<Car>();
        }
        [Key]
        [Display(Name = "Type Id")]
        public int TypeId { get; set; }
        [Display(Name = "Type Name")]
        [Required(ErrorMessage = "Type Name is required."), StringLength(40)]
        public string TypeName { get; set; }

        //Navigation
        public virtual ICollection<Car> Car { get; set; }
    }
    public class Car
    {
        public Car()
        {
            this.CheckAvailability = new List<CheckAvailability>();
        }
        [Key]
        [Display(Name = "Car Id")]
        public int CarId { get; set; }
        [Required(ErrorMessage = "Picture is required."), StringLength(250)]
        public string Picture { get; set; }
        [Required(ErrorMessage = "Model is required."), StringLength(40)]
        public string Model { get; set; }
        [Required(ErrorMessage = "Price is required."), Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Car Id")]
        public int TypeId { get; set; }
        //Navigation
        [ForeignKey("TypeId")]
        public virtual CarType CarType { get; set; }
        public virtual ICollection<CheckAvailability> CheckAvailability { get; set; }

    }
    public class CheckAvailability
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(40)]
        public string Name { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required."), StringLength(50)]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$",
            ErrorMessage = "Invalid email address pattern.")]
        public string Eamil { get; set; }
        [Display(Name = "Entry Date")]
        [Column(TypeName ="date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Required,Display(Name ="Do you have a trade-in?")]
        public bool TradeIn { get; set; }
        [Required]
        [Display(Name = "Car Id")]
        public int CarId { get; set; }
        //Navigation
        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }

    }
    public class CarDbContext : DbContext
    {
        public CarDbContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarDbContext>());
        }
        public DbSet<CarType> carTypes { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CheckAvailability> CheckAvailabilities { get; set; }
    }
}
