using MockQueryable;
using Moq;
using WarehouseApp.Data.Models;
using WarehouseApp.Data.Repository.Interfaces;
using WarehouseApp.Data.Seeding.DTO;
using WarehouseApp.Services.Data;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Services.Mapping;
using WarehouseApp.Web.ViewModels;
using WarehouseApp.Web.ViewModels.Product;

namespace WarehouseApp.Services.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private IList<Product> productData = new List<Product>()
        {
            new Product()
            {
                Id = 1,
                Name = "Walnut Vinyl 8mm",
                ImagePath = "/img/walnut-vinyl-8mm.jpg",
                Description = "Water-resistant vinyl flooring with a walnut finish, ideal for kitchens and bathrooms.",
                Price = 18.75M,
                StockQuantity = 300,
                SoftDelete = false
            },
            new Product()
            {
                Id = 2,
                Name = "Herringbone Parquet",
                ImagePath = "/img/herringbone-parquet.jpg",
                Description = "Classic herringbone parquet flooring made from engineered wood for a premium look.",
                Price = 45.00M,
                StockQuantity = 100,
                SoftDelete = false
            }
        };
        private List<Category> categoryData = new List<Category>
            {
                new Category { Id = 1, Name = "Category 1", SoftDelete = false },
                new Category { Id = 2, Name = "Category 2", SoftDelete = false },
                new Category { Id = 3, Name = "Deleted Category", SoftDelete = true }
            };

        private Mock<IRepository> repository;
        IProductService productService;

        [OneTimeSetUp]
        public void OneTimeSetUp() 
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).Assembly, typeof(ImportProductDTO).Assembly);
        }

        [SetUp]
        public void SetUp()
        {
            repository = new Mock<IRepository>();
            productService = new ProductService(repository.Object);
        }

        [Test]
        public async Task GetAllProductsPagedAsync_CheckCountAndID()
        {
            IQueryable<Product> productsMock = productData.BuildMock();
            repository.Setup(p => p.GetAllAttached<Product>())
                .Returns(productsMock);


            (IEnumerable<ProductIndexViewModel>, int) allProductsActual = await productService
                .GetAllProductsPagedAsync(0, 999, null, 1, 6 );

            Assert.IsNotNull(allProductsActual);
            Assert.That(allProductsActual.Item1.Count(), Is.EqualTo(productData.Count()));

            int n = 0;
            foreach (var item in allProductsActual.Item1)
            {
                Assert.That(item.Id, Is.EqualTo(productData[n].Id));
                    n++;
            }
        }

        [Test]
        public async Task GetProductDetailsByIdAsync_CheckCorrectProductDetails()
        {
            var queryableProducts = productData.AsQueryable().BuildMock();
            repository.Setup(r => r.GetAllAttached<Product>()).Returns(queryableProducts);

            var productDetails = await productService.GetProductDetailsByIdAsync(1);

            Assert.IsNotNull(productDetails);
            Assert.That(productDetails.Name, Is.EqualTo(productData[0].Name));
        }

        [Test]
        public async Task GetProductEditByIdAsync_CheckCorrectEditViewModel()
        {
            var queryableProducts = productData.AsQueryable().BuildMock();
            var queryableCategories = categoryData.AsQueryable().BuildMock();

            repository.Setup(r => r.GetAllAttached<Product>()).Returns(queryableProducts);
            repository.Setup(r => r.GetAllAttached<Category>()).Returns(queryableCategories);

            var editViewModel = await productService.GetProductEditByIdAsync(1);

            Assert.IsNotNull(editViewModel);
            Assert.That(editViewModel.Name, Is.EqualTo(productData[0].Name));

            var editViewModelTest2 = await productService.GetProductEditByIdAsync(999);
            Assert.IsNull(editViewModelTest2);
        }

        [Test]
        public async Task AddProductAsync_ShouldAddNewProduct()
        {
            var newProduct = new EditProductViewModel
            {
                Name = "New Product",
                Price = 30.0M,
                StockQuantity = 10,
                SelectedCategoryIds = new List<int> { 1 }
            };

            repository.Setup(r => r.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

            var result = await productService.AddProductAsync(newProduct);

            Assert.IsTrue(result);
            repository.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
        }

        [Test]
        public async Task SaveProductAsync_ShouldUpdateProductDetails()
        {
            var queryableProducts = productData.AsQueryable().BuildMock();
            repository.Setup(r => r.GetAllAttached<Product>()).Returns(queryableProducts);
            repository.Setup(r => r.UpdateAsync(It.IsAny<Product>())).Returns(Task.FromResult(true));


            var updatedProduct = new EditProductViewModel
            {
                Id = 1,
                Name = "Updated Product",
                Price = 25.0M,
                StockQuantity = 150,
                SelectedCategoryIds = new List<int> { 2 }
            };

            var result = await productService.SaveProductAsync(updatedProduct);

            Assert.IsTrue(result);
            repository.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Once);
        }

        [Test]
        public async Task SoftDeleteAsync_ShouldMarkProductAsDeleted()
        {
            var queryableProducts = productData.AsQueryable().BuildMock();
            repository.Setup(r => r.GetAllAttached<Product>()).Returns(queryableProducts);
            repository.Setup(r => r.UpdateAsync(It.IsAny<Product>())).Returns(Task.FromResult(true));


            var result = await productService.SoftDeleteAsync(1);

            Assert.IsTrue(result);
            repository.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Once);
        }
    }
}