using CollectorManager.Models;
using CollectorManager.Services.AppContext;
using CollectorManager.Services.Auth;
using CollectorManager.Services.Auth.DTOs;
using CollectorManager.Services.Users;
using CollectorManager.Services.Users.DTOs;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace CollectorManager.Controllers;

public class HomeController : BaseController
{
    private readonly Services.Auth.IAuthenticationService _authenticationService;
    private readonly IAppContext _appContext;
    private readonly IUserService _userService;

    public HomeController(Services.Auth.IAuthenticationService authenticationService,
        IAppContext appContext,
        IUserService userService)
    {
        _authenticationService = authenticationService;
        _appContext = appContext;
        _userService = userService;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        if (!_appContext.IsAuthenticated)
            return RedirectToAction("Login");

        return RedirectToAction("Index", "Collection");
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
        if (_appContext.IsAuthenticated)
            return RedirectToAction("Index");

        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        if (_appContext.IsAuthenticated)
            return RedirectToAction("Index");

        var authResponse = await _authenticationService.AuthenticateAsync(loginRequest, cancellationToken);
        if (!authResponse.IsSuccess)
        {
            ModelState.AddModelError(nameof(LoginRequest.Username), "Usuário ou senha inválidos");
            return View(loginRequest);
        }

        var user = authResponse.Data ?? throw new Exception("No user returned");

        var claims = new List<Claim>();

        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Username, ClaimValueTypes.String, AuthenticationConstants.ClaimsIssuer));

        var userIdentity = new ClaimsIdentity(claims, AuthenticationConstants.AuthenticationScheme);
        var userPrincipal = new ClaimsPrincipal(userIdentity);

        var authenticationProperties = new AuthenticationProperties
        {
            IsPersistent = loginRequest.Remember,
            IssuedUtc = DateTime.UtcNow
        };

        await HttpContext.SignInAsync(AuthenticationConstants.AuthenticationScheme, userPrincipal, authenticationProperties);

        return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public IActionResult SignUp()
    {
        if (_appContext.IsAuthenticated)
            return RedirectToAction("Index");

        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp(SignUpRequest request, CancellationToken cancellationToken)
    {
        if (_appContext.IsAuthenticated)
            return RedirectToAction("Index");

        var result = await _userService.SignUpAsync(request, cancellationToken);

        if (!result.IsSuccess)
        {
            (await request.ValidateAsync(cancellationToken)).AddToModelState(ModelState);
            return View(request);
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(AuthenticationConstants.AuthenticationScheme);

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}