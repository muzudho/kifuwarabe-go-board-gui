namespace kifuwarabe_uec11_gui
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Ellipse> Stones { get; set; }
        private List<Label> RowLabels { get; set; }
        private List<Label> ColumnLabels { get; set; }
        private Random Random { get; set; }

        /// <summary>
        /// 19本の線を引くから20分割だが、符号の列を1つ足すぜ☆（＾～＾）
        /// </summary>
        private static int BoardDiv = 21;

        /// <summary>
        /// 符号の1列☆（＾～＾）
        /// </summary>
        private static int SignLen = 1;

        public MainWindow()
        {
            this.Stones = new List<Ellipse>();
            this.RowLabels = new List<Label>();
            this.ColumnLabels = new List<Label>();

            // 乱数のタネは固定しておいた方がデバッグしやすいぜ☆（＾～＾）
            this.Random = new Random(0);

            InitializeComponent();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var window = (Window)sender;
            // Trace.WriteLine($"サイズチェンジ 横幅={window.Width} 縦幅={window.Height} グリッド {grid.RenderSize.Width}, {grid.RenderSize.Height}");

            // 昔でいう呼び方で Client area は WPF では grid.RenderSize らしい（＾ｑ＾）
            // 短い方の一辺を求めようぜ☆（＾～＾）ぴったり枠にくっつくと窮屈なんで 0.95 掛けで☆（＾～＾）
            var shortenEdge = System.Math.Min(grid.RenderSize.Width, grid.RenderSize.Height) * 0.95;

            // センターを求めようぜ☆（＾～＾）
            var centerX = grid.RenderSize.Width / 2;
            var centerY = grid.RenderSize.Height / 2;

            // 盤☆（＾～＾）
            var boardLeft = centerX - shortenEdge / 2;
            var boardTop = centerY - shortenEdge / 2;
            Trace.WriteLine($"board ({boardLeft}, {boardTop})");
            Canvas.SetLeft(board, boardLeft);
            Canvas.SetTop(board, boardTop);
            board.Width = shortenEdge;
            board.Height = shortenEdge;
            var paddingLeft = board.Width * 0.05;
            var paddingTop = board.Height * 0.05;
            var columnInterval = board.Width / BoardDiv;
            var rowInterval = board.Height / BoardDiv;

            // タテ線20本☆（＾～＾）
            var verticalLines = new List<Line>() {
                verticalLine0,
                verticalLine1,
                verticalLine2,
                verticalLine3,
                verticalLine4,
                verticalLine5,
                verticalLine6,
                verticalLine7,
                verticalLine8,
                verticalLine9,
                verticalLine10,
                verticalLine11,
                verticalLine12,
                verticalLine13,
                verticalLine14,
                verticalLine15,
                verticalLine16,
                verticalLine17,
                verticalLine18,
                verticalLine19,
            };
            // ヨコ線20本☆（＾～＾）
            var horizontalLines = new List<Line>() {
                horizontalLine0,
                horizontalLine1,
                horizontalLine2,
                horizontalLine3,
                horizontalLine4,
                horizontalLine5,
                horizontalLine6,
                horizontalLine7,
                horizontalLine8,
                horizontalLine9,
                horizontalLine10,
                horizontalLine11,
                horizontalLine12,
                horizontalLine13,
                horizontalLine14,
                horizontalLine15,
                horizontalLine16,
                horizontalLine17,
                horizontalLine18,
                horizontalLine19,
            };
            // タテ線をヨコに並べるぜ☆（＾～＾）
            for (var column = 0; column < 19; column++)
            {
                var verticalLine = verticalLines[column];
                Canvas.SetLeft(verticalLine, 0);
                Canvas.SetTop(verticalLine, 0);
                verticalLine.Width = grid.RenderSize.Width;
                verticalLine.Height = grid.RenderSize.Height;
                verticalLine.Stroke = Brushes.Black;
                verticalLine.StrokeThickness = 1.5;
                Panel.SetZIndex(verticalLine, 110);
                // 盤の幅を20で割ろうぜ☆（＾～＾）
                verticalLine.X1 = boardLeft + board.Width * 0.05 + columnInterval * (column + SignLen);
                verticalLine.Y1 = boardTop + board.Height * 0.05;
                verticalLine.X2 = verticalLine.X1;
                verticalLine.Y2 = verticalLine.Y1 + rowInterval * 18;
            }
            // ヨコ線をタテに並べるぜ☆（＾～＾）
            for (var row = 0; row < 19; row++)
            {
                var horizontalLine = horizontalLines[row];
                Canvas.SetLeft(horizontalLine, 0);
                Canvas.SetTop(horizontalLine, 0);
                horizontalLine.Width = grid.RenderSize.Width;
                horizontalLine.Height = grid.RenderSize.Height;
                horizontalLine.Stroke = Brushes.Black;
                horizontalLine.StrokeThickness = 1.5;
                Panel.SetZIndex(horizontalLine, 110);
                // 盤の幅を20で割ろうぜ☆（＾～＾）
                horizontalLine.X1 = boardLeft + board.Width * 0.05 + columnInterval * SignLen;
                horizontalLine.Y1 = boardTop + board.Height * 0.05 + rowInterval * row;
                horizontalLine.X2 = horizontalLine.X1 + columnInterval * 18;
                horizontalLine.Y2 = horizontalLine.Y1;
            }
            // Trace.WriteLine($"verticalLine0 ({verticalLine0.X1}, {verticalLine0.Y1})  ({verticalLine0.X2}, {verticalLine0.Y2})");

            // 石を描こうぜ☆（＾～＾）？
            for (var i = 0; i < 361; i++)
            {
                var stone = this.Stones[i];
                var row = i / 19;
                var column = i % 19 + SignLen;

                stone.Width = board.Width / BoardDiv * 0.8;
                stone.Height = board.Height / BoardDiv * 0.8;
                stone.StrokeThickness = 1.5;
                // 盤の幅を21で割ろうぜ☆（＾～＾）
                Canvas.SetLeft(stone, boardLeft + paddingLeft - stone.Width / 2 + columnInterval * column);
                Canvas.SetTop(stone, boardTop + paddingTop - stone.Height / 2 + rowInterval * row);
            }

            // 列の符号を描こうぜ☆（＾～＾）？
            for (var column = 0; column < 19; column++)
            {
                var label = this.ColumnLabels[column];

                label.FontSize = columnInterval * 0.9;
                label.Width = columnInterval * 1.8;
                label.Height = rowInterval * 1.8;
                // 文字位置の調整は　良い方法がないので勘で調整☆（＾～＾）
                Canvas.SetLeft(label, boardLeft + paddingLeft * 1.05 - label.Width / 3 + columnInterval * (column + SignLen));
                Canvas.SetTop(label, boardTop + paddingTop - label.Height / 2 + rowInterval * 19);
            }

            // 行の符号を描こうぜ☆（＾～＾）？
            for (var row = 0; row < 19; row++)
            {
                var label = this.RowLabels[row];

                label.FontSize = columnInterval * 0.9;
                label.Width = columnInterval * 1.8;
                label.Height = rowInterval * 1.8;
                // 盤の幅を21で割ろうぜ☆（＾～＾）
                Canvas.SetLeft(label, boardLeft + paddingLeft - label.Width / 2 + columnInterval * 0);
                Canvas.SetTop(label, boardTop + paddingTop - label.Height / 2 + rowInterval * row);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Initialized(object sender, System.EventArgs e)
        {
            // 昔でいう呼び方で Client area は WPF では grid.RenderSize らしい（＾ｑ＾）
            // 短い方の一辺を求めようぜ☆（＾～＾）ぴったり枠にくっつくと窮屈なんで 0.95 掛けで☆（＾～＾）
            var shortenEdge = System.Math.Min(grid.RenderSize.Width, grid.RenderSize.Height) * 0.95;

            // センターを求めようぜ☆（＾～＾）
            var centerX = grid.RenderSize.Width / 2;
            var centerY = grid.RenderSize.Height / 2;

            // 石を描こうぜ☆（＾～＾）？
            for (var i = 0; i < 361; i++)
            {
                var row = i / 19;
                var column = i % 19;

                var stone = new Ellipse();
                stone.Name = $"stone{i}";
                stone.Width = 10;
                stone.Height = 10;
                stone.StrokeThickness = 1.5;
                Panel.SetZIndex(stone, 120);
                // カラー
                if (this.Random.Next(0, 2) == 0)
                {
                    stone.Fill = Brushes.Black;
                    stone.Stroke = Brushes.White;
                }
                else
                {
                    stone.Fill = Brushes.White;
                    stone.Stroke = Brushes.Black;
                }
                // 盤☆（＾～＾）
                Canvas.SetLeft(stone, 0);
                Canvas.SetTop(stone, 0);
                this.Stones.Add(stone);
                canvas.Children.Add(stone);
            }

            // 列の符号を描こうぜ☆（＾～＾）？
            for (var column = 0; column < 19; column++)
            {
                var label = new Label();
                label.Name = $"columnLabel{column + 1}";
                Panel.SetZIndex(label, 130);
                label.Content = (char)(65 + (column<8?column:column+1)); // 65はAsciiCodeのA。コンピューター囲碁では I は抜く慣習。
                this.ColumnLabels.Add(label);
                canvas.Children.Add(label);
            }

            // 行の番号を描こうぜ☆（＾～＾）？
            for (var row = 0; row < 19; row++)
            {
                var number = 19 - row;
                var label = new Label();
                label.Name = $"rowLabel{number}";
                Panel.SetZIndex(label, 130);
                if (9<number)
                {
                    label.Content = number;
                }
                else
                {
                    // 文字位置の調整がうまく行かないので勘で調整☆（＾～＾）
                    label.Content = $"  {number}";
                }
                this.RowLabels.Add(label);
                canvas.Children.Add(label);
            }
        }
    }
}
