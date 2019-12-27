namespace KifuwarabeGoBoardGui.Model.Dto.Application.Extended
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using KifuwarabeGoBoardGui.Model.Dto;
    using KifuwarabeGoBoardGui.View;

    /// <summary>
    /// いくつかの石や駒などのまとまりだぜ☆（＾～＾）
    /// </summary>
    public class PieceBoard
    {
        public PieceBoard()
        {
            this.Pieces = new List<Shape>();
        }

        /// <summary>
        /// 囲碁なら石、将棋なら駒だが、一般化してピースと呼ぶことにするぜ☆（＾～＾）
        /// 並び順を保持するぜ☆（＾～＾）
        /// 楕円なら Ellipse だが、まだ形を決めないなら Shape ☆（＾～＾）
        /// </summary>
        private List<Shape> Pieces { get; set; }

        public delegate void CallbackSome(Shape piece, int index);
        public void ForeachPiace(CallbackSome callbackSome)
        {
            var index = 0;
            foreach (var piece in this.Pieces)
            {
                callbackSome(piece, index);

                index++;
            }
        }

        /*
        public Ellipse GetPieceAt(int index)
        {
            return this.Pieces[index];
        }
        */

        /// <summary>
        /// 黒石を描いて非表示にして持っておこうぜ☆（＾～＾）？
        /// </summary>
        /// <param name="appModel"></param>
        public void Initialize(ApplicationObjectDtoWrapper appModel)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            for (var i = 0; i < HyperParameter.MaxCellCount; i++)
            {
                // var row = i / appModel.ColumnSize;
                // var column = i % appModel.ColumnSize;

                var piece = new Ellipse();
                piece.Name = $"stone{i}";
                piece.Width = 10;
                piece.Height = 10;
                piece.StrokeThickness = 1.5;
                piece.Visibility = Visibility.Hidden;
                Panel.SetZIndex(piece, (int)ZOrder.Stone);

                // とりあえず黒石にして作っておこうぜ☆（＾～＾）
                piece.Fill = Brushes.Black;
                piece.Stroke = Brushes.White;

                // 盤☆（＾～＾）
                Canvas.SetLeft(piece, 0);
                Canvas.SetTop(piece, 0);
                this.Pieces.Add(piece);
            }
        }
    }
}
