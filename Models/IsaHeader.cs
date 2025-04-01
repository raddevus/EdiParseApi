//IsaHeader.cs

using Microsoft.AspNetCore.SignalR;

public class IsaHeader
{

    // MAX LENGTH of a ISA HEADER is 106 chars
    public int Id { get; set; }
    public string AuthInfoQualifier{get;set;} // two chars max (02 & 03 are possible)
    public string AuthInfo{get;set;}
    public string SecurityInfoQualifier{get;set;} // two chars (00 & 01 are possible)
    public string SecurityInfo{get;set;} // 10 chars max & any data
    public string SenderQualifier{get;set;}
    public string SenderId{get;set;}
    public string ReceiverQualifier{get;set;}
    public string ReceiverId{get;set;}
    public string IsaDate{get;set;}
    public string IsaTime{get;set;}
    public char RepetitionSeparator{get;set;} // 
    public string ControlVersionNumber{get;set;}
    public string ControlNumber{get;set;}
    public bool AckRequested{get;set;} // possible values are 0 & 1
    public char UsageIndicator{get;set;} // this indicates test/prod
    public char ComponentElementSeparator{get;set;} // char is UTF-16

}

// Authorization Info Qualifier: 00
//  - Authorization Info:           
//  - Security Info Qualifier: 00
//  - Security Info:           
//  - Interchange ID Qualifier: ZZ
//  - Interchange Sender ID: 901429JKLM     
//  - Interchange ID Qualifier: 01
//  - Interchange Receiver ID: 529700941      
//  - Date: 210323
//  - Time: 1435
//  - Repetition Separator: U
//  - Control Version Number: 00401
//  - Control Number: 000000377
//  - Acknowledgment Requested: 0
//  - Usage Indicator: P
//  - Component Element Separator: >
