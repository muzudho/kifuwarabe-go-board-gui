namespace KifuwarabeGoBoardGui.Model.Dto
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// `output.json` をこれで作ろうぜ☆（＾～＾）
    /// 
    /// C#とRustで互換できるデータ形式にすること、JSONに出力される書式も気にして　構造化している☆（＾～＾）
    /// TODO JSONをデシリアライズできる方法が分かれば set を private アクセスにしたいが……☆（＾～＾）
    /// </summary>
    public class ApplicationObjectDtoWrapper
    {
        public ApplicationObjectDtoWrapper()
            : this(new ApplicationDto())
        {
        }

        public ApplicationObjectDtoWrapper(ApplicationDto applicationObjectModel)
        {
            this.ApplicationObjectModel = applicationObjectModel ?? throw new ArgumentNullException(nameof(applicationObjectModel));

            // 盤ラッパー☆（＾～＾）
            this.Board = new BoardDtoWrapper(this.ApplicationObjectModel.Board);

            this.rowNumbersTrimed = new List<string>();

            // 特殊処理
            {
                this.GetStringList(ApplicationDto.RowNumbersRealName).SetAfterSetsValueCallback(this.TrimRowNumbers);
                // イベント・ハンドラーを起こすぜ☆（＾～＾）
                this.GetStringList(ApplicationDto.RowNumbersRealName).Value = this.GetStringList(ApplicationDto.RowNumbersRealName).Value;
            }

            // モデル変更のログを書き込むやつ☆（＾～＾）
            this.ModelChangeLogWriter = new ModelChangeLogWriter("model-change.log");
        }

        public void TrimRowNumbers(List<string> value)
        {
            // 位置調整のためのスペースが含まれていると　検索のとき、やっかい☆（＾～＾）取り除いたリストも作っておくぜ☆（＾～＾）
            this.rowNumbersTrimed = new List<string>();
            for (int i = 0; i < value.Count; i++)
            {
                this.rowNumbersTrimed.Add(value[i].Trim());
            }
        }

        /// <summary>
        /// モデル変更ログ を書き込むやつ☆（＾～＾）
        /// </summary>
        public ModelChangeLogWriter ModelChangeLogWriter { get; private set; }

        public ApplicationDto ApplicationObjectModel { get; private set; }

        public BoardDtoWrapper Board { get; private set; }

        /// <summary>
        /// UIタイマーのTick。
        /// </summary>
        public TimeSpan IntervalTimeSpan { get; set; }

        public int RowSize
        {
            get
            {
                return this.ApplicationObjectModel.GetRowSize();
            }
            set
            {
                this.ApplicationObjectModel.SetRowSize(value);
            }
        }

        public int ColumnSize
        {
            get
            {
                return this.ApplicationObjectModel.GetColumnSize();
            }
            set
            {
                this.ApplicationObjectModel.SetColumnSize(value);
            }
        }

        /// <summary>
        /// 19本の線を引くから20分割だが、符号の列を1つ足すぜ☆（＾～＾）
        /// </summary>
        public int GetRowDiv()
        {
            return this.ApplicationObjectModel.GetRowDiv();
        }

        /// <summary>
        /// 19本の線を引くから20分割だが、符号の列を1つ足すぜ☆（＾～＾）
        /// </summary>
        public int GetColumnDiv()
        {
            return this.ApplicationObjectModel.GetColumnDiv();
        }

        /// <summary>
        /// 19路盤の最終行のインデックス 0 から始まる（0 Origin）ので、 -1 する☆（＾～＾）
        /// </summary>
        public int GetRowLastO0()
        {
            return this.ApplicationObjectModel.GetRowLastO0();
        }

        /// <summary>
        /// 19路盤の最終行のインデックス 0 から始まる（0 Origin）ので、 -1 する☆（＾～＾）
        /// </summary>
        public int GetColumnLastO0()
        {
            return this.ApplicationObjectModel.GetColumnLastO0();
        }

        /// <summary>
        /// 石を置ける場所の数☆（＾～＾）
        /// </summary>
        public int GetCellCount()
        {
            return this.ApplicationObjectModel.GetCellCount();
        }

        public delegate void BoolCallback(PropertyBool value);
        public delegate void NumberCallback(PropertyNumber value);
        public delegate void StringCallback(PropertyString value);
        public delegate void StringListCallback(PropertyStringList value);

        public bool TryAddObjectRealName(AliasName aliasName, RealName realName)
        {
            return this.ApplicationObjectModel.ObjectRealName.TryAdd(aliasName.Value, realName.Value);
        }

        public bool ContainsKeyOfObjectRealName(AliasName aliasName)
        {
            return this.ApplicationObjectModel.ObjectRealName.ContainsKey(aliasName.Value);
        }

        public RealName GetObjectRealName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            AliasName aliasName = new AliasName(name);

            // エイリアスが設定されていれば変換するぜ☆（＾～＾）
            if (this.ApplicationObjectModel.ObjectRealName.ContainsKey(aliasName.Value))
            {
                return new RealName(this.ApplicationObjectModel.ObjectRealName[aliasName.Value]);
            }
            else
            {
                return new RealName(name);
            }
        }

        public bool ContainsKeyOfBooleans(RealName realName)
        {
            return this.ApplicationObjectModel.Booleans.ContainsKey(realName.Value);
        }

        public bool ContainsKeyOfNumbers(RealName realName)
        {
            return this.ApplicationObjectModel.Numbers.ContainsKey(realName.Value);
        }

        public bool ContainsKeyOfStrings(RealName realName)
        {
            return this.ApplicationObjectModel.Strings.ContainsKey(realName.Value);
        }

        public bool ContainsKeyOfStringLists(RealName realName)
        {
            return this.ApplicationObjectModel.StringLists.ContainsKey(realName.Value);
        }

        public PropertyBool GetBool(RealName realName)
        {
            return this.ApplicationObjectModel.Booleans[realName.Value];
        }

        public PropertyNumber GetNumber(RealName realName)
        {
            return this.ApplicationObjectModel.Numbers[realName.Value];
        }

        public PropertyString GetString(RealName realName)
        {
            return this.ApplicationObjectModel.Strings[realName.Value];
        }

        public PropertyStringList GetStringList(RealName realName)
        {
            return this.ApplicationObjectModel.StringLists[realName.Value];
        }

        public void AddBool(RealName realName, PropertyBool value)
        {
            this.ApplicationObjectModel.Booleans[realName.Value] = value;
        }

        public void AddNumber(RealName realName, PropertyNumber value)
        {
            this.ApplicationObjectModel.Numbers[realName.Value] = value;
        }

        public void AddString(RealName realName, PropertyString value)
        {
            this.ApplicationObjectModel.Strings[realName.Value] = value;
        }

        public void AddStringList(RealName realName, PropertyStringList value)
        {
            this.ApplicationObjectModel.StringLists[realName.Value] = value;
        }

        public bool RemoveBool(RealName realName)
        {
            return this.ApplicationObjectModel.Booleans.Remove(realName.Value);
        }

        public bool RemoveNumber(RealName realName)
        {
            return this.ApplicationObjectModel.Numbers.Remove(realName.Value);
        }

        public bool RemoveString(RealName realName)
        {
            return this.ApplicationObjectModel.Strings.Remove(realName.Value);
        }

        public bool RemoveStringList(RealName realName)
        {
            return this.ApplicationObjectModel.StringLists.Remove(realName.Value);
        }

        public delegate void SomeModelCallback(IPropertyValue value);
        public delegate void NoneModelCallback();

        public void MatchPropertyOption(
            RealName realName,
            SomeModelCallback someCallback,
            NoneModelCallback noneCallback
        )
        {
            if (this.ContainsKeyOfBooleans(realName))
            {
                someCallback(this.GetBool(realName));
            }
            else if (this.ContainsKeyOfNumbers(realName))
            {
                someCallback(this.GetNumber(realName));
            }
            else if (this.ContainsKeyOfStrings(realName))
            {
                someCallback(this.GetString(realName));
            }
            else if (this.ContainsKeyOfStringLists(realName))
            {
                someCallback(this.GetStringList(realName));
            }
            else
            {
                // 該当なし。
                noneCallback();
            }
        }

        public void MatchPropertyByType(
            RealName realName,
            BoolCallback boolCallback,
            NumberCallback numberCallback,
            StringCallback stringCallback,
            StringListCallback stringListCallback
        )
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

            if (this.ContainsKeyOfBooleans(realName))
            {
                boolCallback(this.GetBool(realName));
            }
            else if (this.ContainsKeyOfNumbers(realName))
            {
                numberCallback(this.GetNumber(realName));
            }
            else if (this.ContainsKeyOfStrings(realName))
            {
                stringCallback(this.GetString(realName));
            }
            else if (this.ContainsKeyOfStringLists(realName))
            {
                stringListCallback(this.GetStringList(realName));
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="realName"></param>
        /// <returns></returns>
        public void AddProperty(
            RealName realName,
            IPropertyValue value
        )
        {
            if (value is PropertyString)
            {
                this.AddString(realName, (PropertyString)value);
            }
            else if (value is PropertyNumber)
            {
                this.AddNumber(realName, (PropertyNumber)value);
            }
            else if (value is PropertyBool)
            {
                this.AddBool(realName, (PropertyBool)value);
            }
            else if (value is PropertyStringList)
            {
                this.AddStringList(realName, (PropertyStringList)value);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="realName"></param>
        /// <returns></returns>
        public void RemoveProperty(
            RealName realName,
            SomeModelCallback someCallback,
            NoneModelCallback noneCallback
        )
        {
            if (someCallback == null)
            {
                throw new ArgumentNullException(nameof(someCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            if (this.ContainsKeyOfStrings(realName))
            {
                var old = this.GetString(realName);
                this.RemoveString(realName);
                someCallback(old);
            }
            else if (this.ContainsKeyOfNumbers(realName))
            {
                var old = this.GetNumber(realName);
                this.RemoveNumber(realName);
                someCallback(old);
            }
            else if (this.ContainsKeyOfBooleans(realName))
            {
                var old = this.GetBool(realName);
                this.RemoveBool(realName);
                someCallback(old);
            }
            else if (this.ContainsKeyOfStringLists(realName))
            {
                var old = this.GetStringList(realName);
                this.RemoveStringList(realName);
                someCallback(old);
            }
            else
            {
                noneCallback();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="realName"></param>
        /// <returns></returns>
        public void FindProperty(
            RealName realName,
            SomeModelCallback someCallback,
            NoneModelCallback noneCallback
        )
        {
            if (someCallback == null)
            {
                throw new ArgumentNullException(nameof(someCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            if (this.ContainsKeyOfStrings(realName))
            {
                someCallback(this.GetString(realName));
            }
            else if (this.ContainsKeyOfNumbers(realName))
            {
                someCallback(this.GetNumber(realName));
            }
            else if (this.ContainsKeyOfBooleans(realName))
            {
                someCallback(this.GetBool(realName));
            }
            else if (this.ContainsKeyOfStringLists(realName))
            {
                someCallback(this.GetStringList(realName));
            }
            else
            {
                noneCallback();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="realName"></param>
        /// <returns></returns>
        public bool ContainsKeyOfProperty(RealName realName)
        {
            return this.ContainsKeyOfStrings(realName) ||
                this.ContainsKeyOfNumbers(realName) ||
                this.ContainsKeyOfBooleans(realName) ||
                this.ContainsKeyOfStringLists(realName);
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
