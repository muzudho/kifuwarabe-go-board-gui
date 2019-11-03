namespace KifuwarabeUec11Gui.Script.Translator
{
    using System;
    using KifuwarabeUec11Gui.Script.ExcelGo;
    using KifuwarabeUec11Gui.Script.InternationalGo;

    public static class ExcelToInternational
    {
        public static InternationalCellRange ConvertCellRange(CellRange cellRange)
        {
            if (cellRange == null)
            {
                throw new ArgumentNullException(nameof(cellRange));
            }

            return new InternationalCellRange(
                ConvertCellAddress(cellRange.StartsCellAddress),
                ConvertCellAddress(cellRange.EndsCellAddress));
        }

        public static InternationalCellAddress ConvertCellAddress(CellAddress cellAddress)
        {
            if (cellAddress == null)
            {
                throw new ArgumentNullException(nameof(cellAddress));
            }

            return new InternationalCellAddress(
                    ConvertRowAddress(cellAddress.RowAddress),
                    ConvertColumnAddress(cellAddress.ColumnAddress));
        }

        public static InternationalColumnAddress ConvertColumnAddress(ColumnAddress columnAddress)
        {
            if (columnAddress==null)
            {
                throw new ArgumentNullException(nameof(columnAddress));
            }

            return new InternationalColumnAddress(columnAddress.Number);
        }

        public static InternationalRowAddress ConvertRowAddress(RowAddress rowAddress)
        {
            if (rowAddress == null)
            {
                throw new ArgumentNullException(nameof(rowAddress));
            }

            return new InternationalRowAddress(rowAddress.Number);
        }
    }
}
