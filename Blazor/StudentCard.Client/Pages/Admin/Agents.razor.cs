using System.Net;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using StudentCard.Client.Application.Common.Interfaces.Services;
using StudentCard.Client.Resources;
using StudentCard.Domain;
using StudentCard.Domain.Models;
using StudentCard.Domain.ResponseModels;

namespace StudentCard.Client.Pages.Admin;

public partial class Agents:ComponentBase
{
    [Inject]private ILocalStorageService _localStorageService{ get; set; } = null!;
    [Inject]private IAgentService _agentService{ get; set; } = null!;
    [Inject]private NavigationManager _navigationManager{ get; set; } = null!;


    private List<AgentResponseModel> _agents = new();
    protected override async Task OnInitializedAsync()
    {
        var token = await _localStorageService.GetItemAsStringAsync(LocalStorageKeys.Token);
        if (token == null)
        {
            _navigationManager.NavigateTo("/");
            return;
        }
        
        var agentsResult = await _agentService.GetAllAsync(token);
        switch (agentsResult)
        {
            case OkResult<List<AgentResponseModel>> okResult:
                _agents = okResult.Result;
                break;
            case ErrorResult errorResult:
                _navigationManager.NavigateTo($"/{WebUtility.UrlEncode(errorResult.Message+Environment.NewLine+errorResult.Exception)}");
                break;
        }
    }
}