public class IdisRow
{
    public IdisRow(string line, IdisSchema schema)
    {
        Fields = new();
        foreach(IdisColumn column in schema.Columns)
        {            
            try
            {
                string field = line.Substring(column.StartPosition - 1, column.Length);
                Fields.Add(field);
            }
            catch (ArgumentOutOfRangeException)
            {
                IsValid = false;
                return;
            }            
        }
    }

    public List<string> Fields {get; set;}

    public bool IsValid {get; set;} = true;

    public override string ToString()
    {
        return string.Join("\t", Fields.ToArray());

    }
}