namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class RowNumbersController
    {
        public static void ChangeModel(ApplicationObjectModelWrapper appModel, SetsInstructionArgument args)
        {
            if (appModel==null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            switch (args.Property)
            {
                case "value":
                    appModel.GetStringList(ApplicationObjectModel.RowNumbersRealName).Value = PropertyStringList.FromString(args.Value);
                    break;
            }
        }
    }
}
