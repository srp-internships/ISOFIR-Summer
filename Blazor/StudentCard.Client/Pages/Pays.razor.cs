using System.Net;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using StudentCard.Client.Application.Common.Interfaces.Services;
using StudentCard.Client.Application.Services;
using StudentCard.Client.Resources;
using StudentCard.Domain;
using StudentCard.Domain.ResponseModels;

namespace StudentCard.Client.Pages;

public partial class Pays : ComponentBase
{
    [Inject] private IPayService _studentService { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private ILocalStorageService _localStorageService { get; set; } = null!;
    
    private List<PayResponseModel>? _pays;
    
    protected override async Task OnInitializedAsync()
    {
        var token = await _localStorageService.GetItemAsStringAsync(LocalStorageKeys.Token);
        if (token == null)
        {
            _navigationManager.NavigateTo("/");
            return;
        }


        var allAsync = await _studentService.GetAllAsync(token);
        switch (allAsync)
        {
            case OkResult<List<PayResponseModel>> okResult:
                _pays = okResult.Result;
                break;
            case ErrorResult errorResult:
                _navigationManager.NavigateTo(
                    $"/error/${WebUtility.UrlEncode(errorResult.Message + Environment.NewLine + errorResult.Exception)}");
                break;
        }
    }
}