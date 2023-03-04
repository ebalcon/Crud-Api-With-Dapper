namespace CrudApiWithDapper.Models;

public class crewModel
{
    public int id { get; set; }
    public string name { get; set; } = String.Empty;
    public string description { get; set; } = String.Empty;
    public DateTime dateOfCreation { get; set; }
    public int isStillActif { get; set; }
}