namespace KifuwarabeUec11Gui.Script
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class ScriptDocument
    {
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
