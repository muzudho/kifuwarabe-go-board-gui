namespace KifuwarabeUec11Gui.InputScript.InternationalGo
{
    using System;
    using System.Globalization;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Output;

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

        public new static (InternationalRowAddress, int) Parse(string text, int start, BoardModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var (rowAddress, next) = RowAddress.Parse(text, start, model);

            // 内部的には Z字方向式表記で持つ☆（＾～＾）
            return (new InternationalRowAddress(model.GetRowLastO0() - rowAddress.NumberO0), next);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public override string ToDisplay(BoardModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            // 上下をひっくり返して 1 を足す☆（＾ｑ＾）
            return (model.GetRowLastO0() - this.NumberO0 + 1).ToString(CultureInfo.CurrentCulture);
        }
    }
}
