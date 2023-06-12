using crud_api.Data;
using crud_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crud_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public UserController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    /// <summary>
    /// Получить список пользователей
    /// </summary>
    // GET: api/users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }

    /// <summary>
    /// Получить пользователя
    /// </summary>
    /// <param name="id">id пользователя</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        return user;
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
        if (id != user.Id)
            return BadRequest();

        _dbContext.Entry(user).State = EntityState.Modified;

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _dbContext.Users.FindAsync();
        if (user == null)
            return NotFound();

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(int id)
    {
        return _dbContext.Users.Any(e => e.Id == id);
    }

    [HttpGet("hello")]
    public string Test()
    {
        return "Hello world!";
    }
}