using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sirius.Entities;
using sirius.Models.Expense;
using sirius.Repositories;

namespace sirius.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseRepository _repository;
    private readonly IMapper _mapper;

    public ExpenseController(IExpenseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllExpenses()
    {
        var expenses = await _repository.GetAllExpenses();
        return Ok(expenses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpenseById(long id)
    {
        var expense = await _repository.GetExpenseById(id);
        var expenseView = _mapper.Map<ExpenseViewDto>(expense);
        
        return Ok(expenseView);
    }

    [HttpPost]
    public async Task<IActionResult> AddExpense([FromForm] ExpenseCreateDto expenseCreateDto)
    {
        try
        {
            var expense = await _repository.CreateExpense(expenseCreateDto);
            
            var expenseView = _mapper.Map<ExpenseViewDto>(expense);
            return CreatedAtAction(nameof(GetExpenseById), new { id = expenseView.Id }, expenseView);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(long id, [FromForm] ExpenseUpdateDto expenseUpdateDto)
    {
        try
        {
            var existingExpense = await _repository.GetExpenseById(id);
            if (existingExpense == null) return NotFound();

            var updatedExpense = _mapper.Map(expenseUpdateDto, existingExpense);
            var result  = await _repository.UpdateExpense(updatedExpense);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(long id)
    {
        var result = await _repository.DeleteExpense(id);
        if (!result) return NotFound();
        
        return NoContent();
    }
    
}