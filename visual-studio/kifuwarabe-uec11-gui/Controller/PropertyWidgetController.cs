namespace KifuwarabeUec11Gui
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class PropertyWidgetController
    {
        /// <summary>
        /// 外向きの名前を、内向きの名前に変換だぜ☆（＾～＾）
        /// </summary>
        private static Dictionary<string, string> inwardDictionary = new Dictionary<string, string>()
            {
                { "ply", "plyCanvas" },
                { "move", "lastMoveCanvas" },
                { "b-name", "blackNameCanvas" },
                { "b-time", "blackTimeCanvas" },
                { "b-hama", "blackAgehamaCanvas" },
                { "w-name", "whiteNameCanvas" },
                { "w-time", "whiteTimeCanvas" },
                { "w-hama", "whiteAgehamaCanvas" },
                { "komi", "komiCanvas" },
                { "info", "infoCanvas" },
            };

        public delegate void MatchCanvasCallbackDone(PropertyWidgetModel model, Canvas view);
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

            if (inwardDictionary.ContainsKey(outsideName))
            {
                var insideName = inwardDictionary[outsideName];
                Canvas canvas = (Canvas)view.FindName(insideName);

                PropertyWidgetModel widgetModel = null;
                switch (outsideName)
                {
                    case "ply":
                        widgetModel = model.State.Ply;
                        break;
                    case "move":
                        widgetModel = model.State.Move;
                        break;
                    case "b-name":
                        widgetModel = model.State.BlackName;
                        break;
                    case "b-time":
                        widgetModel = model.State.BlackTime;
                        break;
                    case "b-hama":
                        widgetModel = model.State.BlackHama;
                        break;
                    case "w-name":
                        widgetModel = model.State.WhiteName;
                        break;
                    case "w-time":
                        widgetModel = model.State.WhiteTime;
                        break;
                    case "w-hama":
                        widgetModel = model.State.WhiteHama;
                        break;
                    case "komi":
                        widgetModel = model.State.Komi;
                        break;
                    case "info":
                        widgetModel = model.State.Info;
                        break;
                    default:
                        Trace.WriteLine($"Error           | widgetName:[{outsideName}] is not found.");
                        break;
                }

                callbackDone(widgetModel, canvas);
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

        public static void ChangeProperty(PropertyWidgetModel model, Canvas view, WidgetInstructionArgument args)
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
