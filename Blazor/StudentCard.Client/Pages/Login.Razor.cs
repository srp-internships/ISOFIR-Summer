using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using StudentCard.Client.Application.Common.Interfaces.Services;
using StudentCard.Client.Resources;
using StudentCard.Domain;
using StudentCard.Domain.Models;

namespace StudentCard.Client.Pages;

public partial class Login:ComponentBase
{
    [Inject] private IAuthService _authService { get; set; } = null!;
    
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    
    [Inject] private ILocalStorageService _localStorageService{ get; set; } = null!;
    private string Username { get; set; } = string.Empty;
    private string Password { get; set; } = string.Empty;
    private string? ErrorMessage { get; set; }

    private async Task LoginAsync()
    {
        Console.WriteLine(Username);
        Console.WriteLine(Password);
        
        var result = await _authService.GetTokenAsync(Username, Password);
        if (result is OkResult<string> okResult)
        {
            await _localStorageService.SetItemAsStringAsync(LocalStorageKeys.Token, okResult.Result);
            var agentResult = await _authService.GetAgentFromTokenAsync(okResult.Result);
            if (agentResult is OkResult<Agent> agentOkResult)
            {
                await _localStorageService.SetItemAsync(LocalStorageKeys.Agent, agentOkResult.Result);
                _navigationManager.NavigateTo("/");
            }
            else
            {
                ErrorMessage = (result as ErrorResult)?.Message ?? "login failed";
            }

        }
        else
        {
            ErrorMessage = (result as ErrorResult)?.Message ?? "login failed";
        }

    }
}