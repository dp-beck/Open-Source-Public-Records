using Microsoft.AspNetCore.Components;
using Shared.Dtos;
using WebClient.Services;

namespace WebClient.Pages
{
    public partial class Home()
    {
        private IEnumerable<RecordDto>? Records;
        private bool _isLoading = true;
        private string? _searchString;
        
        [Inject]
        public IRecordsService? RecordsService { get; set; }
        
        // quick filter - filter globally across multiple columns with the same input
        private Func<RecordDto, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            
            if (x.Country.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            
            if (x.State.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            
            if (x.County.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            
             if (x.RecordType.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                 return true;
             
            return false;
        };
        
        protected override async Task OnInitializedAsync()
        {
            Records = await RecordsService.GetAllRecords();
            _isLoading = false;
        }
        
    }
}