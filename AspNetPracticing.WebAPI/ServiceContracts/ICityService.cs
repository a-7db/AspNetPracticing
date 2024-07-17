using AspNetPracticing.WebAPI.DTOs;
using AspNetPracticing.WebAPI.Models;

namespace AspNetPracticing.WebAPI.ServiceContracts
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAll();

        Task<City> Create(CityDTO city);
    }
}
