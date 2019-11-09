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

        public static InputScriptDocument Parse(string text, ApplicationObjectModel appModel)
        {
            // 空行は無視☆（＾～＾）
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            var instructions = new List<Instruction>();

            foreach (var line in text.Split(Environment.NewLine))
            {
                var start = 0;
                WhiteSpace.Parse(line, start, (whiteSpace, curr) =>
                {
                    // 行頭のスペースは読み飛ばすぜ☆（＾～＾）

                    return ExactlyKeyword.Parse("#", line, curr, (commentSymbol, curr) =>
                    {
                        if (commentSymbol != null)
                        {
                            // 行頭が `#` なら、その行は読み飛ばせだぜ☆（＾～＾）
                            Trace.WriteLine($"Comment         | {line}");
                        }
                        else
                        {
                            Trace.WriteLine($"Read            | {line}");

                            Word commandName;
                            (commandName, curr) = Word.Parse(line, curr);
                            Trace.WriteLine($"Command         | {commandName?.Text}");

                            if (commandName != null)
                            {
                                if (commandName.Text == InputScriptDocument.InfoCommand)
                                {
                                    InfoInstructionArgument argument;
                                    (argument, curr) = InfoInstructionArgument.Parse(line, curr);
                                    if (argument == null)
                                    {
                                        Trace.WriteLine($"Error           | {line}");
                                    }
                                    else
                                    {
                                        Trace.WriteLine($"Arg             | {commandName.Text} {argument.ToDisplay()}");
                                        instructions.Add(new Instruction(commandName.Text, argument));
                                    }
                                }
                                else if (commandName.Text == InputScriptDocument.BlackCommand ||
                                    commandName.Text == InputScriptDocument.WhiteCommand ||
                                    commandName.Text == InputScriptDocument.SpaceCommand)
                                {
                                    ColorInstructionArgument argument;
                                    (argument, curr) = ColorInstructionArgument.Parse(line, curr, appModel);
                                    if (argument == null)
                                    {
                                        Trace.WriteLine($"Error           | {line}");
                                    }
                                    else
                                    {
                                        Trace.WriteLine($"Arg             | {commandName.Text} {argument.ToDisplay(appModel)}");
                                        instructions.Add(new Instruction(commandName.Text, argument));
                                    }
                                }
                                else if (commandName.Text == InputScriptDocument.BoardCommand)
                                {
                                    BoardInstructionArgument argument;
                                    (argument, curr) = BoardInstructionArgument.Parse(line, curr, appModel);
                                    if (argument == null)
                                    {
                                        Trace.WriteLine($"Error           | {line}");
                                    }
                                    else
                                    {
                                        Trace.WriteLine($"Arg             | {commandName.Text} {argument.ToDisplay(appModel)}");
                                        instructions.Add(new Instruction(commandName.Text, argument));
                                    }
                                }
                                else if (commandName.Text == InputScriptDocument.JsonCommand)
                                {
                                    JsonInstructionArgument argument;
                                    (argument, curr) = JsonInstructionArgument.Parse(line, curr);
                                    if (argument == null)
                                    {
                                        Trace.WriteLine($"Error           | {line}");
                                    }
                                    else
                                    {
                                        Trace.WriteLine($"Arg             | {commandName.Text} {argument.ToDisplay()}");
                                        instructions.Add(new Instruction(commandName.Text, argument));
                                    }
                                }
                                else if (commandName.Text == InputScriptDocument.WidgetCommand)
                                {
                                    WidgetInstructionArgument argument;
                                    (argument, curr) = WidgetInstructionArgument.Parse(line, curr);
                                    if (argument == null)
                                    {
                                        Trace.WriteLine($"Error           | {line}");
                                    }
                                    else
                                    {
                                        Trace.WriteLine($"Arg             | {commandName.Text} {argument.ToDisplay()}");
                                        instructions.Add(new Instruction(commandName.Text, argument));
                                    }
                                }
                                else if (commandName.Text == InputScriptDocument.SetsCommand)
                                {
                                    SetsInstructionArgument argument;
                                    (argument, curr) = SetsInstructionArgument.Parse(line, curr);
                                    if (argument == null)
                                    {
                                        Trace.WriteLine($"Error           | {line}");
                                    }
                                    else
                                    {
                                        Trace.WriteLine($"Arg             | {commandName.Text} {argument.ToDisplay()}");
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

                        return curr;
                    });
                });
            }

            return new InputScriptDocument(instructions);
        }
    }
}
