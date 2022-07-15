using C21_PAP.Models.TiposPropriedades;

namespace C21_PAP.Models.Property;

public class PropertyModel
{
    public string? _Id { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public string District { get; set; } = null!;
    
    public string County { get; set; } = null!; 
    
    public string Parish { get; set; } = null!;
    
    public string Street { get; set; } = null!;
    
    public string PropertyTypeId { get; set; } = null!;
}

public class CreatePropertyModel
{
    public string PostalCode { get; set; } = null!;

    public string District { get; set; } = null!;
    
    public string County { get; set; } = null!; 
    
    public string Parish { get; set; } = null!;
    
    public string Street { get; set; } = null!;
    
    public string PropertyTypeId { get; set; } = null!;
}