namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Windows.Controls;
    using KifuwarabeUec11Gui.Model;

    public delegate void SomeViewCallback(Canvas canvas);
    public delegate void NoneViewCallback(string err);

    public static class ApplicationViewController
    {
        public static void MatchCanvasBy(
            MainWindow appView,
            RealName realName,
            SomeViewCallback someCallback,
            NoneViewCallback noneCallback
        )
        {
            if (appView == null)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            if (realName == null)
            {
                throw new ArgumentNullException(nameof(realName));
            }

            if (someCallback == null)
            {
                throw new ArgumentNullException(nameof(someCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            // UIオブジェクトを検索するぜ☆（＾～＾）
            var tagName = $"{realName.Value}Canvas";
            Canvas propView = (Canvas)appView.FindName(tagName);
            if (propView == null)
            {
                noneCallback($"Warning         | {tagName} tag is not found in xaml.");
            }
            else
            {
                someCallback(propView);
            }
        }
    }
}
