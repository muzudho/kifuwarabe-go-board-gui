namespace KifuwarabeUec11Gui.Model
{
    using System.Diagnostics;
    using System.Text.Json;
 
    /// <summary>
    /// `output.json` をこれで作ろうぜ☆（＾～＾）
    /// </summary>
    public class ApplicationObjectModel
    {
        public BoardModel Board { get; set; }
        public PropertiesModel Properties { get; set; }

        public ApplicationObjectModel()
        {
            this.Properties = new PropertiesModel();

            // 盤☆（＾～＾）
            this.Board = new BoardModel();
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
