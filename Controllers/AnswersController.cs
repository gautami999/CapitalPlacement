using Microsoft.AspNetCore.Mvc;
using CapitalPlacement.Repositories.Contract;


[ApiController]
[Route("api/[controller]")]
public class AnswersController : ControllerBase
{
    private readonly IRepository<AnswerModel> _answerRepository;

    public AnswersController(IRepository<AnswerModel> answerRepository)
    {
        _answerRepository = answerRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<AnswerModel>> GetAnswers()
    {
        return await _answerRepository.GetAllAsync();
    }

    [HttpPost]
    public async Task<ActionResult<AnswerModel>> CreateAnswer(AnswerDto answerDto)
    {
        var answer = new AnswerModel
        {
            QuestionId = answerDto.QuestionId,
            Value = answerDto.Value
        };

        await _answerRepository.CreateAsync(answer);

        return CreatedAtAction(nameof(GetAnswers), new { id = answer.Id }, answer);
    }
}