namespace KifuwarabeUec11Gui.InputScript
{
    using System;

    /// <summary>
    /// 次のようなコマンド☆（＾～＾）
    /// 
    /// `put black to K10`
    /// `put white to L11`
    /// 
    /// 構造としては
    /// 
    /// `put {name} to {cellAddress}`
    /// 
    /// だぜ☆（＾～＾）
    /// </summary>
    public class PutsInstructionArgument
    {
        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public string Destination { get; private set; }

        public PutsInstructionArgument(string name, string destination)
        {
            this.Name = name;
            this.Destination = destination;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static (PutsInstructionArgument, int) Parse(string text, int start)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            PutsInstructionArgument putsInstructionArgument = null;

            // Trace.WriteLine($"Text            | [{text}]");
            var next = WhiteSpace.Parse(text, start,
                (_, curr) =>
                {
                    // 最初のスペースは読み飛ばした☆（＾～＾）

                    var objectName = string.Empty;

                    // 次の `to` の手前までを読み取るぜ☆（＾～＾）
                    return WordUpToDelimiter.Parse("to", text, curr, (leftSide, curr) =>
                    {
                        // Trace.WriteLine($"Left side       | [{leftSide.Text}], curr={curr}");

                        // 左辺の、次のドットの手前までを読み取るぜ☆（＾～＾）
                        objectName = leftSide.Text.Trim();
                        // Trace.WriteLine($"objectName      | {objectName}");

                        // `to` は読み飛ばすぜ☆（＾～＾）
                        curr += "to".Length;
                        // Trace.WriteLine($"curr            | {curr}");

                        // 次のスペースは読み飛ばすぜ☆（＾～＾）
                        return WhiteSpace.Parse(text, curr,
                            (_, curr) =>
                            {
                                // 行の残り全部を読み取るぜ☆（＾～＾）
                                string destination = text.Substring(curr);
                                // Trace.WriteLine($"value           | {value}");

                                // 列と行の両方マッチ☆（＾～＾）
                                putsInstructionArgument = new PutsInstructionArgument(objectName, destination.Trim());
                                return curr + destination.Length;
                            });
                    });
                });

            return (putsInstructionArgument, next);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            return $"{this.Name} = {this.Destination}";
        }
    }
}
