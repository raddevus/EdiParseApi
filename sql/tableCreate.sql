CREATE TABLE EdiSegments (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    SegmentIdentifier TEXT,
    Elements TEXT,
    ProcessedDate TEXT NOT NULL
);


CREATE TABLE EdiSegment (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    DocumentId INTEGER,
    Name TEXT
);

CREATE TABLE EdiElement (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    SegmentId INTEGER,
    Name TEXT,
    Value TEXT
);

CREATE TABLE EdiDocument (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FileName TEXT,
    [FileCreated] NVARCHAR(30) check(length(FileCreated) <= 30)
);


