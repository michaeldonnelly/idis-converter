public class IdisColumn
{
    public IdisColumn(string line)
    {
        string[] pieces = line.Split(' ');
        StartPosition = Int32.Parse(pieces[0].Split('-')[0]);
        EndPosition = Int32.Parse(pieces[0].Split('-')[1]);
        Length = EndPosition - StartPosition + 1;

        int position = 1;
        while (string.IsNullOrWhiteSpace(pieces[position]))
        {
            position += 1;
        }
        Name = pieces[position];

        position += 1;
        while (string.IsNullOrWhiteSpace(pieces[position]))
        {
            position += 1;
        }
        DataType = pieces[position];

        position += 1;
        while (string.IsNullOrWhiteSpace(pieces[position]))
        {
            position += 1;
        }
        string size = pieces[position];
        if (size.Contains(','))
        {
            Precision = Int32.Parse(size.Split(',')[1]);
        }
    }
    public int StartPosition {get; set;}
    public int EndPosition {get; set;}
    public int Length {get; set;}
    public string Name {get; set;}
    public string DataType {get; set;}
    public int Precision {get; set;} = 0; 

    public override string ToString()
    {
        return $"{StartPosition}-{EndPosition} {Name} {DataType} {Length}";
    }
}