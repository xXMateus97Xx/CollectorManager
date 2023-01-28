using CollectorManager.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectorManager.Controllers;

[Authorize(AuthenticationSchemes = AuthenticationConstants.AuthenticationScheme)]
public abstract class BaseController : Controller
{
}
