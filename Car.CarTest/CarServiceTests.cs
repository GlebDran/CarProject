using Car.ApplicationServices.Services;
using Car.Core.Domain;
using Car.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Car.CarTest
{
    public class CarServiceTests
    {
        // Метод для создания тестового контекста БД (виртуальная база)
        private CarDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<CarDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Уникальное имя для каждого теста
                .Options;

            var context = new CarDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async Task Create_ShouldAddCar_WhenInputIsValid()
        {
            // Arrange (Подготовка)
            var context = GetDbContext();
            var service = new CarServices(context);
            var newCar = new Core.Domain.Car
            {
                Make = "TestBMW",
                Model = "X5",
                Year = 2022,
                Price = 50000,
                IsUsed = false
            };

            // Act (Действие)
            var createdCar = await service.Create(newCar);

            // Assert (Проверка)
            Assert.NotNull(createdCar);
            Assert.NotEqual(Guid.Empty, createdCar.Id); // ID должен был сгенерироваться
            Assert.Equal("TestBMW", createdCar.Make);
        }

        [Fact]
        public async Task Get_ShouldReturnCar_WhenIdExists()
        {
            // Arrange
            var context = GetDbContext();
            var service = new CarServices(context);

            // Сначала добавим машину вручную в базу
            var carId = Guid.NewGuid();
            var existingCar = new Core.Domain.Car
            {
                Id = carId,
                Make = "Audi",
                Model = "A6",
                Year = 2020,
                Price = 30000,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
            context.Cars.Add(existingCar);
            context.SaveChanges();

            // Act
            var result = await service.Get(carId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Audi", result.Make);
            Assert.Equal(carId, result.Id);
        }

        [Fact]
        public async Task Update_ShouldModifyCar_WhenCarExists()
        {
            // Arrange
            var context = GetDbContext();
            var service = new CarServices(context);

            var carId = Guid.NewGuid();
            var originalCar = new Core.Domain.Car
            {
                Id = carId,
                Make = "Toyota",
                Model = "Corolla",
                Year = 2010,
                Price = 5000,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
            context.Cars.Add(originalCar);
            context.SaveChanges();

            // Изменяем данные
            originalCar.Price = 6000;
            originalCar.IsUsed = true;

            // Act
            var updatedCar = await service.Update(originalCar);

            // Assert
            Assert.Equal(6000, updatedCar.Price);
            Assert.True(updatedCar.IsUsed);
            // Проверим, что в базе тоже обновилось
            var carInDb = await context.Cars.FindAsync(carId);
            Assert.Equal(6000, carInDb.Price);
        }

        [Fact]
        public async Task Delete_ShouldRemoveCar_WhenIdExists()
        {
            // Arrange
            var context = GetDbContext();
            var service = new CarServices(context);

            var carId = Guid.NewGuid();
            var carToDelete = new Core.Domain.Car
            {
                Id = carId,
                Make = "Lada",
                Model = "Sedan",
                Year = 1990,
                Price = 500,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
            context.Cars.Add(carToDelete);
            context.SaveChanges();

            // Act
            var deletedCar = await service.Delete(carId);

            // Assert
            Assert.NotNull(deletedCar); // Метод возвращает удаленный объект

            // Проверяем, что в базе его больше нет
            var carInDb = await context.Cars.FindAsync(carId);
            Assert.Null(carInDb);
        }
    }
}