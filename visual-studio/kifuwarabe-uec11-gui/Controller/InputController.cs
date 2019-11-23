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

        public delegate void InfoViewCallback(string text);
        public delegate void JsonViewCallback(ApplicationObjectModelWrapper appModel);
        public delegate void SetsViewCallback(SetsInstructionArgument args);

        public static void ParseByLine(
            ApplicationObjectModelWrapper appModel,
            string line,
            InfoViewCallback infoViewCallback,
            JsonViewCallback jsonViewCallback,
            SetsViewCallback setsViewCallback
        )
        {
            if (null == appModel)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            InputScriptDocument.ParseByLine(line, appModel, (scriptDocument) =>
            {
                if (scriptDocument == null)
                {
                    return;
                }

                foreach (var instruction in scriptDocument.Instructions)
                {
                    Trace.WriteLine($"Command         | {instruction.Command}");

                    if (instruction.Command == InputScriptDocument.InfoCommand)
                    {
                        // `set info = banana` のシンタックス・シュガーだぜ☆（＾～＾）

                        // プロパティ☆（＾～＾）
                        var args = (InfoInstructionArgument)instruction.Argument;

                        // 改行コードに対応☆（＾～＾）ただし 垂直タブ（めったに使わんだろ） は除去☆（＾～＾）
                        infoViewCallback(MainWindow.SoluteNewline(args.Text));
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
                        Trace.WriteLine($"Json            | {instruction.Command} args.Json.Length={args.Json.Length}");

                        jsonViewCallback(new ApplicationObjectModelWrapper(ApplicationObjectModel.Parse(args.Json)));
                    }
                    else if (instruction.Command == InputScriptDocument.AliasCommand)
                    {
                        var args = (AliasInstructionArgument)instruction.Argument;
                        Trace.WriteLine($"Info            | Alias1 RealName=[{args.RealName.Value}] args=[{args.ToDisplay()}]");

                        foreach (var alias in args.AliasList)
                        {
                            Trace.WriteLine($"Info            | Alias2 [{alias}] = [{args.RealName.Value}]");
                            if (!appModel.TryAddObjectRealName(alias, args.RealName))
                            {
                                Trace.WriteLine($"Info            | Alias2b [{alias}] is already exists.");
                            }
                        }
                        Trace.WriteLine($"Info            | Alias3 {instruction.Command} RealName={args.RealName.Value} args=[{args.ToDisplay()}]");
                    }
                    else if (instruction.Command == InputScriptDocument.SetsCommand)
                    {
                        // モデルに値をセットしようぜ☆（＾～＾）
                        var args = (SetsInstructionArgument)instruction.Argument;

                        // エイリアスが設定されていれば変換するぜ☆（＾～＾）
                        RealName realName = appModel.GetObjectRealName(args.Name);

                        // これが参照渡しになっているつもりだが……☆（＾～＾）
                        var (type, propModel) = appModel.GetProperty(realName);

                        // .typeプロパティなら、propModelはヌルで構わない。
                        PropertyController.ChangeModel(appModel, realName, propModel, args);

                        // ビューの更新は、呼び出し元でしろだぜ☆（＾～＾）
                        setsViewCallback(args);
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
