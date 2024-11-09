using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Data.Models.Interfaces
{
    public interface IRequester
    {
        Guid Id { get; }
        string? UserName { get; }
    }
}
