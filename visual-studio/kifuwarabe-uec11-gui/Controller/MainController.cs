namespace KifuwarabeUec11Gui
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
    public static class MainController
    {
        public static void Go(ApplicationObjectModel model, MainWindow view)
        {
            if (null== model)
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
                                            if (int.TryParse(prop.Value, out int outValue))
                                            {
                                                model.State.Ply = outValue;
                                                view.plyValue.Content = outValue.ToString(CultureInfo.CurrentCulture);
                                            }
                                        }
                                        break;
                                    case "move":
                                        {
                                            var (cellAddress, next) = CellAddress.Parse(prop.Value, 0, model);
                                            if (cellAddress != null)
                                            {
                                                LastMoveMarkerController.SetAddress(model, view, cellAddress);
                                            }
                                        }
                                        break;
                                    case "b-name":
                                        model.State.BlackName = prop.Value;
                                        view.blackNameValue.Content = prop.Value;
                                        break;
                                    case "b-time":
                                        model.State.BlackTime = prop.Value;
                                        view.blackTimeValue.Content = prop.Value;
                                        break;
                                    case "b-hama":
                                        {
                                            if (int.TryParse(prop.Value, out int outValue))
                                            {
                                                model.State.BlackHama = outValue;
                                                view.blackAgehamaValue.Content = outValue.ToString(CultureInfo.CurrentCulture);
                                            }
                                        }
                                        break;
                                    case "w-name":
                                        model.State.WhiteName = prop.Value;
                                        view.whiteNameValue.Content = prop.Value;
                                        break;
                                    case "w-time":
                                        model.State.WhiteTime = prop.Value;
                                        view.whiteTimeValue.Content = prop.Value;
                                        break;
                                    case "w-hama":
                                        {
                                            if (int.TryParse(prop.Value, out int outValue))
                                            {
                                                model.State.WhiteHama = outValue;
                                                view.whiteAgehamaValue.Content = outValue.ToString(CultureInfo.CurrentCulture);
                                            }
                                        }
                                        break;
                                    case "komi":
                                        {
                                            if (double.TryParse(prop.Value, out double outValue))
                                            {
                                                model.State.Komi = outValue;
                                                view.komiValue.Content = outValue.ToString(CultureInfo.CurrentCulture);
                                            }
                                        }
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
                                        LastMoveMarkerController.SetIndex(model, view, zShapedIndex);
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
                                        LastMoveMarkerController.SetIndex(model, view, zShapedIndex);
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
                                        ChangeCanvasProperty(view.plyCanvas, args);
                                        break;

                                    case "move":
                                        ChangeCanvasProperty(view.lastMoveCanvas, args);
                                        break;

                                    case "b-name":
                                        ChangeCanvasProperty(view.blackNameCanvas, args);
                                        break;

                                    case "b-time":
                                        ChangeCanvasProperty(view.blackTimeCanvas, args);
                                        break;

                                    case "b-hama":
                                        ChangeCanvasProperty(view.blackAgehamaCanvas, args);
                                        break;

                                    case "w-name":
                                        ChangeCanvasProperty(view.whiteNameCanvas, args);
                                        break;

                                    case "w-time":
                                        ChangeCanvasProperty(view.whiteTimeCanvas, args);
                                        break;

                                    case "w-hama":
                                        ChangeCanvasProperty(view.whiteAgehamaCanvas, args);
                                        break;

                                    case "komi":
                                        ChangeCanvasProperty(view.komiCanvas, args);
                                        break;

                                    case "row-numbers":
                                        ChangeRowNumbers(args, model);
                                        break;

                                    case "column-numbers":
                                        ChangeColumnNumbers(args, model);
                                        break;

                                    case "stars":
                                        ChangeStars(args, model);
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
                                    if (length<=cellIndex)
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
                    }
                }

                // 全ての入力に対応したぜ☆（＾～＾）！
                {
                    // GUI出力 を書き込むやつ☆（＾～＾）
                    // Tickイベントでファイルの入出力するのも度胸があるよな☆（＾～＾）
                    // using文を使えば、開いたファイルは 終わったらすぐ閉じるぜ☆（＾～＾）
                    using (var outputJsonWriter = new OutputJsonWriter("output.json"))
                    {
                        outputJsonWriter.WriteLine(model.ToJson());
                        outputJsonWriter.Flush();
                    }
                    // 画面の再描画をしようぜ☆（＾～＾）
                    view.RepaintWindow();
                    view.InvalidateVisual();
                }
            }
        }

        private static void ChangeRowNumbers(WidgetInstructionArgument args, ApplicationObjectModel model)
        {
            switch (args.Property)
            {
                case "value":
                    var rows = args.Value.Split(',');
                    for (int i = 0; i < rows.Length; i++)
                    {
                        // ダブル・クォーテーションに挟まれているという前提だぜ☆（＾～＾）
                        var token = rows[i].Trim();
                        if (1 < token.Length)
                        {
                            rows[i] = token.Substring(1, token.Length - 2);
                        }
                    }

                    model.Board.SetRowNumbers(new List<string>(rows));
                    break;
            }
        }

        private static void ChangeColumnNumbers(WidgetInstructionArgument args, ApplicationObjectModel model)
        {
            switch (args.Property)
            {
                case "value":
                    var columns = args.Value.Split(',');
                    for (int i = 0; i < columns.Length; i++)
                    {
                        // ダブル・クォーテーションに挟まれているという前提だぜ☆（＾～＾）
                        var token = columns[i].Trim();
                        if (1 < token.Length)
                        {
                            columns[i] = token.Substring(1, token.Length - 2);
                        }
                    }

                    model.Board.SetColumnNumbers(new List<string>(columns));
                    break;
            }
        }

        private static void ChangeStars(WidgetInstructionArgument args, ApplicationObjectModel model)
        {
            switch (args.Property)
            {
                case "value":
                    var cellAddresses = args.Value.Split(',');
                    for (int i = 0; i < cellAddresses.Length; i++)
                    {
                        // ダブル・クォーテーションに挟まれているという前提だぜ☆（＾～＾）
                        var token = cellAddresses[i].Trim();
                        if (1 < token.Length)
                        {
                            cellAddresses[i] = token.Substring(1, token.Length - 2);
                        }
                    }

                    model.Board.SetStarCellAddresses(new List<string>(cellAddresses));
                    break;
            }
        }

        private static void ChangeCanvasProperty(Canvas canvas, WidgetInstructionArgument args)
        {
            switch (args.Property)
            {
                case "visible":
                    switch (args.Value)
                    {
                        case "true":
                            canvas.Visibility = Visibility.Visible;
                            break;
                        case "false":
                            canvas.Visibility = Visibility.Hidden;
                            break;
                    }
                    break;
            }
        }
    }
}
