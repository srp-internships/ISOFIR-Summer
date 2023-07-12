using AutoMapper;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Core.ActionResults;
using Report.Core.Models;

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

    public async Task<Result> CreateOrUpdate(FirmRequestModel firmDto)
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
                _firmRepository.Add(firm);
            }

            _firmRepository.SaveChanges();
            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public Result Remove(int id)
    {
        try
        {
            _firmRepository.Remove(id);
            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e, "Не возможно удалить несуществующий обект");
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