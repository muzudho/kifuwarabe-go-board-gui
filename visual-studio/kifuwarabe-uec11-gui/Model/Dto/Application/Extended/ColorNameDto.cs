namespace KifuwarabeGoBoardGui.Model.Dto
{
    /// <summary>
    /// 色名☆（＾～＾）
    /// </summary>
    public class ColorNameDto
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public ColorNameDto(string source)
        {
            this.Source = source;
        }

        /// <summary>
        /// スクリプトに記入された通りの文字列☆（＾～＾）
        /// </summary>
        public string Source { get; private set; }

        public string TrimHyphen()
        {
            return this.Source.Replace("-", "");
        }
    }
}
