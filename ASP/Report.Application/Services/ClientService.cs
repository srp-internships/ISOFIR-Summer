﻿using AutoMapper;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Domain.ActionResults;
using Report.Domain.Models;

namespace Report.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    public ClientService(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    public async Task<Result> CreateOrUpdateAsync(ClientRequestModel clientDto)
    {
        try
        {
            var client = _mapper.Map<ClientRequestModel, Client>(clientDto);
            if (client == null)
            {
                return new ErrorResult(new Exception(), "Невозможно обработать ваши данные");
            }

            var old = await _clientRepository.GetByIdAsync(client.Id);
            if (old != null)
            {
                old.Name = client.Name;
            }
            else
            {
                await _clientRepository.AddAsync(client);
            }

            await _clientRepository.SaveChangesAsync();
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
            await _clientRepository.RemoveAsync(id);
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
            var result = await _clientRepository.GetAllAsync();
            return new OkResult<List<ClientResponseModel>>(result
                .Select(s => _mapper.Map<Client, ClientResponseModel>(s)).ToList());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> GetClientsForSelectAsync()
    {
        try
        {
            var result = await _clientRepository.GetAllAsync();
            return new OkResult<List<GetClientForSelectResponseModel>>(result
                .Select(s => _mapper.Map<Client, GetClientForSelectResponseModel>(s)).ToList());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }
}