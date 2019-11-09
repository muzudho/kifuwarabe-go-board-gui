namespace KifuwarabeUec11Gui.InputScript
{
    using System;
    using System.Collections.Generic;

    /// <summary>
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
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            SetsInstructionArgument setsInstructionArgument = null;

            // 最初のスペースは読み飛ばすぜ☆（＾～＾）
            var next = WhiteSpace.Parse2(text, start,
                (_, curr) =>
                {
                    // 次のイコールの手前までを読み取るぜ☆（＾～＾）
                    WordUpToDelimiter name;
                    (name, curr) = WordUpToDelimiter.Parse("=", text, curr);
                    if (name == null)
                    {
                        // 不一致☆（＾～＾）
                        return start;
                    }
                    else
                    {
                        // イコールは読み飛ばすぜ☆（＾～＾）
                        curr++;

                        // 最初のスペースは読み飛ばすぜ☆（＾～＾）
                        return WhiteSpace.Parse2(text, curr,
                            (_, curr) =>
                            {
                                // 行の残り全部を読み取るぜ☆（＾～＾）
                                string value = text.Substring(curr);
                                setsInstructionArgument = new SetsInstructionArgument(name.Text.Trim(), value.Trim());
                                return curr + value.Length;
                            });
                    }
                });

            // 列と行の両方マッチ☆（＾～＾）
            return (setsInstructionArgument, next);
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
