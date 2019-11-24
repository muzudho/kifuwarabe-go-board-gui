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
        public delegate void CallbackErr(string message);

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
            ApplicationViewController.MatchCanvasBy(
                appView,
                realName,
                (propView) =>
                {
                    // これが参照渡しになっているつもりだが……☆（＾～＾）
                    appModel.MatchPropertyOption(
                        realName,
                        (propModel) =>
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
                        () =>
                        {
                            // モデルが無いなら値が分からん☆（＾～＾）
                            Trace.WriteLine($"Repaint Warning | [{realName.Value}] model is not found. In PropertyController.Repaint.");
                        });
                },
                (err) =>
                {
                    // ビューがないなら何もできん☆（＾～＾）
                    Trace.WriteLine(err);
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
                        Trace.WriteLine($"Warning         | {realName.Value}.title is fail. {realName.Value} is not found.");
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
                    // TODO 型を変更☆（＾～＾）既存のものがあればタイトルは引き継ぐが、 Value はクリアーされるぜ☆（＾～＾）
                    var title = string.Empty;

                    {
                        appModel.RemoveProperty(
                            realName,
                            (old) =>
                            {
                                if (old != null)
                                {
                                    title = old.Title;
                                }
                            },
                            () =>
                            {
                            });
                    }

                    // 新しい型のオブジェクトに換装☆（＾～＾）
                    if (args.Value == ApplicationObjectModel.StringType)
                    {
                        var brandnew = new PropertyString(title);
                        appModel.AddProperty(realName, brandnew);
                    }
                    else if (args.Value == ApplicationObjectModel.NullType)
                    {
                        // どこにも追加しないぜ☆（＾～＾）
                    }
                    else if (args.Value == ApplicationObjectModel.NumberType)
                    {
                        var brandnew = new PropertyNumber(title);
                        appModel.AddProperty(realName, brandnew);
                    }
                    else if (args.Value == ApplicationObjectModel.BoolType)
                    {
                        var brandnew = new PropertyBool(title);
                        appModel.AddProperty(realName, brandnew);
                    }
                    else if (args.Value == ApplicationObjectModel.StringListType)
                    {
                        var brandnew = new PropertyStringList(title, new List<string>());
                        appModel.AddProperty(realName, brandnew);
                    }
                    else
                    {
                        Trace.WriteLine($"Warning         | [{realName.Value}].type is fail. [{realName.Value}] is not found.");
                    }

                    break;

                case "value":
                    // モデルに値をセット☆（＾～＾）
                    if (propModel == null)
                    {
                        Trace.WriteLine($"Warning         | {realName.Value}.value is fail. {realName.Value} is not found.");
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
                        Trace.WriteLine($"Warning         | {realName.Value}.visible is fail. {realName.Value} is not found.");
                    }
                    else
                    {
                        switch (args.Value)
                        {
                            case "true":
                                propModel.Visible = true;
                                break;
                            case "false":
                                propModel.Visible = false;
                                break;
                        }
                    }
                    break;
            }
        }
    }
}
