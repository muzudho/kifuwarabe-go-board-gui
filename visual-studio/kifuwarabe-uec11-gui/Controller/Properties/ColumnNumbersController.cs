namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Collections.Generic;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class ColumnNumbersController
    {
        public static string OutsideName => "column-numbers";

        public static List<string> FromString(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            var columns = text.Split(',');
            for (int i = 0; i < columns.Length; i++)
            {
                // ダブル・クォーテーションに挟まれているという前提だぜ☆（＾～＾）
                var token = columns[i].Trim();
                if (1 < token.Length)
                {
                    columns[i] = token.Substring(1, token.Length - 2);
                }
            }

            return new List<string>(columns);
        }

        public static void ChangeModel(ApplicationObjectModel appModel, SetsInstructionArgument args)
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
                case "value":
                    appModel.Properties[OutsideName].Value = args.Value;
                    break;
            }
        }
    }
}
