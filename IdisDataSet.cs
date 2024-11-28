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
            
            try
            {
                IdisSchema schema = new(streamReader);
                IdisFile file = new(streamReader, schema, entry.Name);
                Files.Add(file);
            }
            catch(IdisException exception)
            {
                if(verbose)
                {
                    ConsoleColor currentForeground = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"    {exception.Message}");
                    Console.WriteLine($"    Error reading line:");
                    Console.WriteLine($"       {exception.Line}");
                    Console.WriteLine($"    This file cannot be included in the workbook.");
                    Console.ForegroundColor = currentForeground;
                }
                else
                {
                    throw;
                }               
            }
        }
    }

    public void SaveToExcel(string outputFile, bool verbose = false)
    {
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