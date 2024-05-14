public class QuestionModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public QuestionType? Type { get; set; }
    public string? Options { get; set; } // For dropdown, multiple choice
}

public enum QuestionType
{
    Paragraph,
    YesNo,
    Dropdown,
    MultipleChoice,
    Date,
    Number
}