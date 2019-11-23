namespace KifuwarabeUec11Gui.InputScript
{
    using System;

    /// <summary>
    /// `JSON {ここにJSON}` みたいなコマンド☆（＾～＾）
    /// </summary>
    public class JsonInstructionArgument
    {
        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public string Json { get; private set; }

        public JsonInstructionArgument(string text)
        {
            this.Json = text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static (JsonInstructionArgument, int) Parse(string text, int start)
        {
            if (text==null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            var next = start;

            // 行の残り全部を読み取るぜ☆（＾～＾）
            string value = text.Substring(next);

            // 列と行の両方マッチ☆（＾～＾）
            return (new JsonInstructionArgument(value.Trim()), next);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            return $"{this.Json}";
        }
    }
}
