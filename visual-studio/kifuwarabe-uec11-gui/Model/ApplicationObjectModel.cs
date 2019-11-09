namespace KifuwarabeUec11Gui.Model
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text.Json;
 
    /// <summary>
    /// `output.json` をこれで作ろうぜ☆（＾～＾）
    /// </summary>
    public class ApplicationObjectModel
    {
        public BoardModel Board { get; set; }

        /// <summary>
        /// JSONに出力される書式も気にして　構造化している☆（＾～＾）
        /// TODO JSONをデシリアライズできる方法が分かれば private アクセスにしたいが……☆（＾～＾）
        /// </summary>
        public Dictionary<string, IPropertyValue> Properties { get; set; }

        public ApplicationObjectModel()
        {
            // 盤☆（＾～＾）
            this.Board = new BoardModel();

            this.Properties = new Dictionary<string, IPropertyValue>()
            {
                // 何ミリ秒ごとに `input.txt` を確認するか（＾～＾）
                // 初期値は 2 秒☆（＾～＾）
                {"interval-msec", new PropertyString("2000") },
                // 何手目か。
                {"ply", new PropertyString("0") },

                // 最後の着手点。
                {"move", new PropertyString("0") },

                // 黒の選手名。
                {"b-name", new PropertyString("player1") },

                // 黒の残り時間。
                {"b-time", new PropertyString("00:00") },

                // 黒のアゲハマ。
                // 囲碁の白石がハマグリで作られているから石のことをハマと呼ぶが、取り揚げた石はアゲハマと呼ぶ☆（＾～＾）
                // でもアゲハマは、略してハマと呼ばれる☆（＾～＾）
                {"b-hama", new PropertyString("0") },

                // 白の選手名。
                {"w-name", new PropertyString("player2") },

                // 白の残り時間。
                {"w-time", new PropertyString("00:00") },

                // 白のアゲハマ。
                {"w-hama", new PropertyString("0") },

                // 白のコミ。
                {"komi", new PropertyString("6.5") },

                // GUIの画面上にメッセージを表示するぜ☆（＾～＾）
                // 改行は "\n" にだけ対応☆（＾～＾） 代わりに "\v" （垂直タブ）は使えなくなった☆（＾～＾）
                {"info", new PropertyString("") },
            };
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

            return JsonSerializer.Serialize(this,option);
        }
    }
}
