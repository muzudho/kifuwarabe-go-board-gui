namespace KifuwarabeUec11Gui.Model
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text.Json;
    using KifuwarabeUec11Gui.Controller;

    /// <summary>
    /// `output.json` をこれで作ろうぜ☆（＾～＾）
    /// 
    /// C#とRustで互換できるデータ形式にすること、JSONに出力される書式も気にして　構造化している☆（＾～＾）
    /// TODO JSONをデシリアライズできる方法が分かれば set を private アクセスにしたいが……☆（＾～＾）
    /// </summary>
    public class ApplicationObjectModel
    {
        /// <summary>
        /// UIオブジェクトの名前☆（＾～＾）　画面から見て　上、右、左に並んでるぜ☆（＾～＾）
        /// </summary>
        public static string Top1OutsideName => "top1";
        public static string Top2OutsideName => "top2";
        public static string Right1OutsideName => "right1";
        public static string Right2OutsideName => "right2";
        public static string Right3OutsideName => "right3";
        public static string Left1OutsideName => "left1";
        public static string Left2OutsideName => "left2";
        public static string Left3OutsideName => "left3";
        public static string Left4OutsideName => "left4";
        public static string InfoOutsideName => "info";
        public static string ColumnSizeOutsideName => "column-size";
        public static string RowSizeOutsideName => "row-size";
        public static string IntervalMsecOutsideName => "interval-msec";

        public ApplicationObjectModel()
        {
            this.Board = new BoardModel();

            this.Booleans = new Dictionary<string, PropertyBool>()
            {

            };

            this.Numbers = new Dictionary<string, PropertyNumber>()
            {
                // 何ミリ秒ごとに `input.txt` を確認するか（＾～＾）
                // 初期値は 2 秒☆（＾～＾）
                {ApplicationObjectModel.IntervalMsecOutsideName, new PropertyNumber("#intervalMSec", 2000) },

                // 何手目か。
                {ApplicationObjectModel.Top2OutsideName, new PropertyNumber("手目", 0) },

                // 黒のアゲハマ。
                // 囲碁の白石がハマグリで作られているから石のことをハマと呼ぶが、取り揚げた石はアゲハマと呼ぶ☆（＾～＾）
                // でもアゲハマは、略してハマと呼ばれる☆（＾～＾）
                {ApplicationObjectModel.Right1OutsideName, new PropertyNumber("黒アゲハマ", 0) },

                // 白のアゲハマ。
                {ApplicationObjectModel.Left3OutsideName, new PropertyNumber("白アゲハマ", 0) },

                // 白のコミ。
                {ApplicationObjectModel.Left4OutsideName, new PropertyNumber("コミ", 6.5) },
            };

            this.Strings = new Dictionary<string, PropertyString>()
            {
                // 最後の着手点。
                {ApplicationObjectModel.Top1OutsideName, new PropertyString("着手", "---") },

                // 黒の選手名。
                {ApplicationObjectModel.Right3OutsideName, new PropertyString("名前", "player1") },

                // 黒の残り時間。
                {ApplicationObjectModel.Right2OutsideName, new PropertyString("残り時間", "00:00") },

                // 白の選手名。
                {ApplicationObjectModel.Left1OutsideName, new PropertyString("名前", "player2") },

                // 白の残り時間。
                {ApplicationObjectModel.Left2OutsideName, new PropertyString("残り時間", "00:00") },

                // GUIの画面上にメッセージを表示するぜ☆（＾～＾）
                // 改行は "\n" にだけ対応☆（＾～＾） 代わりに "\v" （垂直タブ）は使えなくなった☆（＾～＾）
                {ApplicationObjectModel.InfoOutsideName, new PropertyString("#info", "") },
            };

            this.StringLists = new Dictionary<string, PropertyStringList>()
            {
                // 各列番号☆（＾～＾）
                // I列がない☆（＾～＾）棋譜に I1 I11 I17 とか書かれたら字が汚くて読めなくなるのだろう☆（＾～＾）
                {
                    ColumnNumbersController.OutsideName,
                    new PropertyStringList(
                        $"#{ColumnNumbersController.OutsideName}",
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
                        $"#{RowNumbersController.OutsideName}",
                        new List<string>(){
                            "19", "18", "17", "16", "15", "14", "13", "12", "11", "10", "  9", "  8", "  7", "  6", "  5", "  4", "  3", "  2", "  1"
                        }
                    )
                },

                // 星の番地☆（＾～＾）
                // 初期値は19路盤だぜ☆（＾～＾）
                // TODO JSONをデシリアライズできる方法が分かれば private アクセスにしたいが……☆（＾～＾）
                {
                    StarsController.OutsideName,
                    new PropertyStringList(
                        $"#{StarsController.OutsideName}",
                        new List<string>(){
                            "D16", "K16", "Q16", "D10", "K10", "Q10", "D4", "K4", "Q4"
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
