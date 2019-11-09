namespace KifuwarabeUec11Gui.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text.Json;

    /// <summary>
    /// `output.json` をこれで作ろうぜ☆（＾～＾）
    /// </summary>
    public class ApplicationObjectModel
    {
        public ApplicationObjectModel()
        {
            // 盤☆（＾～＾）
            this.Board = new BoardModel();
            this.rowNumbersTrimed = new List<string>();

            this.Properties = new Dictionary<string, PropertyValue>()
            {
                // 各列番号☆（＾～＾）
                // I列がない☆（＾～＾）棋譜に I1 I11 I17 とか書かれたら字が汚くて読めなくなるのだろう☆（＾～＾）
                {
                    "column-numbers",
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
                    "row-numbers",
                    new PropertyStringList(
                        new List<string>(){
                            "19", "18", "17", "16", "15", "14", "13", "12", "11", "10", "  9", "  8", "  7", "  6", "  5", "  4", "  3", "  2", "  1"
                        },
                        (value) =>
                        {
                            // 位置調整のためのスペースが含まれていると　検索のとき、やっかい☆（＾～＾）取り除いたリストも作っておくぜ☆（＾～＾）
                            {
                                for (int i = 0; i < value.Count; i++)
                                {
                                    value[i] = value[i].Trim();
                                }

                                this.rowNumbersTrimed = new List<string>(value);
                            }
                        }
                    )
                },

                // 星の番地☆（＾～＾）
                // 初期値は19路盤だぜ☆（＾～＾）
                // TODO JSONをデシリアライズできる方法が分かれば private アクセスにしたいが……☆（＾～＾）
                {
                    "stars",
                    new PropertyStringList(
                        new List<string>(){
                            "D16", "K16", "Q16", "D10", "K10", "Q10", "D4", "K4", "Q4"
                        }
                    )
                },

                // 何ミリ秒ごとに `input.txt` を確認するか（＾～＾）
                // 初期値は 2 秒☆（＾～＾）
                {"interval-msec", new PropertyNumber(2000) },

                // 何手目か。
                {"ply", new PropertyNumber(0) },

                // 最後の着手点。
                {"move", new PropertyNumber(0) },

                // 黒の選手名。
                {"b-name", new PropertyString("player1") },

                // 黒の残り時間。
                {"b-time", new PropertyString("00:00") },

                // 黒のアゲハマ。
                // 囲碁の白石がハマグリで作られているから石のことをハマと呼ぶが、取り揚げた石はアゲハマと呼ぶ☆（＾～＾）
                // でもアゲハマは、略してハマと呼ばれる☆（＾～＾）
                {"b-hama", new PropertyNumber(0) },

                // 白の選手名。
                {"w-name", new PropertyString("player2") },

                // 白の残り時間。
                {"w-time", new PropertyString("00:00") },

                // 白のアゲハマ。
                {"w-hama", new PropertyNumber(0) },

                // 白のコミ。
                {"komi", new PropertyNumber(6.5) },

                // GUIの画面上にメッセージを表示するぜ☆（＾～＾）
                // 改行は "\n" にだけ対応☆（＾～＾） 代わりに "\v" （垂直タブ）は使えなくなった☆（＾～＾）
                {"info", new PropertyString("") },
            };
        }

        public BoardModel Board { get; set; }

        /// <summary>
        /// JSONに出力される書式も気にして　構造化している☆（＾～＾）
        /// TODO JSONをデシリアライズできる方法が分かれば private アクセスにしたいが……☆（＾～＾）
        /// </summary>
        public Dictionary<string, PropertyValue> Properties { get; set; }

        /// <summary>
        /// 各行番号☆（＾～＾）トリムしているやつだぜ☆（＾～＾）
        /// JSONにシリアライズ（出力）されないように、フィールドとメソッドにしているぜ☆（＾～＾）
        /// </summary>
        private List<string> rowNumbersTrimed;
        public List<string> RowNumbersTrimed
        {
            get
            {
                return this.rowNumbersTrimed;
            }
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
