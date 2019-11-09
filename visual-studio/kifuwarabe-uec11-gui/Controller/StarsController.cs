namespace KifuwarabeUec11Gui
{
    using System;
    using System.Collections.Generic;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class StarsController
    {
        public static string OutsideName => "stars";

        public static void ChangeProperty(ApplicationObjectModel model, WidgetInstructionArgument args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            switch (args.Property)
            {
                case "value":
                    var cellAddresses = args.Value.Split(',');
                    for (int i = 0; i < cellAddresses.Length; i++)
                    {
                        // ダブル・クォーテーションに挟まれているという前提だぜ☆（＾～＾）
                        var token = cellAddresses[i].Trim();
                        if (1 < token.Length)
                        {
                            cellAddresses[i] = token.Substring(1, token.Length - 2);
                        }
                    }

                    model.Properties[OutsideName].Value = new List<string>(cellAddresses);
                    break;
            }
        }
    }
}
