namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Controls;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class PropertyModelController
    {
        public delegate void MatchCanvasCallbackDone(IPropertyValue model, Canvas view);
        public delegate void CallbackErr(string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appModel"></param>
        /// <param name="realName"></param>
        /// <param name="propModel">.typeプロパティを最初に設定するときはヌルで構わない。</param>
        /// <param name="args"></param>
        public static void ChangeModel(
            ApplicationObjectModelWrapper appModel,
            RealName realName,
            IPropertyValue propModel,
            SetsInstructionArgument args
        )
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
                case "title":
                    // モデルにタイトルをセット☆（＾～＾）
                    if (propModel == null)
                    {
                        Trace.WriteLine($"Warning         | {realName.Value}.title is fail. {realName.Value} is not found.");
                    }
                    else if (propModel is PropertyBool)
                    {
                        Trace.WriteLine($"Change          | PropertyBool title.");
                        ((PropertyBool)propModel).Title = args.Value;
                    }
                    else if (propModel is PropertyNumber)
                    {
                        Trace.WriteLine($"Change          | PropertyNumber title.");
                        ((PropertyNumber)propModel).Title = args.Value;
                    }
                    else if (propModel is PropertyString)
                    {
                        Trace.WriteLine($"Change          | PropertyString title.");
                        ((PropertyString)propModel).Title = args.Value;
                    }
                    else if (propModel is PropertyStringList)
                    {
                        Trace.WriteLine($"Change          | PropertyStringList title.");
                        ((PropertyStringList)propModel).Title = args.Value;
                    }

                    break;

                case "type":
                    // TODO 型を変更☆（＾～＾）既存のものがあればタイトルは引き継ぐが、 Value はクリアーされるぜ☆（＾～＾）
                    var title = string.Empty;

                    {
                        appModel.RemoveProperty(
                            realName,
                            (old) =>
                            {
                                if (old != null)
                                {
                                    title = old.Title;
                                }
                            },
                            () =>
                            {
                            });
                    }

                    // 新しい型のオブジェクトに換装☆（＾～＾）
                    if (args.Value == ApplicationObjectModel.StringType)
                    {
                        Trace.WriteLine($"Change          | Add new PropertyString.");
                        var brandnew = new PropertyString(title);
                        appModel.AddProperty(realName, brandnew);
                    }
                    else if (args.Value == ApplicationObjectModel.NullType)
                    {
                        // どこにも追加しないぜ☆（＾～＾）
                    }
                    else if (args.Value == ApplicationObjectModel.NumberType)
                    {
                        Trace.WriteLine($"Change          | Add new PropertyNumber.");
                        var brandnew = new PropertyNumber(title);
                        appModel.AddProperty(realName, brandnew);
                    }
                    else if (args.Value == ApplicationObjectModel.BoolType)
                    {
                        Trace.WriteLine($"Change          | Add new PropertyBool.");
                        var brandnew = new PropertyBool(title);
                        appModel.AddProperty(realName, brandnew);
                    }
                    else if (args.Value == ApplicationObjectModel.StringListType)
                    {
                        Trace.WriteLine($"Change          | Add new PropertyStringList.");
                        var brandnew = new PropertyStringList(title, new List<string>());
                        appModel.AddProperty(realName, brandnew);
                    }
                    else
                    {
                        Trace.WriteLine($"Warning         | [{realName.Value}].type is fail. [{realName.Value}] is not found.");
                    }

                    break;

                case "value":
                    // モデルに値をセット☆（＾～＾）
                    if (propModel == null)
                    {
                        Trace.WriteLine($"Warning         | {realName.Value}.value is fail. {realName.Value} is not found.");
                    }
                    else if (propModel is PropertyBool)
                    {
                        if (bool.TryParse(args.Value, out bool outValue))
                        {
                            Trace.WriteLine($"Change          | PropertyBool value.");
                            ((PropertyBool)propModel).Value = outValue;
                        }
                    }
                    else if (propModel is PropertyNumber)
                    {
                        if (double.TryParse(args.Value, out double outValue))
                        {
                            Trace.WriteLine($"Change          | PropertyNumber value.");
                            ((PropertyNumber)propModel).Value = outValue;
                        }
                    }
                    else if (propModel is PropertyString)
                    {
                        Trace.WriteLine($"Change          | PropertyString value.");
                        ((PropertyString)propModel).Value = args.Value;
                    }
                    else if (propModel is PropertyStringList)
                    {
                        Trace.WriteLine($"Change          | PropertyStringList value.");
                        ((PropertyStringList)propModel).Value = PropertyStringList.FromString(args.Value);
                    }

                    break;

                case "visible":
                    if (propModel == null)
                    {
                        Trace.WriteLine($"Warning         | {realName.Value}.visible is fail. {realName.Value} is not found.");
                    }
                    else
                    {
                        switch (args.Value)
                        {
                            case "true":
                                Trace.WriteLine($"Change          | Property visible true.");
                                propModel.Visible = true;
                                break;
                            case "false":
                                Trace.WriteLine($"Change          | Property visible false.");
                                propModel.Visible = false;
                                break;
                        }
                    }
                    break;
            }
        }
    }
}
