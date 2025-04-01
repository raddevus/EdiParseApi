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

// MAX LENGTH of a ISA HEADER is 106 chars
CREATE TABLE IsaHeader (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
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
)


