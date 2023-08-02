using System.Net;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using StudentCard.Client.Application.Common.Interfaces.Services;
using StudentCard.Client.Resources;
using StudentCard.Domain;
using StudentCard.Domain.RequestModels;

namespace StudentCard.Client.Pages.Admin;

public partial class CreateAgent : ComponentBase
{
    [Parameter]public int? id{get; set; }
    
    [Inject] private IAgentService _agentService { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private IJSRuntime _jsRuntime { get; set; } = null!;
    [Inject] private ILocalStorageService _localStorageService { get; set; } = null!;

    private AgentRequestModel _agent=new();

    private async Task SubmitAsync()
    {
        var token = await _localStorageService.GetItemAsStringAsync(LocalStorageKeys.Token);
        if (token == null)
        {
            _navigationManager.NavigateTo("/");
            return;
        }
        
        if (id is null or 0)
        {
            var addAsync = await _agentService.AddAsync(_agent ,token);
            switch (addAsync)
            {
                case OkResult:
                    await _jsRuntime.InvokeVoidAsync("alert", "Agent added successfully");
                    _navigationManager.NavigateTo("/agents");
                    break;
                case ErrorResult errorResult:
                    await _jsRuntime.InvokeVoidAsync("alert", "Error");
                    _navigationManager.NavigateTo($"/error/{WebUtility.UrlEncode(errorResult.Message+Environment.NewLine+errorResult.Exception)}");
                    break;
            }
        }
    }
}