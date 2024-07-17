using AspNetPracticing.WebAPI.Data;
using AspNetPracticing.WebAPI.DTOs;
using AspNetPracticing.WebAPI.Models;
using AspNetPracticing.WebAPI.ServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace AspNetPracticing.WebAPI.ImplementedServices
{
    public class CityService : ICityService
    {
        private readonly AppDbContext _db;

        public CityService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<City> Create(CityDTO cityDto)
        {
            City city = new City()
            {
                Id = Guid.NewGuid(),
                Name = cityDto.Name,
            };

            _db.City.Add(city);
            await _db.SaveChangesAsync();

            return city;
        }

        public async Task<IEnumerable<City>> GetAll()
        {
            return await _db.City.ToListAsync();
        }
    }
}
