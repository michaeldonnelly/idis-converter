if (args.Length < 1)
{
    string instructionsFile = "instructions.txt"; 
    string instructions = File.ReadAllText(instructionsFile); 
    Console.WriteLine(instructions);
}
else
{
    string zipFile = args[0];
    IdisExcelConverter.ConvertZip(zipFile, verbose: true);
}

Console.Write("\r\nPress any key to close....");
Console.ReadKey();
