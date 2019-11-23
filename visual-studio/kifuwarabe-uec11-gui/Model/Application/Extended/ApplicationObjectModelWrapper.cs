namespace KifuwarabeUec11Gui.Model
{
    using System;
    using System.Collections.Generic;
    using KifuwarabeUec11Gui.Controller;

    /// <summary>
    /// `output.json` をこれで作ろうぜ☆（＾～＾）
    /// 
    /// C#とRustで互換できるデータ形式にすること、JSONに出力される書式も気にして　構造化している☆（＾～＾）
    /// TODO JSONをデシリアライズできる方法が分かれば set を private アクセスにしたいが……☆（＾～＾）
    /// </summary>
    public class ApplicationObjectModelWrapper
    {
        public ApplicationObjectModelWrapper()
            : this(new ApplicationObjectModel())
        {

        }

        public ApplicationObjectModelWrapper(ApplicationObjectModel applicationObjectModel)
        {
            if (applicationObjectModel == null)
            {
                throw new ArgumentNullException(nameof(applicationObjectModel));
            }

            this.ApplicationObjectModel = applicationObjectModel;

            // 盤ラッパー☆（＾～＾）
            this.Board = new BoardModelWrapper(this.ApplicationObjectModel.Board);

            this.rowNumbersTrimed = new List<string>();
            this.StringLists[RowNumbersController.OutsideName].SetAfterSetsValueCallback((value) =>
            {
                // 位置調整のためのスペースが含まれていると　検索のとき、やっかい☆（＾～＾）取り除いたリストも作っておくぜ☆（＾～＾）
                this.rowNumbersTrimed = new List<string>();
                for (int i = 0; i < value.Count; i++)
                {
                    this.rowNumbersTrimed.Add(value[i].Trim());
                }
            });
            // イベント・ハンドラーを起こすぜ☆（＾～＾）
            this.StringLists[RowNumbersController.OutsideName].Value = this.StringLists[RowNumbersController.OutsideName].Value;
        }

        public ApplicationObjectModel ApplicationObjectModel { get; private set; }

        public BoardModelWrapper Board { get; private set; }

        /// <summary>
        /// 論理値型を持つウィジェット☆（＾～＾）
        /// </summary>
        public Dictionary<string, PropertyBool> Booleans
        {
            get
            {
                return this.ApplicationObjectModel.Booleans;
            }
            set
            {
                this.ApplicationObjectModel.Booleans = value;
            }
        }

        /// <summary>
        /// 数値型を持つウィジェット☆（＾～＾）
        /// </summary>
        public Dictionary<string, PropertyNumber> Numbers
        {
            get
            {
                return this.ApplicationObjectModel.Numbers;
            }
            set
            {
                this.ApplicationObjectModel.Numbers = value;
            }
        }

        /// <summary>
        /// 文字列型を持つウィジェット☆（＾～＾）
        /// </summary>
        public Dictionary<string, PropertyString> Strings
        {
            get
            {
                return this.ApplicationObjectModel.Strings;
            }
            set
            {
                this.ApplicationObjectModel.Strings = value;
            }
        }

        /// <summary>
        /// 文字列リストを持つウィジェット☆（＾～＾）
        /// </summary>
        public Dictionary<string, PropertyStringList> StringLists
        {
            get
            {
                return this.ApplicationObjectModel.StringLists;
            }
            set
            {
                this.ApplicationObjectModel.StringLists = value;
            }
        }


        public delegate void BoolCallback(PropertyBool value);
        public delegate void NumberCallback(PropertyNumber value);
        public delegate void StringCallback(PropertyString value);
        public delegate void StringListCallback(PropertyStringList value);

        public IPropertyValue ReadProperty(string name)
        {
            if (this.Booleans.ContainsKey(name))
            {
                return this.Booleans[name];
            }

            if (this.Numbers.ContainsKey(name))
            {
                return this.Numbers[name];
            }

            if (this.Strings.ContainsKey(name))
            {
                return this.Strings[name];
            }

            if (this.StringLists.ContainsKey(name))
            {
                return this.StringLists[name];
            }

            return null;
        }

        public void ReadProperty(string name, BoolCallback boolCallback, NumberCallback numberCallback, StringCallback stringCallback, StringListCallback stringListCallback)
        {
            if (boolCallback == null)
            {
                throw new ArgumentNullException(nameof(boolCallback));
            }

            if (numberCallback == null)
            {
                throw new ArgumentNullException(nameof(numberCallback));
            }

            if (stringCallback == null)
            {
                throw new ArgumentNullException(nameof(stringCallback));
            }

            if (stringListCallback == null)
            {
                throw new ArgumentNullException(nameof(stringListCallback));
            }

            if (this.Booleans.ContainsKey(name))
            {
                boolCallback(this.Booleans[name]);
            }
            else if (this.Numbers.ContainsKey(name))
            {
                numberCallback(this.Numbers[name]);
            }
            else if (this.Strings.ContainsKey(name))
            {
                stringCallback(this.Strings[name]);
            }
            else if (this.StringLists.ContainsKey(name))
            {
                stringListCallback(this.StringLists[name]);
            }
        }

        /// <summary>
        /// 各行番号☆（＾～＾）トリムしているやつだぜ☆（＾～＾）
        /// JSONにシリアライズ（出力）されないように、フィールドとメソッドにしているぜ☆（＾～＾）
        /// </summary>
        private List<string> rowNumbersTrimed;
        public List<string> GetRowNumbersTrimed()
        {
            return this.rowNumbersTrimed;
        }
    }
}
