using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models.Users;
using WarehouseApp.Data.Models;
using WarehouseApp.Services.Mapping;
using AutoMapper;
using WarehouseApp.Web.ViewModels.ShoppingCart;

namespace WarehouseApp.Web.ViewModels.Message
{
    public class MessageViewModel //: IMapFrom<Data.Models.Message>, IHaveCustomMappings
    {
        public int MessageId { get; set; }

        public Guid SenderId { get; set; }

        public string SenderEmail { get; set; } = null!; 

        public Guid? ReceiverId { get; set; }

        public string? ReceiverName { get; set; } 

        public string MessageType { get; set; } = null!;

        public string MessageContent { get; set; } = null!;

        public DateTime SentDate { get; set; }

        public string Status { get; set; } = null!;

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration.CreateMap<Data.Models.Message, MessageViewModel>()
        //        .ForMember(m => m.ReceiverName, x => x.MapFrom(r => r.Receiver != null ? $"{r.Receiver.FirstName} {r.Receiver.LastName}" : null));
        //}
    }
}                   
