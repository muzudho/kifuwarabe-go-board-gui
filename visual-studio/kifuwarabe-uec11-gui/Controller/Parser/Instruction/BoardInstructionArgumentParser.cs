﻿namespace KifuwarabeGoBoardGui.Controller.Parser
{
    using System;
    using KifuwarabeGoBoardGui.Model.Dto;

    /// <summary>
    /// board 構文☆（＾～＾）
    /// 
    /// board 5 .....
    /// board 4 .....
    /// board 3 .....
    /// board 2 .....
    /// board 1 .....
    /// #       12345
    /// 
    /// </summary>
    public static class BoardInstructionArgumentParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int SomeCallback(BoardArgumentDto matched, int curr);
        public delegate int NoneCallback();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns>Next.</returns>
        public static int Parse(
            string text,
            int start,
            ApplicationObjectDtoWrapper appModel,
            SomeCallback someCallback,
            NoneCallback noneCallback
            )
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (someCallback == null)
            {
                throw new ArgumentNullException(nameof(someCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            // 行番号を読めだぜ☆（＾～＾）数字とは限らないからな☆ｍ９（＾～＾）
            BoardArgumentDto boardInstructionArgument = null;

            // 最初のスペースは読み飛ばすぜ☆（＾～＾）
            var curr = WhiteSpaceParser.Parse(
                text,
                start,
                (matched, curr) =>
                {
                    return curr;
                },
                ()=>
                {
                    return start;
                });

            curr = RowAddressParser.Parse(
                text,
                curr,
                appModel,
                (rowAddress, curr) =>
                {
                    // 途中のスペースは読み飛ばすぜ☆（＾～＾）
                    curr = WhiteSpaceParser.Parse(
                        text,
                        curr,
                        (matched, curr) =>
                        {
                            return curr;
                        },
                        () =>
                        {
                            return curr;
                        });

                    // 行の残り全部を読み取るぜ☆（＾～＾）
                    string columns = text.Substring(curr);

                    // 列と行の両方マッチ☆（＾～＾）
                    boardInstructionArgument = new BoardArgumentDto(rowAddress, columns.Trim());
                    return curr + columns.Length;
                },
                () =>
                {
                    // 不一致☆（＾～＾）
                    return curr;
                });

            if (boardInstructionArgument != null)
            {
                return someCallback(boardInstructionArgument, curr);
            }
            else
            {
                return noneCallback();
            }
        }
    }
}
