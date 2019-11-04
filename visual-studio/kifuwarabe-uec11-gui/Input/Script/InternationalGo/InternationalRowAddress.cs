namespace KifuwarabeUec11Gui.InputScript.InternationalGo
{
    using System.Globalization;
    using KifuwarabeUec11Gui.InputScript;

    /// <summary>
    /// 国際囲碁の行番号表記☆（＾～＾）
    /// 行番号は下端が 1。
    /// 
    /// このオブジェクトは、Z字方向式で使い回せるものは　どんどん使い回せだぜ☆（＾～＾）
    /// </summary>
    public class InternationalRowAddress : RowAddress
    {
        public InternationalRowAddress(int rowNumberO0)
            : base(rowNumberO0)
        {
        }

        public new static (InternationalRowAddress, int) Parse(string text, int start)
        {
            var (rowAddress, next) = RowAddress.Parse(text, start);

            // 内部的には Z字方向式表記で持つ☆（＾～＾）
            return (new InternationalRowAddress(InputScriptDocument.RowLastO0 - rowAddress.NumberO0), next);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public override string ToDisplay()
        {
            // 上下をひっくり返して 1 を足す☆（＾ｑ＾）
            return (InputScriptDocument.RowLastO0 - this.NumberO0 + 1).ToString(CultureInfo.CurrentCulture);
        }
    }
}
