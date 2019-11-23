namespace KifuwarabeUec11Gui.Model
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text.Json;

    /// <summary>
    /// `output.json` をこれで作ろうぜ☆（＾～＾）
    /// 
    /// C#とRustで互換できるデータ形式にすること、JSONに出力される書式も気にして　構造化している☆（＾～＾）
    /// TODO JSONをデシリアライズできる方法が分かれば set を private アクセスにしたいが……☆（＾～＾）
    /// </summary>
    public class ApplicationObjectModel
    {
        public static string NullType = "null";
        public static string BoolType = "bool";
        public static string NumberType = "number";
        public static string StringType = "string";
        public static string StringListType = "string-list";

        /// <summary>
        /// リアルネームは、後ろに Canvas を付けてXMLタグ名に使う☆（＾～＾） top2 なら top2Canvas だぜ☆（＾～＾）
        /// エイリアスは、打鍵しやすい名前だぜ☆（＾～＾）
        /// UIオブジェクトの名前☆（＾～＾）　画面から見て　上、右、左に並んでるぜ☆（＾～＾）
        /// </summary>
        public static RealName Top1RealName => new RealName("top1");
        public static RealName MoveRealName => new RealName("top1");
        public static AliasName MoveAliasName => new AliasName("move");

        public static RealName Top2RealName => new RealName("top2");
        public static RealName PlyRealName => new RealName("top2");
        public static AliasName PlyAliasName => new AliasName("ply");

        public static RealName Right1RealName => new RealName("right1");
        public static RealName BlackHamaRealName => new RealName("right1");
        public static AliasName BlackHamaAliasName => new AliasName("b-hama");

        public static RealName Right2RealName => new RealName("right2");
        public static RealName BlackTimeRealName => new RealName("right2");
        public static AliasName BlackTimeAliasName => new AliasName("b-time");

        public static RealName Right3RealName => new RealName("right3");
        public static RealName BlackNameRealName => new RealName("right3");
        public static AliasName BlackNameAliasName => new AliasName("b-name");

        public static RealName Left1RealName => new RealName("left1");
        public static RealName WhiteNameRealName => new RealName("left1");
        public static AliasName WhiteNameAliasName => new AliasName("w-name");

        public static RealName Left2RealName => new RealName("left2");
        public static RealName WhiteTimeRealName => new RealName("left2");
        public static AliasName WhiteTimeAliasName => new AliasName("w-time");

        public static RealName Left3RealName => new RealName("left3");
        public static RealName WhiteHamaRealName => new RealName("left3");
        public static AliasName WhiteHamaAliasName => new AliasName("w-hama");

        public static RealName Left4RealName => new RealName("left4");
        public static RealName KomiRealName => new RealName("left4");
        public static AliasName KomiAliasName => new AliasName("komi");

        public static RealName InfoRealName => new RealName("info");
        public static AliasName InfoAliasName => new AliasName("info");

        public static RealName ColumnSizeRealName => new RealName("column-size");
        public static AliasName ColumnSizeAliasName => new AliasName("column-size");

        public static RealName RowSizeRealName => new RealName("row-size");
        public static AliasName RowSizeAliasName => new AliasName("row-size");

        public static RealName IntervalMsecRealName => new RealName("interval-msec");
        public static AliasName IntervalMsecAliasName => new AliasName("interval-msec");

        public static RealName ColumnNumbersRealName => new RealName("column-numbers");
        public static AliasName ColumnNumbersAliasName => new AliasName("column-numbers");

        public static RealName RowNumbersRealName => new RealName("row-numbers");
        public static AliasName RowNumbersAliasName => new AliasName("row-numbers");

        public static RealName StarsRealName => new RealName("stars");
        public static AliasName StarsAliasName => new AliasName("stars");

        /// <summary>
        /// alias top1 = move
        /// alias top2 = ply
        /// alias right1 = b-hama
        /// alias right2 = b-time
        /// alias right3 = b-name
        /// alias left1 = w-name
        /// alias left2 = w-time
        /// alias left3 = w-hama
        /// alias left4 = komi
        /// </summary>
        public ApplicationObjectModel()
        {
            this.Board = new BoardModel();
            this.ObjectRealName = new Dictionary<string, string>();

            this.Booleans = new Dictionary<string, PropertyBool>()
            {
            };

            this.Numbers = new Dictionary<string, PropertyNumber>()
            {
                // 何ミリ秒ごとに `input.txt` を確認するか（＾～＾）
                // 初期値は 2 秒☆（＾～＾）
                {IntervalMsecRealName.Value, new PropertyNumber("#intervalMSec", 2000) },
            };

            this.Strings = new Dictionary<string, PropertyString>()
            {
                // GUIの画面上にメッセージを表示するぜ☆（＾～＾）
                // 改行は "\n" にだけ対応☆（＾～＾） 代わりに "\v" （垂直タブ）は使えなくなった☆（＾～＾）
                {InfoRealName.Value, new PropertyString(string.Empty, string.Empty) },
            };

            this.StringLists = new Dictionary<string, PropertyStringList>()
            {
                // 各列番号☆（＾～＾）
                // I列がない☆（＾～＾）棋譜に I1 I11 I17 とか書かれたら字が汚くて読めなくなるのだろう☆（＾～＾）
                {
                    ColumnNumbersRealName.Value,
                    new PropertyStringList(
                        $"#{ColumnNumbersRealName.Value}",
                        new List<string>(){
                            /* "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T" */
                        }
                    )
                },

                // 各行番号☆（＾～＾）半角スペースで位置調整するとか前時代的なことしたいだろ☆（＾～＾）
                // 初期状態で入れ物だけ作っておくぜ☆（＾～＾）あとでイベント・ハンドラを追加したいからな☆（＾～＾）
                // 1桁の数は、文字位置の調整がうまく行かないので勘で調整☆（＾～＾）盤の上側から順に並べろだぜ☆（＾～＾）
                // TODO JSONをデシリアライズできる方法が分かれば private アクセスにしたいが……☆（＾～＾）
                {
                    RowNumbersRealName.Value,
                    new PropertyStringList(
                        $"#{RowNumbersRealName.Value}",
                        new List<string>(){
                            /* "19", "18", "17", "16", "15", "14", "13", "12", "11", "10", "  9", "  8", "  7", "  6", "  5", "  4", "  3", "  2", "  1" */
                        }
                    )
                },

                // 星の番地☆（＾～＾）
                // 初期値は19路盤だぜ☆（＾～＾）
                // TODO JSONをデシリアライズできる方法が分かれば private アクセスにしたいが……☆（＾～＾）
                {
                    StarsRealName.Value,
                    new PropertyStringList(
                        $"#{StarsRealName.Value}",
                        new List<string>(){
                            /* "D16", "K16", "Q16", "D10", "K10", "Q10", "D4", "K4", "Q4" */
                        }
                    )
                },
            };
        }

        /// <summary>
        /// 盤☆（＾～＾）
        /// </summary>
        public BoardModel Board { get; set; }

        /// <summary>
        /// 外向きの名前（JSON用）を、内向きの名前（XAML用）に変換だぜ☆（＾～＾）
        /// </summary>
        public Dictionary<string, string> ObjectRealName { get; set; }

        /// <summary>
        /// 論理値型を持つウィジェット☆（＾～＾）
        /// </summary>
        public Dictionary<string, PropertyBool> Booleans { get; set; }

        /// <summary>
        /// 数値型を持つウィジェット☆（＾～＾）
        /// </summary>
        public Dictionary<string, PropertyNumber> Numbers { get; set; }

        /// <summary>
        /// 文字列型を持つウィジェット☆（＾～＾）
        /// </summary>
        public Dictionary<string, PropertyString> Strings { get; set; }

        /// <summary>
        /// 文字列リストを持つウィジェット☆（＾～＾）
        /// </summary>
        public Dictionary<string, PropertyStringList> StringLists { get; set; }

        public static ApplicationObjectModel Parse(string json)
        {
            Trace.WriteLine($"json input      | {json}");

            var option1 = new JsonSerializerOptions();
            option1.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            var newModel = JsonSerializer.Deserialize(json, typeof(ApplicationObjectModel), option1) as ApplicationObjectModel;
            Trace.WriteLine($"ColumnSize      | {newModel.Board.ColumnSize}");
            Trace.WriteLine($"RowSize         | {newModel.Board.RowSize}");

            /*
            {
                var option2 = new JsonSerializerOptions();
                option2.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                Trace.WriteLine($"json re         | {JsonSerializer.Serialize(newModel, option2)}");
            }
            */

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
