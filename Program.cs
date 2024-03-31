if (args.Length < 1)
{
    Console.WriteLine("Pass in a ZIP file with an IDIS data extract.");
}
else
{
    string zipFile = args[0];
    IdisExcelConverter.ConvertZip(zipFile, verbose: true);
}