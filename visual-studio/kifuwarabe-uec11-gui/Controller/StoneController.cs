namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using KifuwarabeUec11Gui.Model;

    /// <summary>
    /// 石を操作するぜ☆（＾～＾）
    /// 
    /// こっちを設定して、あっちを設定して、また　こっちに戻ってきて設定して、というような
    /// 無限ループしないようにセットするのもコントローラーのメリットだぜ☆（＾～＾）
    /// </summary>
    public static class StoneController
    {
        public static void Repaint(ApplicationObjectModelWrapper appModel, MainWindow appView, int zShapedIndex)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (appView == null)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            // ビュー☆（＾～＾）
            {
                var stone = appView.GetStone(zShapedIndex);

                if (zShapedIndex < appModel.Board.Stones.Count)
                {
                    switch (appModel.Board.Stones[zShapedIndex])
                    {
                        case Stone.Black:
                            stone.Fill = Brushes.Black;
                            stone.Stroke = Brushes.White;
                            stone.Visibility = Visibility.Visible;
                            break;
                        case Stone.White:
                            stone.Fill = Brushes.White;
                            stone.Stroke = Brushes.Black;
                            stone.Visibility = Visibility.Visible;
                            break;
                        case Stone.None:
                            stone.Visibility = Visibility.Hidden;
                            break;
                    }
                }
                else
                {
                    // 範囲外☆（＾～＾）
                    stone.Visibility = Visibility.Hidden;
                }
            }
        }

        /// <summary>
        /// 黒石に変えようぜ☆（＾～＾）
        /// </summary>
        public static void ChangeModelToBlack(ApplicationObjectModelWrapper appModel, int zShapedIndex)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            // モデル☆（＾～＾）
            appModel.Board.SetStone(zShapedIndex, Stone.Black);
        }

        public static void ChangeModelToWhite(ApplicationObjectModelWrapper appModel, int zShapedIndex)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            // モデル☆（＾～＾）
            appModel.Board.SetStone(zShapedIndex, Stone.White);
        }

        public static void ChangeModelToSpace(ApplicationObjectModelWrapper appModel, int zShapedIndex)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            // モデル☆（＾～＾）
            appModel.Board.SetStone(zShapedIndex, Stone.None);
        }
    }
}
