
using AspDotNet9ApiSample.Data.Entities.CarEntities;
using AspDotNet9ApiSample.Data.Entities.CommonEntities;

namespace AspDotNet9ApiSample.Data.Entities.DealerEntities
{
    public class Dealer : BaseAuditableEntity
    {
        public string? CrmID { get; set; }
        public string? Name { get; set; }
        public string? AddressUnit { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZIP { get; set; }
        public string? Country { get; set; }
        public string? CRMEmail { get; set; }
        public bool IsAdfEmail { get; set; }
        public string? SecondaryCRMEmail { get; set; }
        public string? PhoneNumber { get; set; }
        public string? SecondaryPhoneNumber { get; set; }
        public string? DealerWebsiteURL { get; set; }
        public string? UploadFolderName { get; set; }
        public string? CRMSystem { get; set; }
        public string? Subscription { get; set; }
        public bool IsFranchise { get; set; }

        public IList<Car> Cars { get; } = new List<Car>();
    }
}
