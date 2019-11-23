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
        public delegate void MatchCanvasCallbackDone(IPropertyValue model, Canvas view, string insideStem);
        public delegate void MatchCanvasCallbackErr(string message);

        public static void MatchCanvasBy(
            ApplicationObjectModelWrapper appModel,
            MainWindow appView,
            string outsideName,
            MatchCanvasCallbackDone callbackDone,
            MatchCanvasCallbackErr callbackErr)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (appView == null)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            if (callbackDone == null)
            {
                throw new ArgumentNullException(nameof(callbackDone));
            }

            if (callbackErr == null)
            {
                throw new ArgumentNullException(nameof(callbackErr));
            }

            // エイリアスが設定されていれば変換するぜ☆（＾～＾）
            var insideStem = string.Empty;
            if (appModel.ObjectRealName.ContainsKey(outsideName))
            {
                insideStem = appModel.ObjectRealName[outsideName];
            }

            // UIオブジェクトを検索するぜ☆（＾～＾）
            Canvas propView = (Canvas)appView.FindName($"{insideStem}Canvas");
            if (propView == null)
            {
                callbackErr($"outsideName:[{outsideName}] is not found in xaml.");
            }
            else
            {
                // これが参照渡しになっているつもりだが……☆（＾～＾）
                IPropertyValue propModel = appModel.ReadProperty(outsideName);

                callbackDone(propModel, propView, insideStem);
            }
        }

        /// <summary>
        /// モデルに合わせるように、ビューを更新するぜ☆（＾～＾）
        /// </summary>
        /// <param name="appModel"></param>
        /// <param name="appView"></param>
        /// <param name="outsideName"></param>
        public static void RepaintByOutsideName(ApplicationObjectModelWrapper appModel, MainWindow appView, string outsideName)
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
                    // あればタイトル☆（＾～＾）
                    {
                        var tagName = $"{insideStem}Title";
                        var tagView = (Label)propView.FindName(tagName);
                        if (tagView != null)
                        {
                            // 改行コードに対応☆（＾～＾）ただし 垂直タブ（めったに使わんだろ） は除去☆（＾～＾）
                            tagView.Content = MainWindow.SoluteNewline(propModel.Title);
                        }
                        else
                        {
                            Trace.WriteLine($"Error           | tagName=[{tagName}] is not found.");
                        }
                    }

                    // あれば値☆（＾～＾）
                    {
                        var tagName = $"{insideStem}Value";
                        var tagView = (Label)propView.FindName(tagName);
                        if (tagView != null)
                        {
                            // 改行コードに対応☆（＾～＾）ただし 垂直タブ（めったに使わんだろ） は除去☆（＾～＾）
                            tagView.Content = MainWindow.SoluteNewline(propModel.ValueAsText());
                        }
                        else
                        {
                            Trace.WriteLine($"Error           | tagName=[{tagName}] is not found.");
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
                (err) =>
                {
                    Trace.WriteLine($"Error           | {err} In PropertyController.Repaint.");
                });
        }

        public static void ChangeModel(IPropertyValue propModel, Canvas propView, SetsInstructionArgument args)
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
                case "title":
                    // モデルにタイトルをセット☆（＾～＾）
                    if (propModel is PropertyBool)
                    {
                        ((PropertyBool)propModel).Title = args.Value;
                    }
                    else if (propModel is PropertyNumber)
                    {
                        ((PropertyNumber)propModel).Title = args.Value;
                    }
                    else if (propModel is PropertyString)
                    {
                        ((PropertyString)propModel).Title = args.Value;
                    }
                    else if (propModel is PropertyStringList)
                    {
                        ((PropertyStringList)propModel).Title = args.Value;
                    }

                    break;

                case "value":
                    // モデルに値をセット☆（＾～＾）
                    if (propModel is PropertyBool)
                    {
                        if (bool.TryParse(args.Value, out bool outValue))
                        {
                            ((PropertyBool)propModel).Value = outValue;
                        }
                    }
                    else if (propModel is PropertyNumber)
                    {
                        if (double.TryParse(args.Value, out double outValue))
                        {
                            ((PropertyNumber)propModel).Value = outValue;
                        }
                    }
                    else if (propModel is PropertyString)
                    {
                        ((PropertyString)propModel).Value = args.Value;
                    }
                    else if (propModel is PropertyStringList)
                    {
                        ((PropertyStringList)propModel).Value = PropertyStringList.FromString(args.Value);
                    }

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
