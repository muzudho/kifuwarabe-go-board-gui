namespace KifuwarabeUec11Gui.Controller.Parser
{
    using System;
    using System.Globalization;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class ColumnAddressParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int ParsesCallback(ColumnAddress matched, int curr);

        /// <summary>
        /// 列番号の文字をパースして数にするぜ☆（＾～＾）
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static int Parse(string text, int start, ApplicationObjectModelWrapper appModel, ParsesCallback callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (text.Length < start + 1 || appModel == null)
            {
                return callback(null, start);
            }

            var oneChar = text[start].ToString(CultureInfo.CurrentCulture);
            var index = appModel.GetStringList(ApplicationObjectModel.ColumnNumbersRealName).Value.IndexOf(oneChar);

            if (index < 0)
            {
                // 該当なし☆（＾～＾）
                return callback(null, start);
            }

            return callback(new ColumnAddress(index), start + 1);
        }
    }
}
