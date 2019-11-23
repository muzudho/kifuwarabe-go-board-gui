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

        /// <summary>
        /// TODO 内部では Alias ではなく、 RealName の方を使いたい☆（＾～＾）
        /// </summary>
        /// <param name="appModel"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static (PropertyType, IPropertyValue) FindProperty(
            ApplicationObjectModelWrapper appModel,
            string alias
        )
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (appModel.Strings.ContainsKey(alias))
            {
                return (PropertyType.StringType, appModel.Strings[alias]);
            }

            if (appModel.Numbers.ContainsKey(alias))
            {
                return (PropertyType.Number, appModel.Numbers[alias]);
            }

            if (appModel.Booleans.ContainsKey(alias))
            {
                return (PropertyType.Bool, appModel.Booleans[alias]);
            }

            if (appModel.StringLists.ContainsKey(alias))
            {
                return (PropertyType.StringList, appModel.StringLists[alias]);
            }

            return (PropertyType.None, null);
        }

        /// <summary>
        /// TODO 内部では Alias ではなく、 RealName の方を使いたい☆（＾～＾）
        /// </summary>
        /// <param name="appModel"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static (PropertyType, IPropertyValue) RemoveProperty(
            ApplicationObjectModelWrapper appModel,
            string alias
        )
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (appModel.Strings.ContainsKey(alias))
            {
                var old = appModel.Strings[alias];
                appModel.Strings.Remove(alias);
                return (PropertyType.StringType, old);
            }
            else if (appModel.Numbers.ContainsKey(alias))
            {
                var old = appModel.Numbers[alias];
                appModel.Numbers.Remove(alias);
                return (PropertyType.Number, old);
            }
            else if (appModel.Booleans.ContainsKey(alias))
            {
                var old = appModel.Booleans[alias];
                appModel.Booleans.Remove(alias);
                return (PropertyType.Bool, old);
            }
            else if (appModel.StringLists.ContainsKey(alias))
            {
                var old = appModel.StringLists[alias];
                appModel.StringLists.Remove(alias);
                return (PropertyType.StringList, old);
            }

            return (PropertyType.None, null);
        }

        /// <summary>
        /// TODO 内部では Alias ではなく、 RealName の方を使いたい☆（＾～＾）
        /// </summary>
        /// <param name="appModel"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static void AddProperty(
            ApplicationObjectModelWrapper appModel,
            string alias,
            IPropertyValue value
        )
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (value is PropertyString)
            {
                appModel.Strings.Add(alias, (PropertyString)value);
            }
            else if (value is PropertyNumber)
            {
                appModel.Numbers.Add(alias, (PropertyNumber)value);
            }
            else if (appModel.Booleans.ContainsKey(alias))
            {
                appModel.Booleans.Add(alias, (PropertyBool)value);
            }
            else if (appModel.StringLists.ContainsKey(alias))
            {
                appModel.StringLists.Add(alias, (PropertyStringList)value);
            }
        }

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
            if (appModel.ObjectRealNames.ContainsKey(outsideName))
            {
                insideStem = appModel.ObjectRealNames[outsideName];
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

                if (propModel == null)
                {
                    callbackErr($"outsideName:[{outsideName}] is null in model.");
                }
                else
                {
                    callbackDone(propModel, propView, insideStem);
                }
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

        public static void ChangeModel(ApplicationObjectModelWrapper appModel, string alias, IPropertyValue propModel, Canvas propView, SetsInstructionArgument args)
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

                case "type":
                    // TODO 型を変更☆（＾～＾） Value はクリアーされるぜ☆（＾～＾）
                    // var (propType, propValue) = FindProperty(appModel, alias);
                    var (propType, old) = RemoveProperty(appModel, alias);

                    // 新しい型のオブジェクトに換装☆（＾～＾）
                    switch (propType)
                    {
                        case PropertyType.StringType:
                            {
                                var brandnew = new PropertyString(old.Title);
                                AddProperty(appModel, alias, brandnew);
                            }
                            break;
                        case PropertyType.Number:
                            {
                                var brandnew = new PropertyNumber(old.Title);
                                AddProperty(appModel, alias, brandnew);
                            }
                            break;
                        case PropertyType.Bool:
                            {
                                var brandnew = new PropertyBool(old.Title);
                                AddProperty(appModel, alias, brandnew);
                            }
                            break;
                        case PropertyType.StringList:
                            {
                                var brandnew = new PropertyStringList(old.Title, new List<string>());
                                AddProperty(appModel, alias, brandnew);
                            }
                            break;
                        default:
                            Trace.WriteLine($"Error           | {propType.GetType().Name} type are not implemented.");
                            break;
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
