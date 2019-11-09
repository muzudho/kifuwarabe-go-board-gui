namespace KifuwarabeUec11Gui
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
        private static Dictionary<string, string> inwardCanvasDictionary = new Dictionary<string, string>()
            {
                { ApplicationObjectModel.PlyOutsideName, "plyCanvas" },
                { LastMoveMarkerController.OutsideName, "lastMoveCanvas" },
                { ApplicationObjectModel.BlackNameOutsideName, "blackNameCanvas" },
                { ApplicationObjectModel.BlackTimeOutsideName, "blackTimeCanvas" },
                { ApplicationObjectModel.BlackHamaOutsideName, "blackAgehamaCanvas" },
                { ApplicationObjectModel.WhiteNameOutsideName, "whiteNameCanvas" },
                { ApplicationObjectModel.WhiteTimeOutsideName, "whiteTimeCanvas" },
                { ApplicationObjectModel.WhiteHamaOutsideName, "whiteAgehamaCanvas" },
                { ApplicationObjectModel.KomiOutsideName, "komiCanvas" },
                { ApplicationObjectModel.InfoOutsideName, "infoCanvas" },
            };

        public delegate void MatchCanvasCallbackDone(PropertyValue model, Canvas view);
        public delegate void MatchCanvasCallbackErr();

        public static void MatchCanvasBy(ApplicationObjectModel model, MainWindow view, string outsideName, MatchCanvasCallbackDone callbackDone, MatchCanvasCallbackErr callbackErr)
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

            if (inwardCanvasDictionary.ContainsKey(outsideName))
            {
                var insideName = inwardCanvasDictionary[outsideName];
                Canvas canvas = (Canvas)view.FindName(insideName);

                PropertyValue propValue = model.Properties[outsideName];

                callbackDone(propValue, canvas);
            }
            else
            {
                Trace.WriteLine($"Error           | widgetName:[{outsideName}] is not found.");
            }

            callbackErr();
        }

        public static void Repaint(ApplicationObjectModel model, MainWindow view, string widgetName)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            // JSONで使われている名前と、内部で使われている名前は分けるぜ☆（＾～＾）
            MatchCanvasBy(model, view, widgetName,
                (canvasWidgetModel, canvas)=>
                {
                    if (canvasWidgetModel.Visible)
                    {
                        canvas.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        canvas.Visibility = Visibility.Hidden;
                    }
                },
                ()=>
                {
                    Trace.WriteLine($"Error           | [{widgetName}] is not found in repaint.");
                });
        }

        public static void ChangeProperty(PropertyValue model, Canvas view, WidgetInstructionArgument args)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            switch (args.Property)
            {
                case "visible":
                    switch (args.Value)
                    {
                        case "true":
                            model.Visible = true;
                            view.Visibility = Visibility.Visible;
                            break;
                        case "false":
                            model.Visible = false;
                            view.Visibility = Visibility.Hidden;
                            break;
                    }
                    break;
            }
        }
    }
}
