public class IdisFile
{
    public IdisFile(StreamReader streamReader, IdisSchema schema)
    {
        Schema = schema;
        Rows = new();
        while (!streamReader.EndOfStream)
        {
            string? line = streamReader.ReadLine();
            if (!string.IsNullOrEmpty(line))
            {
                IdisRow row = new(line, schema);
                if (row.IsValid)
                {
                    Rows.Add(row);
                }
            }
        }
    }

    public List<IdisRow> Rows {get; set;}

    public IdisSchema Schema {get; set;}
    public void SaveToTsv(string fileName)
    {
        StreamWriter streamWriter = new(fileName);

        foreach (IdisColumn column in Schema.Columns)
        {
            streamWriter.Write(column.Name + "\t");
        }
        streamWriter.WriteLine();

        foreach (IdisRow row in Rows)
        {
            streamWriter.WriteLine(row.ToString());
        }
    }

    public void ListRows()
    {
        foreach (IdisRow row in Rows)
        {
            Console.WriteLine(row.ToString());
        }
    }
}