using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models.Interfaces;
using WarehouseApp.Data.Models.Users;

namespace WarehouseApp.Data.Models
{
    public class Request
    {
        [Key]
        public int RequestId { get; set; }

        [Required]
        public Guid RequesterId { get; set; }

        [ForeignKey(nameof(RequesterId))]
        public RequesterAndBuyerUser Requester { get; set; } = null!;

        [Required]
        public string Status { get; set; } = null!;

        [Required]
        public DateTime RequestDate { get; set; }

        
        public Guid ProcessedByWorkerId { get; set; }

        [ForeignKey(nameof(ProcessedByWorkerId))]
        public WarehouseWorker ProcessedByWorker { get; set; } = null!;

        public string? Notes { get; set; }

        public ICollection<RequestProduct> RequestProducts { get; set; } = new HashSet<RequestProduct>();
    }
}
