﻿namespace KifuwarabeUec11Gui
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows;
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
                    switch (instruction.Command)
                    {
                        case "exit":
                            {
                                // このアプリケーションを終了します。
                                System.Windows.Application.Current.Shutdown();
                            }
                            break;

                        case "info":
                            {
                                // `set info = banana` のシンタックス・シュガーだぜ☆（＾～＾）

                                // プロパティ☆（＾～＾）
                                var args = (InfoInstructionArgument)instruction.Argument;

                                // 改行コードに対応☆（＾～＾）ただし 垂直タブ（めったに使わんだろ） は除去☆（＾～＾）
                                view.infoValue.Content = MainWindow.SoluteNewline(args.Text);
                            }
                            break;

                        case "set":
                            {
                                // プロパティ☆（＾～＾）
                                var prop = (SetsInstructionArgument)instruction.Argument;
                                switch (prop.Name)
                                {
                                    case "row-size":
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
                                        break;

                                    case "column-size":
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
                                        break;

                                    case "ply":
                                        {
                                            model.State.Ply.Value = prop.Value;
                                            view.plyValue.Content = prop.Value;
                                        }
                                        break;
                                    case "move":
                                        {
                                            var (cellAddress, next) = CellAddress.Parse(prop.Value, 0, model);
                                            if (cellAddress != null)
                                            {
                                                var text1 = cellAddress.ToDisplayTrimed(model);
                                                model.State.Move.Value = text1;
                                                view.lastMoveValue.Content = text1;
                                            }
                                        }
                                        break;
                                    case "b-name":
                                        model.State.BlackName.Value = prop.Value;
                                        view.blackNameValue.Content = prop.Value;
                                        break;
                                    case "b-time":
                                        model.State.BlackTime.Value = prop.Value;
                                        view.blackTimeValue.Content = prop.Value;
                                        break;
                                    case "b-hama":
                                        model.State.BlackHama.Value = prop.Value;
                                        view.blackAgehamaValue.Content = prop.Value;
                                        break;
                                    case "w-name":
                                        model.State.WhiteName.Value = prop.Value;
                                        view.whiteNameValue.Content = prop.Value;
                                        break;
                                    case "w-time":
                                        model.State.WhiteTime.Value = prop.Value;
                                        view.whiteTimeValue.Content = prop.Value;
                                        break;
                                    case "w-hama":
                                        model.State.WhiteHama.Value = prop.Value;
                                        view.whiteAgehamaValue.Content = prop.Value;
                                        break;
                                    case "komi":
                                        model.State.Komi.Value = prop.Value;
                                        view.komiValue.Content = prop.Value;
                                        break;

                                    case "info":
                                        model.State.Info = prop.Value;

                                        // 改行コードに対応☆（＾～＾）ただし 垂直タブ（めったに使わんだろ） は除去☆（＾～＾）
                                        view.infoValue.Content = MainWindow.SoluteNewline(prop.Value);
                                        break;

                                    case "interval-msec":
                                        {
                                            if (int.TryParse(prop.Value, out int outValue))
                                            {
                                                model.State.IntervalMsec = outValue;
                                                view.DispatchTimer.Interval = TimeSpan.FromMilliseconds(outValue);
                                                Trace.WriteLine($"interval-msec: {model.State.IntervalMsec}");
                                            }
                                        }
                                        break;
                                }
                            }
                            break;

                        case "black":
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
                                        model.State.Move.Value = text1;
                                        view.lastMoveValue.Content = text1;
                                    }
                                }
                            }
                            break;

                        case "white":
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
                                        model.State.Move.Value = text1;
                                        view.lastMoveValue.Content = text1;
                                    }
                                }
                            }
                            break;

                        case "space":
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
                            break;

                        case "widget":
                            {
                                var args = (WidgetInstructionArgument)instruction.Argument;
                                switch (args.Name)
                                {
                                    case "ply":
                                        CanvasWidgetController.ChangeProperty(model.State.Ply, view.plyCanvas, args);
                                        break;

                                    case "move":
                                        CanvasWidgetController.ChangeProperty(model.State.Move, view.lastMoveCanvas, args);
                                        break;

                                    case "b-name":
                                        CanvasWidgetController.ChangeProperty(model.State.BlackName, view.blackNameCanvas, args);
                                        break;

                                    case "b-time":
                                        CanvasWidgetController.ChangeProperty(model.State.BlackTime, view.blackTimeCanvas, args);
                                        break;

                                    case "b-hama":
                                        CanvasWidgetController.ChangeProperty(model.State.BlackHama, view.blackAgehamaCanvas, args);
                                        break;

                                    case "w-name":
                                        CanvasWidgetController.ChangeProperty(model.State.WhiteName, view.whiteNameCanvas, args);
                                        break;

                                    case "w-time":
                                        CanvasWidgetController.ChangeProperty(model.State.WhiteTime, view.whiteTimeCanvas, args);
                                        break;

                                    case "w-hama":
                                        CanvasWidgetController.ChangeProperty(model.State.WhiteHama, view.whiteAgehamaCanvas, args);
                                        break;

                                    case "komi":
                                        CanvasWidgetController.ChangeProperty(model.State.Komi, view.komiCanvas, args);
                                        break;

                                    case "row-numbers":
                                        RowNumbersWidgetController.ChangeProperty(model, args);
                                        break;

                                    case "column-numbers":
                                        ColumnNumbersWidgetController.ChangeProperty(model, args);
                                        break;

                                    case "stars":
                                        StarsWidgetController.ChangeProperty(model, args);
                                        break;
                                }
                            }
                            break;

                        case "board":
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
                            break;

                        case "JSON":
                            {
                                var args = (JsonInstructionArgument)instruction.Argument;
                                Trace.WriteLine($"Command            | {instruction.Command} args.Json.Length={args.Json.Length}");

                                view.SetModel(ApplicationObjectModel.Parse(args.Json));
                            }
                            break;
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
                    ColumnNumberController.Repaint(view.Model, view);
                    RowNumberController.Repaint(view.Model, view);
                    StarController.Repaint(view.Model, view);
                    for (int index = 0; index < HyperParameter.MaxCellCount; index++)
                    {
                        StoneController.Repaint(view.Model, view, index);
                    }
                    LastMoveMarkerController.Repaint(view.Model, view);

                    // 画面の再描画をしようぜ☆（＾～＾）
                    view.RepaintWindow();
                    view.InvalidateVisual();
                }
            }
        }
    }
}
