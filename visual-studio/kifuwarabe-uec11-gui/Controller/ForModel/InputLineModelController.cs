namespace KifuwarabeGoBoardGui.Controller
{
    using System;
    using System.Diagnostics;
    using KifuwarabeGoBoardGui.Controller.Parser;
    using KifuwarabeGoBoardGui.InputScript;
    using KifuwarabeGoBoardGui.Model;

    /// <summary>
    /// メイン・ウィンドウがでかくなるから　こっちへ切り離すぜ☆（＾～＾）
    /// </summary>
    public class InputLineModelController
    {
        /// <summary>
        /// </summary>
        private InputLineModelController(ApplicationObjectModelWrapper appModel, string line)
        {
            this.AppModel = appModel;
            this.Line = line;
        }

        private ApplicationObjectModelWrapper AppModel { get; set; }
        private string Line { get; set; }

        public delegate void ReadsCallback(string text);

        public static void Read(ApplicationObjectModelWrapper appModel, MainWindow appView, ReadsCallback callback)
        {
            if (null == appModel)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (null == appView)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            if (null == callback)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            var text = appView.InputTextReader.ReadToEnd();

            // 空行は無視☆（＾～＾）
            if (!string.IsNullOrWhiteSpace(text))
            {
                // ログに取るぜ☆（＾～＾）
                Trace.WriteLine($"Text            | {text}");
                appView.CommunicationLogWriter.WriteLine(text);
                appView.CommunicationLogWriter.Flush();
            }

            foreach (var line in text.Split(Environment.NewLine))
            {
                callback(line);
            }
        }

        public delegate void CommentViewCallback(string commentLine);
        public delegate void AliasViewCallback(Instruction aliasInstruction);
        public delegate void InfoViewCallback(string infoLine);
        public delegate void JsonViewCallback(ApplicationObjectModelWrapper jsonAppModel);
        public delegate void PutsViewCallback(PutsInstructionArgument putsArgs);
        public delegate void SetsViewCallback(SetsInstructionArgument setsArgs);

        public delegate void NoneCallback();

        private Instruction AliasInstruction { get; set; }
        private string CommentLine { get; set; }
        private ApplicationObjectModelWrapper JsonAppModel { get; set; }
        private PutsInstructionArgument PutsArg { get; set; }
        private SetsInstructionArgument SetsArg { get; set; }

        public static InputLineModelController ParseLine(ApplicationObjectModelWrapper appModel, string line)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (line == null)
            {
                throw new ArgumentNullException(nameof(line));
            }

            var instance = new InputLineModelController(appModel, line);

            InputLineParser.ParseByLine(
                line,
                appModel,
                (aliasInstruction) =>
                {
                    var args = (AliasInstructionArgument)aliasInstruction.Argument;
                    // Trace.WriteLine($"Info            | Alias1 RealName=[{args.RealName.Value}] args=[{args.ToDisplay()}]");

                    foreach (var alias in args.AliasList)
                    {
                        // Trace.WriteLine($"Info            | Alias2 [{alias.Value}] = [{args.RealName.Value}]");
                        if (!appModel.TryAddObjectRealName(alias, args.RealName))
                        {
                            Trace.WriteLine($"Warning         | Alias2b [{alias.Value}] is already exists.");
                        }
                    }
                    // Trace.WriteLine($"Info            | Alias3 {aliasInstruction.Command} RealName={args.RealName.Value} args=[{args.ToDisplay()}]");
                    instance.AliasInstruction = aliasInstruction;
                },
                (boardInstruction) =>
                {
                    var args = (BoardInstructionArgument)boardInstruction.Argument;
                    int zShapedIndex = CellAddress.ToIndex(args.RowAddress.NumberO0, 0, appModel);
                    int length = zShapedIndex + appModel.Board.ColumnSize;
                    // Trace.WriteLine($"Command            | {instruction.Command} row={args.RowAddress.NumberO0} cellIndex={cellIndex} columns={args.Columns}");

                    // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                    foreach (var columnChar in args.Columns)
                    {
                        // Trace.WriteLine($"Column          | Ch=[{columnChar}]");
                        if (length <= zShapedIndex)
                        {
                            break;
                        }

                        switch (columnChar)
                        {
                            case 'b':
                                // 黒石にするぜ☆（＾～＾）
                                StoneModelController.ChangeModelToBlack(appModel, zShapedIndex);
                                zShapedIndex++;
                                break;
                            case 'w':
                                // 白石にするぜ☆（＾～＾）
                                StoneModelController.ChangeModelToWhite(appModel, zShapedIndex);
                                zShapedIndex++;
                                break;
                            case '.':
                                // 空点にするぜ☆（＾～＾）
                                StoneModelController.ChangeModelToSpace(appModel, zShapedIndex);
                                zShapedIndex++;
                                break;
                        }
                    }
                },
                (commentLine) =>
                {
                    instance.CommentLine = commentLine;
                },
                (exitsInstruction) =>
                {
                    // このアプリケーションを終了します。
                    System.Windows.Application.Current.Shutdown();
                },
                (infoInstruction) =>
                {
                    // `set info = banana` のシンタックス・シュガーだぜ☆（＾～＾）

                    // プロパティ☆（＾～＾）
                    var args = (InfoInstructionArgument)infoInstruction.Argument;

                    // 改行コードに対応☆（＾～＾）ただし 垂直タブ（めったに使わんだろ） は除去☆（＾～＾）
                    var text = MainWindow.SoluteNewline(args.Text);
                    instance.AppModel.AddString(ApplicationObjectModel.InfoRealName, new PropertyString("", text));
                },
                (jsonInstruction) =>
                {
                    var args = (JsonInstructionArgument)jsonInstruction.Argument;
                    Trace.WriteLine($"Json            | {jsonInstruction.Command} args.Json.Length={args.Json.Length}");

                    instance.JsonAppModel = new ApplicationObjectModelWrapper(ApplicationObjectModel.Parse(args.Json));
                },
                (putsInstruction) =>
                {
                    // モデルに値をセットしようぜ☆（＾～＾）
                    var args1 = (PutsInstructionArgument)putsInstruction.Argument;

                    // エイリアスが設定されていれば変換するぜ☆（＾～＾）
                    var realName = appModel.GetObjectRealName(args1.Name);

                    if (realName.Value == InputLineParser.BlackObject)
                    {
                        var args2 = (PutsInstructionArgument)putsInstruction.Argument;
                        // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                        foreach (var cellRange in args2.Destination.CellRanges)
                        {
                            foreach (var zShapedIndex in cellRange.ToIndexes(appModel))
                            {
                                // 黒石にするぜ☆（＾～＾）
                                StoneModelController.ChangeModelToBlack(appModel, zShapedIndex);
                            }
                        }
                    }
                    else if (realName.Value == InputLineParser.WhiteObject)
                    {
                        var args2 = (PutsInstructionArgument)putsInstruction.Argument;
                        // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                        foreach (var cellRange in args2.Destination.CellRanges)
                        {
                            foreach (var zShapedIndex in cellRange.ToIndexes(appModel))
                            {
                                // 白石にするぜ☆（＾～＾）
                                StoneModelController.ChangeModelToWhite(appModel, zShapedIndex);
                            }
                        }
                    }
                    else if (realName.Value == InputLineParser.SpaceObject)
                    {
                        var args2 = (PutsInstructionArgument)putsInstruction.Argument;
                        // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                        foreach (var cellRange in args2.Destination.CellRanges)
                        {
                            foreach (var zShapedIndex in cellRange.ToIndexes(appModel))
                            {
                                // 石を取り除くぜ☆（＾～＾）
                                StoneModelController.ChangeModelToSpace(appModel, zShapedIndex);
                            }
                        }
                    }
                    else
                    {
                        Trace.WriteLine($"Warning         | {putsInstruction.Command} RealName=[{realName.Value}] args=[{args1.ToDisplay(appModel)}] are not implemented.");
                    }

                    // ビューの更新は、呼び出し元でしろだぜ☆（＾～＾）
                    instance.PutsArg = args1;
                },
                (setsInstruction) =>
                {
                    // モデルに値をセットしようぜ☆（＾～＾）
                    var args1 = (SetsInstructionArgument)setsInstruction.Argument;

                    // エイリアスが設定されていれば変換するぜ☆（＾～＾）
                    var realName = appModel.GetObjectRealName(args1.Name);

                    // これが参照渡しになっているつもりだが……☆（＾～＾）
                    appModel.MatchPropertyOption(
                        realName,
                        (propModel) =>
                        {
                                // .typeプロパティなら、propModelはヌルで構わない。
                                PropertyModelController.ChangeModel(appModel, realName, propModel, args1);
                        },
                        () =>
                        {
                                // モデルが無くても .typeプロパティ は働く☆（＾～＾）
                                PropertyModelController.ChangeModel(appModel, realName, null, args1);

                                // というか、一般プロパティじゃない可能性があるぜ☆（＾～＾）
                                // 行サイズ☆（＾～＾）
                                if (realName.Value == ApplicationObjectModel.RowSizeRealName.Value)
                            {
                                if (int.TryParse(args1.Value, out int outValue))
                                {
                                        // 一応サイズに制限を付けておくぜ☆（＾～＾）
                                        if (0 < outValue && outValue <= HyperParameter.MaxRowSize)
                                    {
                                        appModel.Board.RowSize = outValue;
                                        Trace.WriteLine($"Info            | Row size. value=[{outValue}]");
                                    }
                                    else
                                    {
                                        Trace.WriteLine($"Warning         | Row size out of range. value=[{outValue}]");
                                    }
                                }
                                else
                                {
                                    Trace.WriteLine($"Warning         | Row size parse fail. value=[{args1.Value}]");
                                }
                            }
                                // 列サイズ☆（＾～＾）
                                else if (realName.Value == ApplicationObjectModel.ColumnSizeRealName.Value)
                            {
                                if (int.TryParse(args1.Value, out int outValue))
                                {
                                        // 一応サイズに制限を付けておくぜ☆（＾～＾）
                                        if (0 < outValue && outValue <= HyperParameter.MaxColumnSize)
                                    {
                                        appModel.Board.ColumnSize = outValue;
                                        Trace.WriteLine($"Info            | Column size {outValue}.");
                                    }
                                    else
                                    {
                                        Trace.WriteLine($"Warning         | Column size out of range. value=[{outValue}]");
                                    }
                                }
                                else
                                {
                                    Trace.WriteLine($"Warning         | Column size parse fail. value=[{args1.Value}]");
                                }
                            }
                                // 列番号☆（＾～＾）
                                else if (realName.Value == ApplicationObjectModel.ColumnNumbersRealName.Value)
                            {
                                Trace.WriteLine($"Info            | Column numbers.");
                                ColumnNumbersModelController.ChangeModel(appModel, args1);
                            }
                                // 行番号☆（＾～＾）
                                else if (realName.Value == ApplicationObjectModel.RowNumbersRealName.Value)
                            {
                                Trace.WriteLine($"Info            | Row numbers.");
                                RowNumbersModelController.ChangeModel(appModel, args1);
                            }
                                // 盤上の星☆（＾～＾）
                                else if (realName.Value == ApplicationObjectModel.StarsRealName.Value)
                            {
                                Trace.WriteLine($"Info            | Stars.");
                                StarsModelController.ChangeModel(appModel, args1);
                            }
                        });

                    // ビューの更新は、呼び出し元でしろだぜ☆（＾～＾）
                    instance.SetsArg = args1;
                },
                () =>
                {
                    // 何もしないぜ☆（＾～＾）
                });

            return instance;
        }

        public InputLineModelController ThenAlias(AliasViewCallback aliasViewCallback, NoneCallback noneCallback)
        {
            if (aliasViewCallback == null)
            {
                throw new ArgumentNullException(nameof(aliasViewCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            if (this.AliasInstruction == null)
            {
                noneCallback();
            }
            else
            {
                aliasViewCallback(this.AliasInstruction);
            }

            return this;
        }

        public InputLineModelController ThenComment(CommentViewCallback commentViewCallback, NoneCallback noneCallback)
        {
            if (commentViewCallback == null)
            {
                throw new ArgumentNullException(nameof(commentViewCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            if (this.CommentLine == null)
            {
                noneCallback();
            }
            else
            {
                commentViewCallback(this.CommentLine);
            }

            return this;
        }

        public InputLineModelController ThenInfo(InfoViewCallback infoViewCallback, NoneCallback noneCallback)
        {
            if (infoViewCallback == null)
            {
                throw new ArgumentNullException(nameof(infoViewCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            if (this.AppModel.ContainsKeyOfStrings(ApplicationObjectModel.InfoRealName))
            {
                var infoProperty = this.AppModel.GetString(ApplicationObjectModel.InfoRealName);
                if (infoProperty == null)
                {
                    noneCallback();
                }
                else
                {
                    infoViewCallback(infoProperty.ValueAsText());
                }
            }
            else
            {
                noneCallback();
            }

            return this;
        }

        public InputLineModelController ThenJson(JsonViewCallback jsonViewCallback, NoneCallback noneCallback)
        {
            if (jsonViewCallback == null)
            {
                throw new ArgumentNullException(nameof(jsonViewCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            if (this.JsonAppModel == null)
            {
                noneCallback();
            }
            else
            {
                jsonViewCallback(this.JsonAppModel);
            }

            return this;
        }

        public InputLineModelController ThenPut(PutsViewCallback putsViewCallback, NoneCallback noneCallback)
        {
            if (putsViewCallback == null)
            {
                throw new ArgumentNullException(nameof(putsViewCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            if (this.PutsArg == null)
            {
                noneCallback();
            }
            else
            {
                putsViewCallback(this.PutsArg);
            }

            return this;
        }

        public InputLineModelController ThenSet(SetsViewCallback setsViewCallback, NoneCallback noneCallback)
        {
            if (setsViewCallback == null)
            {
                throw new ArgumentNullException(nameof(setsViewCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            if (this.SetsArg == null)
            {
                noneCallback();
            }
            else
            {
                setsViewCallback(this.SetsArg);
            }

            return this;
        }
    }
}
