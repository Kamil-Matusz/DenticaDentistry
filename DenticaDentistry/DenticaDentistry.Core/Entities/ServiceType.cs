namespace DenticaDentistry.Core.Entities;

public class ServiceType
{
    public int ServiceTypeId { get; private set; }
    public string ServiceTypeName { get; private set; }

    public ServiceType(int serviceTypeId, string serviceTypeName)
    {
        ServiceTypeId = serviceTypeId;
        ServiceTypeName = serviceTypeName;
    }

    public void ChangeServiceName(string name)
    {
        ServiceTypeName = name;
    }
}