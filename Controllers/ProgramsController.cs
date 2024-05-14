using Microsoft.AspNetCore.Mvc;
using CapitalPlacement.Repositories.Contract;


[ApiController]
[Route("api/[controller]")]
public class ProgramsController : ControllerBase
{
    private readonly IRepository<ProgramModel> _programRepository;

    public ProgramsController(IRepository<ProgramModel> programRepository)
    {
        _programRepository = programRepository;
    }

    [HttpGet("GetPrograms")]
    public async Task<IEnumerable<ProgramModel>> GetPrograms()
    {
        return await _programRepository.GetAllAsync();
    }

    [HttpGet("GetProgram/{id}")]
    public async Task<ActionResult<ProgramModel>> GetProgram(int id)
    {
        var program = await _programRepository.GetByIdAsync(id);
        if (program == null)
        {
            return NotFound();
        }

        return program;
    }

    [HttpPost("CreateProgram")]
    public async Task<ActionResult<ProgramModel>> CreateProgram(ProgramDto programDto)
    {
        var program = new ProgramModel
        {
            Title = programDto.Title,
            Description = programDto.Description,
            Questions = programDto.Questions.Select(q => new QuestionModel
            {
                Title = q.Title,
                Type = q.Type,
                Options = q.Options
            }).ToList()
        };

        await _programRepository.CreateAsync(program);

        return CreatedAtAction(nameof(GetProgram), new { id = program.Id }, program);
    }

    [HttpPut("UpdateProgram/{id}")]
    public async Task<IActionResult> UpdateProgram(int id, ProgramDto programDto)
    {
        var program = await _programRepository.GetByIdAsync(id);
        if (program == null)
        {
            return NotFound();
        }

        program.Title = programDto.Title;
        program.Description = programDto.Description;
        program.Questions = programDto.Questions.Select(q => new QuestionModel
        {
            Title = q.Title,
            Type = q.Type,
            Options = q.Options
        }).ToList();

        await _programRepository.UpdateAsync(program);

        return NoContent();
    }

    [HttpDelete("DeleteProgram/{id}")]
    public async Task<IActionResult> DeleteProgram(int id)
    {
        var program = await _programRepository.GetByIdAsync(id);
        if (program == null)
        {
            return NotFound();
        }

        await _programRepository.DeleteAsync(program.Id);

        return NoContent();
    }
}