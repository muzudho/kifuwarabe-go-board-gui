namespace KifuwarabeUec11Gui.InputScript.Translator
{
    using System;
    using KifuwarabeUec11Gui.InputScript.InternationalGo;

    public static class ZShapedToInternational
    {
        public static int ConvertIndex(int zShapedIndexO0)
        {
            var zShapedRowO0 = zShapedIndexO0 / InputScriptDocument.ColumnSize;
            var zShapedColumnO0 = zShapedIndexO0 % InputScriptDocument.ColumnSize;

            return (InputScriptDocument.RowLastO0 - zShapedRowO0) * InputScriptDocument.ColumnSize + zShapedColumnO0;
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
