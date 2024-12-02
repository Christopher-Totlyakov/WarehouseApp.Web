﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Web.ViewModels.Sales;

namespace WarehouseApp.Services.Data.Interfaces
{
    public interface ISalesService
    {
        Task<IEnumerable<SaleViewModel>> GetAllSalesAsync();

        Task<SaleDetailsViewModel> GetSaleDetailsAsync(int saleId);
    }
}