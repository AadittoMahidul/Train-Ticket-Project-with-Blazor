using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TrainTicketProjectwithBlazor.Shared.Models;

namespace TrainTicketProjectwithBlazor.Shared.DTO
{
    public class TicketEditDTO
    {
        [Key]
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
        [Required]
        public int PassengerId { get; set; }
        public ICollection<TicketItemEditDTO> TicketItems { get; set; } = new List<TicketItemEditDTO>();
    }
}
