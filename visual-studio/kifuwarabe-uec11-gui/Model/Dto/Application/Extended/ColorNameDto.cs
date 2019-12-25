namespace KifuwarabeGoBoardGui.Model.Dto
{
    /// <summary>
    /// 色名☆（＾～＾）
    /// </summary>
    public class ColorNameDto
    {
        public ColorNameDto(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}
