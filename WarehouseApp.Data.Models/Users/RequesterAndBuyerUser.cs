using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models.Interfaces;

namespace WarehouseApp.Data.Models.Users
{
    public class RequesterAndBuyerUser : SenderMessageUser, IRequester, IBuyer
    {

    }
}
