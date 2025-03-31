// Services/EdiParser.cs
public class EdiParser : IEdiParser
{
    private static readonly Dictionary<string, string[]> ElementNames = new Dictionary<string, string[]>();

    public EdiParser(){
        InitializeElementNames();
    }
    public async Task<List<EdiSegment>> ParseEdiAsync(string ediData)
    {
        var segments = new List<EdiSegment>();
        string[] lines = ediData.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            string[] elements = line.Split('*');
            if (elements.Length > 0)
            {
                var segment = new EdiSegment { Name = elements[0] };
                string[] elementNames = ElementNames.ContainsKey(segment.Name) ? ElementNames[segment.Name] : null;

                for (int i = 1; i < elements.Length; i++)
                {
                    string elementName = elementNames != null && i - 1 < elementNames.Length ? elementNames[i - 1] : $"Element {i}";
                    segment.Elements.Add(new EdiElement { Name = elementName, Value = elements[i] });
                }
                segments.Add(segment);
            }
        }
        
        return await Task.FromResult(segments);
    }
    private void InitializeElementNames()
    {
        ElementNames["ISA"] = new[] { "Authorization Info Qualifier", "Authorization Info", "Security Info Qualifier", "Security Info", "Interchange ID Qualifier", "Interchange Sender ID", "Interchange ID Qualifier", "Interchange Receiver ID", "Date", "Time", "Repetition Separator", "Control Version Number", "Control Number", "Acknowledgment Requested", "Usage Indicator", "Component Element Separator" };
        ElementNames["GS"] = new[] { "Functional ID Code", "Application Sender Code", "Application Receiver Code", "Date", "Time", "Group Control Number", "Responsible Agency Code", "Version/Release" };
        ElementNames["ST"] = new[] { "Transaction Set Identifier Code", "Transaction Set Control Number" };
        ElementNames["BIG"] = new[] { "Invoice Date", "Invoice Number", "Purchase Order Date", "Purchase Order Number", "Release Number", "Invoice Type Code", "Invoice Reason Code" };
        ElementNames["REF"] = new[] { "Reference Identification Qualifier", "Reference Identification" };
        ElementNames["N1"] = new[] { "Entity Identifier Code", "Name", "Identification Code Qualifier", "Identification Code" };
        ElementNames["N3"] = new[] { "Address Information" };
        ElementNames["N4"] = new[] { "City Name", "State or Province Code", "Postal Code" };
        ElementNames["ITD"] = new[] { "Terms Type Code", "Terms Basis Date Code", "Terms Discount Percent", "Terms Discount Due Date", "Terms Discount Days Due", "Terms Net Due Date", "Terms Net Days", "Terms Discount Amount", "Terms Deferred Due Date", "Percentage" };
        ElementNames["FOB"] = new[] { "Shipment Method of Payment", "Location Qualifier", "Description" };
        ElementNames["IT1"] = new[] { "Assigned ID", "Quantity Invoiced", "Unit of Measure Code", "Unit Price", "Product/Service ID Qualifier", "Product/Service ID", "Product/Service ID Qualifier", "Product/Service ID", "Purchase Order Line Number" };
        ElementNames["PID"] = new[] { "Item Description Type", "Product/Process Characteristic Code", "Agency Qualifier Code", "Product Description Code", "Description" };
        ElementNames["TDS"] = new[] { "Monetary Amount" };
        ElementNames["CAD"] = new[] { "Transportation Method/Type Code", "Equipment Description Code", "Equipment Initial", "Equipment Number" };
        ElementNames["ISS"] = new[] { "Quantity", "Unit or Basis for Measurement Code", "Weight", "Unit or Basis for Measurement Code" };
        ElementNames["CTT"] = new[] { "Number of Line Items" };
        ElementNames["SE"] = new[] { "Number of Included Segments", "Transaction Set Control Number" };
        ElementNames["GE"] = new[] { "Number of Transaction Sets Included", "Group Control Number" };
        ElementNames["IEA"] = new[] { "Number of Included Functional Groups", "Interchange Control Number" };
    }

}
