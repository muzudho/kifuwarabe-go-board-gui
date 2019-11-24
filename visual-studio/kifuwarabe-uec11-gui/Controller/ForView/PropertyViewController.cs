namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using KifuwarabeUec11Gui.Model;

    public static class PropertyViewController
    {
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
    }
}
