using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Photo
{
    [Key]
    public int Id { get; set; }
    public string url { get; set; }
    public string Description { get; set; }
    public DateTime DateAdded { get; set; }
    public DateTime? DateUpdated { get; set; }
    public bool IsMain { get; set; }
    public string PublicId { get; set; }
    public bool IsApproved { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }


}