namespace KifuwarabeUec11Gui.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text.Json;
    using KifuwarabeUec11Gui.Controller;

    /// <summary>
    /// `output.json` をこれで作ろうぜ☆（＾～＾）
    /// </summary>
    public class ApplicationObjectModel
    {
        /// <summary>
        /// コマンドの名前☆（＾～＾）
        /// </summary>
        public static string IntervalMsecOutsideName => "interval-msec";
        public static string PlyOutsideName => "ply";
        public static string BlackNameOutsideName => "b-name";
        public static string BlackTimeOutsideName => "b-time";
        public static string BlackHamaOutsideName => "b-hama";
        public static string WhiteNameOutsideName => "w-name";
        public static string WhiteTimeOutsideName => "w-time";
        public static string WhiteHamaOutsideName => "w-hama";
        public static string KomiOutsideName => "komi";

        /// <summary>
        /// オブジェクトの名前☆（＾～＾）　コマンド名とは別ものだぜ☆（＾～＾）
        /// </summary>
        public static string InfoOutsideName => "info";

        public ApplicationObjectModel()
        {
            // 盤☆（＾～＾）
            this.Board = new BoardModel();
            this.rowNumbersTrimed = new List<string>();

            this.Booleans = new Dictionary<string, PropertyBool>()
            {

            };

            this.Numbers = new Dictionary<string, PropertyNumber>()
            {
                // 何ミリ秒ごとに `input.txt` を確認するか（＾～＾）
                // 初期値は 2 秒☆（＾～＾）
                {IntervalMsecOutsideName, new PropertyNumber(2000) },

                // 何手目か。
                {PlyOutsideName, new PropertyNumber(0) },

                // 黒のアゲハマ。
                // 囲碁の白石がハマグリで作られているから石のことをハマと呼ぶが、取り揚げた石はアゲハマと呼ぶ☆（＾～＾）
                // でもアゲハマは、略してハマと呼ばれる☆（＾～＾）
                {BlackHamaOutsideName, new PropertyNumber(0) },

                // 白のアゲハマ。
                {WhiteHamaOutsideName, new PropertyNumber(0) },

                // 白のコミ。
                {KomiOutsideName, new PropertyNumber(6.5) },
            };

            this.Strings = new Dictionary<string, PropertyString>()
            {
                // 最後の着手点。
                {LastMoveMarkerController.OutsideName, new PropertyString("---") },

                // 黒の選手名。
                {BlackNameOutsideName, new PropertyString("player1") },

                // 黒の残り時間。
                {BlackTimeOutsideName, new PropertyString("00:00") },

                // 白の選手名。
                {WhiteNameOutsideName, new PropertyString("player2") },

                // 白の残り時間。
                {WhiteTimeOutsideName, new PropertyString("00:00") },

                // GUIの画面上にメッセージを表示するぜ☆（＾～＾）
                // 改行は "\n" にだけ対応☆（＾～＾） 代わりに "\v" （垂直タブ）は使えなくなった☆（＾～＾）
                {InfoOutsideName, new PropertyString("") },
            };

            this.StringLists = new Dictionary<string, PropertyStringList>()
            {
                // 各列番号☆（＾～＾）
                // I列がない☆（＾～＾）棋譜に I1 I11 I17 とか書かれたら字が汚くて読めなくなるのだろう☆（＾～＾）
                {
                    ColumnNumbersController.OutsideName,
                    new PropertyStringList(
                        new List<string>(){
                            "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T"
                        }
                    )
                },

                // 各行番号☆（＾～＾）半角スペースで位置調整するとか前時代的なことしてるんだろ、トリムしてないやつだぜ☆（＾～＾）
                // 1桁の数は、文字位置の調整がうまく行かないので勘で調整☆（＾～＾）盤の上側から順に並べろだぜ☆（＾～＾）
                // TODO JSONをデシリアライズできる方法が分かれば private アクセスにしたいが……☆（＾～＾）
                {
                    RowNumbersController.OutsideName,
                    new PropertyStringList(
                        new List<string>(){
                            "19", "18", "17", "16", "15", "14", "13", "12", "11", "10", "  9", "  8", "  7", "  6", "  5", "  4", "  3", "  2", "  1"
                        },
                        (value) =>
                        {
                            // 位置調整のためのスペースが含まれていると　検索のとき、やっかい☆（＾～＾）取り除いたリストも作っておくぜ☆（＾～＾）
                            this.rowNumbersTrimed = new List<string>();
                            for (int i = 0; i < value.Count; i++)
                            {
                                this.rowNumbersTrimed.Add( value[i].Trim());
                            }
                        }
                    )
                },

                // 星の番地☆（＾～＾）
                // 初期値は19路盤だぜ☆（＾～＾）
                // TODO JSONをデシリアライズできる方法が分かれば private アクセスにしたいが……☆（＾～＾）
                {
                    StarsController.OutsideName,
                    new PropertyStringList(
                        new List<string>(){
                            "D16", "K16", "Q16", "D10", "K10", "Q10", "D4", "K4", "Q4"
                        }
                    )
                },
            };

        }

        public BoardModel Board { get; set; }

        /// <summary>
        /// JSONに出力される書式も気にして　構造化している☆（＾～＾）
        /// TODO JSONをデシリアライズできる方法が分かれば private アクセスにしたいが……☆（＾～＾）
        /// </summary>
        public Dictionary<string, PropertyBool> Booleans { get; set; }
        public Dictionary<string, PropertyNumber> Numbers { get; set; }
        public Dictionary<string, PropertyString> Strings { get; set; }
        public Dictionary<string, PropertyStringList> StringLists { get; set; }

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

        public static ApplicationObjectModel Parse(string json)
        {
            Trace.WriteLine($"json input      | {json}");

            var option1 = new JsonSerializerOptions();
            option1.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            var newModel = JsonSerializer.Deserialize(json, typeof(ApplicationObjectModel), option1) as ApplicationObjectModel;

            {
                var option2 = new JsonSerializerOptions();
                option2.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                Trace.WriteLine($"json re         | {JsonSerializer.Serialize(newModel, option2)}");
            }

            return newModel;
        }

        public string ToJson()
        {
            var option = new JsonSerializerOptions();

            // JSON は JavaScript 由来だろ☆（＾～＾） JavaScript に合わせようぜ☆（＾～＾）
            // camelCase
            option.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            // インデントしようぜ☆（＾～＾）
            // option.WriteIndented = true;
            // インデントすると、 1, 1, 1, 1, …みたいなのが縦長に３６１行も出るので止めようぜ☆（＾～＾）？

            // 読取専用の項目は　無視しようぜ☆（＾～＾）と思ったら全部消えた……☆（＾～＾）
            // option.IgnoreReadOnlyProperties = true;

            return JsonSerializer.Serialize(this, option);
        }
    }
}
