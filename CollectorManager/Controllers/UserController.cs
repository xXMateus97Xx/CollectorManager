using CollectorManager.Services.Users;
using CollectorManager.Services.Users.DTOs;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace CollectorManager.Controllers;

public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.ChangePasswordAsync(request, cancellationToken);
        if (!result.IsSuccess)
        {
            (await request.ValidateAsync(cancellationToken)).AddToModelState(ModelState);
            return View(request);
        }

        return RedirectToAction("Index", "Home");
    }
}
