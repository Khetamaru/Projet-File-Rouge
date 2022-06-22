using System;

namespace Projet_File_Rouge.EBPObject
{
    public class SaleDocumentLine
    {
        public Guid DocumentId { get; set; }
        public int LineOrder { get; set; }
        public string DescriptionClear { get; set; }
        public Decimal NetPriceVatIncluded { get; set; }

        public SaleDocumentLine(Guid DocumentId,
                                int LineOrder,
                                string DescriptionClear,
                                Decimal NetPriceVatIncluded)
        {
            this.DocumentId = DocumentId;
            this.LineOrder = LineOrder;
            this.DescriptionClear = DescriptionClear;
            this.NetPriceVatIncluded = NetPriceVatIncluded;
        }

        public string NetPriceVatIncludedFormated => Math.Round(NetPriceVatIncluded, 2) + " €";
    }

    public enum SaleDocumentLineEnum
    {
        documentId,
        lineOrder,
        descriptionClear,
        netPriceVatIncluded
    }
}
