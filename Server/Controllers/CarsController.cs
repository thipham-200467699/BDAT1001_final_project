using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/cars")]
    [ApiController]
    [Authorize]
    public class CarsController : Controller
    {
        Car[] cars = new Car[]
        {
            new Car { Id = 1, Brand = "Toyota", Model = "Camry", Price = 15000 },
            new Car { Id = 2, Brand = "BMW", Model = "BMW X1", Price = 20000 },
            new Car { Id = 3, Brand = "Tesla", Model = "Model Y", Price = 40000 }
        };

        
        public IEnumerable<Car> GetAllCars()
        {
            return cars;
        }
    }
}
