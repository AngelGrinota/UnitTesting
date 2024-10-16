using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingLib.Shop;
using TestingLib.Weather;

namespace UnitTesting.AngelGrinota
{
    public class UnitTestMoq
    {
        private readonly Mock<IWeatherForecastSource> mockWeatherForecastSource;
        private readonly Mock<ICustomerRepository> mockCustomerRepository;
        private readonly Mock<INotificationService> mockNotificationService;
        private readonly Mock<IOrderRepository> mockOrderRepository;

        public UnitTestMoq()
        {
            mockWeatherForecastSource = new Mock<IWeatherForecastSource>();
            mockCustomerRepository = new Mock<ICustomerRepository>();
            mockNotificationService = new Mock<INotificationService>();
            mockOrderRepository = new Mock<IOrderRepository>();
        }

        [Fact]
        public void GetWeatherForecast_ShouldReturnCorrectResult()
        {
            // Arrange
            var weatherForecast = new WeatherForecast { TemperatureC = 36, Summary = "Солнечно" };
            var currentDateTime = DateTime.Now;

            mockWeatherForecastSource.Setup(repo => repo.GetForecast(currentDateTime)).Returns(weatherForecast);

            var service = new WeatherForecastService(mockWeatherForecastSource.Object);

            // Act
            var result = service.GetWeatherForecast(currentDateTime);

            // Assert
            Assert.NotNull(result);
            mockWeatherForecastSource.Verify(repo => repo.GetForecast(It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public void CreateOrder_ShouldCreateNewOrder()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "Kit", Email = "kit@mail.ru" };
            var order = new Order { Id = 2, Date = DateTime.Now, Customer = customer, Amount = 30 };

            mockOrderRepository.Setup(repo => repo.GetOrderById(1)).Returns(order);

            var service = new ShopService(mockCustomerRepository.Object, mockOrderRepository.Object, mockNotificationService.Object);

            // Act
            service.CreateOrder(order);

            // Assert
            mockOrderRepository.Verify(repo => repo.GetOrderById(It.IsAny<int>()), Times.Once);
            mockOrderRepository.Verify(repo => repo.AddOrder(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public void CreateOrder_ShouldSendNotification()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "Kit", Email = "kit@mail.ru" };
            var order = new Order { Id = 2, Date = DateTime.Now, Customer = customer, Amount = 30 };
            mockOrderRepository.Setup(repo => repo.GetOrderById(1)).Returns(order);

            var service = new ShopService(mockCustomerRepository.Object, mockOrderRepository.Object, mockNotificationService.Object);

            // Act
            service.CreateOrder(order);

            // Assert
            mockNotificationService.Verify(repo => repo.SendNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GetCustomerInfo_ShouldReturnCorrectResult()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "Kit", Email = "kit@mail.ru" };
            var order = new Order { Id = 1, Date = DateTime.Now, Customer = customer, Amount = 30 };

            mockOrderRepository.Setup(repo => repo.GetOrders()).Returns(new List<Order> { order });
            mockCustomerRepository.Setup(repo => repo.GetCustomerById(1)).Returns(customer);

            var service = new ShopService(mockCustomerRepository.Object, mockOrderRepository.Object, mockNotificationService.Object);

            // Act
            string result = service.GetCustomerInfo(1);

            // Assert
            Assert.Equal("Customer Kit has 1 orders", result);
            mockCustomerRepository.Verify(repo => repo.GetCustomerById(It.IsAny<int>()), Times.Once);
            mockOrderRepository.Verify(repo => repo.GetOrders(), Times.Once);
        }
    }
}
