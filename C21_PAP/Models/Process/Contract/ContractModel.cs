namespace C21_PAP.Models.Contract;

public class ContractModel
{
    public string _Id { get; set; }
    
    public ContractType ContractType { get; set; }
    
    public string RaisingAgent { get; set; }
    
    public string Status { get; set; }
    
    public string Regime { get; set; }
    
    public string TargetBusiness { get; set; }
    
    public string Observations { get; set; }
    
    public int PropertyValue { get; set; }
    
    public int Commission { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public int Months { get; set; }
    
    public String Key { get; set; }
}

public enum ContractType
{
    Internal,
    External
}