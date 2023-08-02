using AutoMapper;
using StudentCard.Domain.Models;
using StudentCard.Domain.RequestModels;
using StudentCard.Domain.ResponseModels;

namespace StudentCard.Application.Mappers;

public class AutoMapperConfigurations:Profile
{
    public AutoMapperConfigurations()
    {
        CreateMap<StudentRequestModel, Student>();
        CreateMap<Student, StudentResponseModel>();
        
        CreateMap<PayRequestModel, Pay>();
        CreateMap<Pay, PayResponseModel>();

        CreateMap<AgentRequestModel, Agent>();
        CreateMap<Agent, AgentResponseModel>()
            .ForMember(s=>s.Pay,opt=>opt.MapFrom(s=>s.Pays.Select(p=>new PayResponseModel
            {
                Id = p.Id,
                Student = p.Student.FullName,
                Agent = p.Agent.UserName,
                Date = p.Date,
                Sum = p.Sum
            })));
    }
}