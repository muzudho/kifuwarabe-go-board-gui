namespace KifuwarabeGoBoardGui.Model.Dto
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
    public class ApplicationDto
    {
        public static string NullType = "null";
        public static string BoolType = "bool";
        public static string NumberType = "number";
        public static string StringType = "string";
        public static string StringListType = "string-list";

        /// <summary>
        /// リアルネームは、後ろに Canvas を付けてXMLタグ名に使う☆（＾～＾）キャメルケース☆（＾～＾） top2 なら top2Canvas だぜ☆（＾～＾）
        /// エイリアスは、打鍵しやすい名前だぜ☆（＾～＾）
        /// UIオブジェクトの名前☆（＾～＾）　画面から見て　上、右、左に並んでるぜ☆（＾～＾）
        /// </summary>
        public static RealName Top1RealName => new RealName("top1");
        public static RealName MoveRealName => new RealName("top1");

        public static RealName Top2RealName => new RealName("top2");

        public static RealName Right1RealName => new RealName("right1");

        public static RealName Right2RealName => new RealName("right2");

        public static RealName Right3RealName => new RealName("right3");

        public static RealName Left1RealName => new RealName("left1");

        public static RealName Left2RealName => new RealName("left2");

        public static RealName Left3RealName => new RealName("left3");

        public static RealName Left4RealName => new RealName("left4");

        public static RealName InfoRealName => new RealName("info");

        public static RealName ColumnSizeRealName => new RealName("column-size");

        public static RealName RowSizeRealName => new RealName("row-size");

        public static RealName IntervalMsecRealName => new RealName("interval-msec");

        public static RealName ColumnNumbersRealName => new RealName("column-numbers");

        public static RealName RowNumbersRealName => new RealName("row-numbers");

        public static RealName StarsRealName => new RealName("stars");

        public static RealName MoveMarkerRealName => new RealName("moveMarker");

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
        public ApplicationDto()
        {
            this.Board = new BoardDto();
            this.ObjectRealName = new Dictionary<string, string>();

            this.Booleans = new Dictionary<string, PropertyBool>()
            {
            };

            this.Numbers = new Dictionary<string, PropertyNumber>()
            {
                // 初期値は囲碁の１９路盤☆（＾～＾）
                {RowSizeRealName.Value, new PropertyNumber("#row-size", 19) },
                {ColumnSizeRealName.Value, new PropertyNumber("#column-size", 19) },

                // 何ミリ秒ごとに `input.txt` を確認するか（＾～＾）
                // 初期値は 2 秒☆（＾～＾）
                {IntervalMsecRealName.Value, new PropertyNumber("#interval-msec", 2000) },
            };

            this.Strings = new Dictionary<string, PropertyString>()
            {
                /*
                // 'info' は最初から文字列型として使用可能☆（＾～＾） GUIの画面上にメッセージを表示するぜ☆（＾～＾）
                // 改行は "\n" にだけ対応☆（＾～＾） 代わりに "\v" （垂直タブ）は使えなくなった☆（＾～＾）
                {InfoRealName.Value, new PropertyString(string.Empty, string.Empty) },
                */
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

            // 盤のサイズ
            this.Board.Resize(this.RowSize, this.ColumnSize);
        }

        /// <summary>
        /// 盤☆（＾～＾）
        /// </summary>
        public BoardDto Board { get; set; }

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

        public int RowSize
        {
            get
            {
                return (int)this.Numbers[RowSizeRealName.Value].Value;
            }
            set
            {
                this.Numbers[RowSizeRealName.Value].Value = value;
            }
        }

        public int ColumnSize
        {
            get
            {
                return (int)this.Numbers[ColumnSizeRealName.Value].Value;
            }
            set
            {
                this.Numbers[ColumnSizeRealName.Value].Value = value;
            }
        }

        /// <summary>
        /// 19本の線を引くから20分割だが、符号の列を1つ足すぜ☆（＾～＾）
        /// </summary>
        public int GetRowDiv()
        {
            return RowSize + MainWindow.SignLen + 1;
        }

        /// <summary>
        /// 19本の線を引くから20分割だが、符号の列を1つ足すぜ☆（＾～＾）
        /// </summary>
        public int GetColumnDiv()
        {
            return ColumnSize + MainWindow.SignLen + 1;
        }

        /// <summary>
        /// 19路盤の最終行のインデックス 0 から始まる（0 Origin）ので、 -1 する☆（＾～＾）
        /// </summary>
        public int GetRowLastO0()
        {
            return RowSize - 1;
        }

        /// <summary>
        /// 19路盤の最終行のインデックス 0 から始まる（0 Origin）ので、 -1 する☆（＾～＾）
        /// </summary>
        public int GetColumnLastO0()
        {
            return ColumnSize - 1;
        }

        /// <summary>
        /// 石を置ける場所の数☆（＾～＾）
        /// </summary>
        public int GetCellCount()
        {
            return RowSize * ColumnSize;
        }

        public static ApplicationDto Parse(string json)
        {
            Trace.WriteLine($"json input      | {json}");

            var option1 = new JsonSerializerOptions();
            option1.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            var newModel = JsonSerializer.Deserialize(json, typeof(ApplicationDto), option1) as ApplicationDto;
            Trace.WriteLine($"ColumnSize      | {newModel.ColumnSize}");
            Trace.WriteLine($"RowSize         | {newModel.RowSize}");

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
