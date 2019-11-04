namespace KifuwarabeUec11Gui.Output
{
    using System.Text.Json;
    using KifuwarabeUec11Gui.Output;

    /// <summary>
    /// `output.json` をこれで作ろうぜ☆（＾～＾）
    /// </summary>
    public class OutputJsonDocument
    {
        public BoardModel Board { get; private set; }
        public State State { get; private set; }

        public OutputJsonDocument(BoardModel board, State state)
        {
            this.Board = board;
            this.State = state;
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

            return JsonSerializer.Serialize(this,option);
        }
    }
}
