using System;

namespace Projet_File_Rouge.EBPObject
{
    public class SaleDocumentLine
    {
        public Guid DocumentId { get; set; }
        public int LineOrder { get; set; }
        public string DescriptionClear { get; set; }

        public SaleDocumentLine(Guid DocumentId,
                                int LineOrder,
                                string DescriptionClear)
        {
            this.DocumentId = DocumentId;
            this.LineOrder = LineOrder;
            this.DescriptionClear = DescriptionClear;
        }
    }

    public enum SaleDocumentLineEnum
    {
        documentId,
        lineOrder,
        descriptionClear
    }
}
