namespace KifuwarabeUec11Gui.Model
{
    using System;

    /// <summary>
    /// エイリアスの対☆（＾～＾）
    /// </summary>
    public class RealName
    {
        public RealName(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Contains(".", StringComparison.Ordinal))
            {
                throw new ArgumentException($"{value} do not contains '.'.");
            }

            this.Value = value;
        }

        public string Value { get; set; }
    }
}
