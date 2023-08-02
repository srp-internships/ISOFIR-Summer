using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentCard.Application.Infrastructure.DataBase;
using StudentCard.Application.Interfaces.Services;
using StudentCard.Domain;
using StudentCard.Domain.Models;
using StudentCard.Domain.RequestModels;
using StudentCard.Domain.ResponseModels;

namespace StudentCard.Application.Services;

public class PayService:IPayService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    
    public PayService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result> CreateOrUpdateAsync(PayRequestModel pay)
    {
        try
        {
            var old = await _context.Pays.FirstOrDefaultAsync(s => s.Id == pay.Id);
            if (old == null)
            {
                await _context.Pays.AddAsync(_mapper.Map<Pay>(pay));
            }
            else
            {
                _mapper.Map(pay, old);
            }

            await _context.SaveChangesAsync();
            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            _context.Pays.Remove(await _context.Pays.FirstOrDefaultAsync(s=>s.Id==id) ?? throw new InvalidOperationException());
            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> GetByIdAsync(int id)
    {
        try
        {
            return new OkResult<PayResponseModel>(_mapper.Map<PayResponseModel>(await _context.Pays.FirstOrDefaultAsync(s => s.Id == id) ??
                                       throw new InvalidOperationException()));
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> GetAllAsync()
    {
        try
        {
            return new OkResult<List<PayResponseModel>>(await _context.Pays.Select(s=>_mapper.Map<PayResponseModel>(s)).ToListAsync());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }
}