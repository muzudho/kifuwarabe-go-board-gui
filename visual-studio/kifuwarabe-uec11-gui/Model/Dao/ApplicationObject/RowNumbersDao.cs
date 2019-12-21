namespace KifuwarabeGoBoardGui.Model.Dao
{
    using System;
    using KifuwarabeGoBoardGui.Model.Dto;

    public static class RowNumbersDao
    {
        public static void ChangeModel(ApplicationObjectDtoWrapper appModel, SetsInstructionArgumentDto args)
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
                case "value": // thru
                case "":
                    var oldValue = appModel.GetStringList(ApplicationDto.RowNumbersRealName).Value;
                    var newValue = PropertyStringList.FromString(args.Value);
                    appModel.ModelChangeLogWriter.WriteLine($"{args.Name}.{args.Property}", string.Join(' ', oldValue), string.Join(' ', newValue));

                    // 特殊処理
                    {
                        appModel.GetStringList(ApplicationDto.RowNumbersRealName).SetAfterSetsValueCallback(appModel.TrimRowNumbers);
                    }

                    // イベント・ハンドラーを起こすぜ☆（＾～＾）
                    appModel.GetStringList(ApplicationDto.RowNumbersRealName).Value = newValue;
                    break;
            }
        }
    }
}
