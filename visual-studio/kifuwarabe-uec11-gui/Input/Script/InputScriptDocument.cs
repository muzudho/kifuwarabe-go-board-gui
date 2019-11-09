namespace KifuwarabeUec11Gui.InputScript
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using KifuwarabeUec11Gui.Model;

    /// <summary>
    /// GUIへの入力スクリプトだぜ☆（＾～＾）
    /// </summary>
    public class InputScriptDocument
    {
        public static string BlackCommand => "black";
        public static string WhiteCommand => "white";
        public static string SpaceCommand => "space";
        public static string BoardCommand => "board";
        public static string JsonCommand => "json";
        public static string WidgetCommand => "widget";
        public static string SetsCommand => "set";
        public static string ExitsCommand => "exit";

        /// <summary>
        /// オブジェクトの名前☆（＾～＾）　コマンド名とは別ものだぜ☆（＾～＾）
        /// </summary>
        public static string InfoCommand => "info";

        public List<Instruction> Instructions { get; private set; }

        public InputScriptDocument(List<Instruction> instructions)
        {
            this.Instructions = instructions;
        }

        public static InputScriptDocument Parse(string text, ApplicationObjectModel model)
        {
            // 空行は無視☆（＾～＾）
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            var instructions = new List<Instruction>();

            foreach (var line in text.Split(Environment.NewLine))
            {
                // 行頭のスペースは読み飛ばすぜ☆（＾～＾）
                var (whiteSpace, next) = WhiteSpace.Parse(line, 0);

                // 行頭が `#` なら、その行は読み飛ばせだぜ☆（＾～＾）
                ExactlyKeyword commentSymbol;
                (commentSymbol, next) = ExactlyKeyword.Parse("#", line, next);
                if (commentSymbol != null)
                {
                    Trace.WriteLine($"Comment         | {line}");
                    continue;
                }

                Trace.WriteLine($"Read            | {line}");

                Word commandName;
                (commandName, next) = Word.Parse(line, 0);
                Trace.WriteLine($"Command         | {commandName?.Text}");

                if (commandName != null)
                {
                    if (commandName.Text == InputScriptDocument.InfoCommand)
                    {
                        InfoInstructionArgument argument;
                        (argument, next) = InfoInstructionArgument.Parse(line, next);
                        if (argument == null)
                        {
                            Trace.WriteLine($"Error           | {line}");
                        }
                        else
                        {
                            instructions.Add(new Instruction(commandName.Text, argument));
                        }
                    }
                    else if (commandName.Text == InputScriptDocument.BlackCommand ||
                        commandName.Text == InputScriptDocument.WhiteCommand ||
                        commandName.Text == InputScriptDocument.SpaceCommand)
                    {
                        ColorInstructionArgument argument;
                        (argument, next) = ColorInstructionArgument.Parse(line, next, model);
                        if (argument == null)
                        {
                            Trace.WriteLine($"Error           | {line}");
                        }
                        else
                        {
                            // Trace.WriteLine($"Test            | {word.Text} {argument.ToDisplay()}");
                            instructions.Add(new Instruction(commandName.Text, argument));
                        }
                    }
                    else if (commandName.Text == InputScriptDocument.BoardCommand)
                    {
                        BoardInstructionArgument argument;
                        (argument, next) = BoardInstructionArgument.Parse(line, next, model);
                        if (argument == null)
                        {
                            Trace.WriteLine($"Error           | {line}");
                        }
                        else
                        {
                            // Trace.WriteLine($"Test            | {commandName.Text} {argument.ToDisplay()}");
                            instructions.Add(new Instruction(commandName.Text, argument));
                        }
                    }
                    else if (commandName.Text == InputScriptDocument.JsonCommand)
                    {
                        JsonInstructionArgument argument;
                        (argument, next) = JsonInstructionArgument.Parse(line, next);
                        if (argument == null)
                        {
                            Trace.WriteLine($"Error           | {line}");
                        }
                        else
                        {
                            // Trace.WriteLine($"Test            | {commandName.Text} {argument.ToDisplay()}");
                            instructions.Add(new Instruction(commandName.Text, argument));
                        }
                    }
                    else if (commandName.Text == InputScriptDocument.WidgetCommand)
                    {
                        WidgetInstructionArgument argument;
                        (argument, next) = WidgetInstructionArgument.Parse(line, next);
                        if (argument == null)
                        {
                            Trace.WriteLine($"Error           | {line}");
                        }
                        else
                        {
                            // Trace.WriteLine($"Test            | {commandName.Text} {argument.ToDisplay()}");
                            instructions.Add(new Instruction(commandName.Text, argument));
                        }
                    }
                    else if (commandName.Text == InputScriptDocument.SetsCommand)
                    {
                        SetsInstructionArgument argument;
                        (argument, next) = SetsInstructionArgument.Parse(line, next);
                        if (argument == null)
                        {
                            Trace.WriteLine($"Error           | {line}");
                        }
                        else
                        {
                            instructions.Add(new Instruction(commandName.Text, argument));
                        }
                    }
                    else if (commandName.Text == InputScriptDocument.ExitsCommand)
                    {
                        instructions.Add(new Instruction(commandName.Text, null));
                    }
                    else
                    {
                    }
                }
            }

            return new InputScriptDocument(instructions);
        }
    }
}
