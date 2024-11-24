using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Web.ViewModels.Message;

namespace WarehouseApp.Services.Data.Interfaces
{
    public interface IMessageServices
    {
        Task<IEnumerable<MessageViewModel>> GetAllMessageAsync();

        Task<MessageViewModel> GetMessageByIdAsync(int id);
    }
}
