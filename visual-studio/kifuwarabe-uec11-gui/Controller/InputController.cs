namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Diagnostics;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    /// <summary>
    /// メイン・ウィンドウがでかくなるから　こっちへ切り離すぜ☆（＾～＾）
    /// </summary>
    public static class InputController
    {
        public delegate void ReadsCallback(string text);

        public static void Read(ApplicationObjectModel appModel, MainWindow appView, ReadsCallback callback)
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

        public static void ParseByLine(ApplicationObjectModel appModel, MainWindow appView, string line)
        {
            if (null == appModel)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (null == appView)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            InputScriptDocument.ParseByLine(line, appModel, (scriptDocument) =>
            {
                if (scriptDocument == null)
                {
                    return;
                }

                foreach (var instruction in scriptDocument.Instructions)
                {
                    if (instruction.Command == InputScriptDocument.InfoCommand)
                    {
                        // `set info = banana` のシンタックス・シュガーだぜ☆（＾～＾）

                        // プロパティ☆（＾～＾）
                        var args = (InfoInstructionArgument)instruction.Argument;

                        // 改行コードに対応☆（＾～＾）ただし 垂直タブ（めったに使わんだろ） は除去☆（＾～＾）
                        appView.infoValue.Content = MainWindow.SoluteNewline(args.Text);
                    }
                    else if (instruction.Command == InputScriptDocument.BlackCommand)
                    {
                        var args = (ColorInstructionArgument)instruction.Argument;
                        // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                        foreach (var cellRange in args.CellRanges)
                        {
                            foreach (var zShapedIndex in cellRange.ToIndexes(appModel))
                            {
                                // 黒石にするぜ☆（＾～＾）
                                StoneController.ChangeModelToBlack(appModel, zShapedIndex);

                                // 最後の着手点☆（＾～＾）
                                var text1 = CellAddress.FromIndex(zShapedIndex, appModel).ToDisplayTrimed(appModel);
                                appModel.Strings[LastMoveMarkerController.OutsideName].Value = text1;
                                appView.lastMoveValue.Content = text1;
                            }
                        }
                    }
                    else if (instruction.Command == InputScriptDocument.WhiteCommand)
                    {
                        var args = (ColorInstructionArgument)instruction.Argument;
                        // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                        foreach (var cellRange in args.CellRanges)
                        {
                            foreach (var zShapedIndex in cellRange.ToIndexes(appModel))
                            {
                                // 白石にするぜ☆（＾～＾）
                                StoneController.ChangeModelToWhite(appModel, zShapedIndex);

                                // 最後の着手点☆（＾～＾）
                                var text1 = CellAddress.FromIndex(zShapedIndex, appModel).ToDisplayTrimed(appModel);
                                appModel.Strings[LastMoveMarkerController.OutsideName].Value = text1;
                                appView.lastMoveValue.Content = text1;
                            }
                        }
                    }
                    else if (instruction.Command == InputScriptDocument.SpaceCommand)
                    {
                        var args = (ColorInstructionArgument)instruction.Argument;
                        // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                        foreach (var cellRange in args.CellRanges)
                        {
                            foreach (var zShapedIndex in cellRange.ToIndexes(appModel))
                            {
                                // 石を取り除くぜ☆（＾～＾）
                                StoneController.ChangeModelToSpace(appModel, zShapedIndex);
                            }
                        }
                    }
                    else if (instruction.Command == InputScriptDocument.BoardCommand)
                    {
                        var args = (BoardInstructionArgument)instruction.Argument;
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
                                    StoneController.ChangeModelToBlack(appModel, zShapedIndex);
                                    zShapedIndex++;
                                    break;
                                case 'w':
                                    // 白石にするぜ☆（＾～＾）
                                    StoneController.ChangeModelToWhite(appModel, zShapedIndex);
                                    zShapedIndex++;
                                    break;
                                case '.':
                                    // 空点にするぜ☆（＾～＾）
                                    StoneController.ChangeModelToSpace(appModel, zShapedIndex);
                                    zShapedIndex++;
                                    break;
                            }
                        }
                    }
                    else if (instruction.Command == InputScriptDocument.JsonCommand)
                    {
                        var args = (JsonInstructionArgument)instruction.Argument;
                        Trace.WriteLine($"Command            | {instruction.Command} args.Json.Length={args.Json.Length}");

                        appView.SetModel(ApplicationObjectModel.Parse(args.Json));
                    }
                    else if (instruction.Command == InputScriptDocument.SetsCommand)
                    {
                        // モデルに値を設定するコマンドだぜ☆（＾～＾）
                        // ビューは、ここでは更新しないぜ☆（＾～＾）
                        var args = (SetsInstructionArgument)instruction.Argument;

                        PropertyController.MatchCanvasBy(appModel, appView, args.Name,
                            (propModel, propView, insideStem) =>
                            {
                                // モデルに値をセット☆（＾～＾）
                                PropertyController.ChangeModel(propModel, propView, args);

                                appModel.ReadProperty(
                                    args.Name,
                                    (b) =>
                                    {
                                        Trace.WriteLine($"Found           | Outside:{args.Name}, Inside:{insideStem} In InputController.Go. Updated={appModel.Booleans[args.Name].ToText()}");
                                    },
                                    (n) =>
                                    {
                                        Trace.WriteLine($"Found           | Outside:{args.Name}, Inside:{insideStem} In InputController.Go. Updated={appModel.Numbers[args.Name].ToText()}");
                                    },
                                    (s) =>
                                    {
                                        Trace.WriteLine($"Found           | Outside:{args.Name}, Inside:{insideStem} In InputController.Go. Updated={appModel.Strings[args.Name].ToText()}");
                                    },
                                    (sList) =>
                                    {
                                        Trace.WriteLine($"Found           | Outside:{args.Name}, Inside:{insideStem} In InputController.Go. Updated={appModel.StringLists[args.Name].ToText()}");
                                    }
                                    );
                            },
                            (err) =>
                            {
                                // Not found property.
                                if (args.Name == ApplicationObjectModel.IntervalMsecOutsideName)
                                {
                                    // インターバル・ミリ秒☆（＾～＾）
                                    if (double.TryParse(args.Value, out double outValue))
                                    {
                                        appModel.Numbers[args.Name].Value = outValue;
                                    }
                                }
                                else if (args.Name == LastMoveMarkerController.OutsideName)
                                {
                                    // 着手マーカー☆（＾～＾）
                                    var start = 0;
                                    CellAddress.Parse(args.Value, start, appModel, (cellAddress, curr) =>
                                    {
                                        if (cellAddress == null)
                                        {
                                            return start;
                                        }

                                        var text1 = cellAddress.ToDisplayTrimed(appModel);
                                        appModel.Strings[LastMoveMarkerController.OutsideName].Value = text1;
                                        appView.lastMoveValue.Content = text1;
                                        return curr;
                                    });
                                }
                                else if (args.Name == BoardModel.RowSizeOutsideName)
                                {
                                    // 行サイズ☆（＾～＾）
                                    if (int.TryParse(args.Value, out int outValue))
                                    {
                                        // 一応サイズに制限を付けておくぜ☆（＾～＾）
                                        if (0 < outValue && outValue < HyperParameter.MaxRowSize)
                                        {
                                            appModel.Board.RowSize = outValue;
                                        }
                                    }
                                }
                                else if (args.Name == BoardModel.ColumnSizeOutsideName)
                                {
                                    // 列サイズ☆（＾～＾）
                                    if (int.TryParse(args.Value, out int outValue))
                                    {
                                        // 一応サイズに制限を付けておくぜ☆（＾～＾）
                                        if (0 < outValue && outValue < HyperParameter.MaxColumnSize)
                                        {
                                            appModel.Board.ColumnSize = outValue;
                                        }
                                    }
                                }
                                else if (args.Name == ColumnNumbersController.OutsideName)
                                {
                                    // 列番号☆（＾～＾）
                                    ColumnNumbersController.ChangeModel(appModel, args);
                                }
                                else if (args.Name == RowNumbersController.OutsideName)
                                {
                                    // 行番号☆（＾～＾）
                                    RowNumbersController.ChangeModel(appModel, args);
                                }
                                else if (args.Name == StarsController.OutsideName)
                                {
                                    // 盤上の星☆（＾～＾）
                                    StarsController.ChangeModel(appModel, args);
                                }
                                else
                                {
                                    Trace.WriteLine($"Error           | {err} In InputController.Go.");
                                }
                            });
                    }
                    else if (instruction.Command == InputScriptDocument.ExitsCommand)
                    {
                        // このアプリケーションを終了します。
                        System.Windows.Application.Current.Shutdown();
                    }
                    else
                    {
                    }
                }
            });
        }
    }
}
