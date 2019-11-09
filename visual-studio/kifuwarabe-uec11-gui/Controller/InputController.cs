namespace KifuwarabeUec11Gui
{
    using System;
    using System.Diagnostics;
    using System.Windows.Controls;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    /// <summary>
    /// メイン・ウィンドウがでかくなるから　こっちへ切り離すぜ☆（＾～＾）
    /// </summary>
    public static class InputController
    {
        public static void Go(ApplicationObjectModel model, MainWindow view)
        {
            if (null == model)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (null == view)
            {
                throw new ArgumentNullException(nameof(view));
            }

            var text = view.InputTextReader.ReadToEnd();

            // 空行は無視☆（＾～＾）
            if (!string.IsNullOrWhiteSpace(text))
            {
                Trace.WriteLine($"Text            | {text}");
                view.CommunicationLogWriter.WriteLine(text);
                view.CommunicationLogWriter.Flush();
            }

            var scriptDocument = InputScriptDocument.Parse(text, model);
            if (scriptDocument != null)
            {
                foreach (var instruction in scriptDocument.Instructions)
                {
                    if (instruction.Command == InputScriptDocument.InfoCommand)
                    {
                        // `set info = banana` のシンタックス・シュガーだぜ☆（＾～＾）

                        // プロパティ☆（＾～＾）
                        var args = (InfoInstructionArgument)instruction.Argument;

                        // 改行コードに対応☆（＾～＾）ただし 垂直タブ（めったに使わんだろ） は除去☆（＾～＾）
                        view.infoValue.Content = MainWindow.SoluteNewline(args.Text);
                    }
                    else if (instruction.Command == InputScriptDocument.BlackCommand)
                    {
                        var args = (ColorInstructionArgument)instruction.Argument;
                        // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                        foreach (var cellRange in args.CellRanges)
                        {
                            foreach (var zShapedIndex in cellRange.ToIndexes(model))
                            {
                                // 黒石にするぜ☆（＾～＾）
                                StoneController.ChangeColorToBlack(model, view, zShapedIndex);

                                // 最後の着手点☆（＾～＾）
                                var text1 = CellAddress.FromIndex(zShapedIndex, model).ToDisplayTrimed(model);
                                model.Properties[LastMoveMarkerController.OutsideName].Value = text1;
                                view.lastMoveValue.Content = text1;
                            }
                        }
                    }
                    else if (instruction.Command == InputScriptDocument.WhiteCommand)
                    {
                        var args = (ColorInstructionArgument)instruction.Argument;
                        // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                        foreach (var cellRange in args.CellRanges)
                        {
                            foreach (var zShapedIndex in cellRange.ToIndexes(model))
                            {
                                // 白石にするぜ☆（＾～＾）
                                StoneController.ChangeColorToWhite(model, view, zShapedIndex);

                                // 最後の着手点☆（＾～＾）
                                var text1 = CellAddress.FromIndex(zShapedIndex, model).ToDisplayTrimed(model);
                                model.Properties[LastMoveMarkerController.OutsideName].Value = text1;
                                view.lastMoveValue.Content = text1;
                            }
                        }
                    }
                    else if (instruction.Command == InputScriptDocument.SpaceCommand)
                    {
                        var args = (ColorInstructionArgument)instruction.Argument;
                        // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                        foreach (var cellRange in args.CellRanges)
                        {
                            foreach (var zShapedIndex in cellRange.ToIndexes(model))
                            {
                                // 石を取り除くぜ☆（＾～＾）
                                StoneController.ChangeColorToSpace(model, view, zShapedIndex);
                            }
                        }
                    }
                    else if (instruction.Command == InputScriptDocument.BoardCommand)
                    {
                        var args = (BoardInstructionArgument)instruction.Argument;
                        int cellIndex = CellAddress.ToIndex(args.RowAddress.NumberO0, 0, model);
                        int length = cellIndex + model.Board.ColumnSize;
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
                                    StoneController.ChangeColorToBlack(model, view, cellIndex);
                                    cellIndex++;
                                    break;
                                case 'w':
                                    // 白石にするぜ☆（＾～＾）
                                    StoneController.ChangeColorToWhite(model, view, cellIndex);
                                    cellIndex++;
                                    break;
                                case '.':
                                    // 空点にするぜ☆（＾～＾）
                                    StoneController.ChangeColorToSpace(model, view, cellIndex);
                                    cellIndex++;
                                    break;
                            }
                        }
                    }
                    else if (instruction.Command == InputScriptDocument.JsonCommand)
                    {
                        var args = (JsonInstructionArgument)instruction.Argument;
                        Trace.WriteLine($"Command            | {instruction.Command} args.Json.Length={args.Json.Length}");

                        view.SetModel(ApplicationObjectModel.Parse(args.Json));
                    }
                    else if (instruction.Command == InputScriptDocument.WidgetCommand)
                    {
                        var args = (WidgetInstructionArgument)instruction.Argument;

                        PropertyController.MatchCanvasBy(model, view, args.Name,
                            (widgetModel, widgetView, insideStem) =>
                            {
                                PropertyController.ChangeProperty(widgetModel, widgetView, args);
                            },
                            (err) =>
                            {
                                // Not found widget.

                                if (args.Name == ColumnNumbersController.OutsideName)
                                {
                                    ColumnNumbersController.ChangeProperty(model, args);
                                }
                                else if (args.Name == RowNumbersController.OutsideName)
                                {
                                    RowNumbersController.ChangeProperty(model, args);
                                }
                                else if (args.Name == StarsController.OutsideName)
                                {
                                    StarsController.ChangeProperty(model, args);
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

                        PropertyController.MatchCanvasBy(model, view, prop.Name,
                            (widgetModel, widgetView, insideStem) =>
                            {
                                Trace.WriteLine($"Found           | Outside:{prop.Name}, Inside:{insideStem} In InputController.Go.");

                                // モデルに値をセット☆（＾～＾）
                                widgetModel.Value = prop.Value;

                                // ビューを更新☆（＾～＾）
                                // PropertyController.RepaintByOutsideName(model, view, prop.Name);
                            },
                            (err) =>
                            {
                                Trace.WriteLine($"Error           | {err} In InputController.Go.");
                            });

                        if (prop.Name == ApplicationObjectModel.IntervalMsecOutsideName)
                        {
                            model.Properties[prop.Name].Value = prop.Value;
                            if (int.TryParse(prop.Value, out int outValue))
                            {
                                view.DispatchTimer.Interval = TimeSpan.FromMilliseconds(outValue);
                                // Trace.WriteLine($"interval-msec: {model.State.IntervalMsec}");
                            }
                        }
                        else if (prop.Name == LastMoveMarkerController.OutsideName)
                        {
                            var (cellAddress, next) = CellAddress.Parse(prop.Value, 0, model);
                            if (cellAddress != null)
                            {
                                var text1 = cellAddress.ToDisplayTrimed(model);
                                model.Properties[LastMoveMarkerController.OutsideName].Value = text1;
                                view.lastMoveValue.Content = text1;
                            }
                        }
                        else if (prop.Name == ApplicationObjectModel.InfoOutsideName)
                        {
                            model.Properties[prop.Name].Value = prop.Value;

                            // 改行コードに対応☆（＾～＾）ただし 垂直タブ（めったに使わんだろ） は除去☆（＾～＾）
                            // view.infoValue.Content = MainWindow.SoluteNewline(prop.Value);
                        }
                        /*
                        else if (prop.Name == ApplicationObjectModel.PlyOutsideName)
                        {
                            model.Properties[prop.Name].Value = prop.Value;
                            view.plyValue.Content = prop.Value;
                        }
                        else if (prop.Name == ApplicationObjectModel.BlackNameOutsideName)
                        {
                            model.Properties[prop.Name].Value = prop.Value;
                            view.blackNameValue.Content = prop.Value;
                        }
                        else if (prop.Name == ApplicationObjectModel.BlackTimeOutsideName)
                        {
                            model.Properties[prop.Name].Value = prop.Value;
                            view.blackTimeValue.Content = prop.Value;
                        }
                        else if (prop.Name == ApplicationObjectModel.BlackHamaOutsideName)
                        {
                            model.Properties[prop.Name].Value = prop.Value;
                            view.blackAgehamaValue.Content = prop.Value;
                        }
                        else if (prop.Name == ApplicationObjectModel.WhiteNameOutsideName)
                        {
                            model.Properties[prop.Name].Value = prop.Value;
                            view.whiteNameValue.Content = prop.Value;
                        }
                        else if (prop.Name == ApplicationObjectModel.WhiteTimeOutsideName)
                        {
                            model.Properties[prop.Name].Value = prop.Value;
                            view.whiteTimeValue.Content = prop.Value;
                        }
                        else if (prop.Name == ApplicationObjectModel.WhiteHamaOutsideName)
                        {
                            model.Properties[prop.Name].Value = prop.Value;
                            view.whiteAgehamaValue.Content = prop.Value;
                        }
                        else if (prop.Name == ApplicationObjectModel.KomiOutsideName)
                        {
                            model.Properties[prop.Name].Value = prop.Value;
                            view.komiValue.Content = prop.Value;
                        }
                        */
                        else if (prop.Name == BoardModel.RowSizeOutsideName)
                        {
                            if (int.TryParse(prop.Value, out int outValue))
                            {
                                // 一応サイズに制限を付けておくぜ☆（＾～＾）
                                if (0 < outValue && outValue < HyperParameter.MaxRowSize)
                                {
                                    model.Board.RowSize = outValue;
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
                                    model.Board.ColumnSize = outValue;
                                }
                            }
                        }
                        else
                        {
                        }
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

                // 全ての入力に対応したぜ☆（＾～＾）！
                {
                    // GUI出力 を書き込むやつ☆（＾～＾）
                    // Tickイベントでファイルの入出力するのも度胸があるよな☆（＾～＾）
                    // using文を使えば、開いたファイルは 終わったらすぐ閉じるぜ☆（＾～＾）
                    using (var outputJsonWriter = new OutputJsonWriter("output.json"))
                    {
                        outputJsonWriter.WriteLine(view.Model.ToJson());
                        outputJsonWriter.Flush();
                    }

                    // JSONを反映しようぜ☆（＾～＾）
                    // 列番号
                    ColumnNumberController.Repaint(view.Model, view);

                    // 行番号
                    RowNumberController.Repaint(view.Model, view);

                    // 星
                    StarController.Repaint(view.Model, view);

                    // 石
                    for (int index = 0; index < HyperParameter.MaxCellCount; index++)
                    {
                        StoneController.Repaint(view.Model, view, index);
                    }

                    // 着手マーカー
                    LastMoveMarkerController.Repaint(view.Model, view);

                    // TODO UIウィジェット
                    PropertyController.RepaintByOutsideName(view.Model, view, ApplicationObjectModel.PlyOutsideName);
                    PropertyController.RepaintByOutsideName(view.Model, view, LastMoveMarkerController.OutsideName);
                    PropertyController.RepaintByOutsideName(view.Model, view, ApplicationObjectModel.BlackNameOutsideName);
                    PropertyController.RepaintByOutsideName(view.Model, view, ApplicationObjectModel.BlackTimeOutsideName);
                    PropertyController.RepaintByOutsideName(view.Model, view, ApplicationObjectModel.BlackHamaOutsideName);
                    PropertyController.RepaintByOutsideName(view.Model, view, ApplicationObjectModel.WhiteNameOutsideName);
                    PropertyController.RepaintByOutsideName(view.Model, view, ApplicationObjectModel.WhiteTimeOutsideName);
                    PropertyController.RepaintByOutsideName(view.Model, view, ApplicationObjectModel.WhiteHamaOutsideName);
                    PropertyController.RepaintByOutsideName(view.Model, view, ApplicationObjectModel.KomiOutsideName);
                    PropertyController.RepaintByOutsideName(view.Model, view, ApplicationObjectModel.InfoOutsideName);

                    // 画面のサイズに合わせて再描画しようぜ☆（＾～＾）
                    view.RepaintWindow();
                    view.InvalidateVisual();
                }
            }
        }
    }
}
