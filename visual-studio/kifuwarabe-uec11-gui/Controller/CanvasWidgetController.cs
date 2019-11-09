namespace KifuwarabeUec11Gui
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class CanvasWidgetController
    {
        private static Dictionary<string, string> nameDictionary = new Dictionary<string, string>()
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

        public static (Canvas, CanvasWidgetModel) GetCanvasBy(ApplicationObjectModel model, MainWindow view, string widgetName)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            if (nameDictionary.ContainsKey(widgetName))
            {
                var name = nameDictionary[widgetName];
                Canvas canvas = (Canvas)view.FindName(name);

                CanvasWidgetModel widgetModel = null;
                switch (widgetName)
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
                    /* TODO
                    case "info":
                        widgetModel = model.State.Info;
                        break;
                    */
                    default:
                        Trace.WriteLine($"Error           | widgetName:[{widgetName}] is not found.");
                        break;
                }

                return (canvas, widgetModel);
            }
            else
            {
                Trace.WriteLine($"Error           | widgetName:[{widgetName}] is not found.");
            }

            return (null, null);
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
            var (canvas, canvasWidgetModel) = GetCanvasBy(model, view, widgetName);

            if (canvas != null)
            {
                if (canvasWidgetModel == null)
                {
                    Trace.WriteLine($"Error           | [{widgetName}] is not found in repaint.");
                }
                else if (canvasWidgetModel.Visible)
                {
                    canvas.Visibility = Visibility.Visible;
                }
                else
                {
                    canvas.Visibility = Visibility.Hidden;
                }
            }
        }

        public static void ChangeProperty(CanvasWidgetModel model, Canvas view, WidgetInstructionArgument args)
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
