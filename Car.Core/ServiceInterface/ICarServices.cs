using Car.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car.Core.ServiceInterface
{
    public interface ICarServices
    {
        Task<IEnumerable<Car.Core.Domain.Car>> GetAll();
        Task<Car.Core.Domain.Car> Get(Guid id);
        Task<Car.Core.Domain.Car> Create(Car.Core.Domain.Car car);
        Task<Car.Core.Domain.Car> Update(Car.Core.Domain.Car car);
        Task<Car.Core.Domain.Car> Delete(Guid id);
    }
}