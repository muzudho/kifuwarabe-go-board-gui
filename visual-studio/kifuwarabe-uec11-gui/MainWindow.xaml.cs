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

            // タテ線をヨコに並べるぜ☆（＾～＾）
            for (var column = 0; column < 19; column++)
            {
                var line = this.VerticalLines[column];
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
            for (var row = 0; row < 19; row++)
            {
                var line = this.HorizontalLines[row];
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
            for (var i = 0; i < 361; i++)
            {
                var stone = this.Stones[i];
                var row = i / 19;
                var column = i % 19 + SignLen;

                stone.Width = board.Width / BoardDiv * 0.8;
                stone.Height = board.Height / BoardDiv * 0.8;
                stone.StrokeThickness = 1.5;
                // 盤☆（＾～＾）
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
                Canvas.SetLeft(label, boardLeft + paddingLeft * 1.05 - label.Width / 3 + columnInterval * 1.01 * (column + SignLen));
                Canvas.SetTop(label, boardTop + paddingTop - label.Height / 2 + rowInterval * 19);
            }

            // 行の番号を描こうぜ☆（＾～＾）？
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

            // 何手目か表示しようぜ☆（＾～＾）？
            {
                ply.FontSize = columnInterval;
                ply.Content = $"{State.Ply}手目";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // テスト

            // 列番号
            {
                Trace.WriteLine($"A               | {InternationalColumnAddress.Parse("A", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"B               | {InternationalColumnAddress.Parse("B", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"C               | {InternationalColumnAddress.Parse("C", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"D               | {InternationalColumnAddress.Parse("ABCDEFGHIJKLMNOPQRST", 3).Item1?.ToDisplay()}");
                Trace.WriteLine($"E               | {InternationalColumnAddress.Parse("ABCDEFGHIJKLMNOPQRST", 4).Item1?.ToDisplay()}");
                Trace.WriteLine($"F               | {InternationalColumnAddress.Parse("ABCDEFGHIJKLMNOPQRST", 5).Item1?.ToDisplay()}");
                Trace.WriteLine($"G               | {InternationalColumnAddress.Parse("G", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"H               | {InternationalColumnAddress.Parse("H", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"I (same H)      | {InternationalColumnAddress.Parse("I", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"J               | {InternationalColumnAddress.Parse("J", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"K               | {InternationalColumnAddress.Parse("K", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"L               | {InternationalColumnAddress.Parse("L", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"M               | {InternationalColumnAddress.Parse("M", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"N               | {InternationalColumnAddress.Parse("N", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"O               | {InternationalColumnAddress.Parse("O", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"P               | {InternationalColumnAddress.Parse("P", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"Q               | {InternationalColumnAddress.Parse("Q", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"R               | {InternationalColumnAddress.Parse("R", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"S               | {InternationalColumnAddress.Parse("S", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"T               | {InternationalColumnAddress.Parse("T", 0).Item1?.ToDisplay()}");
            }

            // 行番号
            {
                Trace.WriteLine($"1               | {InternationalRowAddress.Parse("1", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"2               | {InternationalRowAddress.Parse("2", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"3               | {InternationalRowAddress.Parse("3", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"4               | {InternationalRowAddress.Parse("1234", 3).Item1?.ToDisplay()}");
                Trace.WriteLine($"5               | {InternationalRowAddress.Parse("12345", 4).Item1?.ToDisplay()}");
                Trace.WriteLine($"6               | {InternationalRowAddress.Parse("123456", 5).Item1?.ToDisplay()}");
                Trace.WriteLine($"7               | {InternationalRowAddress.Parse("7", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"8               | {InternationalRowAddress.Parse("8", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"9               | {InternationalRowAddress.Parse("9", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"10              | {InternationalRowAddress.Parse("10", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"11              | {InternationalRowAddress.Parse("11", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"12              | {InternationalRowAddress.Parse("12", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"13              | {InternationalRowAddress.Parse("12345678901213", 12).Item1?.ToDisplay()}");
                Trace.WriteLine($"14              | {InternationalRowAddress.Parse("123456789012314", 13).Item1?.ToDisplay()}");
                Trace.WriteLine($"15              | {InternationalRowAddress.Parse("1234567890123415", 14).Item1?.ToDisplay()}");
                Trace.WriteLine($"16              | {InternationalRowAddress.Parse("16", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"17              | {InternationalRowAddress.Parse("17", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"18              | {InternationalRowAddress.Parse("18", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"19              | {InternationalRowAddress.Parse("19", 0).Item1?.ToDisplay()}");
            }

            // セル番地
            {
                Trace.WriteLine($"A1              | {InternationalCellAddress.Parse("A1", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"B2              | {InternationalCellAddress.Parse("B2", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"C3              | {InternationalCellAddress.Parse("C3", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"D4              | {InternationalCellAddress.Parse("D4", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"E5              | {InternationalCellAddress.Parse("E5", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"F6              | {InternationalCellAddress.Parse("F6", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"G7              | {InternationalCellAddress.Parse("G7", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"H8              | {InternationalCellAddress.Parse("H8", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"J9              | {InternationalCellAddress.Parse("J9", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"K10             | {InternationalCellAddress.Parse("K10", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"L11             | {InternationalCellAddress.Parse("L11", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"M12             | {InternationalCellAddress.Parse("M12", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"N13             | {InternationalCellAddress.Parse("N13", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"O14             | {InternationalCellAddress.Parse("O14", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"P15             | {InternationalCellAddress.Parse("P15", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"Q16             | {InternationalCellAddress.Parse("Q16", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"R17             | {InternationalCellAddress.Parse("R17", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"S18             | {InternationalCellAddress.Parse("S18", 0).Item1?.ToDisplay()}");
                Trace.WriteLine($"T19             | {InternationalCellAddress.Parse("T19", 0).Item1?.ToDisplay()}");
            }

            Trace.WriteLine($"black           | {ExactlyKeyword.Parse("black", "black", 0).Item1?.ToDisplay()}");
            Trace.WriteLine($"white           | {ExactlyKeyword.Parse("white", "white", 0).Item1?.ToDisplay()}");
            Trace.WriteLine($"start           | {ExactlyKeyword.Parse("start", "start", 0).Item1?.ToDisplay()}");
            Trace.WriteLine($"[     ]         | [{WhiteSpace.Parse("     ", 0).Item1?.ToDisplay()}]");

            Trace.WriteLine($"A19             | {InternationalCellAddress.Parse("A19", 0).Item1?.ToDisplay()}");
            Trace.WriteLine($"B19             | {InternationalCellAddress.Parse("B19", 0).Item1?.ToDisplay()}");
            Trace.WriteLine($"S1              | {InternationalCellAddress.Parse("S1", 0).Item1?.ToDisplay()}");
            Trace.WriteLine($"T1              | {InternationalCellAddress.Parse("T1", 0).Item1?.ToDisplay()}");

            Trace.WriteLine($"a19             | {InternationalCellAddress.Parse("a19", 0).Item1?.ToDisplay()}");
            Trace.WriteLine($"b19             | {InternationalCellAddress.Parse("b19", 0).Item1?.ToDisplay()}");
            Trace.WriteLine($"s1              | {InternationalCellAddress.Parse("s1", 0).Item1?.ToDisplay()}");
            Trace.WriteLine($"t1              | {InternationalCellAddress.Parse("t1", 0).Item1?.ToDisplay()}");

            Trace.WriteLine($"black a19 k10 t1| {ColorInstructionArgument.Parse("black a19 k10 t1", 5).Item1?.ToDisplay()}");
            Trace.WriteLine($"white a19 k10 t1| {ColorInstructionArgument.Parse("white a19 k10 t1", 5).Item1?.ToDisplay()}");
            Trace.WriteLine($"space a19 k10 t1| {ColorInstructionArgument.Parse("space a19 k10 t1", 5).Item1?.ToDisplay()}");

            Trace.WriteLine($"black a19 k10 t1| {Word.Parse("black a19 k10 t1", 0).Item1?.ToDisplay()}");

            Trace.WriteLine($"c7:e9           | {InternationalCellRange.Parse("c7:e9", 0).Item1?.ToDisplay()}");
            Trace.Write("c7:e9           |");
            InternationalCellRange.Parse("c7:e9", 0).Item1?.Foreach((index) =>
            {
                Trace.Write($" {InternationalCellAddress.FromIndex(index).ToDisplay()}");
            });
            Trace.WriteLine(".");

            Trace.WriteLine($"e9:c7           | {InternationalCellRange.Parse("e9:c7", 0).Item1?.ToDisplay()}");
            Trace.Write("e9:c7           |");
            InternationalCellRange.Parse("e9:c7", 0).Item1?.Foreach((index) =>
            {
                Trace.Write($" {InternationalCellAddress.FromIndex(index).ToDisplay()}");
            });
            Trace.WriteLine(".");
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
                this.DispatchTimer.Interval = TimeSpan.FromSeconds(5);
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
                                    var ciArg = (ColorInstructionArgument)instruction.Argument;
                                    foreach (var cellAddr in ciArg.CellAddressList)
                                    {
                                        var index = cellAddr.ToIndex();
                                        var stone = this.Stones[index];
                                        switch (instruction.Command)
                                        {
                                            case "black": // thru
                                                // 黒石にするぜ☆（＾～＾）
                                                stone.Fill = Brushes.White;
                                                stone.Stroke = Brushes.Black;
                                                break;
                                            case "white": // thru
                                                // 白石にするぜ☆（＾～＾）
                                                stone.Fill = Brushes.Black;
                                                stone.Stroke = Brushes.White;
                                                break;
                                            case "space":
                                                // 画面外に出すことで非表示にするぜ☆（＾～＾）
                                                Canvas.SetLeft(stone, - stone.Width);
                                                Canvas.SetTop(stone, -stone.Height);
                                                break;
                                        }

                                    }
                                    break;
                            }
                        }

                        // 画面の再描画をしようぜ☆（＾～＾）
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
            for (var column = 0; column < 19; column++)
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
            for (var row = 0; row < 19; row++)
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
                label.Content = (char)(65 + (column < 8 ? column : column + 1)); // 65はAsciiCodeのA。コンピューター囲碁では I は抜く慣習。
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

            // 何手目か表示しようぜ☆（＾～＾）？
            {
                ply.Foreground = new SolidColorBrush(Color.FromArgb(196, 0, 0, 0));
            }
        }
    }
}
