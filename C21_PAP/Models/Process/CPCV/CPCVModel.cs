namespace C21_PAP.Models.Process.CPCV;

public class CPCVModel
{
    public string _Id { get; set; }
    
    public DateTime Date { get; set; }
    
    public int Paid { get; set; }
    
    public int Comission { get; set; }
    
    public int PaidSharing { get; set; }
    
    public DateTime ExpectedCPCVDate { get; set; }
}