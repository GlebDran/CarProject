using System;
using System.ComponentModel.DataAnnotations;

namespace Car.Core.Domain
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }

        public string Make { get; set; } // Производитель (BMW, Audi...)
        public string Model { get; set; } // Модель
        public int Year { get; set; } // Год
        public int Price { get; set; } // Цена
        public bool IsUsed { get; set; } // Б/у или новая (для количества переменных)

        // Обязательные поля по заданию
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}