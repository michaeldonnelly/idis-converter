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
    }
    public int StartPosition {get; set;}
    public int EndPosition {get; set;}
    public int Length {get; set;}
    public string Name {get; set;}
    public string DataType {get; set;}

    public override string ToString()
    {
        return $"{StartPosition}-{EndPosition} {Name} {DataType} {Length}";
    }
}