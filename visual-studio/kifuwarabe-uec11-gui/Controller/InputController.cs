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
        public delegate void PutsViewCallback(PutsInstructionArgument args);
        public delegate void SetsViewCallback(SetsInstructionArgument args);

        public static void ParseByLine(
            ApplicationObjectModelWrapper appModel,
            string line,
            InfoViewCallback infoViewCallback,
            JsonViewCallback jsonViewCallback,
            PutsViewCallback putsViewCallback,
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
                    // Trace.WriteLine($"Command         | {instruction.Command}");

                    if (instruction.Command == InputScriptDocument.InfoCommand)
                    {
                        // `set info = banana` のシンタックス・シュガーだぜ☆（＾～＾）

                        // プロパティ☆（＾～＾）
                        var args = (InfoInstructionArgument)instruction.Argument;

                        // 改行コードに対応☆（＾～＾）ただし 垂直タブ（めったに使わんだろ） は除去☆（＾～＾）
                        infoViewCallback(MainWindow.SoluteNewline(args.Text));
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
                            Trace.WriteLine($"Info            | Alias2 [{alias.Value}] = [{args.RealName.Value}]");
                            if (!appModel.TryAddObjectRealName(alias, args.RealName))
                            {
                                Trace.WriteLine($"Info            | Alias2b [{alias.Value}] is already exists.");
                            }
                        }
                        Trace.WriteLine($"Info            | Alias3 {instruction.Command} RealName={args.RealName.Value} args=[{args.ToDisplay()}]");
                    }
                    else if (instruction.Command == InputScriptDocument.PutsCommand)
                    {
                        // モデルに値をセットしようぜ☆（＾～＾）
                        var args = (PutsInstructionArgument)instruction.Argument;

                        // エイリアスが設定されていれば変換するぜ☆（＾～＾）
                        appModel.MatchObjectRealName(
                            args.Name,
                            (RealName realName) =>
                            {
                                if (realName.Value == InputScriptDocument.BlackObject)
                                {
                                    var args = (PutsInstructionArgument)instruction.Argument;
                                    // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                                    foreach (var cellRange in args.Destination.CellRanges)
                                    {
                                        foreach (var zShapedIndex in cellRange.ToIndexes(appModel))
                                        {
                                            // 黒石にするぜ☆（＾～＾）
                                            StoneModelController.ChangeModelToBlack(appModel, zShapedIndex);
                                        }
                                    }
                                }
                                else if (realName.Value == InputScriptDocument.WhiteObject)
                                {
                                    var args = (PutsInstructionArgument)instruction.Argument;
                                    // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                                    foreach (var cellRange in args.Destination.CellRanges)
                                    {
                                        foreach (var zShapedIndex in cellRange.ToIndexes(appModel))
                                        {
                                            // 白石にするぜ☆（＾～＾）
                                            StoneModelController.ChangeModelToWhite(appModel, zShapedIndex);
                                        }
                                    }
                                }
                                else if (realName.Value == InputScriptDocument.SpaceObject)
                                {
                                    var args = (PutsInstructionArgument)instruction.Argument;
                                    // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                                    foreach (var cellRange in args.Destination.CellRanges)
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
                                    Trace.WriteLine($"Warning         | {instruction.Command} RealName=[{realName.Value}] args=[{args.ToDisplay(appModel)}] are not implemented.");
                                }
                            });

                        // ビューの更新は、呼び出し元でしろだぜ☆（＾～＾）
                        putsViewCallback(args);
                    }
                    else if (instruction.Command == InputScriptDocument.SetsCommand)
                    {
                        // モデルに値をセットしようぜ☆（＾～＾）
                        var args = (SetsInstructionArgument)instruction.Argument;

                        // エイリアスが設定されていれば変換するぜ☆（＾～＾）
                        appModel.MatchObjectRealName(
                            args.Name,
                            (RealName realName) =>
                            {
                                // これが参照渡しになっているつもりだが……☆（＾～＾）
                                appModel.MatchPropertyOption(
                                    realName,
                                    (propModel) =>
                                    {
                                        // .typeプロパティなら、propModelはヌルで構わない。
                                        PropertyModelController.ChangeModel(appModel, realName, propModel, args);
                                    },
                                    () =>
                                    {
                                        // モデルが無くても働くプロパティはある☆（＾～＾）
                                        PropertyModelController.ChangeModel(appModel, realName, null, args);
                                    });
                            });

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
