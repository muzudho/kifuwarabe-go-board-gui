namespace KifuwarabeUec11Gui.Model
{
    using System.Text.Json;
 
    /// <summary>
    /// `output.json` をこれで作ろうぜ☆（＾～＾）
    /// </summary>
    public class ApplicationObjectModel
    {
        public BoardModel Board { get; private set; }
        public State State { get; private set; }

        public ApplicationObjectModel()
        {
            this.State = new State();

            // 盤☆（＾～＾）
            this.Board = new BoardModel();
        }

        public static ApplicationObjectModel Parse(string json)
        {
            return JsonSerializer.Deserialize<ApplicationObjectModel>(json);
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
