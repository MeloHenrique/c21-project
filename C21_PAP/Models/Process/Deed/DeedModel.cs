namespace C21_PAP.Models.Process.Deed;

public class DeedModel
{
    public string _Id { get; set; }
    
    public int FinalValue { get; set; }
    
    public int Commission { get; set; }
    
    public int SharedCommissionPaid { get; set; }
    
    public DateTime Date { get; set; }
}