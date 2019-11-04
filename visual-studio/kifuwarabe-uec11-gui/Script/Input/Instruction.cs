namespace KifuwarabeUec11Gui.Script
{
    /// <summary>
    /// 命令。
    /// </summary>
    public class Instruction
    {
        public string Command { get; private set; }
        public object Argument { get; private set; }

        public Instruction(string command, object argument)
        {
            this.Command = command;
            this.Argument = argument;
        }
    }
}
