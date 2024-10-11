using AdBulletin.Domain.Contracts.Aggregates;
using AdBulletin.Infrastructure.Transport;
using AutoMapper;

namespace AdBulletin.Domain.Managers;

public class AdCategoryManager : IAdCategoryManager
{
    private readonly IAdCategoryRepository _adCategoryRepository;
    private readonly IMapper _mapper;

    public AdCategoryManager(IAdCategoryRepository adCategoryRepository,
                                  IMapper mapper)
    {
        _adCategoryRepository = adCategoryRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<ListAdCategoryResult<AdCategoryDto>>> ListAsync()
    {
        var response = new ListAdCategoryResult<AdCategoryDto>
        {
            Items = _mapper.Map<IEnumerable<AdCategoryDto>>(await _adCategoryRepository.List())
        };

        return BaseResponse<ListAdCategoryResult<AdCategoryDto>>.Ok(response);
    }
}
