namespace KifuwarabeGoBoardGui.Model.Dao
{
    using System;
    using KifuwarabeGoBoardGui.Model.Dto;

    public static class ColumnNumbersDao
    {
        public static void ChangeModel(ApplicationObjectDtoWrapper appModel, SetsInstructionArgumentDto args)
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
                    var oldValue = appModel.GetStringList(ApplicationDto.LineColumnNumbersRealName).Value;
                    var newValue = PropertyStringList.FromString(args.Value);
                    appModel.ModelChangeLogWriter.WriteLine($"{args.Name}.{args.Property}", string.Join(' ', oldValue), string.Join(' ', newValue));
                    appModel.GetStringList(ApplicationDto.LineColumnNumbersRealName).Value = newValue;
                    break;
            }
        }
    }
}
