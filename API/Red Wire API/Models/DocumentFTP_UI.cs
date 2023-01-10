namespace Projet_File_Rouge.Object
{
    public class DocumentFTP_UI
    {
        private readonly int id;
        private readonly string documentRemotePath;
        private readonly string documentName;

        public DocumentFTP_UI(string _documentRemotePath, string _documentName)
        {
            documentRemotePath = _documentRemotePath;
            documentName = _documentName;
        }

        public int Id { get => id; }
        public string DocumentRemotePath { get => documentRemotePath; }
        public string DocumentName { get => documentName; }

        public string UploadString()
        {
            string[] splitTab = DocumentRemotePath.Split("\\");
            string result = "";

            for (int i = 3; i < splitTab.Length; i++)
            {
                result += splitTab[i];
                if (i < splitTab.Length - 1) result += "/";
            }
            return result;
        }
    }
}
