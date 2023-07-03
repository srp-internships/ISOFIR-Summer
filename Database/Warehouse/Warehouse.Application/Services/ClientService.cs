using AutoMapper;
using Warehouse.Application.Common.Interfaces.Repositories;
using Warehouse.Application.Common.Interfaces.Services;
using Warehouse.Application.RequestModels;
using Warehouse.Application.ResponseModels;
using WareHouse.Core.Models;

namespace Warehouse.Application.Services;

public class ClientService:IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    public ClientService(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    public void CreateOrEdit(ClientRequestModel clientDto)
    {
        var client = _mapper.Map<ClientRequestModel, Client>(clientDto);
        if (client!=null)
        {
            var old = _clientRepository.GetIEnumerable().FirstOrDefault(s => s.Id == client.Id);
            if (old!=null)
            {
                _mapper.Map(client, old);
            }
            else
            {
                _clientRepository.Add(client);
            }

            _clientRepository.Save();
        }
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }

    public List<ClientResponseModel> GetAll()
    {
        return _clientRepository.GetIEnumerable().Select(s=>_mapper.Map<Client,ClientResponseModel>(s)).ToList();
    }
}