using AutoMapper;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Application.ResponseModels;
using Report.Domain.ActionResults;
using Report.Domain.Models;

namespace Report.Application.Services;

public class RateService : IRateService
{
    private readonly IMapper _mapper;
    private readonly IRateRepository _rateRepository;

    public RateService(IRateRepository rateRepository, IMapper mapper)
    {
        _rateRepository = rateRepository;
        _mapper = mapper;
    }

    public async Task<Result> CreateAsync(decimal oneDollarIs, int userId)
    {
        try
        {
            var rate = new Rate
            {
                UserId = userId,
                OneDollarIs = oneDollarIs
            };
            await _rateRepository.AddAsync(rate);
            await _rateRepository.SaveChangesAsync();
            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public Task<Result> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> GetAllAsync(int userId)
    {
        try
        {
            var rates = await _rateRepository.GetAllAsync(userId);
            return new OkResult<List<RateResponseModel>>(rates.Select(s => _mapper.Map<RateResponseModel>(s)).ToList());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> GetLastAsync(int userId)
    {
        try
        {
            return new OkResult<decimal>(await _rateRepository.GetLastAsync(userId));
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }
}