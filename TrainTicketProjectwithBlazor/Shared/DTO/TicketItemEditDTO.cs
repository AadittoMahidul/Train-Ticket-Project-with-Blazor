using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketProjectwithBlazor.Shared.DTO
{
    public class TicketItemEditDTO
    {
        [Key]
        public int TicketId { get; set; }
        [Required]
        public int TrainId { get; set; }
        [Required]
        public decimal TicketPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
