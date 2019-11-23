namespace KifuwarabeUec11Gui.Model
{
    /// <summary>
    /// 値テキストがあって、表示・非表示を切り替えられるものは　これだぜ☆（＾～＾）
    /// 名前プロパティは持つなだぜ☆（＾～＾） JSONの出力書式が　イケてなくなるぜ☆（＾～＾）
    /// </summary>
    public interface IPropertyValue
    {
        bool Visible { get; set; }

        string Title { get; set; }

        /// <summary>
        /// ToString() はクラス名が返ってくるやつもあるんで、使わずに別の名前のメソッドにしろだぜ☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        string ValueAsText();
    }
}
