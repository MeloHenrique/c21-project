namespace C21_PAP.Models.PostalCodeInfo;

public class PostalCodeInfo
{
    public string PostalCode { get; set; }
    public List<string> Districts { get; set; }
    public List<string> Counties { get; set; }
    public List<string> Parishes { get; set; }
    public List<string> Streets { get; set; }
}