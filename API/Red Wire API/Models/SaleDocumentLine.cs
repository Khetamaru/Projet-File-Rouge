using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Object
{
    public class SaleDocumentLine
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public int LineOrder { get; set; }
        public string? DescriptionClear { get; set; }
        public decimal NetPriceVatIncluded { get; set; }
    }
}
