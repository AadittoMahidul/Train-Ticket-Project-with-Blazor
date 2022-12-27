using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrainTicketProjectwithBlazor.Shared.Models
{
    public enum Category { AC = 1, Snigdha, Shovon, Shulov }
    public class Passenger
    {
        public int PassengerId { get; set; }
        [Required, StringLength(50), Display(Name = "Passenger Name")]
        public string PassengerName { get; set; } = default!;
        [Required, StringLength(150)]
        public string Address { get; set; } = default!;
        [Required, StringLength(50), EmailAddress]
        public string Email { get; set; } = default!;
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }

    public class Ticket
    {
        public int TicketId { get; set; }
        [Required, StringLength(40), Display(Name = "From Station")]
        public string FromStation { get; set; } = default!;
        [Required, StringLength(40), Display(Name = "To Station")]
        public string ToStation { get; set; } = default!;
        [Required, Column(TypeName = "date"),
        Display(Name = "Journey Date"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public DateTime? JourneyDate { get; set; } = default!;
        [Required, EnumDataType(typeof(Category))]
        public Category Category { get; set; }
        [Required, ForeignKey("Passenger")]
        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; } = default!;
        public ICollection<TicketItem> TicketItems { get; set; } = new List<TicketItem>();
    }
    public class TicketItem
    {
        [ForeignKey("Ticket")]
        public int TicketId { get; set; }
        public virtual Ticket? Ticket { get; set; } = default!;
        [ForeignKey("Train")]
        public int TrainId { get; set; }
        public virtual Train? Train { get; set; } = default!;
        [Required]
        public int Quantity { get; set; }
    }
    public class Train
    {
        public int TrainId { get; set; }
        [Required, StringLength(50), Display(Name = "Train Name")]
        public string TrainName { get; set; } = default!;
        [Required, EnumDataType(typeof(Category))]
        public Category Category { get; set; }
        [Required, Display(Name = "Ticket Price"), Column(TypeName = "money")]
        public decimal TicketPrice { get; set; }
        [Required, StringLength(150)]
        public string Picture { get; set; } = default!;
        public bool IsAvailable { get; set; }
        public ICollection<TicketItem> TicketItems { get; set; } = new List<TicketItem>();
    }

    public class TrainDbContext : DbContext
    {
        public TrainDbContext(DbContextOptions<TrainDbContext> options) : base(options) { }
        public DbSet<Passenger> Passengers { get; set; } = default!;
        public DbSet<Ticket> Tickets { get; set; } = default!;
        public DbSet<TicketItem> TicketItems { get; set; } = default!;
        public DbSet<Train> Trains { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TicketItem>()
                .HasKey(o => new { o.TicketId, o.TrainId });
        }
    }
}
