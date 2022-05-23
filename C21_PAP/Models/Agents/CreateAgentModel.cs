namespace C21_PAP.Models.Agents;

public class CreateAgentModel
{
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public DateTime? DataEntry { get; set; }
    
    public string Contacts { get; set; }
    
    public int Nif { get; set; }
    
    public string Type { get; set; }
}