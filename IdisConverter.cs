using System.IO.Compression;
using System.Runtime.CompilerServices;

public static class IdisConverter
{
    public static void ConvertFile(string inputFile, string? outputFile = null, bool verbose = false)
    {
        if (verbose)
        {
            Console.Write($"Converting {inputFile}: ");
        }
        StreamReader streamReader = new(inputFile);
        if (outputFile is null)
        {
            outputFile = OutputFile(inputFile);
        }
        ConvertStream(streamReader, outputFile, verbose);
    }

    private static void ConvertStream(StreamReader streamReader, string outputFile, bool verbose = false)
    {
        IdisSchema schema = new(streamReader);
        IdisFile file = new(streamReader, schema, "foo");
        file.SaveToTsv(outputFile);
    }

    private static string OutputFile(string inputFile)
    {
        string outputFile = Path.GetFileNameWithoutExtension(inputFile) + ".tsv";
        string? directory = Path.GetDirectoryName(inputFile);
        if (!string.IsNullOrEmpty(directory))
        {
            outputFile = Path.Combine(directory, outputFile);
        }
        return outputFile;
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
            string extension = Path.GetExtension(inputFile);
            if (extension == ".TXT")
            {
                ConvertFile(inputFile, verbose: verbose);
            }
            else
            {
                if (verbose)
                {
                    Console.WriteLine($"{inputFile} not a text file ({extension})");
                }

            }
        }
    }

    public static void ConvertZip(string zipFile, string? outputDirectory = null, bool verbose = false)
    {
        Console.WriteLine($"Extracting {zipFile}");
        ZipArchive zipArchive = ZipFile.Open(zipFile, ZipArchiveMode.Read);
        foreach (ZipArchiveEntry entry in zipArchive.Entries)
        {
            Stream stream = entry.Open();
            StreamReader streamReader = new(stream);
            string outputFile = OutputFile(entry.Name);
            if (verbose)
            {
                Console.Write($"Converting {entry.Name}:  ");
            }
            ConvertStream(streamReader, outputFile, verbose);
            if (verbose)
            {
                Console.WriteLine("done");
            }
        }
    }
}