namespace KifuwarabeUec11Gui
{
    using System;
    using System.Collections.Generic;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class RowNumbersController
    {
        public static void ChangeProperty(ApplicationObjectModel model, WidgetInstructionArgument args)
        {
            if (model==null)
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
                    var rows = args.Value.Split(',');
                    for (int i = 0; i < rows.Length; i++)
                    {
                        // ダブル・クォーテーションに挟まれているという前提だぜ☆（＾～＾）
                        var token = rows[i].Trim();
                        if (1 < token.Length)
                        {
                            rows[i] = token.Substring(1, token.Length - 2);
                        }
                    }

                    model.Board.SetRowNumbers(new List<string>(rows));
                    break;
            }
        }
    }
}
