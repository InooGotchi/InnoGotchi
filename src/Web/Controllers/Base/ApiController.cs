using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : Controller
{ }