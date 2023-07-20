using AutoMapper;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Domain.ActionResults;
using Report.Domain.Models;

namespace Report.Application.Services;

public class FirmService : IFirmService
{
    private readonly IMapper _mapper;
    private readonly IFirmRepository _firmRepository;

    public FirmService(IFirmRepository firmRepository, IMapper mapper)
    {
        _firmRepository = firmRepository;
        _mapper = mapper;
    }

    public async Task<Result> CreateOrUpdateAsync(FirmRequestModel firmDto)
    {
        try
        {
            var firm = _mapper.Map<FirmRequestModel, Firm>(firmDto);
            if (firm == null)
            {
                return new ErrorResult(new Exception(), "Невозможно обработать ваши данные");
            }

            var old = await _firmRepository.GetByIdAsync(firm.Id);
            if (old != null)
            {
                old.Name = firm.Name;
            }
            else
            {
                await _firmRepository.AddAsync(firm);
            }

            await _firmRepository.SaveChangesAsync();
            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> RemoveAsync(int id)
    {
        try
        {
            await _firmRepository.RemoveAsync(id);
            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e, "Не возможно удалить несуществующий объект");
        }
    }

    public async Task<Result> GetAllAsync()
    {
        try
        {
            var result = await _firmRepository.GetAllAsync();
            return new OkResult<List<FirmResponseModel>>(result.Select(s => _mapper.Map<Firm, FirmResponseModel>(s))
                .ToList());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }
}