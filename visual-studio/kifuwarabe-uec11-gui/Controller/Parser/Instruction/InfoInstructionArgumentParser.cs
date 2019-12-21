namespace KifuwarabeGoBoardGui.Controller.Parser
{
    using System;
    using KifuwarabeGoBoardGui.Model.Dto;

    public static class InfoInstructionArgumentParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static (InfoInstructionArgumentDto, int) Parse(string text, int start)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            var next = start;

            // 行の残り全部を読み取るぜ☆（＾～＾）
            string value = text.Substring(next);

            return (new InfoInstructionArgumentDto(value.Trim()), next);
        }
    }
}
