﻿namespace KifuwarabeGoBoardGui.Model.Dto
{
    /// <summary>
    /// `info I have a banana.` みたいなコマンド☆（＾～＾）
    /// </summary>
    public class InfoInstructionArgumentDto
    {
        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public string Text { get; private set; }

        public InfoInstructionArgumentDto(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            return $"{this.Text}";
        }
    }
}
