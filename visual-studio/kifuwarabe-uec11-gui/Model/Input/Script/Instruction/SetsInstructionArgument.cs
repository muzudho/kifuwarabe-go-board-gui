namespace KifuwarabeUec11Gui.InputScript
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// 次のようなコマンド☆（＾～＾）
    /// 
    /// `set b-name = Kifuwarabe`
    /// `set b-name.visible = true`
    /// 
    /// 構造としては
    /// 
    /// `set {name}.{property} = {value}`
    /// 
    /// だぜ☆（＾～＾）`.{property}` が省略されている場合、`.value` を補うぜ☆（＾～＾）
    /// しかし☆
    /// 
    /// `set b-name = 1.5`
    /// 
    /// イコールの後ろにドットが現れたら嫌だよな☆（＾～＾）
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
        public string Property { get; private set; }

        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public string Value { get; private set; }

        public SetsInstructionArgument(string name, string property, string value)
        {
            this.Name = name;
            this.Property = property;
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

            // Trace.WriteLine($"Text            | [{text}]");
            var next = WhiteSpace.Parse(text, start,
                (_, curr) =>
                {
                    // 最初のスペースは読み飛ばした☆（＾～＾）

                    var objectName = string.Empty;
                    var propertyName = string.Empty;

                    // 次のイコールの手前までを読み取るぜ☆（＾～＾）
                    return WordUpToDelimiter.Parse("=", text, curr, (leftSide, curr) =>
                    {
                        // Trace.WriteLine($"Left side       | [{leftSide.Text}], curr={curr}");

                        // 左辺の、次のドットの手前までを読み取るぜ☆（＾～＾）
                        var leftSideStart = 0;
                        var leftSideCurr = WordUpToDelimiter.Parse(".", leftSide.Text, leftSideStart, (leftOfDot, curr) =>
                        {
                            if (leftOfDot == null)
                            {
                                Trace.WriteLine($"Info            | No dot. curr={curr}");
                                // ドット無いぜ……☆（＾～＾）じゃあ `.value` があることにして進めるからな☆（＾～＾）
                                objectName = leftSide.Text.Trim();
                                propertyName = "value";
                            }
                            else
                            {
                                Trace.WriteLine($"Info            | Found dot. curr={curr} leftOfDot.Text=[{leftOfDot.Text}] leftSide.Text=[{leftSide.Text}] leftSide.Text.Substring(curr + 1)=[{leftSide.Text.Substring(curr + 1)}]");
                                // とりあえず２つだけ見るぜ☆（＾～＾）それ以降は知らん☆（＾～＾）
                                objectName = leftOfDot.Text.Trim();
                                propertyName = leftSide.Text.Substring(curr + 1).Trim();
                            }

                            return leftSide.Text.Length;
                        });
                        // Trace.WriteLine($"objectName      | {objectName}");
                        // Trace.WriteLine($"propertyName    | {propertyName}");

                        // イコールは読み飛ばすぜ☆（＾～＾）
                        curr++;
                        // Trace.WriteLine($"curr            | {curr}");

                        // 次のスペースは読み飛ばすぜ☆（＾～＾）
                        return WhiteSpace.Parse(text, curr,
                            (_, curr) =>
                            {
                                // 行の残り全部を読み取るぜ☆（＾～＾）
                                string value = text.Substring(curr);
                                // Trace.WriteLine($"value           | {value}");

                                // 列と行の両方マッチ☆（＾～＾）
                                setsInstructionArgument = new SetsInstructionArgument(objectName, propertyName, value.Trim());
                                return curr + value.Length;
                            });
                    });
                });

            return (setsInstructionArgument, next);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            return $"{this.Name}.{this.Property} = {this.Value}";
        }
    }
}
