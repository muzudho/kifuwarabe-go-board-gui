namespace KifuwarabeGoBoardGui.Model.Dto
{
    using System;

    /// <summary>
    /// 本名の対☆（＾～＾）
    /// </summary>
    public class AliasName
    {
        public AliasName(string value)
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
