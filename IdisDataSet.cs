using System.IO.Compression;
using OfficeOpenXml;
using OfficeOpenXml.Style;
public class IdisDataSet 
{
    public List<IdisFile> Files {get; set;} = new();

    public IdisDataSet(string zipFile, bool verbose = false)
    {
        ZipArchive zipArchive = ZipFile.Open(zipFile, ZipArchiveMode.Read);
        foreach (ZipArchiveEntry entry in zipArchive.Entries)
        {
            if (verbose)
            {
                Console.WriteLine($"  Extracting {entry.Name}");
            }
            Stream stream = entry.Open();
            StreamReader streamReader = new(stream);
            IdisSchema schema = new(streamReader);
            IdisFile file = new(streamReader, schema, entry.Name);
            Files.Add(file);
        }
    }

    public void SaveToExcel(string outputFile, bool verbose = false)
    {
        // If you're going to use this commercially, you need a licence to 
        // EPPlus. Read more here: 
        //   https://epplussoftware.com/en/LicenseOverview/LicenseFAQ
        
        // ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        ExcelPackage package = new();
        if (verbose)
        {
            Console.WriteLine($"Creating Excel workbook {outputFile}");
        }
        foreach (IdisFile idisFile in Files)
        {
            idisFile.SaveToWorksheet(package, verbose);
        }
        FileInfo fileInfo = new FileInfo(outputFile);
        package.SaveAs(fileInfo);
    }
}