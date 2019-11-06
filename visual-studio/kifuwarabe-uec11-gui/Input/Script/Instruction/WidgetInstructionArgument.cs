namespace KifuwarabeUec11Gui.InputScript
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// `widget b-name.visible = true` みたいなコマンド☆（＾～＾）
    /// 構造としては `widget {name}.{property} = {value}` だぜ☆（＾～＾）
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
            if (text==null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            var next = start;

            // 最初のスペースは読み飛ばすぜ☆（＾～＾）
            {
                (_, next) = WhiteSpace.Parse(text, next);
            }

            // 次のドットの手前までを読み取るぜ☆（＾～＾）
            WordUpToDelimiter name;
            {
                (name, next) = WordUpToDelimiter.Parse(".", text, next);
                if (name == null)
                {
                    // 不一致☆（＾～＾）
                    return (null, start);
                }
            }

            // ドットは読み飛ばすぜ☆（＾～＾）
            next++;

            // 次のスペースは読み飛ばすぜ☆（＾～＾）
            {
                (_, next) = WhiteSpace.Parse(text, next);
            }

            // 次のイコールの手前までを読み取るぜ☆（＾～＾）
            WordUpToDelimiter property;
            {
                (property, next) = WordUpToDelimiter.Parse("=", text, next);
                if (property == null)
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
            return (new WidgetInstructionArgument(name.Text.Trim(), property.Text.Trim(), value.Trim()), next);
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
