namespace KifuwarabeUec11Gui.InputScript
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using KifuwarabeUec11Gui.Controller.Parser;
    using KifuwarabeUec11Gui.Model;

    /// <summary>
    /// GUIへの入力スクリプトだぜ☆（＾～＾）
    /// </summary>
    public class InputScriptDocument
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        public delegate void ParsesCallback(InputScriptDocument matched);

        public static string AliasCommand => "alias";
        public static string BlackObject => "black";
        public static string BoardCommand => "board";
        public static string ExitsCommand => "exit";
        public static string JsonCommand => "json";
        public static string PutsCommand => "put";
        public static string SetsCommand => "set";
        public static string SpaceObject => "space";
        public static string WhiteObject => "white";

        /// <summary>
        /// オブジェクトの名前☆（＾～＾）　コマンド名とは別ものだぜ☆（＾～＾）
        /// </summary>
        public static string InfoCommand => "info";

        public List<Instruction> Instructions { get; private set; }

        public InputScriptDocument(List<Instruction> instructions)
        {
            this.Instructions = instructions;
        }

        public static void ParseByLine(string line, ApplicationObjectModelWrapper appModel, ParsesCallback callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            // 空行は無視☆（＾～＾）
            if (string.IsNullOrWhiteSpace(line))
            {
                callback(null);
                return;
            }

            var instructions = new List<Instruction>();

            var start = 0;
            WhiteSpaceParser.Parse(line, start, (whiteSpace, curr) =>
            {
                // 行頭のスペースは読み飛ばすぜ☆（＾～＾）

                return StartsWithKeywordParser.Parse("#", line, curr, (commentSymbol, curr) =>
                {
                    // Trace.WriteLine($"Info            | Line ISD=[{line}]");

                    if (commentSymbol != null)
                    {
                        // 行頭が `#` なら、その行は読み飛ばせだぜ☆（＾～＾）
                        Trace.WriteLine($"Comment         | {line}");
                        return curr;
                    }

                    // 読み込んだ行はトレース表示しようぜ☆（＾～＾）
                    Trace.WriteLine($"Read            | {line}");

                    return WordParser.Parse(line, curr, (commandName, curr) =>
                    {
                        // Trace.WriteLine($"Info            | Command-ISD=[{commandName?.Text}]");

                        if (commandName != null)
                        {
                            if (commandName.Text == InputScriptDocument.AliasCommand)
                            {
                                AliasInstructionArgument argument;
                                (argument, curr) = AliasInstructionArgumentParser.Parse(line, curr);
                                if (argument == null)
                                {
                                    Trace.WriteLine($"Error           | {line}");
                                }
                                else
                                {
                                    // Trace.WriteLine($"Arg             | {commandName.Text} {argument.ToDisplay()}");
                                    instructions.Add(new Instruction(commandName.Text, argument));
                                }
                            }
                            else if (commandName.Text == InputScriptDocument.BoardCommand)
                            {
                                BoardInstructionArgument argument;
                                (argument, curr) = BoardInstructionArgumentParser.Parse(line, curr, appModel);
                                if (argument == null)
                                {
                                    Trace.WriteLine($"Error           | {line}");
                                }
                                else
                                {
                                    // Trace.WriteLine($"Arg             | {commandName.Text} {argument.ToDisplay(appModel)}");
                                    instructions.Add(new Instruction(commandName.Text, argument));
                                }
                            }
                            else if (commandName.Text == InputScriptDocument.ExitsCommand)
                            {
                                instructions.Add(new Instruction(commandName.Text, null));
                            }
                            else if (commandName.Text == InputScriptDocument.InfoCommand)
                            {
                                InfoInstructionArgument argument;
                                (argument, curr) = InfoInstructionArgumentParser.Parse(line, curr);
                                if (argument == null)
                                {
                                    Trace.WriteLine($"Error           | {line}");
                                }
                                else
                                {
                                    Trace.WriteLine($"Info            | Arg {commandName.Text} {argument.ToDisplay()}");
                                    instructions.Add(new Instruction(commandName.Text, argument));
                                }
                            }
                            else if (commandName.Text == InputScriptDocument.JsonCommand)
                            {
                                JsonInstructionArgument argument;
                                (argument, curr) = JsonInstructionArgumentParser.Parse(line, curr);
                                if (argument == null)
                                {
                                    Trace.WriteLine($"Error           | {line}");
                                }
                                else
                                {
                                    // Trace.WriteLine($"Arg             | {commandName.Text} {argument.ToDisplay()}");
                                    instructions.Add(new Instruction(commandName.Text, argument));
                                }
                            }
                            else if (commandName.Text == InputScriptDocument.PutsCommand)
                            {
                                curr = PutsInstructionArgumentParser.Parse(
                                    line,
                                    curr,
                                    appModel,
                                    (argument, curr) =>
                                    {
                                        if (argument == null)
                                        {
                                            Trace.WriteLine($"Error           | {line}");
                                        }
                                        else
                                        {
                                            // Trace.WriteLine($"Arg             | {commandName.Text} {argument.ToDisplay(appModel)}");
                                            instructions.Add(new Instruction(commandName.Text, argument));
                                        }

                                        return curr;
                                    });
                            }
                            else if (commandName.Text == InputScriptDocument.SetsCommand)
                            {
                                curr = SetsInstructionArgumentParser.Parse(
                                    line,
                                    curr,
                                    (argument, curr) =>
                                    {
                                        if (argument == null)
                                        {
                                            Trace.WriteLine($"Error           | {line}");
                                        }
                                        else
                                        {
                                            // Trace.WriteLine($"Arg             | {commandName.Text} {argument.ToDisplay()}");
                                            instructions.Add(new Instruction(commandName.Text, argument));
                                        }

                                        return curr;
                                    });
                            }
                            else
                            {
                                Trace.WriteLine($"Warning         | [{line}] are not implemented.");
                            }
                        }

                        return curr;
                    });
                });
            });

            callback(new InputScriptDocument(instructions));
        }
    }
}
