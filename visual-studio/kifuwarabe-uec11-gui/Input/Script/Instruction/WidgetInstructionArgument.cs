namespace KifuwarabeUec11Gui.InputScript
{
    using System;

    /// <summary>
    /// 次のようなコマンド☆（＾～＾）
    /// 
    /// `set b-name = Kifuwarabe`
    /// `set b-name.visible = true`
    /// 
    /// 構造としては
    /// 
    /// `widget {name}.{property} = {value}`
    /// 
    /// だぜ☆（＾～＾）`.{property}` が省略されている場合、`.value` を補うぜ☆（＾～＾）
    /// </summary>
    public class WidgetInstructionArgument
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

        public WidgetInstructionArgument(string name, string property, string value)
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
        public static (WidgetInstructionArgument, int) Parse(string text, int start)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            WidgetInstructionArgument widgetInstructionArgument = null;

            // 最初のスペースは読み飛ばすぜ☆（＾～＾）
            var next = WhiteSpace.Parse2(text, start,
                (_, curr) =>
                {
                    // 次のドットの手前までを読み取るぜ☆（＾～＾）
                    WordUpToDelimiter name;
                    (name, curr) = WordUpToDelimiter.Parse(".", text, curr);
                    if (name == null)
                    {
                        // 不一致☆（＾～＾）
                        return start;
                    }
                    else
                    {
                        // ドットは読み飛ばすぜ☆（＾～＾）
                        curr++;

                        // 次のスペースは読み飛ばすぜ☆（＾～＾）
                        return WhiteSpace.Parse2(text, curr,
                            (_, curr) =>
                            {
                                // 次のイコールの手前までを読み取るぜ☆（＾～＾）
                                WordUpToDelimiter property;
                                (property, curr) = WordUpToDelimiter.Parse("=", text, curr);
                                if (property == null)
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

                                            // 列と行の両方マッチ☆（＾～＾）
                                            widgetInstructionArgument = new WidgetInstructionArgument(name.Text.Trim(), property.Text.Trim(), value.Trim());
                                            return curr + value.Length;
                                        });
                                }
                            });
                    }
                });

            return (widgetInstructionArgument, next);
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
