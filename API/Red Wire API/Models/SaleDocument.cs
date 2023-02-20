using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Object
{
    public class SaleDocument
    {
        public Guid Id { get; set; }

        public DateTime sysCreatedDate { get; set; }

        public DateTime sysModifiedDate { get; set; }

        public string? DocumentNumber { get; set; }

        public string? NumberPrefix { get; set; }

        public Decimal NumberSuffix { get; set; }

        public string? InvoicingAddress_ThirdName { get; set; }

        public string? InvoicingContact_Phone { get; set; }

        public string? InvoicingContact_CellPhone { get; set; }

        public string? InvoicingContact_Email { get; set; }

        public string? DeliveryAddress_Address1 { get; set; }

        public string? DeliveryAddress_ZipCode { get; set; }

        public string? DeliveryAddress_City { get; set; }

        public string? DeliveryAddress_State { get; set; }

        public string? InvoicingAddress_CountryIsoCode { get; set; }

        public Decimal CommitmentsBalanceDue { get; set; }

        public Decimal TotalDueAmount { get; set; }
    }
}
