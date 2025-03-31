public class EdiDocument
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FileCreated{get;set;}

    public EdiDocument()
    {
        
    }
    public EdiDocument(string filename)
    {
        FileName = filename;
        FileCreated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}