using Microsoft.AspNetCore.Mvc;
using CapitalPlacement.Repositories.Contract;


[ApiController]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly IRepository<QuestionModel> _questionRepository;

    public QuestionsController(IRepository<QuestionModel> questionRepository)
    {
        _questionRepository = questionRepository;
    }

    // Implement GET, POST, PUT, and DELETE actions for CRUD operations on Questions
}