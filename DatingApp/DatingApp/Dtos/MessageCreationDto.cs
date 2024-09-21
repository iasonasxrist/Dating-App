using System;
namespace DatingApp.Dtos;

public class MessageCreationDto
{
    public MessageCreationDto()
    {
        MessageSent = DateTime.Now;
    }
    public int SenderId { get; set; }
    public int RecipientId { get; set; }
    public DateTime MessageSent { get; set; }
    public string Content { get; set; }
}