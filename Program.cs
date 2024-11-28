if (args.Length < 1)
{
    string filePath = "after-install.txt"; 
    string instructions = File.ReadAllText(filePath); 
    Console.WriteLine(instructions);
    Console.ReadKey();
}
else
{
    string zipFile = args[0];
    IdisExcelConverter.ConvertZip(zipFile, verbose: true);
}