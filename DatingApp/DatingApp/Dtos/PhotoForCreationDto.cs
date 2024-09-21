using System;
namespace DatingApp.Dtos;

public class PhotoForCreationDto
{
    public PhotoForCreationDto()
    {
        DateAdded = DateTime.Now;
    }
    public IFormFile File { get; set; }
    public string Description { get; set; }
    public DateTime DateAdded { get; set; }
    public string PublicId { get; set; }
}