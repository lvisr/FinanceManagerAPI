using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceManagerAPI.Data;
using FinanceManagerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Route("api/accounts")]
[ApiController]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] Account account)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        account.UserId = userId;

        var user = await _context.Users.FindAsync(userId);

        _context.Accounts.Add(account);
        user.Accounts.Add(account);
        
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAccountById), new { id = account.Id }, account);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAccounts()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var accounts = await _context.Accounts.Where(a => a.UserId == userId).ToListAsync();
        return Ok(accounts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccountById(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

        if (account == null)
            return NotFound("Account not found.");

        return Ok(account);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAccount(int id, [FromBody] Account updatedAccount)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

        if (account == null)
            return NotFound("Account not found.");

        account.Name = updatedAccount.Name;
        account.Type = updatedAccount.Type;
        account.Balance = updatedAccount.Balance;

        await _context.SaveChangesAsync();
        return Ok(account);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccount(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

        if (account == null)
            return NotFound("Account not found.");

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
