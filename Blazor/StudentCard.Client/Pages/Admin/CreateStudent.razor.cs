using System.Net;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using StudentCard.Client.Application.Common.Interfaces.Services;
using StudentCard.Client.Resources;
using StudentCard.Domain;
using StudentCard.Domain.RequestModels;

namespace StudentCard.Client.Pages.Admin;

public partial class CreateStudent:ComponentBase
{
    [Parameter] public int? id { get; set; }

    [Inject] private IStudentService _studentService { get; set; } = null!;
    [Inject] private ILocalStorageService _localStorageService { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private IJSRuntime _jsRuntime { get; set; } = null!;
    
    private StudentRequestModel _student = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

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
            var addAsync = await _studentService.AddAsync(_student, token);
            switch (addAsync)
            {
                case OkResult:
                    await _jsRuntime.InvokeVoidAsync("alert", "Student added successfully");
                    _navigationManager.NavigateTo("/students");
                    break;
                case ErrorResult errorResult:
                    await _jsRuntime.InvokeVoidAsync("alert", "Error");
                    _navigationManager.NavigateTo($"/error/{WebUtility.UrlEncode(errorResult.Message+Environment.NewLine+errorResult.Exception)}");
                    break;
            }
        }
    }
}