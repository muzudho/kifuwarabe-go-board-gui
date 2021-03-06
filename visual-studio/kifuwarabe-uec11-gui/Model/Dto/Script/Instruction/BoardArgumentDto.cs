﻿namespace KifuwarabeGoBoardGui.Model.Dto
{
    using System;
    using KifuwarabeGoBoardGui.Controller.Parser;
    using KifuwarabeGoBoardGui.Model;

    /// <summary>
    /// `board 19 .www.bbb...........` みたいなコマンドの引数☆（＾～＾）
    /// `board {row-number} {column-chars}` みたいな感じだな☆（＾～＾）
    /// </summary>
    public class BoardArgumentDto
    {
        /// <summary>
        /// 行番号だぜ☆（＾～＾）
        /// </summary>
        public RowAddress RowAddress { get; private set; }

        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public string Columns { get; private set; }

        public BoardArgumentDto(RowAddress rowAddress, string columns)
        {
            this.RowAddress = rowAddress;
            this.Columns = columns;
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay(ApplicationObjectDtoWrapper appModel)
        {
            return $"{this.RowAddress.ToDisplayTrimed(appModel)} {this.Columns}";
        }
    }
}
