using System.Text.Json;
using System.Text.Json.Serialization;
using Shared.Dtos;

namespace WebClient.Services;

public class CountriesService : ICountriesService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions jsonSerializerOptions =
        new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReferenceHandler = ReferenceHandler.Preserve
        };

    public CountriesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<CountryDto>> GetCountries()
    {

        var countriesResponse = await _httpClient.GetAsync("countries/");

        countriesResponse.EnsureSuccessStatusCode();

        var countriesJson = await countriesResponse.Content.ReadAsStringAsync();
        var countryDtos = JsonSerializer.Deserialize<IEnumerable<CountryDto>>(countriesJson, jsonSerializerOptions);

        return countryDtos;    }
}