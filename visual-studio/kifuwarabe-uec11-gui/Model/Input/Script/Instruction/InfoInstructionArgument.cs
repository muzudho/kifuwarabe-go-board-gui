namespace KifuwarabeUec11Gui.InputScript
{
    using System;

    /// <summary>
    /// `info I have a banana.` みたいなコマンド☆（＾～＾）
    /// </summary>
    public class InfoInstructionArgument
    {
        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public string Text { get; private set; }

        public InfoInstructionArgument(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static (InfoInstructionArgument, int) Parse(string text, int start)
        {
            if (text==null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            var next = start;

            // 行の残り全部を読み取るぜ☆（＾～＾）
            string value = text.Substring(next);

            // 列と行の両方マッチ☆（＾～＾）
            return (new InfoInstructionArgument(value.Trim()), next);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            return $"{this.Text}";
        }
    }
}
