using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentCard.Application.Infrastructure.DataBase;
using StudentCard.Application.Interfaces.Services;
using StudentCard.Domain;
using StudentCard.Domain.Models;
using StudentCard.Domain.RequestModels;
using StudentCard.Domain.ResponseModels;

namespace StudentCard.Application.Services;

public class StudentService:IStudentService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public StudentService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result> CreateOrUpdateAsync(StudentRequestModel student)
    {
        try
        {
            var old = await _context.Students.FirstOrDefaultAsync(s => s.Id == student.Id);
            if (old == null)
            {
                await _context.Students.AddAsync(_mapper.Map<Student>(student));
            }
            else
            {
                _mapper.Map(student, old);
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
            _context.Students.Remove(await _context.Students.FirstOrDefaultAsync(s=>s.Id==id) ?? throw new InvalidOperationException());
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
            return new OkResult<StudentResponseModel>(_mapper.Map<StudentResponseModel>(await _context.Students.FirstOrDefaultAsync(s => s.Id == id)) ??
                                       throw new InvalidOperationException());
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
            return new OkResult<List<StudentResponseModel>>((await _context.Students.ToListAsync())
                .Select(s => _mapper.Map<StudentResponseModel>(s)).ToList());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> GetStudentPaysAsync(int studentId)
    {
        try
        {
            var student =  await _context.Students.Include(s=>s.Pays).FirstOrDefaultAsync(s=>s.Id==studentId);
            if (student is { Pays: not null })
                return new OkResult<List<PayResponseModel>>(student.Pays.Select(p => _mapper.Map<PayResponseModel>(p))
                    .ToList());
            return new ErrorResult("Student not found");
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }
}