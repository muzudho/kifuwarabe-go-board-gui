namespace KifuwarabeUec11Gui
{
    using System;
    using System.Collections.Generic;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class ColumnNumbersController
    {
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

        public static void ChangeProperty(ApplicationObjectModel model, WidgetInstructionArgument args)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            switch (args.Property)
            {
                case "value":
                    model.Properties["column-numbers"].Value = args.Value;
                    break;
            }
        }
    }
}
