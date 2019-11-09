namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class PropertyController
    {
        /// <summary>
        /// 外向きの名前を、内向きの名前に変換だぜ☆（＾～＾）
        /// 行番号など　１つの Canvas にまとまっていないものは　ここに入ってないぜ☆（＾～＾）
        /// </summary>
        private static Dictionary<string, string> inwardCanvasStemDictionary = new Dictionary<string, string>()
            {
                { ApplicationObjectModel.PlyOutsideName, "ply" },
                { LastMoveMarkerController.OutsideName, "lastMove" },
                { ApplicationObjectModel.BlackNameOutsideName, "blackName" },
                { ApplicationObjectModel.BlackTimeOutsideName, "blackTime" },
                { ApplicationObjectModel.BlackHamaOutsideName, "blackAgehama" },
                { ApplicationObjectModel.WhiteNameOutsideName, "whiteName" },
                { ApplicationObjectModel.WhiteTimeOutsideName, "whiteTime" },
                { ApplicationObjectModel.WhiteHamaOutsideName, "whiteAgehama" },
                { ApplicationObjectModel.KomiOutsideName, "komi" },
                { ApplicationObjectModel.InfoOutsideName, "info" },
            };

        public delegate void MatchCanvasCallbackDone(PropertyValue model, Canvas view, string insideStem);
        public delegate void MatchCanvasCallbackErr(string message);

        public static void MatchCanvasBy(
            ApplicationObjectModel model,
            MainWindow view,
            string outsideName,
            MatchCanvasCallbackDone callbackDone,
            MatchCanvasCallbackErr callbackErr)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            if (callbackDone == null)
            {
                throw new ArgumentNullException(nameof(callbackDone));
            }

            if (callbackErr == null)
            {
                throw new ArgumentNullException(nameof(callbackErr));
            }

            if (inwardCanvasStemDictionary.ContainsKey(outsideName))
            {
                var insideStem = inwardCanvasStemDictionary[outsideName];
                Canvas propView = (Canvas)view.FindName($"{insideStem}Canvas");

                // これが参照渡しになっているつもりだが……☆（＾～＾）
                PropertyValue propModel = model.Properties[outsideName];

                callbackDone(propModel, propView, insideStem);
            }
            else
            {
                callbackErr($"outsideName:[{outsideName}] is not found.");
            }
        }

        /// <summary>
        /// モデルに合わせるように、ビューを更新するぜ☆（＾～＾）
        /// </summary>
        /// <param name="appModel"></param>
        /// <param name="appView"></param>
        /// <param name="outsideName"></param>
        public static void RepaintByOutsideName(ApplicationObjectModel appModel, MainWindow appView, string outsideName)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (appView == null)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            // JSONで使われている名前と、内部で使われている名前は分けるぜ☆（＾～＾）
            MatchCanvasBy(appModel, appView, outsideName,
                (propModel, propView, insideStem) =>
                {
                    // あれば値☆（＾～＾）
                    {
                        var valueLabelName = $"{insideStem}Value";
                        var valueLabelView = (Label)propView.FindName(valueLabelName);
                        if (valueLabelView != null)
                        {
                            // 改行コードに対応☆（＾～＾）ただし 垂直タブ（めったに使わんだろ） は除去☆（＾～＾）
                            valueLabelView.Content = MainWindow.SoluteNewline(propModel.ToText());
                        }
                        else
                        {
                            Trace.WriteLine($"Error           | [{valueLabelName}] is not found in InputController.Go.");
                        }
                    }

                    // 表示・非表示☆（＾～＾）
                    if (propModel.Visible)
                    {
                        propView.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        propView.Visibility = Visibility.Hidden;
                    }
                },
                (err)=>
                {
                    Trace.WriteLine($"Error           | {err} In PropertyController.Repaint.");
                });
        }

        public static void ChangeModel(PropertyValue propModel, Canvas propView, SetsInstructionArgument args)
        {
            if (propModel == null)
            {
                throw new ArgumentNullException(nameof(propModel));
            }

            if (propView == null)
            {
                throw new ArgumentNullException(nameof(propView));
            }

            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            switch (args.Property)
            {
                case "value":
                    // モデルに値をセット☆（＾～＾）
                    propModel.Value = args.Value;
                    break;

                case "visible":
                    switch (args.Value)
                    {
                        case "true":
                            propModel.Visible = true;
                            // propView.Visibility = Visibility.Visible;
                            break;
                        case "false":
                            propModel.Visible = false;
                            // propView.Visibility = Visibility.Hidden;
                            break;
                    }
                    break;
            }
        }
    }
}
