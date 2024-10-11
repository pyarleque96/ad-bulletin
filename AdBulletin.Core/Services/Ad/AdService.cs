using AdBulletin.Core.Services.Clients;
using AdBulletin.Infrastructure.Transport;

namespace AdBulletin.Core.Services;

public class AdService
{
    private readonly IAdClientAPI _adClientAPI;
    private readonly ILogger<AdService> _logger;

    public AdService(IAdClientAPI adClientAPI,
                     ILogger<AdService> logger)
    {
        _adClientAPI = adClientAPI;
        _logger = logger;
    }

    public async Task<BaseResult<IEnumerable<AdDto>>> ListAsync(ListAdRequest request)
    {
        try
        {
            var response = await _adClientAPI.List(request);

            if (response.State.HasError)
            {
                _logger.LogInformation($"AdService => ListAsync() HasError: -- {response.State.MessageDetail}");
                throw new Exception($"AdService => ListAsync() HasError: -- {response.State.MessageDetail}");
            }

            return response.Result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"AdService => ListAsync() Exception: -- {ex.Message} - {ex.StackTrace}");
            throw;
        }
    }
}
