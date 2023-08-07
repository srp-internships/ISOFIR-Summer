using System.Net;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using StudentCard.Client.Application.Common.Interfaces.Services;
using StudentCard.Client.Resources;
using StudentCard.Domain;
using StudentCard.Domain.RequestModels;
using StudentCard.Domain.ResponseModels;

namespace StudentCard.Client.Pages;

public partial class Pay : ComponentBase
{
    [Inject] private IJSRuntime _jsRuntime { get; set; } = null!;
    [Inject] private IAgentService _agentService { get; set; } = null!;
    [Inject] private IStudentService _studentService { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private ILocalStorageService _localStorageService { get; set; } = null!;
    [Inject] private IPayService _payService { get; set; } = null!;

    private List<AgentResponseModel> _agents = new();
    private List<StudentResponseModel> _students = new();

    private readonly PayRequestModel _pay = new();

    protected override async Task OnInitializedAsync()
    {
        var token = await _localStorageService.GetItemAsStringAsync(LocalStorageKeys.Token);
        if (token == null)
        {
            _navigationManager.NavigateTo("/");
            return;
        }

        var studentsResult = await _studentService.GetAllAsync(token);
        switch (studentsResult)
        {
            case OkResult<List<StudentResponseModel>> students:
                _students = students.Result;
                break;
            case ErrorResult errorResult:
                _navigationManager.NavigateTo(
                    $"/Error/{WebUtility.HtmlEncode(errorResult.Message + "  " + errorResult.Exception)}");
                break;
        }

        var agentsResult = await _agentService.GetAllAsync(token);
        switch (agentsResult)
        {
            case OkResult<List<AgentResponseModel>> agents:
                _agents = agents.Result;
                break;
            case ErrorResult errorResult:
                _navigationManager.NavigateTo(
                    $"/Error/{WebUtility.HtmlEncode(errorResult.Message + "  " + errorResult.Exception)}");
                break;
        }
    }

    private async Task SubmitAsync()
    {
        var token = await _localStorageService.GetItemAsStringAsync(LocalStorageKeys.Token);
        if (token == null)
        {
            _navigationManager.NavigateTo("/login");
            return;
        }

        var addAsync = await _payService.AddAsync(_pay, token);
        switch (addAsync)
        {
            case OkResult:
                await _jsRuntime.InvokeVoidAsync("alert", "Successful");
                _navigationManager.NavigateTo("/Pays");
                break;
            case ErrorResult errorResult:
                await _jsRuntime.InvokeVoidAsync("alert", "Error");
                _navigationManager.NavigateTo(
                    $"/error/{WebUtility.UrlEncode(errorResult.Message + Environment.NewLine + errorResult.Exception)}");
                break;
        }
    }
}