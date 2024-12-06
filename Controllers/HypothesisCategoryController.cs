//using AutoMapper;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sirius.Entities;
using sirius.Models.HypothesisCategory;
using sirius.Repositories.Implementations;

namespace sirius.Controllers;

[Route("api/[controller]s")]
[ApiController]

public class HypothesisCategoryController : ControllerBase
{
    public readonly IMapper _mapper;
    public readonly IHypothesisCategoryRepository _repository;

    public HypothesisCategoryController(IMapper mapper,IHypothesisCategoryRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryViewDto>>> GetAllHypothesisCategories()
    {
        var categories = _mapper.Map<List<CategoryViewDto>>(await _repository.GetAllHypothesisCategory());
        return Ok(categories);
    }
    
    [HttpGet("{id:long}")]
    public async Task<ActionResult<CategoryViewDto>> GetHypothesisCategoryById(long id)
    {
        var category = _mapper.Map<CategoryViewDto>(await _repository.FindHypothesisCategoryById(id));
        return Ok(category);
    }
    
    [HttpPost]
    public async Task<ActionResult<CategoryViewDto>> CreateHypothesisCategory([FromBody] CategoryCreateDto categoryCreate)
    {
        if (!ModelState.IsValid) return BadRequest();
        var category = _mapper.Map<HypothesisCategory>(categoryCreate);
        var createdCategory = await _repository.CreateHypothesisCategory(category);
        var createdCategoryView = _mapper.Map<CategoryViewDto>(createdCategory);
        return new CreatedAtActionResult(nameof(GetHypothesisCategoryById), "HypothesisCategory", new { id = category.Id }, createdCategoryView);
    }
    
    [HttpPut]
    public async Task<ActionResult<CategoryViewDto>> UpdateHypothesisCategory([FromBody] CategoryUpdateDto categoryUpdate)
    {
        if (!ModelState.IsValid) return BadRequest();
        var category = _mapper.Map<HypothesisCategory>(categoryUpdate);
        var updatedCategory = await _repository.UpdateHypothesisCategory(category);
        var updatedCategoryView = _mapper.Map<CategoryViewDto>(updatedCategory);
        return new CreatedAtActionResult(nameof(GetHypothesisCategoryById), "HypothesisCategory", new { id = updatedCategoryView.Id }, updatedCategoryView);
    }
    
    [HttpPut("deactivate/{id}")]
    public async Task<IActionResult> DeactivateHypothesisCategory(long id)
    {
        var result = await _repository.DeactivateHypothesisCategory(id);
        if (!result) return NotFound();

        return NoContent(); 
    }

    [HttpPut("activate/{id}")]
    public async Task<IActionResult> ActivateHypothesisCategory(long id)
    {
        var result = await _repository.ActivateHypothesisCategory(id);
        if (!result) return NotFound();

        return NoContent(); 
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHypothesisCategory(long id)
    {
        var result = await _repository.DeleteHypothesisCategory(id);
        if (!result) return NotFound();

        return NoContent(); 
    }
}

