using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicketProjectwithBlazor.Shared.Models;

namespace TrainTicketProjectwithBlazor.Shared.DTO
{
    public class TicketItemDTO
    {
        [ForeignKey("Ticket")]
        public int TicketId { get; set; }
        [ForeignKey("Train")]
        public int TrainId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
