namespace KifuwarabeGoBoardGui.Controller.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using KifuwarabeGoBoardGui.Model.Dto;

    /// <summary>
    /// 入力スクリプトのパーサーだぜ☆（＾～＾）
    /// 
    /// 行番号など、変更可能な部分も予め知ってないとパースはできないぜ☆（＾～＾）
    /// </summary>
    public class InputLineParserLv1
    {
        public delegate void NoneCallback();
        public delegate void CommentCallback(string commentLine);
        public delegate void CommandCallback(Instruction instruction);

        private static string AliasCommand => "alias";
        private static string BoardCommand => "board";
        private static string ExitsCommand => "exit";
        private static string JsonCommand => "json";
        private static string NewsCommand => "new";
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

        private CommandCallback callbackOnAliasCommand;
        public InputLineParserLv1 AppendCallbackOnAliasCommand(CommandCallback callback)
        {
            this.callbackOnAliasCommand = callback;
            return this;
        }

        private CommandCallback callbackOnBoardCommand;
        public InputLineParserLv1 AppendCallbackOnBoardCommand(CommandCallback callback)
        {
            this.callbackOnBoardCommand = callback;
            return this;
        }

        private CommentCallback callbackOnCommentCommand;
        public InputLineParserLv1 AppendCallbackOnCommentCommand(CommentCallback callback)
        {
            this.callbackOnCommentCommand = callback;
            return this;
        }

        private CommandCallback callbackOnExitsCommand;
        public InputLineParserLv1 AppendCallbackOnExitsCommand(CommandCallback callback)
        {
            this.callbackOnExitsCommand = callback;
            return this;
        }

        private CommandCallback callbackOnInfoCommand;
        public InputLineParserLv1 AppendCallbackOnInfoCommand(CommandCallback callback)
        {
            this.callbackOnInfoCommand = callback;
            return this;
        }

        private CommandCallback callbackOnJsonCommand;
        public InputLineParserLv1 AppendCallbackOnJsonCommand(CommandCallback callback)
        {
            this.callbackOnJsonCommand = callback;
            return this;
        }

        private CommandCallback callbackOnNewsCommand;
        public InputLineParserLv1 AppendCallbackOnNewsCommand(CommandCallback callback)
        {
            this.callbackOnNewsCommand = callback;
            return this;
        }

        private CommandCallback callbackOnPutsCommand;
        public InputLineParserLv1 AppendCallbackOnPutsCommand(CommandCallback callback)
        {
            this.callbackOnPutsCommand = callback;
            return this;
        }

        private CommandCallback callbackOnSetsCommand;
        public InputLineParserLv1 AppendCallbackOnSetsCommand(CommandCallback callback)
        {
            this.callbackOnSetsCommand = callback;
            return this;
        }

        private CommandCallback callbackOnSleepsCommand;
        public InputLineParserLv1 AppendCallbackOnSleepsCommand(CommandCallback callback)
        {
            this.callbackOnSleepsCommand = callback;
            return this;
        }

        private NoneCallback callbackOnNoneCommand;
        public InputLineParserLv1 AppendCallbackOnNoneCommand(NoneCallback callback)
        {
            this.callbackOnNoneCommand = callback;
            return this;
        }

        public InputLineParserLv1()
        {
        }

        public void ParseByLine(string line, ApplicationObjectDtoWrapper appModel)
        {
            // 空行は無視☆（＾～＾）
            if (string.IsNullOrWhiteSpace(line))
            {
                this.callbackOnNoneCommand?.Invoke();
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
                    this.callbackOnCommentCommand?.Invoke(line);
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
                            if (commandName.Text == InputLineParserLv1.AliasCommand)
                            {
                                curr = AliasInstructionArgumentParser.Parse(
                                    line,
                                    curr,
                                    (argument, curr) =>
                                    {
                                        Trace.WriteLine($"Info    | Arg {commandName.Text} {argument.ToDisplay()}");
                                        this.callbackOnAliasCommand?.Invoke(new Instruction(commandName.Text, argument));
                                        return curr;
                                    },
                                    () =>
                                    {
                                        Trace.WriteLine($"Error   | {line}");
                                        return curr;
                                    });
                            }
                            else if (commandName.Text == InputLineParserLv1.BoardCommand)
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
                                            this.callbackOnBoardCommand?.Invoke(new Instruction(commandName.Text, argument));
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
                            else if (commandName.Text == InputLineParserLv1.ExitsCommand)
                            {
                                Trace.WriteLine($"Info    | Arg {commandName.Text}");
                                this.callbackOnExitsCommand?.Invoke(new Instruction(commandName.Text, null));
                            }
                            else if (commandName.Text == InputLineParserLv1.InfoCommand)
                            {
                                InfoInstructionArgumentDto argument;
                                (argument, curr) = InfoInstructionArgumentParser.Parse(line, curr);
                                if (argument == null)
                                {
                                    Trace.WriteLine($"Error   | {line}");
                                }
                                else
                                {
                                    Trace.WriteLine($"Info    | Arg {commandName.Text} {argument.ToDisplay()}");
                                    this.callbackOnInfoCommand?.Invoke(new Instruction(commandName.Text, argument));
                                }
                            }
                            else if (commandName.Text == InputLineParserLv1.JsonCommand)
                            {
                                JsonInstructionArgumentDto argument;
                                (argument, curr) = JsonInstructionArgumentParser.Parse(line, curr);
                                if (argument == null)
                                {
                                    Trace.WriteLine($"Error   | {line}");
                                }
                                else
                                {
                                    Trace.WriteLine($"Arg     | {commandName.Text} {argument.ToDisplay()}");

                                    this.callbackOnJsonCommand?.Invoke(new Instruction(commandName.Text, argument));
                                }
                            }
                            else if (commandName.Text == InputLineParserLv1.NewsCommand)
                            {
                                curr = NewsInstructionArgumentParser.Parse(
                                    line,
                                    curr,
                                    (argument, curr) =>
                                    {
                                        Trace.WriteLine($"Info    | Arg {commandName.Text} {argument.ToDisplay()}");

                                        this.callbackOnNewsCommand?.Invoke(new Instruction(commandName.Text, argument));

                                        return curr;
                                    },
                                    () =>
                                    {
                                        // パース失敗☆（＾～＾）
                                        Trace.WriteLine($"Error   | {line}");
                                        return curr;
                                    });
                            }
                            else if (commandName.Text == InputLineParserLv1.PutsCommand)
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

                                            this.callbackOnPutsCommand?.Invoke(new Instruction(commandName.Text, argument));
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
                            else if (commandName.Text == InputLineParserLv1.SetsCommand)
                            {
                                curr = SetsInstructionArgumentParser.Parse(
                                    line,
                                    curr,
                                    (argument, curr) =>
                                    {
                                        Trace.WriteLine($"Info    | Arg {commandName.Text} {argument.ToDisplay()}");

                                        this.callbackOnSetsCommand?.Invoke(new Instruction(commandName.Text, argument));

                                        return curr;
                                    },
                                    () =>
                                    {
                                        // パース失敗☆（＾～＾）
                                        Trace.WriteLine($"Error   | {line}");
                                        return curr;
                                    });
                            }
                            else if (commandName.Text == InputLineParserLv1.SleepsCommand)
                            {
                                curr = SleepsInstructionArgumentParser.Parse(
                                    line,
                                    curr,
                                    (argument, curr) =>
                                    {
                                        Trace.WriteLine($"Info    | Arg {commandName.Text} {argument.ToDisplay()}");

                                        this.callbackOnSleepsCommand?.Invoke(new Instruction(commandName.Text, argument));

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

            if (instructions == null && this.callbackOnNoneCommand != null)
            {
                this.callbackOnNoneCommand();
            }
        }
    }
}
