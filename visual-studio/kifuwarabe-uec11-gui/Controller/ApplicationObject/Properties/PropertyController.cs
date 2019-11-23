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
        public delegate void MatchCanvasCallbackDone(IPropertyValue model, Canvas view);
        public delegate void MatchCanvasCallbackErr(string message);

        public static void MatchCanvasBy(
            ApplicationObjectModelWrapper appModel,
            MainWindow appView,
            RealName realName,
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

            // UIオブジェクトを検索するぜ☆（＾～＾）
            Canvas propView = (Canvas)appView.FindName($"{realName.Value}Canvas");
            if (propView == null)
            {
                callbackErr($"realName=[{realName.Value}] is not found in xaml.");
            }
            else
            {
                // これが参照渡しになっているつもりだが……☆（＾～＾）
                var (type, propModel) = appModel.GetProperty(realName);

                if (propModel == null)
                {
                    callbackErr($"realName=[{realName.Value}] is null in model.");
                }
                else
                {
                    callbackDone(propModel, propView);
                }
            }
        }

        /// <summary>
        /// モデルに合わせるように、ビューを更新するぜ☆（＾～＾）
        /// </summary>
        /// <param name="appModel"></param>
        /// <param name="appView"></param>
        /// <param name="outsideName"></param>
        public static void RepaintByName(ApplicationObjectModelWrapper appModel, MainWindow appView, RealName realName)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (appView == null)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            if (realName == null)
            {
                throw new ArgumentNullException(nameof(realName));
            }

            // JSONで使われている名前と、内部で使われている名前は分けるぜ☆（＾～＾）
            MatchCanvasBy(appModel, appView, realName,
                (propModel, propView) =>
                {
                    // あればタイトル☆（＾～＾）
                    {
                        var tagName = $"{realName.Value}Title";
                        var tagView = (Label)propView.FindName(tagName);
                        if (tagView != null)
                        {
                            // 改行コードに対応☆（＾～＾）ただし 垂直タブ（めったに使わんだろ） は除去☆（＾～＾）
                            tagView.Content = MainWindow.SoluteNewline(propModel.Title);
                        }
                        else
                        {
                            Trace.WriteLine($"Warning         | [{tagName}] tag is not found in xaml.");
                        }
                    }

                    // あれば値☆（＾～＾）
                    {
                        var tagName = $"{realName.Value}Value";
                        var tagView = (Label)propView.FindName(tagName);
                        if (tagView != null)
                        {
                            // 改行コードに対応☆（＾～＾）ただし 垂直タブ（めったに使わんだろ） は除去☆（＾～＾）
                            tagView.Content = MainWindow.SoluteNewline(propModel.ValueAsText());
                        }
                        else
                        {
                            Trace.WriteLine($"Warning         | [{tagName}] tag is not found in xaml.");
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appModel"></param>
        /// <param name="realName"></param>
        /// <param name="propModel">.typeプロパティを最初に設定するときはヌルで構わない。</param>
        /// <param name="args"></param>
        public static void ChangeModel(
            ApplicationObjectModelWrapper appModel,
            RealName realName,
            IPropertyValue propModel,
            SetsInstructionArgument args
        )
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            switch (args.Property)
            {
                case "title":
                    // モデルにタイトルをセット☆（＾～＾）
                    if (propModel == null)
                    {
                        Trace.WriteLine($"Error           | {realName.Value}.title is fail. {realName.Value} is not found.");
                    }
                    else if (propModel is PropertyBool)
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
                    var (propType, old) = appModel.RemoveProperty(realName);

                    // 新しい型のオブジェクトに換装☆（＾～＾）
                    switch (propType)
                    {
                        case PropertyType.StringType:
                            {
                                var brandnew = new PropertyString(old.Title);
                                appModel.AddProperty(realName, brandnew);
                            }
                            break;
                        case PropertyType.Number:
                            {
                                var brandnew = new PropertyNumber(old.Title);
                                appModel.AddProperty(realName, brandnew);
                            }
                            break;
                        case PropertyType.Bool:
                            {
                                var brandnew = new PropertyBool(old.Title);
                                appModel.AddProperty(realName, brandnew);
                            }
                            break;
                        case PropertyType.StringList:
                            {
                                var brandnew = new PropertyStringList(old.Title, new List<string>());
                                appModel.AddProperty(realName, brandnew);
                            }
                            break;
                        default:
                            Trace.WriteLine($"Error           | [{realName.Value}].type is fail. [{realName.Value}] is not found.");
                            break;
                    }
                    break;

                case "value":
                    // モデルに値をセット☆（＾～＾）
                    if (propModel == null)
                    {
                        Trace.WriteLine($"Error           | {realName.Value}.value is fail. {realName.Value} is not found.");
                    }
                    else if (propModel is PropertyBool)
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
                    if (propModel == null)
                    {
                        Trace.WriteLine($"Error           | {realName}.visible is fail. {realName} is not found.");
                    }
                    else
                    {
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
                    }
                    break;
            }
        }
    }
}
