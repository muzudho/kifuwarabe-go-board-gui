namespace KifuwarabeUec11Gui.Model
{
    // using System.Collections.Generic;

    /// <summary>
    /// 値テキストがあって、表示・非表示を切り替えられるものは　これだぜ☆（＾～＾）
    /// 名前プロパティは持つなだぜ☆（＾～＾） JSONの出力書式が　イケてなくなるぜ☆（＾～＾）
    /// </summary>
    public interface IPropertyValue
    {
        bool Visible { get; set; }

        /// <summary>
        /// ToString() はクラス名が返ってくるやつもあるんで、使わずに別の名前のメソッドにしろだぜ☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        string ToText();

        /*
        public abstract bool ToBool();
        public abstract double ToNumber();
        public abstract List<string> ToTextList();
        */
    }
}
