using System.Globalization;

namespace kifuwarabe_uec11_gui.API
{
    /// <summary>
    /// 列アドレスだぜ☆（＾～＾） A ～ T で、 I が欠番だな☆（＾～＾）
    /// </summary>
    public class ColumnAddress
    {
        private int Number { get; set; }

        public ColumnAddress(int number)
        {
            this.Number = number;
        }

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

            // コンピューター囲碁では I は抜く慣習。
            if (8 <= column)
            {
                column--;
            }

            return (new ColumnAddress(column), start + 1);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            // 65はAsciiCodeのA。コンピューター囲碁では I は抜く慣習。
            return ((char)(65 + (this.Number < 8 ? this.Number : this.Number + 1))).ToString(CultureInfo.CurrentCulture);
        }
    }
}
