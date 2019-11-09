namespace KifuwarabeUec11Gui.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// 値テキストがあって、表示・非表示を切り替えられるものは　これだぜ☆（＾～＾）
    /// 名前プロパティは持つなだぜ☆（＾～＾） JSONの出力書式が　イケてなくなるぜ☆（＾～＾）
    /// </summary>
    public abstract class PropertyValue
    {
        public abstract object Value { get; set; }

        public bool Visible { get; set; }

        /// <summary>
        /// ToString() はクラス名が返ってくるやつもあるんで、使わずに別の名前のメソッドにしろだぜ☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public abstract string ToText();
        public abstract bool ToBool();
        public abstract double ToNumber();
        public abstract List<string> ToTextList();
    }
}
