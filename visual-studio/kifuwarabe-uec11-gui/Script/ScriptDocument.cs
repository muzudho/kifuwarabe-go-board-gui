namespace KifuwarabeUec11Gui.Script
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class ScriptDocument
    {
        /// <summary>
        /// 19路盤の最終行のインデックス 0 から始まる（0 Origin）ので、 -1 する☆（＾～＾）
        /// </summary>
        public static int RowLastO0 => 18;

        /// <summary>
        /// 19路盤☆（＾～＾）
        /// </summary>
        public static int BoardSize => 19;

        /// <summary>
        /// 石を置ける場所の数☆（＾～＾）
        /// </summary>
        public static int CellCount => BoardSize * BoardSize;

        public List<Instruction> Instructions { get; private set; }

        public ScriptDocument(List<Instruction> instructions)
        {
            this.Instructions = instructions;
        }

        public static ScriptDocument Parse(string text)
        {
            // 空行は無視☆（＾～＾）
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            var instructions = new List<Instruction>();

            foreach (var line in text.Split(Environment.NewLine))
            {
                Trace.WriteLine($"Read            | {line}");

                var (word, next) = Word.Parse(line, 0);
                if (word != null)
                {
                    switch (word.Text)
                    {
                        case "black": // thru
                        case "white": // thru
                        case "space":
                            ColorInstructionArgument argument;
                            (argument, next) = ColorInstructionArgument.Parse(line, next);
                            if (argument == null)
                            {
                                Trace.WriteLine($"Error           | {line}");
                            }
                            else
                            {
                                // Trace.WriteLine($"Test            | {word.Text} {argument.ToDisplay()}");
                                instructions.Add(new Instruction(word.Text, argument));
                            }
                            break;
                    }
                }
            }

            return new ScriptDocument(instructions);
        }
    }
}
