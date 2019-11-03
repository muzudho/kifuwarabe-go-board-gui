namespace KifuwarabeUec11Gui.Script.ExcelGo
{
    using System.Globalization;

    /// <summary>
    /// Excel式の列アドレスだぜ☆（＾～＾） A ～ T みたいなやつだぜ☆（＾～＾）
    /// 
    /// このオブジェクトは、国際式囲碁のことは知らなくていいように作れだぜ☆（＾～＾）
    /// </summary>
    public class ColumnAddress
    {
        public int Number { get; private set; }

        public ColumnAddress(int number)
        {
            this.Number = number;
        }

        /// <summary>
        /// Excel表記の列番号をパースするぜ☆（＾～＾）
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static (ColumnAddress, int) Parse(string text, int start)
        {
            if (text == null || text.Length < start + 1)
            {
                return (null, start);
            }

            // 最初の1文字はアルファベット。
            var column = ((char)text[start]) - 65; // 65はAsciiCodeのA。97はa。
            if (32 <= column)
            {
                // 小文字から大文字へ変換。
                column -= 32;
            }

            return (new ColumnAddress(column), start + 1);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public virtual string ToDisplay()
        {
            return ((char)(65 + this.Number)).ToString(CultureInfo.CurrentCulture);
        }
    }
}
