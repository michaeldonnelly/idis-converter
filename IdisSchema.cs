public class IdisSchema
{
    public IdisSchema(StreamReader streamReader)
    {
        Columns = ParseColumns(streamReader);
    }

    public List<IdisColumn> Columns {get; set;}

    private List<IdisColumn> ParseColumns(StreamReader streamReader)
    {
        List<IdisColumn> columns = new();
        PrimeIdisFile(streamReader);
        string? line = streamReader.ReadLine();
        while (!string.IsNullOrWhiteSpace(line))
        {
            IdisColumn column;
            try
            {
                column = new(line);
            }
            catch(Exception ex)
            {
                string message = "Something is wrong with the schema formatting for this file.";
                throw new IdisException(message, ex, line);
            }
            columns.Add(column);
            line = streamReader.ReadLine();
        }
        return columns;
    }

    private void PrimeIdisFile(StreamReader streamReader)
    {
        for (int i = 0; i < 5; i++)
        {
            string? _ = streamReader.ReadLine();            
        }
    }

    public void ListColumns()
    {
        foreach (IdisColumn column in Columns)
        {
            Console.WriteLine($"{column.ToString()}");
        }
    }
}