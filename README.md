Integrated Disbursement and Information System (IDIS) Online is a program of the U.S. Department of Housing and Urban Development (HUD) Office of Community Planning and Development (CPD). A user of IDIS can download a *data extract* containing data for CDBG, HOME, ESG, HTF, and HOPWA.  Each file in this data extract starts with a key that describes the fixed-width columns contained in that file.  A data extract consists of many such files.

This program converts an IDIS extract into an Excel spreadsheet (XSLX).  Each file in the extract becomes a tab in the spreadsheet.  

Download [idis-converter-setup.exe](installer/Output/idis-converter-setup.exe) to install this utility, then follow [instructions.txt](instructions.txt) to convert a file.  

## Acknowledgments

To make the Excel workbook, IDIS Converter uses [EPPlusFree](https://www.nuget.org/packages/EPPlusFree/), a free fork of [EPPlus](https://epplussoftware.com/).

To compile the installer, it uses [Inno Setup](https://jrsoftware.org/isinfo.php).  


