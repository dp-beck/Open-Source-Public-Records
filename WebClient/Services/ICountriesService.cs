using Shared.Dtos;

namespace WebClient.Services;

public interface ICountriesService
{
    public Task<IEnumerable<CountryDto>> GetCountries();
}