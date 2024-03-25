Integrated Disbursement and Information System (IDIS) Online is a program of the U.S. Department of Housing and Urban Development (HUD) Office of Community Planning and Development (CPD). A user of IDIS can download a *data extract* containing financial data for CDBG, HOME, ESG, HTF, and HOPWA.  Each file in this data extract starts with a key that describes the fixed-width columns contained in that file.  A data extract consists of many such files.

This program reads the key and the data in an IDIS data extract and produces a tab separated value (TSV) file.  The first row in the TSV contains the names of the fields in that extract to act as column headers.  

To use this utility:
1. Download `idis-converter.exe`.
2. Move `idis-converter.exe` from your Downloads directory to the directory that contains the an IDIS data extract.  This is the directory full of files with names like `Data_Extract_CDBG_Program-C24601-19.TXT`.
3. Double click on `idis-converter`. The utility will (attempt to) convert every file in that directory that ends in `.TXT`.  For each, it will make a new file with the same name except for the extension, which will be `.tsv`.  
4. When it's done running, you can open those TSV files in Microsoft Excel.  You can probably just double click on one, but if that doesn't work you can open Excel first and open the file from within Excel.
5. Excel's *Text Import Wizard* will pop up.  Step 1 should default to the data being *Delimited*, which is correct.  Click `Next`.
6. Step 2 should detect that *Tab* is the delimiter in this file, which is correct. Click `Next`.
7. In Step 3, click `Finish`. (If you want, you can specify what kind of data each column contains. That isn't my preferred workflow, but you do you.)
