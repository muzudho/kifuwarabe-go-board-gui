﻿namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Collections.Generic;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class RowNumbersController
    {
        public static string OutsideName => "row-numbers";

        public static void ChangeModel(ApplicationObjectModelForCSharp appModel, SetsInstructionArgument args)
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
                case "value":
                    appModel.StringLists[OutsideName].Value = PropertyStringList.FromString(args.Value);
                    break;
            }
        }
    }
}
