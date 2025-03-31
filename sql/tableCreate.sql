CREATE TABLE EdiSegments (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    SegmentIdentifier TEXT,
    Elements TEXT,
    ProcessedDate TEXT NOT NULL
);

CREATE TABLE EdiElement (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT,
    Value TEXT
);

CREATE TABLE EdiSegment (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    SegmentIdentifier TEXT,
    Elements TEXT,
    ProcessedDate TEXT NOT NULL
);