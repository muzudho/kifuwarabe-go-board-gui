namespace KifuwarabeGoBoardGui.Model.Dao
{
    using System;
    using System.Diagnostics;
    using KifuwarabeGoBoardGui.Controller.Parser;
    using KifuwarabeGoBoardGui.Model.Dto;
    using KifuwarabeGoBoardGui.Model.Dto.Application.Standard;

    /// <summary>
    /// メイン・ウィンドウがでかくなるから　こっちへ切り離すぜ☆（＾～＾）
    /// </summary>
    public class InputLineParserLv2
    {
        /// <summary>
        /// </summary>
        private InputLineParserLv2(ApplicationObjectDtoWrapper appModel, string line)
        {
            this.AppModel = appModel;
            this.Line = line;
        }

        private ApplicationObjectDtoWrapper AppModel { get; set; }
        private string Line { get; set; }

        public delegate void ReadsCallback(string text);

        public static void Read(ApplicationObjectDtoWrapper appModel, MainWindow appView, ReadsCallback callback)
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

            appView.InputTextReader.ReadToEnd(
                (text) =>
                {
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
                },
                (e) =>
                {
                    // 無視。
                });
        }

        public delegate void CommentViewCallback(string commentLine);
        public delegate void AliasViewCallback(Instruction aliasInstruction);
        public delegate void InfoViewCallback(string infoLine);
        public delegate void JsonViewCallback(ApplicationObjectDtoWrapper jsonAppModel);
        public delegate void PutsViewCallback(PutsInstructionArgumentDto putsArgs);
        public delegate void SetsViewCallback(SetsInstructionArgumentDto setsArgs);
        public delegate void SleepsViewCallback(SleepsInstructionArgumentDto sleepsArgs);

        public delegate void NoneCallback();

        private Instruction AliasInstruction { get; set; }
        private string CommentLine { get; set; }
        private ApplicationObjectDtoWrapper JsonAppModel { get; set; }
        private PutsInstructionArgumentDto PutsArg { get; set; }
        private SetsInstructionArgumentDto SetsArg { get; set; }
        private SleepsInstructionArgumentDto SleepsArg { get; set; }

        public delegate void CallbackDone(InputLineParserLv2 inputLineModelController);
        public static void ParseLine(ApplicationObjectDtoWrapper appModel, string line, CallbackDone callbackDone)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (line == null)
            {
                throw new ArgumentNullException(nameof(line));
            }

            if (callbackDone == null)
            {
                throw new ArgumentNullException(nameof(callbackDone));
            }

            var instance = new InputLineParserLv2(appModel, line);

            new InputLineParserLv1()
                .AppendCallbackOnAliasCommand((aliasInstruction) =>
                {
                    var args = (AliasArgumentDto)aliasInstruction.Argument;
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
                })
                .AppendCallbackOnBoardCommand((boardInstruction) =>
                {
                    var args = (BoardArgumentDto)boardInstruction.Argument;
                    int zShapedIndex = CellAddress.ToIndex(args.RowAddress.NumberO0, 0, appModel);
                    int length = zShapedIndex + appModel.ColumnSize;
                    // Trace.WriteLine($"Command            | {instruction.Command} row={args.RowAddress.NumberO0} cellIndex={cellIndex} columns={args.Columns}");

                    // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                    foreach (var columnChar in args.Columns)
                    {
                        // Trace.WriteLine($"Column          | Ch=[{columnChar}]");
                        if (length <= zShapedIndex)
                        {
                            break;
                        }

                        // TODO レイヤー番号。
                        var layerIndex = 0;

                        switch (columnChar)
                        {
                            case 'b':
                                // 黒石にするぜ☆（＾～＾）
                                PieceDao.ChangeModel(appModel, new PieceDto(ColorDto.Black, PieceShapes.Stone), layerIndex, zShapedIndex);
                                zShapedIndex++;
                                break;
                            case 'w':
                                // 白石にするぜ☆（＾～＾）
                                PieceDao.ChangeModel(appModel, new PieceDto(ColorDto.White, PieceShapes.Stone), layerIndex, zShapedIndex);
                                zShapedIndex++;
                                break;
                            case '.':
                                // 空点にするぜ☆（＾～＾）
                                PieceDao.ChangeModel(appModel, new PieceDto(ColorDto.Transparent, PieceShapes.Stone), layerIndex, zShapedIndex);
                                zShapedIndex++;
                                break;
                        }
                    }
                })
                .AppendCallbackOnCommentCommand((commentLine) =>
                {
                    instance.CommentLine = commentLine;
                })
                .AppendCallbackOnExitsCommand((exitsInstruction) =>
                {
                    // このアプリケーションを終了します。
                    System.Windows.Application.Current.Shutdown();
                })
                .AppendCallbackOnInfoCommand((infoInstruction) =>
                {
                    // `set info = banana` のシンタックス・シュガーだぜ☆（＾～＾）

                    // プロパティ☆（＾～＾）
                    var args = (InfoInstructionArgumentDto)infoInstruction.Argument;

                    // 改行コードに対応☆（＾～＾）ただし 垂直タブ（めったに使わんだろ） は除去☆（＾～＾）
                    var text = MainWindow.SoluteNewline(args.Text);
                    instance.AppModel.AddString(ApplicationDto.InfoRealName, new PropertyString("", text));
                })
                .AppendCallbackOnJsonCommand((jsonInstruction) =>
                {
                    var args = (JsonInstructionArgumentDto)jsonInstruction.Argument;
                    Trace.WriteLine($"Json            | {jsonInstruction.Command} args.Json.Length={args.Json.Length}");

                    instance.JsonAppModel = new ApplicationObjectDtoWrapper(ApplicationDto.Parse(args.Json));
                })
                .AppendCallbackOnNewsCommand((newsInstruction) =>
                {
                    Trace.WriteLine($"Warning         | Unimplemented newsInstruction.");

                    // 引数を取ろうぜ☆（＾～＾）
                    var args1 = (NewsInstructionArgumentDto)newsInstruction.Argument;

                    // エイリアスが設定されていれば変換するぜ☆（＾～＾）
                    var realName = appModel.GetObjectRealName(args1.InstanceName);

                    // プロパティが無くても働く☆（＾～＾）
                    PropertyDao.CreateProperty(appModel, realName, args1.InstanceName, args1.TypeName);
                })
                .AppendCallbackOnPutsCommand((putsInstruction) =>
                    {
                        // モデルに値をセットしようぜ☆（＾～＾）
                        var args1 = (PutsInstructionArgumentDto)putsInstruction.Argument;

                        // TODO レイヤー番号。
                        var layerIndex = 0;

                        // TODO 色名から色オブジェクトへ変換☆（＾～＾）
                        ColorDao.CreateColor(
                            appModel,
                            args1.ColorName,
                            (colorDto) =>
                            {
                                var args2 = (PutsInstructionArgumentDto)putsInstruction.Argument;
                                // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                                foreach (var cellRange in args2.Destination.CellRanges)
                                {
                                    foreach (var zShapedIndex in cellRange.ToIndexes(appModel))
                                    {
                                        PieceDao.ChangeModel(appModel, new PieceDto(colorDto, PieceShapes.Stone), layerIndex, zShapedIndex);
                                    }
                                }

                                // ビューの更新は、呼び出し元でしろだぜ☆（＾～＾）
                                instance.PutsArg = args1;
                            },
                            (err) =>
                            {
                                Trace.WriteLine($"Error   | Catch.199. {err}");
                            });
                    })
                .AppendCallbackOnSetsCommand((setsInstruction) =>
                    {
                        // 引数を取ろうぜ☆（＾～＾）
                        var args1 = (SetsInstructionArgumentDto)setsInstruction.Argument;

                        // エイリアスが設定されていれば変換するぜ☆（＾～＾）
                        var realName = appModel.GetObjectRealName(args1.Name);

                        // これが参照渡しになっているつもりだが……☆（＾～＾）
                        appModel.MatchPropertyOption(
                            realName,
                            (propModel) =>
                            {
                                // .typeプロパティなら、propModelはヌルで構わない。
                                PropertyDao.ChangeModel(appModel, realName, propModel, args1);
                            },
                            () =>
                            {
                                // モデルが無くても .typeプロパティ は働く☆（＾～＾）
                                PropertyDao.ChangeModel(appModel, realName, null, args1);
                            });

                        // というか、一般プロパティじゃない可能性があるぜ☆（＾～＾）
                        // 列番号☆（＾～＾）
                        if (realName.Value == ApplicationDto.ColumnNumbersRealName.Value)
                        {
                            Trace.WriteLine($"Info    | Column numbers change model.");
                            ColumnNumbersDao.ChangeModel(appModel, args1);
                        }
                        // 行番号☆（＾～＾）
                        else if (realName.Value == ApplicationDto.RowNumbersRealName.Value)
                        {
                            Trace.WriteLine($"Info    | Row numbers change model.");
                            RowNumbersDao.ChangeModel(appModel, args1);
                        }
                        // 盤上の星☆（＾～＾）
                        else if (realName.Value == ApplicationDto.StarsRealName.Value)
                        {
                            Trace.WriteLine($"Info    | Stars change model.");
                            StarsDao.ChangeModel(appModel, args1);
                        }

                        // ビューの更新は、呼び出し元でしろだぜ☆（＾～＾）
                        instance.SetsArg = args1;
                    })
                .AppendCallbackOnSleepsCommand(
                    (sleepsInstruction) =>
                    {
                        // プロパティ☆（＾～＾）
                        var args1 = (SleepsInstructionArgumentDto)sleepsInstruction.Argument;

                        // ビューの更新は、呼び出し元でしろだぜ☆（＾～＾）
                        instance.SleepsArg = args1;

                        // UIタイマーの方で操作することにした☆（＾～＾）
                        /*
                        // 指定ミリ秒待機☆（＾～＾）
                        Task.Run(async () =>
                        {
                            await Task.Delay(args1.MilliSeconds).ConfigureAwait(false);
                        }).Wait();
                        */
                    })
                .ParseByLine(line, appModel);

            callbackDone(instance);
        }

        public InputLineParserLv2 ThenAlias(AliasViewCallback aliasViewCallback, NoneCallback noneCallback)
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

        public InputLineParserLv2 ThenComment(CommentViewCallback commentViewCallback, NoneCallback noneCallback)
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

        public InputLineParserLv2 ThenInfo(InfoViewCallback infoViewCallback, NoneCallback noneCallback)
        {
            if (infoViewCallback == null)
            {
                throw new ArgumentNullException(nameof(infoViewCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            if (this.AppModel.ContainsKeyOfStrings(ApplicationDto.InfoRealName))
            {
                var infoProperty = this.AppModel.GetString(ApplicationDto.InfoRealName);
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

        public InputLineParserLv2 ThenJson(JsonViewCallback jsonViewCallback, NoneCallback noneCallback)
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

        public InputLineParserLv2 ThenPut(PutsViewCallback putsViewCallback, NoneCallback noneCallback)
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

        public InputLineParserLv2 ThenSet(SetsViewCallback setsViewCallback, NoneCallback noneCallback)
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

        public InputLineParserLv2 ThenSleep(SleepsViewCallback sleepsViewCallback, NoneCallback noneCallback)
        {
            if (sleepsViewCallback == null)
            {
                throw new ArgumentNullException(nameof(sleepsViewCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            if (this.SleepsArg == null)
            {
                noneCallback();
            }
            else
            {
                sleepsViewCallback(this.SleepsArg);
            }

            return this;
        }
    }
}
