using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Report.Domain.ActionResults;
using Report.Domain.Models;

namespace Report.Admin;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly DataContext _context = new DataContext();
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void TruncateDatabase(object sender, RoutedEventArgs e)
    {
        try
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("create database reportNew");
            }
            catch (Exception exception)
            {
                
            }
            await _context.Database.EnsureDeletedAsync();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.ToString(), "$$");
        }

        try
        {
            await _context.Database.EnsureCreatedAsync();
            await _context.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.ToString(), "$$");
            return;
        }

        MessageBox.Show("Вроде как сделано", "$$");
    }

    private async void CreateUser(object sender, RoutedEventArgs e)
    {
        var result = await RegisterAsync(Login.Text, Password.Text);

        if (result is ErrorResult errorResult)
        {
            MessageBox.Show(errorResult.Message+">>>"+errorResult.Exception, "$$");return;
        }
        MessageBox.Show("Ok", "$$");

    }
    
    
    
    public async Task<Result> RegisterAsync(string login, string password)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(s=>s.Login==login);
            if (user != null) return new ErrorResult("User already exists");

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user = new User
            {
                Login = login,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac
            .ComputeHash(Encoding.UTF8.GetBytes(password));
    }
}