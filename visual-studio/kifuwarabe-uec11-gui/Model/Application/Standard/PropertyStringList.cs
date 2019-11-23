namespace KifuwarabeUec11Gui.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 値テキストがあって、表示・非表示を切り替えられるものは　これだぜ☆（＾～＾）
    /// 名前プロパティは持つなだぜ☆（＾～＾） JSONの出力書式が　イケてなくなるぜ☆（＾～＾）
    /// </summary>
    public class PropertyStringList : IPropertyValue
    {
        /// <summary>
        /// Valueのセッターの後で☆（＾～＾）
        /// </summary>
        /// <param name="value">代入されてきた値のコピー。</param>
        public delegate void AfterSetsValueCallbackType(List<string> value);

        private List<string> innerValue;

        public PropertyStringList()
        {
            this.Value = new List<string>();
            this.Visible = true;
        }

        public PropertyStringList(string title, List<string> value)
        {
            this.Title = title;
            this.Value = value;
            this.Visible = true;
        }

        public static List<string> FromString(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            var columns = text.Split(',');
            for (int i = 0; i < columns.Length; i++)
            {
                // ダブル・クォーテーションに挟まれているという前提だぜ☆（＾～＾）
                var token = columns[i].Trim();
                if (1 < token.Length)
                {
                    columns[i] = token.Substring(1, token.Length - 2);
                }
            }

            return new List<string>(columns);
        }

        private AfterSetsValueCallbackType AfterSetsValueCallback { get; set; }
        public void SetAfterSetsValueCallback(AfterSetsValueCallbackType callback)
        {
            this.AfterSetsValueCallback = callback;
        }

        public string Title { get; set; }

        public List<string> Value
        {
            get
            {
                return this.innerValue;
            }
            set
            {
                this.innerValue = value;

                if (this.AfterSetsValueCallback != null)
                {
                    this.AfterSetsValueCallback(this.innerValue);
                }
            }
        }

        public bool Visible { get; set; }

        public string ValueAsText()
        {
            return $"\"{string.Join("\",\"", this.Value)}\"";
        }
    }
}
