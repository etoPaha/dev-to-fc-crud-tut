using crud_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace crud_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostgresSettingController : Controller
{
    private readonly PostgresSettings _postgresSettings;

    public PostgresSettingController(PostgresSettings postgresSettings)
    {
        _postgresSettings = postgresSettings;
    }

    [HttpGet("get")]
    public IActionResult Get()
    {
        return Json(_postgresSettings);
    }
}