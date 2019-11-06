namespace KifuwarabeUec11Gui
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Output;

    /// <summary>
    /// メイン・ウィンドウがでかくなるから　こっちへ切り離すぜ☆（＾～＾）
    /// </summary>
    public static class MainController
    {
        public static void Go(BoardModel boardModel, MainWindow view)
        {
            if (null== boardModel)
            {
                throw new ArgumentNullException(nameof(boardModel));
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

            var scriptDocument = InputScriptDocument.Parse(text, boardModel);
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
                                                    boardModel.RowSize = outValue;
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
                                                    boardModel.ColumnSize = outValue;
                                                }
                                            }
                                        }
                                        break;

                                    case "ply":
                                        {
                                            if (int.TryParse(prop.Value, out int outValue))
                                            {
                                                view.State.Ply = outValue;
                                                view.plyValue.Content = outValue.ToString(CultureInfo.CurrentCulture);
                                            }
                                        }
                                        break;
                                    case "move":
                                        {
                                            var (cellAddress, next) = CellAddress.Parse(prop.Value, 0, boardModel);
                                            if (cellAddress != null)
                                            {
                                                LastMoveMarkerController.SetAddress(view.State, view, cellAddress);
                                            }
                                        }
                                        break;
                                    case "b-name":
                                        view.State.BlackName = prop.Value;
                                        view.blackNameValue.Content = prop.Value;
                                        break;
                                    case "b-time":
                                        view.State.BlackTime = prop.Value;
                                        view.blackTimeValue.Content = prop.Value;
                                        break;
                                    case "b-hama":
                                        {
                                            if (int.TryParse(prop.Value, out int outValue))
                                            {
                                                view.State.BlackHama = outValue;
                                                view.blackAgehamaValue.Content = outValue.ToString(CultureInfo.CurrentCulture);
                                            }
                                        }
                                        break;
                                    case "w-name":
                                        view.State.WhiteName = prop.Value;
                                        view.whiteNameValue.Content = prop.Value;
                                        break;
                                    case "w-time":
                                        view.State.WhiteTime = prop.Value;
                                        view.whiteTimeValue.Content = prop.Value;
                                        break;
                                    case "w-hama":
                                        {
                                            if (int.TryParse(prop.Value, out int outValue))
                                            {
                                                view.State.WhiteHama = outValue;
                                                view.whiteAgehamaValue.Content = outValue.ToString(CultureInfo.CurrentCulture);
                                            }
                                        }
                                        break;
                                    case "komi":
                                        {
                                            if (double.TryParse(prop.Value, out double outValue))
                                            {
                                                view.State.Komi = outValue;
                                                view.komiValue.Content = outValue.ToString(CultureInfo.CurrentCulture);
                                            }
                                        }
                                        break;

                                    case "info":
                                        view.State.Info = prop.Value;

                                        // 改行コードに対応☆（＾～＾）ただし 垂直タブ（めったに使わんだろ） は除去☆（＾～＾）
                                        view.infoValue.Content = MainWindow.SoluteNewline(prop.Value);
                                        break;

                                    case "interval-msec":
                                        {
                                            if (int.TryParse(prop.Value, out int outValue))
                                            {
                                                view.State.IntervalMsec = outValue;
                                                view.DispatchTimer.Interval = TimeSpan.FromMilliseconds(outValue);
                                                Trace.WriteLine($"interval-msec: {view.State.IntervalMsec}");
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
                                    foreach (var zShapedIndex in cellRange.ToIndexes(boardModel))
                                    {
                                        // 黒石にするぜ☆（＾～＾）
                                        StoneController.ChangeColorToBlack(boardModel, view, zShapedIndex);

                                        // 最後の着手点☆（＾～＾）
                                        LastMoveMarkerController.SetIndex(view.State, view, zShapedIndex);
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
                                    foreach (var zShapedIndex in cellRange.ToIndexes(boardModel))
                                    {
                                        // 白石にするぜ☆（＾～＾）
                                        StoneController.ChangeColorToWhite(boardModel, view, zShapedIndex);

                                        // 最後の着手点☆（＾～＾）
                                        LastMoveMarkerController.SetIndex(view.State, view, zShapedIndex);
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
                                    foreach (var zShapedIndex in cellRange.ToIndexes(boardModel))
                                    {
                                        // 石を取り除くぜ☆（＾～＾）
                                        StoneController.ChangeColorToSpace(boardModel, view, zShapedIndex);
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
                                        ChangeRowNumbers(args, boardModel);
                                        break;

                                    case "column-numbers":
                                        ChangeColumnNumbers(args, boardModel);
                                        break;
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
                        outputJsonWriter.WriteLine(new OutputJsonDocument(boardModel, view.State).ToJson());
                        outputJsonWriter.Flush();
                    }
                    // 画面の再描画をしようぜ☆（＾～＾）
                    view.RepaintWindow();
                    view.InvalidateVisual();
                }
            }
        }

        private static void ChangeRowNumbers(WidgetInstructionArgument args, BoardModel model)
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

                    model.SetRowNumbers(new List<string>(rows));
                    break;
            }
        }

        private static void ChangeColumnNumbers(WidgetInstructionArgument args, BoardModel model)
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

                    model.SetColumnNumbers(new List<string>(columns));
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
