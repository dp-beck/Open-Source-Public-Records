using Microsoft.AspNetCore.Components;
using Shared.Dtos;
using WebClient.Services;

namespace WebClient.Pages
{
    public partial class Home()
    {
        private IEnumerable<CountryDto> Countries;
        
        [Inject]
        public ICountriesService CountriesService { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            Countries = await CountriesService.GetCountries();
        }
        
    }
}