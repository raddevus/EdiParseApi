using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

public class EdiElementContext : DbContext
{
    // The variable name must match the name of the table.
    public DbSet<EdiElement> EdiElement { get; set; }
    public string DbPath { get; }

    public EdiElementContext()
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
        @"CREATE TABLE EdiElement (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            SegmentId INTEGER,
            Name TEXT,
            Value TEXT
        );"
    };

}