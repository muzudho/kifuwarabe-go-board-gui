namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Collections.Generic;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class RowNumbersController
    {
        public static string OutsideName => "row-numbers";

        public static void ChangeProperty(ApplicationObjectModel model, SetsInstructionArgument args)
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

                    model.Properties[OutsideName].Value = new List<string>(rows);
                    break;
            }
        }
    }
}
