public static class IdisConverter
{
    public static void ConvertFile(string inputFile, string? outputFile = null)
    {
        if (string.IsNullOrEmpty(outputFile))
        {
            outputFile = Path.GetFileNameWithoutExtension(inputFile) + ".tsv";
            string? directory = Path.GetDirectoryName(inputFile);
            if (!string.IsNullOrEmpty(directory))
            {
                outputFile = Path.Combine(directory, outputFile);
            }
        }
        StreamReader streamReader = new(inputFile);
        IdisSchema schema = new(streamReader);
        IdisFile file = new(streamReader, schema);
        file.SaveToTsv(outputFile);
    }

    public static void ConvertDirectory(string directory, bool verbose = false)
    {
        if (verbose)
        {
            Console.WriteLine("Converting files in " + directory);
        }
        string[] inputFiles = Directory.GetFiles(directory);
        foreach(string inputFile in inputFiles)
        {
            if (verbose)
            {
                Console.Write(inputFile);
            }

            string extension = Path.GetExtension(inputFile);
            if (extension == ".TXT")
            {
                if (verbose)
                {
                    Console.WriteLine(" converting");
                }
                ConvertFile(inputFile);
            }
            else
            {
                if (verbose)
                {
                    Console.WriteLine($" not a text file ({extension})");
                }

            }
        }
    }
}