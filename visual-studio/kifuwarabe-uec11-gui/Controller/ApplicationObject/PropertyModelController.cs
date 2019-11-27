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

            if (realName == null)
            {
                throw new ArgumentNullException(nameof(realName));
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
                        var oldValue = ((PropertyBool)propModel).Title;
                        var newValue = args.Value;
                        appModel.ModelChangeLogWriter.WriteLine($"{args.Name}.{args.Property}", oldValue, newValue);
                        ((PropertyBool)propModel).Title = newValue;
                    }
                    else if (propModel is PropertyNumber)
                    {
                        var oldValue = ((PropertyNumber)propModel).Title;
                        var newValue = args.Value;
                        appModel.ModelChangeLogWriter.WriteLine($"{args.Name}.{args.Property}", oldValue, newValue);
                        ((PropertyNumber)propModel).Title = newValue;
                    }
                    else if (propModel is PropertyString)
                    {
                        var oldValue = ((PropertyString)propModel).Title;
                        var newValue = args.Value;
                        appModel.ModelChangeLogWriter.WriteLine($"{args.Name}.{args.Property}", oldValue, newValue);
                        ((PropertyString)propModel).Title = newValue;
                    }
                    else if (propModel is PropertyStringList)
                    {
                        var oldValue = ((PropertyStringList)propModel).Title;
                        var newValue = args.Value;
                        appModel.ModelChangeLogWriter.WriteLine($"{args.Name}.{args.Property}", oldValue, newValue);
                        ((PropertyStringList)propModel).Title = newValue;
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
                        var newValue = new PropertyString(title);
                        appModel.ModelChangeLogWriter.WriteLine($"{args.Name}.{args.Property}", string.Empty, newValue.ValueAsText());
                        appModel.AddProperty(realName, newValue);
                    }
                    else if (args.Value == ApplicationObjectModel.NullType)
                    {
                        // どこにも追加しないぜ☆（＾～＾）
                    }
                    else if (args.Value == ApplicationObjectModel.NumberType)
                    {
                        var newValue = new PropertyNumber(title);
                        appModel.ModelChangeLogWriter.WriteLine($"{args.Name}.{args.Property}", string.Empty, newValue.ValueAsText());
                        appModel.AddProperty(realName, newValue);
                    }
                    else if (args.Value == ApplicationObjectModel.BoolType)
                    {
                        var newValue = new PropertyBool(title);
                        appModel.ModelChangeLogWriter.WriteLine($"{args.Name}.{args.Property}", string.Empty, newValue.ValueAsText());
                        appModel.AddProperty(realName, newValue);
                    }
                    else if (args.Value == ApplicationObjectModel.StringListType)
                    {
                        var newValue = new PropertyStringList(title, new List<string>());
                        appModel.ModelChangeLogWriter.WriteLine($"{args.Name}.{args.Property}", string.Empty, newValue.ValueAsText());
                        appModel.AddProperty(realName, newValue);
                    }
                    else
                    {
                        Trace.WriteLine($"Warning         | [{realName.Value}].type is fail. [{realName.Value}] is not found.");
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
                                {
                                    var oldValue = propModel.Visible;
                                    var newValue = true;
                                    appModel.ModelChangeLogWriter.WriteLine($"{args.Name}.{args.Property}", oldValue.ToString(), newValue.ToString());
                                    propModel.Visible = newValue;
                                }
                                break;
                            case "false":
                                {
                                    var oldValue = propModel.Visible;
                                    var newValue = false;
                                    appModel.ModelChangeLogWriter.WriteLine($"{args.Name}.{args.Property}", oldValue.ToString(), newValue.ToString());
                                    propModel.Visible = newValue;
                                }
                                break;
                            default:
                                Trace.WriteLine($"Warning         | {realName.Value}.visible is fail. {realName.Value} are not implemented.");
                                break;
                        }
                    }
                    break;

                case "value": // thru
                default:
                    // モデルに値をセット☆（＾～＾）
                    if (propModel == null)
                    {
                        Trace.WriteLine($"Warning         | {realName.Value}.value is fail. {realName.Value} is not found.");
                    }
                    else if (propModel is PropertyBool)
                    {
                        if (bool.TryParse(args.Value, out bool newValue))
                        {
                            var oldValue = ((PropertyBool)propModel).Value;
                            appModel.ModelChangeLogWriter.WriteLine($"{args.Name}.{args.Property}", oldValue.ToString(), newValue.ToString());
                            ((PropertyBool)propModel).Value = newValue;
                        }
                    }
                    else if (propModel is PropertyNumber)
                    {
                        if (double.TryParse(args.Value, out double newValue))
                        {
                            var oldValue = ((PropertyNumber)propModel).Value;
                            appModel.ModelChangeLogWriter.WriteLine($"{args.Name}.{args.Property}", oldValue.ToString(), newValue.ToString());
                            ((PropertyNumber)propModel).Value = newValue;
                        }
                    }
                    else if (propModel is PropertyString)
                    {
                        var oldValue = ((PropertyString)propModel).Value;
                        var newValue = args.Value;
                        appModel.ModelChangeLogWriter.WriteLine($"{args.Name}.{args.Property}", oldValue, newValue);
                        ((PropertyString)propModel).Value = newValue;
                    }
                    else if (propModel is PropertyStringList)
                    {
                        var oldValue = ((PropertyStringList)propModel).Value;
                        var newValue = PropertyStringList.FromString(args.Value);
                        appModel.ModelChangeLogWriter.WriteLine($"{args.Name}.{args.Property}", string.Join(' ', oldValue), string.Join(' ', newValue));
                        ((PropertyStringList)propModel).Value = newValue;
                    }

                    break;

            }
        }
    }
}
