using AdBulletin.Domain.Contracts.Aggregates;
using AdBulletin.Infrastructure.Transport;
using AutoMapper;

namespace AdBulletin.Domain.Managers;

public class AdManager : IAdManager
{
    private readonly IAdRepository _adRepository;
    private readonly IMapper _mapper;

    public AdManager(IAdRepository adRepository,
                     IMapper mapper)
    {
        _adRepository = adRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<BaseResult<IEnumerable<AdDto>>>> ListAsync(ListAdRequest request)
    {
        try
        {
            var result = await _adRepository.List(request.Parameters);

            var response = new BaseResult<IEnumerable<AdDto>>
            {
                Pagination = result.Pagination,
                Result = _mapper.Map<IEnumerable<AdDto>>(result.Result)
            };

            return BaseResponse<BaseResult<IEnumerable<AdDto>>>.Ok(response);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<BaseResponse<BaseResult<IEnumerable<AdDto>>>> ListForAdminAsync(ListAdRequest request)
    {
        var result = await _adRepository.ListForAdmin(request.Parameters);

        var response = new BaseResult<IEnumerable<AdDto>>
        {
            Pagination = result.Pagination,
            Result = _mapper.Map<IEnumerable<AdDto>>(result.Result)
        };

        return BaseResponse<BaseResult<IEnumerable<AdDto>>>.Ok(response);
    }
}
