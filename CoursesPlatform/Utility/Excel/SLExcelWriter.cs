using System;
using System.Collections.Generic;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using HRsystem.Models;

namespace OpenXMLExcel.SLExcelUtility
{
    public class SLExcelWriter
    {
        private string ColumnLetter(int intCol)
        {
            var intFirstLetter = ((intCol) / 676) + 64;
            var intSecondLetter = ((intCol % 676) / 26) + 64;
            var intThirdLetter = (intCol % 26) + 65;

            var firstLetter = (intFirstLetter > 64) ? (char)intFirstLetter : ' ';
            var secondLetter = (intSecondLetter > 64) ? (char)intSecondLetter : ' ';
            var thirdLetter = (char)intThirdLetter;

            return string.Concat(firstLetter, secondLetter, thirdLetter).Trim();
        }

        private Cell CreateTextCell(string header, UInt32 index, string text)
        {
            var cell = new Cell
            {
                DataType = CellValues.InlineString,
                CellReference = header + index
                //,StyleIndex = (UInt32Value)1U
            };

            var istring = new InlineString();
            var t = new Text { Text = text };
            istring.AppendChild(t);
            cell.AppendChild(istring);
            return cell;
        }

        public byte[] GenerateExcel(SLExcelData data)
        {
            var stream = new MemoryStream();
            var document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);

            var workbookpart = document.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            var sheetData = new SheetData();

            worksheetPart.Worksheet = new Worksheet(sheetData);

            var sheets = document.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());

            var sheet = new Sheet() { Id = document.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1, Name = data.SheetName??"Sheet 1" };
            sheets.AppendChild(sheet);


            var stylesPart = workbookpart.AddNewPart<WorkbookStylesPart>();
            stylesPart.Stylesheet = new Stylesheet();
            // create fills
            stylesPart.Stylesheet.Fills = new Fills();

            // create a solid red fill
            var solidRed = new PatternFill() { PatternType = PatternValues.Solid };
            solidRed.ForegroundColor = new ForegroundColor { Rgb = HexBinaryValue.FromString("FFFF0000") }; // red fill
            solidRed.BackgroundColor = new BackgroundColor { Indexed = 64 };

            stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.None } }); // required, reserved by Excel
            stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.Gray125 } }); // required, reserved by Excel
            stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = solidRed });
            stylesPart.Stylesheet.Fills.Count = 3;

            // Add header
            UInt32 rowIdex = 0;
            var row = new Row { RowIndex = ++rowIdex/*,StyleIndex= 1*/ };
            sheetData.AppendChild(row);
            var cellIdex = 0;

            foreach (var header in data.Headers)
            {
                row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++), rowIdex, header??string.Empty));
            }
            if (data.Headers.Count > 0)
            {
                // Add the column configuration if available
                if (data.ColumnConfigurations != null)
                {
                    var columns = (Columns)data.ColumnConfigurations.Clone();
                    worksheetPart.Worksheet
                        .InsertAfter(columns, worksheetPart.Worksheet.SheetFormatProperties);
                }
            }

            // Add sheet data
            foreach (var rowData in data.DataRows)
            {
                cellIdex = 0;
                row = new Row { RowIndex = ++rowIdex };
                sheetData.AppendChild(row);
                foreach (var callData in rowData)
                {
                    var cell = CreateTextCell(ColumnLetter(cellIdex++), rowIdex, callData??string.Empty);
                    row.AppendChild(cell);
                }
            }

            workbookpart.Workbook.Save();
            document.Close();

            return stream.ToArray();
        }


        public byte[] ExcelTemplate(List<Columns_Excel> columns)
        {
            var stream = new MemoryStream();
            var document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);

            var workbookpart = document.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
          

            var sheetData = new SheetData();

            worksheetPart.Worksheet = new Worksheet();

            //----------------------custom width cols-----------------------------------
            Columns cols = new Columns();
            UInt32Value i = 1;
            foreach (var header in columns)
            {
                cols.Append(new Column() { Min = i, Max = i, Width = header.Width, CustomWidth = true });
                i++;
            }
            worksheetPart.Worksheet.Append(cols);
            worksheetPart.Worksheet.Append(sheetData);

            var sheets = document.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());
            
            var sheet = new Sheet()
            {
                Id = document.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Template"
            };
            sheets.AppendChild(sheet);


            var stylesPart = workbookpart.AddNewPart<WorkbookStylesPart>();
            stylesPart.Stylesheet = CreateStylesheet(); //new Stylesheet();

           

            // Add header
            UInt32 rowIdex = 0;
            var row = new Row { RowIndex = ++rowIdex };
            sheetData.AppendChild(row);
            var cellIdex = 0;
            var dropdownIdex_start = columns.Count+40;

          

            //----------------------create text for headers-----------------------------------
            foreach (var header in columns)
            {
                row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++), rowIdex, header.Colum_name));
            }

            int drops_index = 2;
            int general_index = 1;

            DataValidations dataValidations = new DataValidations();
            bool put_validations = false;
            //----------------------generate Dropdowns-----------------------------------
            foreach (var drop in columns)
            {
                if (drop.DropDown_List != null)
                {
                    put_validations = true;

                    var dropdownIdex = dropdownIdex_start;
                    row = new Row { RowIndex = ++rowIdex };
                    sheetData.AppendChild(row);

                    // Add sheet data
                    foreach (var rowData in drop.DropDown_List)
                    {
                        var cell = CreateTextCell(ColumnLetter(dropdownIdex++), rowIdex, rowData);
                        row.AppendChild(cell);
                    }

                    string start_letter = "$" + ColumnLetter(dropdownIdex_start) + "$" + drops_index;
                    string end_letter = "$" + ColumnLetter(dropdownIdex-1) + "$" + drops_index;
                    string formual = "Template!" + start_letter + ":" + end_letter;

                    //DefinedNames definedNamesCol = new DefinedNames();    //Create the collection
                    //DefinedName definedName = new DefinedName()
                    //{ Name = drop.Colum_name, Text = formual };       // Create a new range
                    //definedNamesCol.Append(definedName);                  // Add it to the collection
                    //workbookpart.Workbook.Append(definedNamesCol);                     // Add collection to the workbook  

                    string col_name = ColumnLetter(general_index - 1);

                    DataValidation dataValidation = 
                        new DataValidation()
                        {   Formula1 = new Formula1(formual),
                            Type = DataValidationValues.List,
                            AllowBlank = true,ShowInputMessage = true,ShowErrorMessage = true,
                            SequenceOfReferences = new ListValue<StringValue>() { InnerText = col_name + ":"+ col_name } };
                    dataValidations.Append(dataValidation);

                    drops_index++;
                }
                general_index++;
            }
            if (put_validations)
            {
                worksheetPart.Worksheet.Append(dataValidations);
            }

            workbookpart.Workbook.Save();
            document.Close();

            return stream.ToArray();
        }



        private static Stylesheet CreateStylesheet()
        {
            Stylesheet stylesheet1 = new Stylesheet() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" } };
            stylesheet1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            stylesheet1.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");

            Fonts fonts1 = new Fonts() { Count = (UInt32Value)1U, KnownFonts = true };

            Font font1 = new Font();
            FontSize fontSize1 = new FontSize() { Val = 11D };
            Color color1 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName1 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering1 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme1 = new FontScheme() { Val = FontSchemeValues.Minor };

            font1.Append(fontSize1);
            font1.Append(color1);
            font1.Append(fontName1);
            font1.Append(fontFamilyNumbering1);
            font1.Append(fontScheme1);

            fonts1.Append(font1);

            Fills fills1 = new Fills() { Count = (UInt32Value)5U };

            // FillId = 0
            Fill fill1 = new Fill();
            PatternFill patternFill1 = new PatternFill() { PatternType = PatternValues.None };
            fill1.Append(patternFill1);

            // FillId = 1
            Fill fill2 = new Fill();
            PatternFill patternFill2 = new PatternFill() { PatternType = PatternValues.Gray125 };
            fill2.Append(patternFill2);

            // FillId = 2,RED
            Fill fill3 = new Fill();
            PatternFill patternFill3 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor1 = new ForegroundColor() { Rgb = "FFFF0000" };
            BackgroundColor backgroundColor1 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill3.Append(foregroundColor1);
            patternFill3.Append(backgroundColor1);
            fill3.Append(patternFill3);

            // FillId = 3,BLUE
            Fill fill4 = new Fill();
            PatternFill patternFill4 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor2 = new ForegroundColor() { Rgb = "FF0070C0" };
            BackgroundColor backgroundColor2 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill4.Append(foregroundColor2);
            patternFill4.Append(backgroundColor2);
            fill4.Append(patternFill4);

            // FillId = 4,YELLO
            Fill fill5 = new Fill();
            PatternFill patternFill5 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor3 = new ForegroundColor() { Rgb = "FFFFFF00" };
            BackgroundColor backgroundColor3 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill5.Append(foregroundColor3);
            patternFill5.Append(backgroundColor3);
            fill5.Append(patternFill5);

            fills1.Append(fill1);
            fills1.Append(fill2);
            fills1.Append(fill3);
            fills1.Append(fill4);
            fills1.Append(fill5);

            Borders borders1 = new Borders() { Count = (UInt32Value)1U };

            Border border1 = new Border();
            LeftBorder leftBorder1 = new LeftBorder();
            RightBorder rightBorder1 = new RightBorder();
            TopBorder topBorder1 = new TopBorder();
            BottomBorder bottomBorder1 = new BottomBorder();
            DiagonalBorder diagonalBorder1 = new DiagonalBorder();

            border1.Append(leftBorder1);
            border1.Append(rightBorder1);
            border1.Append(topBorder1);
            border1.Append(bottomBorder1);
            border1.Append(diagonalBorder1);

            borders1.Append(border1);

            CellStyleFormats cellStyleFormats1 = new CellStyleFormats() { Count = (UInt32Value)1U };
            CellFormat cellFormat1 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U };

            cellStyleFormats1.Append(cellFormat1);

            CellFormats cellFormats1 = new CellFormats() { Count = (UInt32Value)4U };
            CellFormat cellFormat2 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U };
            CellFormat cellFormat3 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
            CellFormat cellFormat4 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)3U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
            CellFormat cellFormat5 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)4U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };

            cellFormats1.Append(cellFormat2);
            cellFormats1.Append(cellFormat3);
            cellFormats1.Append(cellFormat4);
            cellFormats1.Append(cellFormat5);

            CellStyles cellStyles1 = new CellStyles() { Count = (UInt32Value)1U };
            CellStyle cellStyle1 = new CellStyle() { Name = "Normal", FormatId = (UInt32Value)0U, BuiltinId = (UInt32Value)0U };

            cellStyles1.Append(cellStyle1);
            DifferentialFormats differentialFormats1 = new DifferentialFormats() { Count = (UInt32Value)0U };
            TableStyles tableStyles1 = new TableStyles() { Count = (UInt32Value)0U, DefaultTableStyle = "TableStyleMedium2", DefaultPivotStyle = "PivotStyleMedium9" };

            StylesheetExtensionList stylesheetExtensionList1 = new StylesheetExtensionList();

            StylesheetExtension stylesheetExtension1 = new StylesheetExtension() { Uri = "{EB79DEF2-80B8-43e5-95BD-54CBDDF9020C}" };
            stylesheetExtension1.AddNamespaceDeclaration("x14", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");

            stylesheetExtensionList1.Append(stylesheetExtension1);

            stylesheet1.Append(fonts1);
            stylesheet1.Append(fills1);
            stylesheet1.Append(borders1);
            stylesheet1.Append(cellStyleFormats1);
            stylesheet1.Append(cellFormats1);
            stylesheet1.Append(cellStyles1);
            stylesheet1.Append(differentialFormats1);
            stylesheet1.Append(tableStyles1);
            stylesheet1.Append(stylesheetExtensionList1);
            return stylesheet1;
        }
    }
}