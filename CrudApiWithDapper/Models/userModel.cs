namespace CrudApiWithDapper.Models;

public class userModel
{
    public int id { get; set; }
    public string username { get; set; } = String.Empty;
    public string gender { get; set; } = String.Empty;
    public DateTime birthDate { get; set; }
    public string birthPlace { get; set; } = String.Empty;
    public string email { get; set; } = String.Empty;
    public int crewId { get; set; }
}