﻿namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Diagnostics;
    using KifuwarabeUec11Gui.Model;

    /// <summary>
    /// 石を操作するぜ☆（＾～＾）
    /// 
    /// こっちを設定して、あっちを設定して、また　こっちに戻ってきて設定して、というような
    /// 無限ループしないようにセットするのもコントローラーのメリットだぜ☆（＾～＾）
    /// </summary>
    public static class StoneModelController
    {
        /// <summary>
        /// 黒石に変えようぜ☆（＾～＾）
        /// </summary>
        public static void ChangeModelToBlack(ApplicationObjectModelWrapper appModel, int zShapedIndex)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            Trace.WriteLine($"Change          | Stone to black.");
            appModel.Board.SetStone(zShapedIndex, Stone.Black);
        }

        public static void ChangeModelToWhite(ApplicationObjectModelWrapper appModel, int zShapedIndex)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            Trace.WriteLine($"Change          | Stone to white.");
            appModel.Board.SetStone(zShapedIndex, Stone.White);
        }

        public static void ChangeModelToSpace(ApplicationObjectModelWrapper appModel, int zShapedIndex)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            Trace.WriteLine($"Change          | Stone to space.");
            appModel.Board.SetStone(zShapedIndex, Stone.None);
        }
    }
}
