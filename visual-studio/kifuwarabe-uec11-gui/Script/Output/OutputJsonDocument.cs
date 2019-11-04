namespace KifuwarabeUec11Gui.Script
{
    using System.Text.Json;

    /// <summary>
    /// `output.json` をこれで作ろうぜ☆（＾～＾）
    /// </summary>
    public class OutputJsonDocument
    {
        public State State { get; private set; }

        public OutputJsonDocument(State state)
        {
            this.State = state;
        }

        public string ToJson()
        {
            var option = new JsonSerializerOptions();

            // JSON は JavaScript 由来だろ☆（＾～＾） JavaScript に合わせようぜ☆（＾～＾）
            // camelCase
            option.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            // インデントしようぜ☆（＾～＾）
            option.WriteIndented = true;

            return JsonSerializer.Serialize(this,option);
        }
    }
}
