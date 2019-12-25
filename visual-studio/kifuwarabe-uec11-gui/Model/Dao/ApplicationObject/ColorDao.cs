namespace KifuwarabeGoBoardGui.Model.Dao
{
    using System;
    using System.Windows.Media;
    using KifuwarabeGoBoardGui.Model.Dto;

    public static class ColorDao
    {
        public delegate void CallbackDone(ColorDto color);
        public delegate void CallbackErr(string err);

        public static void CreateColor(
            ApplicationObjectDtoWrapper appModel,
            ColorNameDto colorName,
            CallbackDone callbackSome,
            CallbackErr callbackNone)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (colorName == null)
            {
                throw new ArgumentNullException(nameof(colorName));
            }

            if (callbackSome == null)
            {
                throw new ArgumentNullException(nameof(callbackSome));
            }

            if (callbackNone == null)
            {
                throw new ArgumentNullException(nameof(callbackNone));
            }

            // エイリアスが設定されていれば変換するぜ☆（＾～＾）
            var colorRealName = new ColorNameDto(appModel.GetObjectRealName(colorName.Source).Value);

            try
            {
                var color = (Color)ColorConverter.ConvertFromString(colorRealName.TrimHyphen());
                callbackSome(new ColorDto(color.A, color.R, color.G, color.B));
            }
            catch (FormatException e)
            {
                callbackNone($"Warning | Invalid color name. colorName.Source=[{colorName.Source}]. / {e.Message}");
            }
        }
    }
}
