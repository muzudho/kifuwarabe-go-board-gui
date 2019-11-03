namespace KifuwarabeUec11Gui
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using System.Windows.Threading;
    using KifuwarabeUec11Gui.Script;
    using KifuwarabeUec11Gui.Script.InternationalGo;
    using KifuwarabeUec11Gui.Script.Translator;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 通信ログを書き込むやつ☆（＾～＾）
        /// </summary>
        private CommunicationLogWriter CommunicationLogWriter { get; set; }

        /// <summary>
        /// 入力を読み取るやつ☆（＾～＾）
        /// </summary>
        private InputTextReader InputTextReader { get; set; }

        /// <summary>
        /// UIスレッドで動くタイマー☆（＾～＾）
        /// </summary>
        private DispatcherTimer DispatchTimer { get; set; }

        /// <summary>
        /// 内部状態。
        /// </summary>
        private State State { get; set; }

        private List<Line> VerticalLines { get; set; }
        private List<Line> HorizontalLines { get; set; }
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
            this.State = new State();
            this.VerticalLines = new List<Line>();
            this.HorizontalLines = new List<Line>();
            this.Stones = new List<Ellipse>();
            this.RowLabels = new List<Label>();
            this.ColumnLabels = new List<Label>();

            // 乱数のタネは固定しておいた方がデバッグしやすいぜ☆（＾～＾）
            this.Random = new Random(0);

            InitializeComponent();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RepaintWindow((MainWindow)sender);
        }

        private static void RepaintWindow(MainWindow mainWindow)
        {
            var grid = mainWindow.grid;
            var board = mainWindow.board;
            var lastMove = mainWindow.lastMove;

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

            // タテ線をヨコに並べるぜ☆（＾～＾）
            for (var column = 0; column < ScriptDocument.BoardSize; column++)
            {
                var line = mainWindow.VerticalLines[column];
                Canvas.SetLeft(line, 0);
                Canvas.SetTop(line, 0);
                line.Width = grid.RenderSize.Width;
                line.Height = grid.RenderSize.Height;
                line.StrokeThickness = 1.5;
                Panel.SetZIndex(line, 110);
                // 盤の幅を20で割ろうぜ☆（＾～＾）
                line.X1 = boardLeft + board.Width * 0.05 + columnInterval * (column + SignLen);
                line.Y1 = boardTop + board.Height * 0.05;
                line.X2 = line.X1;
                line.Y2 = line.Y1 + rowInterval * 18;
            }
            // ヨコ線をタテに並べるぜ☆（＾～＾）
            for (var row = 0; row < ScriptDocument.BoardSize; row++)
            {
                var line = mainWindow.HorizontalLines[row];
                Canvas.SetLeft(line, 0);
                Canvas.SetTop(line, 0);
                line.Width = grid.RenderSize.Width;
                line.Height = grid.RenderSize.Height;
                line.StrokeThickness = 1.5;
                Panel.SetZIndex(line, 110);
                // 盤☆（＾～＾）
                line.X1 = boardLeft + board.Width * 0.05 + columnInterval * SignLen;
                line.Y1 = boardTop + board.Height * 0.05 + rowInterval * row;
                line.X2 = line.X1 + columnInterval * 18;
                line.Y2 = line.Y1;
            }
            // Trace.WriteLine($"verticalLine0 ({verticalLine0.X1}, {verticalLine0.Y1})  ({verticalLine0.X2}, {verticalLine0.Y2})");

            // 石を描こうぜ☆（＾～＾）？
            for (var i = 0; i < ScriptDocument.CellCount; i++)
            {
                var stone = mainWindow.Stones[i];
                PutAnythingOnNode(mainWindow, i, (left, top) =>
                {
                    stone.Width = board.Width / BoardDiv * 0.8;
                    stone.Height = board.Height / BoardDiv * 0.8;

                    Canvas.SetLeft(stone, left - stone.Width / 2);
                    Canvas.SetTop(stone, top - stone.Height / 2);
                });
            }

            // 最後の着手点を描こうぜ☆（＾～＾）？
            Trace.WriteLine($"this.State.LastMoveIndex | {mainWindow.State.LastMoveIndex}");
            if (-1 < mainWindow.State.LastMoveIndex)
            {
                lastMove.Visibility = Visibility.Visible;
                PutAnythingOnNode(mainWindow, mainWindow.State.LastMoveIndex, (left, top) =>
                {
                    Trace.WriteLine($"this.State.LastMoveIndex | left={left} top={top}");

                    lastMove.Width = board.Width / BoardDiv * 0.4;
                    lastMove.Height = board.Height / BoardDiv * 0.4;

                    Canvas.SetLeft(lastMove, left - lastMove.Width / 2);
                    Canvas.SetTop(lastMove, top - lastMove.Height / 2);
                });
            }
            else
            {
                // TODO lastMove.Visibility = Visibility.Hidden;
            }

            // 列の符号を描こうぜ☆（＾～＾）？
            for (var column = 0; column < ScriptDocument.BoardSize; column++)
            {
                var label = mainWindow.ColumnLabels[column];

                label.FontSize = columnInterval * 0.9;
                label.Width = columnInterval * 1.8;
                label.Height = rowInterval * 1.8;
                // 文字位置の調整は　良い方法がないので勘で調整☆（＾～＾）
                Canvas.SetLeft(label, boardLeft + paddingLeft * 1.05 - label.Width / 3 + columnInterval * 1.01 * (column + SignLen));
                Canvas.SetTop(label, boardTop + paddingTop - label.Height / 2 + rowInterval * ScriptDocument.BoardSize);
            }

            // 行の番号を描こうぜ☆（＾～＾）？
            for (var row = 0; row < ScriptDocument.BoardSize; row++)
            {
                var label = mainWindow.RowLabels[row];

                label.FontSize = columnInterval * 0.9;
                label.Width = columnInterval * 1.8;
                label.Height = rowInterval * 1.8;
                // 盤の幅を21で割ろうぜ☆（＾～＾）
                Canvas.SetLeft(label, boardLeft + paddingLeft - label.Width / 2 + columnInterval * 0);
                Canvas.SetTop(label, boardTop + paddingTop - label.Height / 2 + rowInterval * row);
            }

            // 何手目か表示しようぜ☆（＾～＾）？
            {
                // mainWindow.plyLabel.FontSize = columnInterval;
                mainWindow.plyNumber.Content = $"{mainWindow.State.Ply}";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Initialized(object sender, System.EventArgs e)
        {
            // 通信ログを書き込むやつ☆（＾～＾）
            {
                this.CommunicationLogWriter = new CommunicationLogWriter("communication.log");
                this.CommunicationLogWriter.WriteLine("テスト書込み☆（＾～＾）");
                this.CommunicationLogWriter.Flush();
            }

            // 入力を読み取るやつ☆（＾～＾）
            {
                this.InputTextReader = new InputTextReader("input.txt");
            }

            // UIスレッドで動くタイマー☆（＾～＾）
            {
                this.DispatchTimer = new DispatcherTimer();
                this.DispatchTimer.Start();

                // 5秒置きでも長い感じ☆（＾～＾）
                this.DispatchTimer.Interval = TimeSpan.FromSeconds(3);

                this.DispatchTimer.Tick += (s, e) =>
                {
                    var text = this.InputTextReader.ReadToEnd();

                    // 空行は無視☆（＾～＾）
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        Trace.WriteLine($"Text            | {text}");
                        this.CommunicationLogWriter.WriteLine(text);
                        this.CommunicationLogWriter.Flush();
                    }

                    var scriptDocument = ScriptDocument.Parse(text);
                    if (scriptDocument != null)
                    {
                        foreach (var instruction in scriptDocument.Instructions)
                        {
                            switch (instruction.Command)
                            {
                                case "black": // thru
                                case "white": // thru
                                case "space":
                                    var instArgs = (ColorInstructionArgument)instruction.Argument;
                                    foreach (var cellRange in instArgs.CellRanges)
                                    {
                                        // 内部的には Z字方向式 で持っている☆（＾～＾）
                                        var zShapedIndexes = cellRange.ToIndexes();

                                        foreach (var zShapedIndex in zShapedIndexes)
                                        {
                                            // Trace.WriteLine($"zShapedIndex={zShapedIndex}");

                                            // 内部的な操作では、上下を逆さにしなくていい☆（＾～＾）
                                            var stone = this.Stones[zShapedIndex];
                                            switch (instruction.Command)
                                            {
                                                case "black": // thru
                                                              // 黒石にするぜ☆（＾～＾）
                                                    stone.Fill = Brushes.Black;
                                                    stone.Stroke = Brushes.White;
                                                    stone.Visibility = Visibility.Visible;

                                                    // 最後の着手点☆（＾～＾）
                                                    this.State.LastMoveIndex = zShapedIndex;

                                                    break;

                                                case "white": // thru
                                                              // 白石にするぜ☆（＾～＾）
                                                    stone.Fill = Brushes.White;
                                                    stone.Stroke = Brushes.Black;
                                                    stone.Visibility = Visibility.Visible;

                                                    // 最後の着手点☆（＾～＾）
                                                    this.State.LastMoveIndex = zShapedIndex;

                                                    break;

                                                case "space":
                                                    stone.Visibility = Visibility.Hidden;
                                                    break;
                                            }
                                        }
                                    }
                                    break;
                            }
                        }

                        // 画面の再描画をしようぜ☆（＾～＾）
                        RepaintWindow(this);
                        this.InvalidateVisual();
                    }
                };
            }

            // 昔でいう呼び方で Client area は WPF では grid.RenderSize らしい（＾ｑ＾）
            // 短い方の一辺を求めようぜ☆（＾～＾）ぴったり枠にくっつくと窮屈なんで 0.95 掛けで☆（＾～＾）
            var shortenEdge = System.Math.Min(grid.RenderSize.Width, grid.RenderSize.Height) * 0.95;

            // センターを求めようぜ☆（＾～＾）
            var centerX = grid.RenderSize.Width / 2;
            var centerY = grid.RenderSize.Height / 2;

            // 盤☆（＾～＾）
            var boardLeft = centerX - shortenEdge / 2;
            var boardTop = centerY - shortenEdge / 2;
            var columnInterval = board.Width / BoardDiv;
            var rowInterval = board.Height / BoardDiv;

            // タテ線をヨコに並べるぜ☆（＾～＾）
            for (var column = 0; column < ScriptDocument.BoardSize; column++)
            {
                var line = new Line();
                line.Name = $"verticalLine{column}";
                Canvas.SetLeft(line, 0);
                Canvas.SetTop(line, 0);
                line.Width = grid.RenderSize.Width;
                line.Height = grid.RenderSize.Height;
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 1.5;
                Panel.SetZIndex(line, 110);
                // 盤の幅を20で割ろうぜ☆（＾～＾）
                line.X1 = boardLeft + board.Width * 0.05 + columnInterval * (column + SignLen);
                line.Y1 = boardTop + board.Height * 0.05;
                line.X2 = line.X1;
                line.Y2 = line.Y1 + rowInterval * 18;

                this.VerticalLines.Add(line);
                canvas.Children.Add(line);
            }
            // ヨコ線をタテに並べるぜ☆（＾～＾）
            for (var row = 0; row < ScriptDocument.BoardSize; row++)
            {
                var line = new Line();
                line.Name = $"horizontalLine{row}";
                Canvas.SetLeft(line, 0);
                Canvas.SetTop(line, 0);
                line.Width = grid.RenderSize.Width;
                line.Height = grid.RenderSize.Height;
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 1.5;
                Panel.SetZIndex(line, 110);
                // 盤☆（＾～＾）
                line.X1 = boardLeft + board.Width * 0.05 + columnInterval * SignLen;
                line.Y1 = boardTop + board.Height * 0.05 + rowInterval * row;
                line.X2 = line.X1 + columnInterval * 18;
                line.Y2 = line.Y1;

                this.HorizontalLines.Add(line);
                canvas.Children.Add(line);
            }

            // 石を描こうぜ☆（＾～＾）？
            for (var i = 0; i < ScriptDocument.CellCount; i++)
            {
                var row = i / ScriptDocument.BoardSize;
                var column = i % ScriptDocument.BoardSize;

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
            for (var column = 0; column < ScriptDocument.BoardSize; column++)
            {
                var label = new Label();
                label.Name = $"columnLabel{column + 1}";
                Panel.SetZIndex(label, 130);
                label.Content = (char)(65 + (column < 8 ? column : column + 1)); // 65はAsciiCodeのA。コンピューター囲碁では I は抜く慣習。
                this.ColumnLabels.Add(label);
                canvas.Children.Add(label);
            }

            // 行の番号を描こうぜ☆（＾～＾）？
            for (var row = 0; row < ScriptDocument.BoardSize; row++)
            {
                var number = ScriptDocument.BoardSize - row;
                var label = new Label();
                label.Name = $"rowLabel{number}";
                Panel.SetZIndex(label, 130);
                if (9 < number)
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

        private delegate void NodeCallback(double left, double top);

        /// <summary>
        /// 碁盤の線上の交点に何か置くぜ☆（＾～＾）
        /// 石１個置くたびに再計算するのは　無駄な気もするが、GUIでは、コーディングの楽さ優先だぜ☆（＾～＾）
        /// </summary>
        /// <param name="mainWindow"></param>
        /// <param name="index"></param>
        private static void PutAnythingOnNode(MainWindow mainWindow, int index, NodeCallback stoneCallback)
        {
            // 盤☆（＾～＾）
            var board = mainWindow.board;
            var grid = mainWindow.grid;
            var centerX = grid.RenderSize.Width / 2;
            var centerY = grid.RenderSize.Height / 2;

            // 昔でいう呼び方で Client area は WPF では grid.RenderSize らしい（＾ｑ＾）
            // 短い方の一辺を求めようぜ☆（＾～＾）ぴったり枠にくっつくと窮屈なんで 0.95 掛けで☆（＾～＾）
            var shortenEdge = System.Math.Min(grid.RenderSize.Width, grid.RenderSize.Height) * 0.95;
            var boardLeft = centerX - shortenEdge / 2;
            var boardTop = centerY - shortenEdge / 2;
            var paddingLeft = board.Width * 0.05;
            var paddingTop = board.Height * 0.05;
            var columnInterval = board.Width / BoardDiv;
            var rowInterval = board.Height / BoardDiv;
            var row = index / ScriptDocument.BoardSize;
            var column = index % ScriptDocument.BoardSize + SignLen;
            var left = boardLeft + paddingLeft + columnInterval * column;
            var top = boardTop + paddingTop + rowInterval * row;
            stoneCallback(left, top);
        }
    }
}
