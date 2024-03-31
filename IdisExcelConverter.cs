public static class IdisExcelConverter
{
    public static void ConvertZip(string zipFile, string? excelFile = null, bool verbose = false)
    {
        if (excelFile is null)
        {
            excelFile = OutputFile(zipFile, "xlsx");
        }

        if (FileIsLocked(excelFile))
        {
            string error = $"I can't write to {excelFile} because it's locked.";
            if (verbose)
            {
                Console.WriteLine(error);
                return;
            }
            else
            {
                throw new IOException(error);
            }
        }

        if (verbose)
        {
            Console.WriteLine($"Converting {zipFile}");
        }

        IdisDataSet idisDataSet = new(zipFile, verbose);
        Console.WriteLine("Auk");
        idisDataSet.SaveToExcel(excelFile, verbose);
        
        if (verbose)
        {
            Console.WriteLine($"Done");
        }
    }

    private static string OutputFile(string inputFile, string extension)
    {
        string outputFile = Path.GetFileNameWithoutExtension(inputFile);
        outputFile += "." + extension;
        string? directory = Path.GetDirectoryName(inputFile);
        if (!string.IsNullOrEmpty(directory))
        {
            outputFile = Path.Combine(directory, outputFile);
        }
        return outputFile;
    }

    private static bool FileIsLocked(string fileName)
    {
        if (!File.Exists(fileName))
        {
            return false;
        }
        try
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Write);
            fileStream.Close();
            return false;
        }
        catch (IOException)
        {
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }
}