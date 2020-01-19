namespace KifuwarabeGoBoardGui.Model.Dao
{
    using System;
    using System.Collections.Generic;
    using KifuwarabeGoBoardGui.Model.Dto;

    public static class StarsDao
    {
        public static void ChangeModel(ApplicationObjectDtoWrapper appModel, SetsInstructionArgumentDto args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            switch (args.Property)
            {
                case "value": // thru
                case "":
                    var cellAddresses = args.Value.Split(',');
                    for (int i = 0; i < cellAddresses.Length; i++)
                    {
                        // ダブル・クォーテーションに挟まれているという前提だぜ☆（＾～＾）
                        var token = cellAddresses[i].Trim();
                        if (1 < token.Length)
                        {
                            cellAddresses[i] = token[1..^1];
                        }
                    }

                    var oldValue = appModel.GetStringList(ApplicationDto.StarsRealName).Value;
                    var newValue = new List<string>(cellAddresses);
                    appModel.ModelChangeLogWriter.WriteLine($"{args.Name}.{args.Property}", string.Join(' ', oldValue), string.Join(' ', newValue));
                    appModel.GetStringList(ApplicationDto.StarsRealName).Value = newValue;
                    break;
            }
        }
    }
}
