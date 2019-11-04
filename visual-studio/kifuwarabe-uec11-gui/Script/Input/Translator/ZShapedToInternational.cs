namespace KifuwarabeUec11Gui.Script.Translator
{
    using System;
    using KifuwarabeUec11Gui.Script.ZShaped;
    using KifuwarabeUec11Gui.Script.InternationalGo;

    public static class ZShapedToInternational
    {
        public static int ConvertIndex(int indexO0)
        {
            var zShapedRowO0 = indexO0 / InputScriptDocument.BoardSize;
            var zShapedColumnO0 = indexO0 % InputScriptDocument.BoardSize;

            return (InputScriptDocument.RowLastO0 - zShapedRowO0) + zShapedColumnO0;
        }

        /*
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
        */

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
            if (columnAddress == null)
            {
                throw new ArgumentNullException(nameof(columnAddress));
            }

            return new InternationalColumnAddress(columnAddress.NumberO0);
        }

        public static InternationalRowAddress ConvertRowAddress(RowAddress rowAddress)
        {
            if (rowAddress == null)
            {
                throw new ArgumentNullException(nameof(rowAddress));
            }

            return new InternationalRowAddress(rowAddress.NumberO0);
        }
    }
}
