using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceManagerAPI.Data;
using FinanceManagerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Route("api/transactions")]
[ApiController]
[Authorize]
public class TransactionController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransactionController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] Transaction transaction)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == transaction.AccountId && a.UserId == userId);

        if (account == null)
            return BadRequest("Invalid account.");

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.Id }, transaction);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTransactions()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var transactions = await _context.Transactions
            .Where(t => t.UserId == userId) //    .Where(t => t.Account.UserId == userId)
            .ToListAsync();

        return Ok(transactions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransactionById(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null)
            return NotFound("Transaction not found.");

        return Ok(transaction);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransaction(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null)
            return NotFound("Transaction not found.");

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
