namespace KifuwarabeGoBoardGui.Controller.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using KifuwarabeGoBoardGui.InputScript;
    using KifuwarabeGoBoardGui.Model;

    /// <summary>
    /// 入力スクリプトのパーサーだぜ☆（＾～＾）
    /// 
    /// 行番号など、変更可能な部分も予め知ってないとパースはできないぜ☆（＾～＾）
    /// </summary>
    public class InputLineParser
    {
        public delegate void NoneCallback();
        public delegate void CommentCallback(string commentLine);
        public delegate void CommandCallback(Instruction instruction);

        private static string AliasCommand => "alias";
        private static string BoardCommand => "board";
        private static string ExitsCommand => "exit";
        private static string JsonCommand => "json";
        private static string PutsCommand => "put";
        private static string SetsCommand => "set";
        public static string SleepsCommand => "sleep";

        public static string BlackObject => "black";
        public static string SpaceObject => "space";
        public static string WhiteObject => "white";

        /// <summary>
        /// オブジェクトの名前☆（＾～＾）　コマンド名とは別ものだぜ☆（＾～＾）
        /// </summary>
        public static string InfoCommand => "info";

        public List<Instruction> Instructions { get; private set; }

        public InputLineParser(List<Instruction> instructions)
        {
            this.Instructions = instructions;
        }

        public static void ParseByLine(
            string line,
            ApplicationObjectDtoWrapper appModel,
            CommandCallback aliasCommandCallback,
            CommandCallback boardCommandCallback,
            CommentCallback commentCallback,
            CommandCallback exitsCommandCallback,
            CommandCallback infoCommandCallback,
            CommandCallback jsonCommandCallback,
            CommandCallback putsCommandCallback,
            CommandCallback setsCommandCallback,
            CommandCallback sleepsCommandCallback,
            NoneCallback noneCallback)
        {
            if (commentCallback == null)
            {
                throw new ArgumentNullException(nameof(commentCallback));
            }

            if (aliasCommandCallback == null)
            {
                throw new ArgumentNullException(nameof(aliasCommandCallback));
            }

            if (boardCommandCallback == null)
            {
                throw new ArgumentNullException(nameof(boardCommandCallback));
            }

            if (exitsCommandCallback == null)
            {
                throw new ArgumentNullException(nameof(exitsCommandCallback));
            }

            if (infoCommandCallback == null)
            {
                throw new ArgumentNullException(nameof(infoCommandCallback));
            }

            if (jsonCommandCallback == null)
            {
                throw new ArgumentNullException(nameof(jsonCommandCallback));
            }

            if (putsCommandCallback == null)
            {
                throw new ArgumentNullException(nameof(putsCommandCallback));
            }

            if (setsCommandCallback == null)
            {
                throw new ArgumentNullException(nameof(setsCommandCallback));
            }

            if (sleepsCommandCallback == null)
            {
                throw new ArgumentNullException(nameof(sleepsCommandCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            // 空行は無視☆（＾～＾）
            if (string.IsNullOrWhiteSpace(line))
            {
                noneCallback();
                return;
            }

            var instructions = new List<Instruction>();

            var start = 0;
            var curr = WhiteSpaceParser.Parse(
                line,
                start,
                (whiteSpace, curr) =>
                {
                    return curr;
                },
                () =>
                {
                    return start;
                });

            // 行頭のスペースは読み飛ばすぜ☆（＾～＾）

            curr = StartsWithKeywordParser.Parse(
                "#",
                line,
                curr,
                (commentSymbol, curr) =>
                {
                    // 行頭が `#` なら、その行は読み飛ばせだぜ☆（＾～＾）
                    commentCallback(line);
                    return curr;
                },
                () =>
                {
                    // コメントではないなら。
                    // 読み込んだ行はトレース表示しようぜ☆（＾～＾）
                    Trace.WriteLine($"Info    | Read {line}");

                    // TODO コマンド名ごとに分けるぜ☆（＾～＾）
                    return WordParser.Parse(
                        line,
                        curr,
                        (commandName, curr) =>
                        {
                            if (commandName.Text == InputLineParser.AliasCommand)
                            {
                                curr = AliasInstructionArgumentParser.Parse(
                                    line,
                                    curr,
                                    (argument, curr) =>
                                    {
                                        Trace.WriteLine($"Info    | Arg {commandName.Text} {argument.ToDisplay()}");
                                        aliasCommandCallback(new Instruction(commandName.Text, argument));
                                        return curr;
                                    },
                                    () =>
                                    {
                                        Trace.WriteLine($"Error   | {line}");
                                        return curr;
                                    });
                            }
                            else if (commandName.Text == InputLineParser.BoardCommand)
                            {
                                curr = BoardInstructionArgumentParser.Parse(
                                    line,
                                    curr,
                                    appModel,
                                    (argument, curr) =>
                                    {
                                        if (argument == null)
                                        {
                                            Trace.WriteLine($"Error   | {line}");
                                        }
                                        else
                                        {
                                            Trace.WriteLine($"Info    | Arg {commandName.Text} {argument.ToDisplay(appModel)}");
                                            boardCommandCallback(new Instruction(commandName.Text, argument));
                                        }

                                        return curr;
                                    },
                                    () =>
                                    {
                                        // パース失敗☆（＾～＾）
                                        Trace.WriteLine($"Error   | {line}");
                                        return curr;
                                    });
                            }
                            else if (commandName.Text == InputLineParser.ExitsCommand)
                            {
                                Trace.WriteLine($"Info    | Arg {commandName.Text}");
                                exitsCommandCallback(new Instruction(commandName.Text, null));
                            }
                            else if (commandName.Text == InputLineParser.InfoCommand)
                            {
                                InfoInstructionArgument argument;
                                (argument, curr) = InfoInstructionArgumentParser.Parse(line, curr);
                                if (argument == null)
                                {
                                    Trace.WriteLine($"Error   | {line}");
                                }
                                else
                                {
                                    Trace.WriteLine($"Info    | Arg {commandName.Text} {argument.ToDisplay()}");
                                    infoCommandCallback(new Instruction(commandName.Text, argument));
                                }
                            }
                            else if (commandName.Text == InputLineParser.JsonCommand)
                            {
                                JsonInstructionArgument argument;
                                (argument, curr) = JsonInstructionArgumentParser.Parse(line, curr);
                                if (argument == null)
                                {
                                    Trace.WriteLine($"Error   | {line}");
                                }
                                else
                                {
                                    Trace.WriteLine($"Arg     | {commandName.Text} {argument.ToDisplay()}");
                                    jsonCommandCallback(new Instruction(commandName.Text, argument));
                                }
                            }
                            else if (commandName.Text == InputLineParser.PutsCommand)
                            {
                                curr = PutsInstructionArgumentParser.Parse(
                                    line,
                                    curr,
                                    appModel,
                                    (argument, curr) =>
                                    {
                                        if (argument == null)
                                        {
                                            Trace.WriteLine($"Error   | Null argument. {line}");
                                        }
                                        else
                                        {
                                            Trace.WriteLine($"Info    | Arg {commandName.Text} {argument.ToDisplay(appModel)}");
                                            putsCommandCallback(new Instruction(commandName.Text, argument));
                                        }

                                        return curr;
                                    },
                                    () =>
                                    {
                                        // パース失敗☆（＾～＾）
                                        Trace.WriteLine($"Error   | Parse fail. {line}");
                                        return curr;
                                    });
                            }
                            else if (commandName.Text == InputLineParser.SetsCommand)
                            {
                                curr = SetsInstructionArgumentParser.Parse(
                                    line,
                                    curr,
                                    (argument, curr) =>
                                    {
                                        Trace.WriteLine($"Info    | Arg {commandName.Text} {argument.ToDisplay()}");
                                        setsCommandCallback(new Instruction(commandName.Text, argument));
                                        return curr;
                                    },
                                    () =>
                                    {
                                        // パース失敗☆（＾～＾）
                                        Trace.WriteLine($"Error   | {line}");
                                        return curr;
                                    });
                            }
                            else if (commandName.Text == InputLineParser.SleepsCommand)
                            {
                                curr = SleepsInstructionArgumentParser.Parse(
                                    line,
                                    curr,
                                    (argument, curr) =>
                                    {
                                        Trace.WriteLine($"Info    | Arg {commandName.Text} {argument.ToDisplay()}");
                                        sleepsCommandCallback(new Instruction(commandName.Text, argument));
                                        return curr;
                                    },
                                    () =>
                                    {
                                        // パース失敗☆（＾～＾）
                                        Trace.WriteLine($"Error   | {line}");
                                        return curr;
                                    });
                            }
                            else
                            {
                                Trace.WriteLine($"Warning | [{line}] are not implemented.");
                            }

                            return curr;
                        },
                        () =>
                        {
                            // パース失敗☆（＾～＾）
                            Trace.WriteLine($"Error   | {line}");
                            return curr;
                        });
                });

            if (instructions == null)
            {
                noneCallback();
            }
        }
    }
}
