using System;

public class ProgramModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<QuestionModel>? Questions { get; set; }
}
