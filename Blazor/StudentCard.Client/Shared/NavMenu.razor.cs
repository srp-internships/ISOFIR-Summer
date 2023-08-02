using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using StudentCard.Client.Application.Common.Interfaces.Services;
using StudentCard.Client.Resources;
using StudentCard.Domain.Models;

namespace StudentCard.Client.Shared;

public partial class NavMenu
{
    private bool collapseNavMenu = true;
    private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

    private void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }

    private Agent? _agent = null;
    
    [Inject] private ILocalStorageService _localStorageService { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IAuthService _authService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        _agent = await _localStorageService.GetItemAsync<Agent>(LocalStorageKeys.Agent);
    }
}