using System.Runtime.CompilerServices;

namespace WarehouseApp.Common
{
    public static class EntityValidationConstants
    {
        public static class Category
        {
            public const int CategoryNameMaxLength = 20;
            public const int CategoryNameMinLength = 3;

        }

        public static class Message 
        {
            public const int MassageTypeMaxLength = 20;
            public const int MassageTypeMinLength = 4;

            public const int MassageContentMaxLength = 500;
            public const int MassageContentMinLength = 10;

            public const int MassageStatusMaxLength = 10;
            public const int MassageStatusMinLength = 4;
        }

        public static class Order 
        {
            public const int OrderStatusMaxLength = 10;
            public const int OrderStatusMinLength = 4;

        }
        public static class OrderProduct 
        {
            public const int QuantityOrderedMaxLength = 99_999;
            public const int QuantityOrderedMinLength = 1;
        }

        public static class Product
        {
            public const int ProductNameMaxLength = 20;
            public const int ProductNameMinLength = 3;

            public const int ImagePathMaxLength = 20;


            public const int ProductDescriptionMaxLength = 500;
            public const int ProductDescriptionMinLength = 10;

            public const double ProductPriceMax = 999.99;
            public const double ProductPriceMin = 0.01;

            public const int ProductStockQuantityMax = 999;
            public const int ProductStockQuantityMin = 0;
        }

        public static class Request 
        {
            public const int RequestStatusMaxLength = 15;
            public const int ProductStockQuantityMinLength = 4;

            public const int RequestNoteMaxLength = 300;
            public const int ProductNoteQuantityMinLength = 0;
        }

        public static class RequestProduct
        {
            public const int QuantityRequestedMax = 999;
            public const int QuantityRequestedMin = 1;

            public const double PriceUponRequestMax = 999.99;
            public const double PPriceUponRequestMin = 0.01;
        }

        public static class Sale
        {
            public const double TotalAmountMax = 999_999_999.99;
            public const double TotalAmountMin = 0.01;
        }

        public static class SaleProduct
        {
            public const int QuantitySoldMax = 999;
            public const int QuantitySoldMin = 1;

            public const double UnitPriceMax = 999.99;
            public const double UnitPriceMin = 0.01;
        }

        public static class Users
        {
            public const int NameMaxLength = 20;
            public const int NameMinLength = 2;

            public const int CompanyNameMaxLength = 20;
            public const int CompanyNameMinLength = 4;

            public const int TaxNumberMaxLength = 15;
            public const int TaxNumberMinLength = 6;

            public const int MOLMaxLength = 40;
            public const int MOLMinLength = 4;

            public const int EmailMaxLength = 25;
            public const int EmailMinLength = 6;

            public const int PhoneMaxLength = 15;
            public const int PhoneMinLength = 4;


            public const int AddressMaxLength = 20;
            public const int AddressMinLength = 4;

            public const double DiscountRateMax = 75;
            public const double DiscountRateMin = 0;

            public const int PreferredDeliveryMethodMaxLength = 15;
            public const int PreferredDeliveryMethodMinLength = 3;
        }
    }
}
