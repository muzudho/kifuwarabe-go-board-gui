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
        /// 外向きの名前（JSON用）を、内向きの名前（XAML用）に変換だぜ☆（＾～＾）
        /// </summary>
        public Dictionary<string, string> ObjectRealNames
        {
            get
            {
                return this.ApplicationObjectModel.ObjectRealName;
            }
            set
            {
                this.ApplicationObjectModel.ObjectRealName = value;
            }
        }

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

        public (PropertyType, IPropertyValue) GetProperty(string name)
        {
            if (this.Booleans.ContainsKey(name))
            {
                return (PropertyType.Bool, this.Booleans[name]);
            }

            if (this.Numbers.ContainsKey(name))
            {
                return (PropertyType.Number, this.Numbers[name]);
            }

            if (this.Strings.ContainsKey(name))
            {
                return (PropertyType.StringType, this.Strings[name]);
            }

            if (this.StringLists.ContainsKey(name))
            {
                return (PropertyType.StringList, this.StringLists[name]);
            }

            // 該当なし。
            return (PropertyType.None, null);
        }

        public void GetProperty(string name, BoolCallback boolCallback, NumberCallback numberCallback, StringCallback stringCallback, StringListCallback stringListCallback)
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
        /// TODO 内部では Alias ではなく、 RealName の方を使いたい☆（＾～＾）
        /// </summary>
        /// <param name="appModel"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public void AddProperty(
            string alias,
            IPropertyValue value
        )
        {
            if (value is PropertyString)
            {
                this.Strings.Add(alias, (PropertyString)value);
            }
            else if (value is PropertyNumber)
            {
                this.Numbers.Add(alias, (PropertyNumber)value);
            }
            else if (value is PropertyBool)
            {
                this.Booleans.Add(alias, (PropertyBool)value);
            }
            else if (value is PropertyStringList)
            {
                this.StringLists.Add(alias, (PropertyStringList)value);
            }
        }

        /// <summary>
        /// TODO 内部では Alias ではなく、 RealName の方を使いたい☆（＾～＾）
        /// </summary>
        /// <param name="appModel"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public (PropertyType, IPropertyValue) RemoveProperty(
            string alias
        )
        {
            if (this.Strings.ContainsKey(alias))
            {
                var old = this.Strings[alias];
                this.Strings.Remove(alias);
                return (PropertyType.StringType, old);
            }
            else if (this.Numbers.ContainsKey(alias))
            {
                var old = this.Numbers[alias];
                this.Numbers.Remove(alias);
                return (PropertyType.Number, old);
            }
            else if (this.Booleans.ContainsKey(alias))
            {
                var old = this.Booleans[alias];
                this.Booleans.Remove(alias);
                return (PropertyType.Bool, old);
            }
            else if (this.StringLists.ContainsKey(alias))
            {
                var old = this.StringLists[alias];
                this.StringLists.Remove(alias);
                return (PropertyType.StringList, old);
            }

            return (PropertyType.None, null);
        }

        /// <summary>
        /// TODO 内部では Alias ではなく、 RealName の方を使いたい☆（＾～＾）
        /// </summary>
        /// <param name="appModel"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public (PropertyType, IPropertyValue) FindProperty(
            string alias
        )
        {
            if (this.Strings.ContainsKey(alias))
            {
                return (PropertyType.StringType, this.Strings[alias]);
            }

            if (this.Numbers.ContainsKey(alias))
            {
                return (PropertyType.Number, this.Numbers[alias]);
            }

            if (this.Booleans.ContainsKey(alias))
            {
                return (PropertyType.Bool, this.Booleans[alias]);
            }

            if (this.StringLists.ContainsKey(alias))
            {
                return (PropertyType.StringList, this.StringLists[alias]);
            }

            return (PropertyType.None, null);
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
