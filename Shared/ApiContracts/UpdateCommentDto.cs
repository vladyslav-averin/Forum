namespace ApiContracts;

public class UpdateCommentDto
{
    public required string Body { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
}
