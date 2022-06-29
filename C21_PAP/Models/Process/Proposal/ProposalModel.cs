using C21_PAP.Models.Agents;

namespace C21_PAP.Models.Process.Proposal;

public class ProposalModel
{
    public string _Id { get; set; }
    public AgentModel SellingAgent { get; set; }
    
    public DateTime AcceptedProposalDate { get; set; }
    
    public int AcceptedValue { get; set; }
    
    public int TotalComission { get; set; }
    
    public int ShareValue { get; set; }
    
    public DateTime ExpectedCPCVDate { get; set; }
    
}