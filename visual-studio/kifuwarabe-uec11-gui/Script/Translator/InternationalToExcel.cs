namespace KifuwarabeUec11Gui.Script.Translator
{
    using System;
    using KifuwarabeUec11Gui.Script.ExcelGo;
    using KifuwarabeUec11Gui.Script.InternationalGo;

    public static class InternationalToExcel
    {
        public static CellRange ConvertCellRange(InternationalCellRange cellRange)
        {
            if (cellRange == null)
            {
                throw new ArgumentNullException(nameof(cellRange));
            }

            return new CellRange(
                ConvertCellAddress((InternationalCellAddress)cellRange.StartsCellAddress),
                ConvertCellAddress((InternationalCellAddress)cellRange.EndsCellAddress));
        }

        public static CellAddress ConvertCellAddress(InternationalCellAddress cellAddress)
        {
            if (cellAddress == null)
            {
                throw new ArgumentNullException(nameof(cellAddress));
            }

            return new CellAddress(
                    ConvertRowAddress((InternationalRowAddress)cellAddress.RowAddress),
                    ConvertColumnAddress((InternationalColumnAddress)cellAddress.ColumnAddress));
        }

        public static ColumnAddress ConvertColumnAddress(InternationalColumnAddress columnAddress)
        {
            if (columnAddress==null)
            {
                throw new ArgumentNullException(nameof(columnAddress));
            }

            return new ColumnAddress(columnAddress.Number);
        }

        public static RowAddress ConvertRowAddress(InternationalRowAddress rowAddress)
        {
            if (rowAddress == null)
            {
                throw new ArgumentNullException(nameof(rowAddress));
            }

            return new RowAddress(rowAddress.Number);
        }
    }
}
