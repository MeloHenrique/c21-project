using C21_PAP.Models.Contract;
using C21_PAP.Models.Property;
using C21_PAP.Models.Process.CPCV;
using C21_PAP.Models.Process.Deed;
using C21_PAP.Models.Process.Proposal;

namespace C21_PAP.Models.Process;

public class ProcessModel
{
    public string _Id { get; set; }
    
    public PropertyModel Imovel { get; set; }
    
    public ContractModel Contract { get; set; }
    
    public ProposalModel Proposal { get; set; }
    
    public CPCVModel Cpcv { get; set; }
    
    public DeedModel Deed { get; set; }
}

public class CreateProcessModel
{
    public CreatePropertyModel Property { get; set; } = null!;

    public CreateContractModel Contract { get; set; } = null!;


}