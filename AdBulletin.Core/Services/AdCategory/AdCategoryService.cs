using AdBulletin.Core.Services.Clients;
using AdBulletin.Infrastructure.Transport;

namespace AdBulletin.Core.Services;

public class AdCategoryService
{
    private readonly IAdCategoryClientAPI _adCategoryAPI;
    private readonly ILogger<AdCategoryService> _logger;

    public AdCategoryService(IAdCategoryClientAPI adCategoryAPI,
                             ILogger<AdCategoryService> logger)
    {
        _adCategoryAPI = adCategoryAPI;
        _logger = logger;
    }

    public async Task<IEnumerable<AdCategoryDto>> ListAsync()
    {
        try
        {
            var response = await _adCategoryAPI.List();
            
            if (response.State.HasError)
            {
                _logger.LogInformation($"AdCategoryService => ListAsync() HasError: -- {response.State.MessageDetail}");
                throw new Exception($"AdCategoryService => ListAsync() HasError: -- {response.State.MessageDetail}");
            }
                
            return response.Result.Items;
        }
        catch (Exception ex)
        {
            _logger.LogError($"AdCategoryService => ListAsync() Exception: -- {ex.Message} - {ex.StackTrace}");
            throw;
        }
    }
}
