using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

public class EdiDocumentContext : DbContext
{
    // The variable name must match the name of the table.
    public DbSet<EdiDocument> EdiDocument { get; set; }
    public string DbPath { get; }

    public EdiDocumentContext()
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
        @"CREATE TABLE EdiDocument (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            FileName TEXT,
            [FileCreated] NVARCHAR(30) check(length(FileCreated) <= 30)
        );"
    };

}