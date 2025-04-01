using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

public class IsaHeaderContext : DbContext
{
    // The variable name must match the name of the table.
    public DbSet<IsaHeader> IsaHeader { get; set; }
    public string DbPath { get; }

    public IsaHeaderContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "edi.db");
        Console.WriteLine(DbPath);

        SqliteConnection connection = new SqliteConnection($"Data Source={DbPath}");
        // ########### FYI THE DB is created when it is OPENED ########
        connection.Open();
        SqliteCommand command = connection.CreateCommand();
        FileInfo fi = new FileInfo(DbPath);
        // check to see if db file is 0 length, if so, it needs to have table added
        if (fi.Length == 0){
            foreach (String tableCreate in allTableCreation){
                command.CommandText = tableCreate;
                command.ExecuteNonQuery();
            }
        }
    }

    // configures the database for use by EF
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
    protected String [] allTableCreation = {
        @"CREATE TABLE IsaHeader (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            DocumentId INTEGER,
            AuthInfoQualifier NVARCHAR(2) check(length(AuthInfoQualifier) <= 2), 
            AuthInfo NVARCHAR(10) check(length(AuthInfoQualifier) <= 10), 
            SecurityInfoQualifier NVARCHAR(2) check(length(SecurityInfoQualifier) <= 2), 
            SecurityInfo NVARCHAR(10) check(length(SecurityInfo) <= 10),
            SenderQualifier NVARCHAR(2) check(length(SenderQualifier) <= 2),
            SenderId NVARCHAR(15) check(length(SenderId) <= 15),
            ReceiverQualifier NVARCHAR(2) check(length(ReceiverQualifier) <= 2),
            ReceiverId NVARCHAR(15) check(length(ReceiverId) <= 15),
            IsaDate NVARCHAR(6) check(length(IsaDate) <= 6),
            IsaTime NVARCHAR(4) check(length(IsaTime) <= 4),
            RepetitionSeparator NVARCHAR(1) check(length(RepetitionSeparator) <= 1),
            ControlVersionNumber NVARCHAR(5) check(length(ControlVersionNumber) <= 5),
            ControlNumber NVARCHAR(9) check(length(ControlNumber) <= 9),
            AckRequested BOOLEAN,
            UsageIndicator NVARCHAR(1) check(length(UsageIndicator) <= 1),
            ComponentElementSeparator  NVARCHAR(1) check(length(ComponentElementSeparator) <= 1)
        );"
    };

}