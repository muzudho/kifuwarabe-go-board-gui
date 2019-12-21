namespace KifuwarabeGoBoardGui.Controller
{
    using System;
    using KifuwarabeGoBoardGui.InputScript;
    using KifuwarabeGoBoardGui.Model;

    public static class ColumnNumbersModelController
    {
        public static void ChangeModel(ApplicationObjectModelWrapper appModel, SetsInstructionArgument args)
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
                case "value": // thru
                case "":
                    var oldValue = appModel.GetStringList(ApplicationObjectModel.ColumnNumbersRealName).Value;
                    var newValue = PropertyStringList.FromString(args.Value);
                    appModel.ModelChangeLogWriter.WriteLine($"{args.Name}.{args.Property}", string.Join(' ', oldValue), string.Join(' ', newValue));
                    appModel.GetStringList(ApplicationObjectModel.ColumnNumbersRealName).Value = newValue;
                    break;
            }
        }
    }
}
