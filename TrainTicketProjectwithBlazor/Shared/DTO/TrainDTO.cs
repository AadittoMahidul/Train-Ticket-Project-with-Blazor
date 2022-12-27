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
    public class TrainDTO
    {
        public int TrainId { get; set; }
        [Required, StringLength(50)]
        public string TrainName { get; set; } = default!;
        [Required, EnumDataType(typeof(Category))]
        public Category Category { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal TicketPrice { get; set; }
        [Required, StringLength(150)]
        public string Picture { get; set; } = default!;
        public bool IsAvailable { get; set; }
    }
}
