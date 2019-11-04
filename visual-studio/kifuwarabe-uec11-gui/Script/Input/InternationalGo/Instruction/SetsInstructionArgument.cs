namespace KifuwarabeUec11Gui.Script.InternationalGo
{
    using System.Collections.Generic;

    /// <summary>
    /// `set b-name = Kifuwarabe` みたいなコマンド☆（＾～＾）
    /// </summary>
    public class SetsInstructionArgument
    {
        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public string Value { get; private set; }

        public SetsInstructionArgument(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static (SetsInstructionArgument, int) Parse(string text, int start)
        {
            var next = start;

            // 最初のスペースは読み飛ばすぜ☆（＾～＾）
            {
                (_, next) = WhiteSpace.Parse(text, next);
            }

            // 次のイコールの手前までを読み取るぜ☆（＾～＾）
            WordUpToDelimiter name;
            {
                (name, next) = WordUpToDelimiter.Parse("=", text, next);
                if (name == null)
                {
                    // 不一致☆（＾～＾）
                    return (null, start);
                }
            }

            // イコールは読み飛ばすぜ☆（＾～＾）
            next++;

            // 最初のスペースは読み飛ばすぜ☆（＾～＾）
            {
                (_, next) = WhiteSpace.Parse(text, next);
            }

            // 行の残り全部を読み取るぜ☆（＾～＾）
            string value = text.Substring(next);

            // 列と行の両方マッチ☆（＾～＾）
            return (new SetsInstructionArgument(name.Text.Trim(), value.Trim()), next);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            return $"{this.Name} = {this.Value}";
        }
    }
}
