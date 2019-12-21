namespace KifuwarabeGoBoardGui.Controller.Parser
{
    using System;
    using KifuwarabeGoBoardGui.Model.Dto;

    public static class SleepsInstructionArgumentParser
    {
        public delegate int CallbackSome(SleepsInstructionArgumentDto arg, int next);
        public delegate int CallbackNone();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <param name="callbackNone"></param>
        /// <param name="callbackSome"></param>
        /// <returns>Next</returns>
        public static int Parse(string text, int start, CallbackSome callbackSome, CallbackNone callbackNone)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (callbackSome == null)
            {
                throw new ArgumentNullException(nameof(callbackSome));
            }

            if (callbackNone == null)
            {
                throw new ArgumentNullException(nameof(callbackNone));
            }

            var next = start;

            // 行の残り全部を読み取るぜ☆（＾～＾）
            if (int.TryParse(text.Substring(next), out int ms))
            {
                return callbackSome(new SleepsInstructionArgumentDto(ms), next);
            }
            else
            {
                return callbackNone();
            }
        }
    }
}
