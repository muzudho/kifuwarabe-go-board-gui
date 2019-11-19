namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Collections.Generic;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class StarsController
    {
        public static string OutsideName => "stars";

        public static void ChangeModel(ApplicationObjectModelForCSharp appModel, SetsInstructionArgument args)
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

                    appModel.StringLists[OutsideName].Value = new List<string>(cellAddresses);
                    break;
            }
        }
    }
}
