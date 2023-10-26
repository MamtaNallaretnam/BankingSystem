﻿using BankingSystem.DbOperations;
using BankingSystem.Services;
using BankingSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers;

public class AccountController : Controller
{
    private readonly IAuthenticationService _authService;

    public AccountController(IAuthenticationService authService)
    {
        this._authService = authService;
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        if (HttpContext!.User.Identity!.IsAuthenticated)
            return RedirectToAction("Index", "Bank");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
    {
        var user = await _authService.Login(model.Username, model.Password);

        if (user == false)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }

        return RedirectToAction("Index", "Bank");

    }
    
    public async Task<IActionResult> Logout()
    {
        if (HttpContext!.User.Identity!.IsAuthenticated) 
            await _authService.Logout();
        return RedirectToAction("Index", "Bank");
    }

    [HttpGet]
    public async Task<IActionResult> CustomerSelfRegister()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CustomerSelfRegister(SelfRegistrationViewModel model)
    {
        return View(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> CustomerFinalizeSelfRegister()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CustomerFinalizeSelfRegister(FinalizeSelfRegisterViewModel model)
    {
        return View(model);
    }
    
    
}