using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;

public class IdisFile
{
    public IdisFile(StreamReader streamReader, IdisSchema schema, string name)
    {
        Schema = schema;
        Name = CleanName(name);
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

    private string CleanName(string fileName)
    {
        string name = Path.GetFileNameWithoutExtension(fileName);
        name = name.Replace("Data_Extract_","");
        return name;
    }

    public string Name {get; set;}
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

    public void SaveToWorksheet(ExcelPackage package, bool verbose = false)
    {
        if (verbose)
        {
            Console.WriteLine($"  Adding {Name}");
        }
        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(Name);
        AddHeaderToWorksheet(worksheet);
        int row = 1;
        foreach (IdisRow idisRow in Rows)
        {
            row += 1;
            AddRowToWorksheet(worksheet, idisRow, row);
        }
        FormatWorksheet(worksheet, row);
    }

    private void AddHeaderToWorksheet(ExcelWorksheet worksheet)
    {
        int column = 0;
        foreach (IdisColumn idisColumn in Schema.Columns)
        {          
            column += 1;  
            worksheet.Cells[1, column].Value = idisColumn.Name;        
        }

        ExcelRange range = worksheet.Cells[1, 1, 1, column];
        range.Style.Font.Bold = true;
    }

    private void AddRowToWorksheet(ExcelWorksheet worksheet, IdisRow idisRow, int row)
    {
            int column = 0;
            foreach (string field in idisRow.Fields)
            {
                column += 1;
                AddCellToWorksheet(worksheet, field, row, column);
                // worksheet.Cells[row, column].Value = field;
            } 

    }

    private void AddCellToWorksheet(ExcelWorksheet worksheet, string field, int row, int column)
    {
        IdisColumn idisColumn = Schema.Columns[column-1];
        String dataType = idisColumn.DataType;
        if ((!string.IsNullOrWhiteSpace(field)) && (dataType == "NUMBER"))
        {
            try
            {
                string trimmedField = field.Trim();
                if (idisColumn.Precision > 0)
                {
                    double numberField = double.Parse(trimmedField);
                    worksheet.Cells[row, column].Value = numberField;
                }
                else
                {
                    long numberField = long.Parse(trimmedField);
                    worksheet.Cells[row, column].Value = numberField;
                }
            }
            catch
            {
                worksheet.Cells[row, column].Value = field;
            }
        }
        else
        {
            worksheet.Cells[row, column].Value = field;
        }
    }

    private void FormatWorksheet(ExcelWorksheet worksheet, int lastRow)
    {
        if (lastRow < 2) // No data to format
        {
            return;
        }

        int column = 0;
        foreach (IdisColumn idisColumn in Schema.Columns)
        {
            column += 1;
            ExcelRange range = worksheet.Cells[2, column, lastRow, column];
            FormatColumn(range, idisColumn);
        }

        worksheet.View.FreezePanes(2, 1);
        // TODO: figure out column widths - can't use AutoFitColumns because it isn't in EPPlusFree
    }

    private void FormatColumn(ExcelRange range, IdisColumn idisColumn)
    {
        string dataType = idisColumn.DataType;
        string? format = null;
        if (dataType.Contains("NUMBER"))
        {
            format = FormatNumber(dataType, idisColumn.Precision);
        }
        else if (dataType.Contains("DATE"))
        {
            format = "yyyy-mm-dd";
        }
        else if (dataType.Contains("CHAR"))
        {
            format = "@";
        }
        
        if (format is not null)
        {
            range.Style.Numberformat.Format = format;
        }
    }

    private string FormatNumber(string dataType, int precision)
    {
        string format;
        if (precision > 0)
        {
            format = "#,##0.";
            for (int i = 0; i < precision; i++)
            {
                format += '0';
            }
        }
        else
        {
            format = "#0";
        }
        return format;
    }

    public void ListRows()
    {
        foreach (IdisRow row in Rows)
        {
            Console.WriteLine(row.ToString());
        }
    }
}