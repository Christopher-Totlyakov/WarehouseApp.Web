using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WarehouseApp.Common.EntityValidationConstants.Message;

namespace WarehouseApp.Web.ViewModels.Message
{
    public class SendMessage
    {
        public Guid SenderId { get; set; }

        [Required]
        [MaxLength(MassageTypeMaxLength)]
        [MinLength(MassageTypeMinLength)]
        public string MessageType { get; set; } = null!;

        [Required]
        [MaxLength(MassageContentMaxLength)]
        [MinLength(MassageContentMinLength)]
        public string MessageContent { get; set; } = null!;
    }
}
