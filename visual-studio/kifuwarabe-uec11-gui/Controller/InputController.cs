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
        public static void Go(ApplicationObjectModel appModel, MainWindow appView)
        {
            if (null == appModel)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (null == appView)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            var text = appView.InputTextReader.ReadToEnd();

            // 空行は無視☆（＾～＾）
            if (!string.IsNullOrWhiteSpace(text))
            {
                Trace.WriteLine($"Text            | {text}");
                appView.CommunicationLogWriter.WriteLine(text);
                appView.CommunicationLogWriter.Flush();
            }

            InputScriptDocument.Parse(text, appModel, (scriptDocument) =>
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
                                StoneController.ChangeColorToBlack(appModel, appView, zShapedIndex);

                                // 最後の着手点☆（＾～＾）
                                var text1 = CellAddress.FromIndex(zShapedIndex, appModel).ToDisplayTrimed(appModel);
                                appModel.Properties[LastMoveMarkerController.OutsideName].Value = text1;
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
                                StoneController.ChangeColorToWhite(appModel, appView, zShapedIndex);

                                // 最後の着手点☆（＾～＾）
                                var text1 = CellAddress.FromIndex(zShapedIndex, appModel).ToDisplayTrimed(appModel);
                                appModel.Properties[LastMoveMarkerController.OutsideName].Value = text1;
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
                                StoneController.ChangeColorToSpace(appModel, appView, zShapedIndex);
                            }
                        }
                    }
                    else if (instruction.Command == InputScriptDocument.BoardCommand)
                    {
                        var args = (BoardInstructionArgument)instruction.Argument;
                        int cellIndex = CellAddress.ToIndex(args.RowAddress.NumberO0, 0, appModel);
                        int length = cellIndex + appModel.Board.ColumnSize;
                        // Trace.WriteLine($"Command            | {instruction.Command} row={args.RowAddress.NumberO0} cellIndex={cellIndex} columns={args.Columns}");

                        // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                        foreach (var columnChar in args.Columns)
                        {
                            // Trace.WriteLine($"Column          | Ch=[{columnChar}]");
                            if (length <= cellIndex)
                            {
                                break;
                            }

                            switch (columnChar)
                            {
                                case 'b':
                                    // 黒石にするぜ☆（＾～＾）
                                    StoneController.ChangeColorToBlack(appModel, appView, cellIndex);
                                    cellIndex++;
                                    break;
                                case 'w':
                                    // 白石にするぜ☆（＾～＾）
                                    StoneController.ChangeColorToWhite(appModel, appView, cellIndex);
                                    cellIndex++;
                                    break;
                                case '.':
                                    // 空点にするぜ☆（＾～＾）
                                    StoneController.ChangeColorToSpace(appModel, appView, cellIndex);
                                    cellIndex++;
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
                    else if (instruction.Command == InputScriptDocument.WidgetCommand)
                    {
                        var args = (SetsInstructionArgument)instruction.Argument;

                        PropertyController.MatchCanvasBy(appModel, appView, args.Name,
                            (widgetModel, widgetView, insideStem) =>
                            {
                                PropertyController.ChangeProperty(widgetModel, widgetView, args);
                            },
                            (err) =>
                            {
                                    // Not found widget.

                                    if (args.Name == ColumnNumbersController.OutsideName)
                                {
                                    ColumnNumbersController.ChangeProperty(appModel, args);
                                }
                                else if (args.Name == RowNumbersController.OutsideName)
                                {
                                    RowNumbersController.ChangeProperty(appModel, args);
                                }
                                else if (args.Name == StarsController.OutsideName)
                                {
                                    StarsController.ChangeProperty(appModel, args);
                                }
                                else
                                {
                                }
                            });
                    }
                    else if (instruction.Command == InputScriptDocument.SetsCommand)
                    {
                        // モデルに値を設定するコマンドだぜ☆（＾～＾）
                        // ビューは、ここでは更新しないぜ☆（＾～＾）
                        var prop = (SetsInstructionArgument)instruction.Argument;

                        PropertyController.MatchCanvasBy(appModel, appView, prop.Name,
                            (propModel, propView, insideStem) =>
                            {
                                    // モデルに値をセット☆（＾～＾）
                                    propModel.Value = prop.Value;

                                Trace.WriteLine($"Found           | Outside:{prop.Name}, Inside:{insideStem} In InputController.Go. Updated={appModel.Properties[prop.Name].ToText()}");
                            },
                            (err) =>
                            {
                                if (prop.Name == ApplicationObjectModel.IntervalMsecOutsideName)
                                {
                                        // インターバル・ミリ秒☆（＾～＾）
                                        appModel.Properties[prop.Name].Value = prop.Value;
                                }
                                else if (prop.Name == LastMoveMarkerController.OutsideName)
                                {
                                    var start = 0;
                                    CellAddress.Parse(prop.Value, start, appModel, (cellAddress, curr) =>
                                    {
                                        if (cellAddress == null)
                                        {
                                            return start;
                                        }

                                        var text1 = cellAddress.ToDisplayTrimed(appModel);
                                        appModel.Properties[LastMoveMarkerController.OutsideName].Value = text1;
                                        appView.lastMoveValue.Content = text1;
                                        return curr;
                                    });
                                }
                                else if (prop.Name == BoardModel.RowSizeOutsideName)
                                {
                                    if (int.TryParse(prop.Value, out int outValue))
                                    {
                                            // 一応サイズに制限を付けておくぜ☆（＾～＾）
                                            if (0 < outValue && outValue < HyperParameter.MaxRowSize)
                                        {
                                            appModel.Board.RowSize = outValue;
                                        }
                                    }
                                }
                                else if (prop.Name == BoardModel.ColumnSizeOutsideName)
                                {
                                    if (int.TryParse(prop.Value, out int outValue))
                                    {
                                            // 一応サイズに制限を付けておくぜ☆（＾～＾）
                                            if (0 < outValue && outValue < HyperParameter.MaxColumnSize)
                                        {
                                            appModel.Board.ColumnSize = outValue;
                                        }
                                    }
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

                // 全ての入力から　モデルの変更に対応したぜ☆（＾～＾）！
                // あとは　モデルに合わせてビューを更新するだけだな☆（＾～＾）！
                {
                    // GUI出力 を書き込むやつ☆（＾～＾）
                    // Tickイベントでファイルの入出力するのも度胸があるよな☆（＾～＾）
                    // using文を使えば、開いたファイルは 終わったらすぐ閉じるぜ☆（＾～＾）
                    using (var outputJsonWriter = new OutputJsonWriter("output.json"))
                    {
                        outputJsonWriter.WriteLine(appModel.ToJson());
                        outputJsonWriter.Flush();
                    }
                }
            });
        }
    }
}
