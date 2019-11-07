namespace KifuwarabeUec11Gui
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class CanvasWidgetController
    {
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
