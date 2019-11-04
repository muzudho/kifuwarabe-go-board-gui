namespace KifuwarabeUec11Gui
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using System.Windows.Threading;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.InputScript.InternationalGo;
    using KifuwarabeUec11Gui.Output;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 通信ログ を書き込むやつ☆（＾～＾）
        /// </summary>
        private CommunicationLogWriter CommunicationLogWriter { get; set; }

        /// <summary>
        /// GUI出力 を書き込むファイルの名前だぜ☆（＾～＾）
        /// </summary>
        private static string OutputJsonName => "./output.json";

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

        /// <summary>
        /// 盤の状態☆（＾～＾）
        /// </summary>
        public BoardModel BoardModel { get; private set; }

        private List<Line> VerticalLines { get; set; }
        private List<Line> HorizontalLines { get; set; }
        private List<Ellipse> Stones { get; set; }
        private List<Ellipse> Stars { get; set; }
        private List<Label> RowLabels { get; set; }
        private List<Label> ColumnLabels { get; set; }
        private Random Random { get; set; }

        /// <summary>
        /// 19本の線を引くから20分割だが、符号の列を1つ足すぜ☆（＾～＾）
        /// </summary>
        public static int BoardDiv => 21;

        /// <summary>
        /// 符号の1列☆（＾～＾）
        /// </summary>
        private static int SignLen => 1;

        public MainWindow()
        {
            this.State = new State();
            this.BoardModel = new BoardModel();

            this.VerticalLines = new List<Line>();
            this.HorizontalLines = new List<Line>();
            this.Stones = new List<Ellipse>();
            this.Stars = new List<Ellipse>();
            this.RowLabels = new List<Label>();
            this.ColumnLabels = new List<Label>();

            // 乱数のタネは固定しておいた方がデバッグしやすいぜ☆（＾～＾）
            this.Random = new Random(0);

            InitializeComponent();
        }

        public Ellipse GetStone(int zShapedIndex)
        {
            return this.Stones[zShapedIndex];
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RepaintWindow((MainWindow)sender);
        }

        /// <summary>
        /// TODO リサイズしてないなら　設定しなおさなくていいものも　ここに書いてあるな☆（＾～＾）減らせそう☆（＾～＾）
        /// </summary>
        /// <param name="mainWindow"></param>
        private static void RepaintWindow(MainWindow mainWindow)
        {
            var grid = mainWindow.grid;
            var board = mainWindow.board;
            var lastMoveMarker = mainWindow.lastMoveMarker;

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
            for (var column = 0; column < mainWindow.BoardModel.ColumnSize; column++)
            {
                var line = mainWindow.VerticalLines[column];
                Canvas.SetLeft(line, 0);
                Canvas.SetTop(line, 0);
                line.Width = grid.RenderSize.Width;
                line.Height = grid.RenderSize.Height;
                line.StrokeThickness = 1.5;
                Panel.SetZIndex(line, (int)ZOrder.Line);
                // 盤の幅を20で割ろうぜ☆（＾～＾）
                line.X1 = boardLeft + board.Width * 0.05 + columnInterval * (column + SignLen);
                line.Y1 = boardTop + board.Height * 0.05;
                line.X2 = line.X1;
                line.Y2 = line.Y1 + rowInterval * 18;
            }

            // ヨコ線をタテに並べるぜ☆（＾～＾）
            for (var row = 0; row < mainWindow.BoardModel.RowSize; row++)
            {
                var line = mainWindow.HorizontalLines[row];
                Canvas.SetLeft(line, 0);
                Canvas.SetTop(line, 0);
                line.Width = grid.RenderSize.Width;
                line.Height = grid.RenderSize.Height;
                line.StrokeThickness = 1.5;
                Panel.SetZIndex(line, (int)ZOrder.Line);
                // 盤☆（＾～＾）
                line.X1 = boardLeft + board.Width * 0.05 + columnInterval * SignLen;
                line.Y1 = boardTop + board.Height * 0.05 + rowInterval * row;
                line.X2 = line.X1 + columnInterval * 18;
                line.Y2 = line.Y1;
            }
            // Trace.WriteLine($"verticalLine0 ({verticalLine0.X1}, {verticalLine0.Y1})  ({verticalLine0.X2}, {verticalLine0.Y2})");

            // １９路盤の星を描こうぜ☆（＾～＾）？
            var starSigns = new string[] { "D16", "K16", "Q16", "D10", "K10", "Q10", "D4", "K4", "Q4" };
            for (var i = 0; i < 9; i++)
            {
                var star = mainWindow.Stars[i];
                var (internationalCellAddress, next) = InternationalCellAddress.Parse(starSigns[i], 0, mainWindow.BoardModel);
                if (internationalCellAddress!=null)
                {
                    MainWindow.PutAnythingOnNode(mainWindow, internationalCellAddress.ToIndex(mainWindow.BoardModel), (left, top)=>
                    {
                        // 大きさ☆（＾～＾） 黒石と間違わないぐらい小さくしないとな☆（＾～＾）
                        star.Width = board.Width / BoardDiv * 0.3;
                        star.Height = board.Height / BoardDiv * 0.3;

                        Canvas.SetLeft(star, left - star.Width / 2);
                        Canvas.SetTop(star, top - star.Height / 2);
                    });
                }
            }

            // 石を描こうぜ☆（＾～＾）？
            for (var i = 0; i < mainWindow.BoardModel.CellCount; i++)
            {
                var stone = mainWindow.Stones[i];
                PutAnythingOnNode(mainWindow, i, (left, top) =>
                {
                    // 大きさ☆（＾～＾）
                    stone.Width = board.Width / BoardDiv * 0.8;
                    stone.Height = board.Height / BoardDiv * 0.8;

                    Canvas.SetLeft(stone, left - stone.Width / 2);
                    Canvas.SetTop(stone, top - stone.Height / 2);
                });
            }

            // 最後の着手点を描こうぜ☆（＾～＾）？
            LastMoveMarkerController.Repaint(mainWindow.State, mainWindow);

            // 列の符号を描こうぜ☆（＾～＾）？
            for (var column = 0; column < mainWindow.BoardModel.ColumnSize; column++)
            {
                var label = mainWindow.ColumnLabels[column];

                label.FontSize = columnInterval * 0.9;
                label.Width = columnInterval * 1.8;
                label.Height = rowInterval * 1.8;
                // 文字位置の調整は　良い方法がないので勘で調整☆（＾～＾）
                Canvas.SetLeft(label, boardLeft + paddingLeft * 1.05 - label.Width / 3 + columnInterval * 1.01 * (column + SignLen));
                Canvas.SetTop(label, boardTop + paddingTop - label.Height / 2 + rowInterval * mainWindow.BoardModel.RowSize);
            }

            // 行の番号を描こうぜ☆（＾～＾）？
            for (var row = 0; row < mainWindow.BoardModel.RowSize; row++)
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
                mainWindow.plyValue.Content = $"{mainWindow.State.Ply}";
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
                this.CommunicationLogWriter.WriteLine("> I am a KifuwarabeUEC11Gui!");
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

                // 何ミリ秒ごとに `input.txt` を書くにするか☆（＾～＾）これは初期値☆（＾～＾）
                this.DispatchTimer.Interval = TimeSpan.FromMilliseconds(this.State.IntervalMsec);

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

                    var scriptDocument = InputScriptDocument.Parse(text, this.BoardModel);
                    if (scriptDocument != null)
                    {
                        foreach (var instruction in scriptDocument.Instructions)
                        {
                            switch (instruction.Command)
                            {
                                case "exit":
                                    {
                                        // このアプリケーションを終了します。
                                        System.Windows.Application.Current.Shutdown();
                                    }
                                    break;

                                case "info":
                                    {
                                        // `set info = banana` のシンタックス・シュガーだぜ☆（＾～＾）

                                        // プロパティ☆（＾～＾）
                                        var args = (InfoInstructionArgument)instruction.Argument;

                                        // 改行コードに対応☆（＾～＾）ただし 垂直タブ（めったに使わんだろ） は除去☆（＾～＾）
                                        infoValue.Content = SoluteNewline(args.Text);
                                    }
                                    break;

                                case "set":
                                    {
                                        // プロパティ☆（＾～＾）
                                        var prop = (SetsInstructionArgument)instruction.Argument;
                                        switch (prop.Name)
                                        {
                                            case "ply":
                                                {
                                                    if (int.TryParse(prop.Value, out int outValue))
                                                    {
                                                        this.State.Ply = outValue;
                                                        plyValue.Content = outValue.ToString(CultureInfo.CurrentCulture);
                                                    }
                                                }
                                                break;
                                            case "move":
                                                {
                                                    var (cellAddress, next) = InternationalCellAddress.Parse(prop.Value, 0, this.BoardModel);
                                                    if (cellAddress != null)
                                                    {
                                                        LastMoveMarkerController.SetAddress(this.State, this, cellAddress);
                                                    }
                                                }
                                                break;
                                            case "b-name":
                                                this.State.BlackName = prop.Value;
                                                blackNameValue.Content = prop.Value;
                                                break;
                                            case "b-time":
                                                this.State.BlackTime = prop.Value;
                                                blackTimeValue.Content = prop.Value;
                                                break;
                                            case "b-hama":
                                                {
                                                    if (int.TryParse(prop.Value, out int outValue))
                                                    {
                                                        this.State.BlackHama = outValue;
                                                        blackAgehamaValue.Content = outValue.ToString(CultureInfo.CurrentCulture);
                                                    }
                                                }
                                                break;
                                            case "w-name":
                                                this.State.WhiteName = prop.Value;
                                                whiteNameValue.Content = prop.Value;
                                                break;
                                            case "w-time":
                                                this.State.WhiteTime = prop.Value;
                                                whiteTimeValue.Content = prop.Value;
                                                break;
                                            case "w-hama":
                                                {
                                                    if (int.TryParse(prop.Value, out int outValue))
                                                    {
                                                        this.State.WhiteHama = outValue;
                                                        whiteAgehamaValue.Content = outValue.ToString(CultureInfo.CurrentCulture);
                                                    }
                                                }
                                                break;
                                            case "komi":
                                                {
                                                    if (double.TryParse(prop.Value, out double outValue))
                                                    {
                                                        this.State.Komi = outValue;
                                                        komiValue.Content = outValue.ToString(CultureInfo.CurrentCulture);
                                                    }
                                                }
                                                break;

                                            case "info":
                                                this.State.Info = prop.Value;

                                                // 改行コードに対応☆（＾～＾）ただし 垂直タブ（めったに使わんだろ） は除去☆（＾～＾）
                                                infoValue.Content = SoluteNewline(prop.Value);
                                                break;

                                            case "interval-msec":
                                                {
                                                    if (int.TryParse(prop.Value, out int outValue))
                                                    {
                                                        this.State.IntervalMsec = outValue;
                                                        this.DispatchTimer.Interval = TimeSpan.FromMilliseconds(outValue);
                                                        Trace.WriteLine($"interval-msec: {this.State.IntervalMsec}");
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                    break;

                                case "black":
                                    {
                                        var args = (ColorInstructionArgument)instruction.Argument;
                                        // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                                        foreach (var cellRange in args.CellRanges)
                                        {
                                            foreach (var zShapedIndex in cellRange.ToIndexes(this.BoardModel))
                                            {
                                                // 黒石にするぜ☆（＾～＾）
                                                BoardController.ChangeColorToBlack(this.BoardModel, this, zShapedIndex);

                                                // 最後の着手点☆（＾～＾）
                                                LastMoveMarkerController.SetIndex(this.State, this, zShapedIndex);
                                            }
                                        }
                                    }
                                    break;

                                case "white":
                                    {
                                        var args = (ColorInstructionArgument)instruction.Argument;
                                        // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                                        foreach (var cellRange in args.CellRanges)
                                        {
                                            foreach (var zShapedIndex in cellRange.ToIndexes(this.BoardModel))
                                            {
                                                // 白石にするぜ☆（＾～＾）
                                                BoardController.ChangeColorToWhite(this.BoardModel, this, zShapedIndex);

                                                // 最後の着手点☆（＾～＾）
                                                LastMoveMarkerController.SetIndex(this.State, this, zShapedIndex);
                                            }
                                        }
                                    }
                                    break;

                                case "space":
                                    {
                                        var args = (ColorInstructionArgument)instruction.Argument;
                                        // インデックスの並びは、内部的には Z字方向式 だぜ☆（＾～＾）
                                        foreach (var cellRange in args.CellRanges)
                                        {
                                            foreach (var zShapedIndex in cellRange.ToIndexes(this.BoardModel))
                                            {
                                                // 石を取り除くぜ☆（＾～＾）
                                                BoardController.ChangeColorToSpace(this.BoardModel, this, zShapedIndex);
                                            }
                                        }
                                    }
                                    break;
                            }
                        }

                        // 全ての入力に対応したぜ☆（＾～＾）！
                        {
                            // GUI出力 を書き込むやつ☆（＾～＾）
                            // Tickイベントでファイルの入出力するのも度胸があるよな☆（＾～＾）
                            // using文を使えば、開いたファイルは 終わったらすぐ閉じるぜ☆（＾～＾）
                            using (var outputJsonWriter = new OutputJsonWriter("output.json"))
                            {
                                outputJsonWriter.WriteLine(new OutputJsonDocument(this.BoardModel, this.State).ToJson());
                                outputJsonWriter.Flush();
                            }
                            // 画面の再描画をしようぜ☆（＾～＾）
                            RepaintWindow(this);
                            this.InvalidateVisual();
                        }
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
            for (var column = 0; column < BoardModel.ColumnSize; column++)
            {
                var line = new Line();
                line.Name = $"verticalLine{column}";
                Canvas.SetLeft(line, 0);
                Canvas.SetTop(line, 0);
                line.Width = grid.RenderSize.Width;
                line.Height = grid.RenderSize.Height;
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 1.5;
                // Panel.SetZIndex(line, (int)ZOrder.Line);
                // 盤の幅を20で割ろうぜ☆（＾～＾）
                line.X1 = boardLeft + board.Width * 0.05 + columnInterval * (column + SignLen);
                line.Y1 = boardTop + board.Height * 0.05;
                line.X2 = line.X1;
                line.Y2 = line.Y1 + rowInterval * 18;

                this.VerticalLines.Add(line);
                canvas.Children.Add(line);
            }

            // ヨコ線をタテに並べるぜ☆（＾～＾）
            for (var row = 0; row < BoardModel.RowSize; row++)
            {
                var line = new Line();
                line.Name = $"horizontalLine{row}";
                Canvas.SetLeft(line, 0);
                Canvas.SetTop(line, 0);
                line.Width = grid.RenderSize.Width;
                line.Height = grid.RenderSize.Height;
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 1.5;
                // Panel.SetZIndex(line, (int)ZOrder.Line);
                // 盤☆（＾～＾）
                line.X1 = boardLeft + board.Width * 0.05 + columnInterval * SignLen;
                line.Y1 = boardTop + board.Height * 0.05 + rowInterval * row;
                line.X2 = line.X1 + columnInterval * 18;
                line.Y2 = line.Y1;

                this.HorizontalLines.Add(line);
                canvas.Children.Add(line);
            }

            // 星を９つ描いて持っておこうぜ☆（＾～＾）？
            for (var i = 0; i < 9; i++)
            {
                var row = i / BoardModel.ColumnSize;
                var column = i % BoardModel.ColumnSize;

                var star = new Ellipse();
                star.Name = $"star{i}";
                star.Width = 1;
                star.Height = 1;
                Panel.SetZIndex(star, (int)ZOrder.Star);

                // 黒丸で☆（＾～＾）
                star.Fill = Brushes.Black;

                // 盤☆（＾～＾）
                Canvas.SetLeft(star, 0);
                Canvas.SetTop(star, 0);
                this.Stars.Add(star);
                canvas.Children.Add(star);
            }

            // 黒石を描いて非表示にして持っておこうぜ☆（＾～＾）？
            for (var i = 0; i < BoardModel.CellCount; i++)
            {
                var row = i / BoardModel.ColumnSize;
                var column = i % BoardModel.ColumnSize;

                var stone = new Ellipse();
                stone.Name = $"stone{i}";
                stone.Width = 10;
                stone.Height = 10;
                stone.StrokeThickness = 1.5;
                stone.Visibility = Visibility.Hidden;
                Panel.SetZIndex(stone, (int)ZOrder.Stone);

                // とりあえず黒石にして作っておこうぜ☆（＾～＾）
                stone.Fill = Brushes.Black;
                stone.Stroke = Brushes.White;

                // 盤☆（＾～＾）
                Canvas.SetLeft(stone, 0);
                Canvas.SetTop(stone, 0);
                this.Stones.Add(stone);
                canvas.Children.Add(stone);
            }

            // 列の符号を描こうぜ☆（＾～＾）？
            for (var column = 0; column < BoardModel.ColumnSize; column++)
            {
                var label = new Label();
                label.Name = $"columnLabel{column + 1}";
                Panel.SetZIndex(label, (int)ZOrder.LineNumber);
                label.Content = (char)(65 + (column < 8 ? column : column + 1)); // 65はAsciiCodeのA。コンピューター囲碁では I は抜く慣習。
                this.ColumnLabels.Add(label);
                canvas.Children.Add(label);
            }

            // 行の番号を描こうぜ☆（＾～＾）？
            for (var row = 0; row < BoardModel.RowSize; row++)
            {
                var number = BoardModel.RowSize - row;
                var label = new Label();
                label.Name = $"rowLabel{number}";
                Panel.SetZIndex(label, (int)ZOrder.LineNumber);
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

            // 着手のマーカー☆（＾～＾）
            Panel.SetZIndex(lastMoveMarker, (int)ZOrder.MoveMarker);

            // UI表示物☆（＾～＾）
            {
                Panel.SetZIndex(plyCanvas, (int)ZOrder.UI);
                Panel.SetZIndex(lastMoveCanvas, (int)ZOrder.UI);
                Panel.SetZIndex(blackNameCanvas, (int)ZOrder.UI);
                Panel.SetZIndex(blackTimeCanvas, (int)ZOrder.UI);
                Panel.SetZIndex(blackAgehamaCanvas, (int)ZOrder.UI);
                Panel.SetZIndex(whiteNameCanvas, (int)ZOrder.UI);
                Panel.SetZIndex(whiteTimeCanvas, (int)ZOrder.UI);
                Panel.SetZIndex(whiteAgehamaCanvas, (int)ZOrder.UI);
                Panel.SetZIndex(komiCanvas, (int)ZOrder.UI);
                Panel.SetZIndex(infoCanvas, (int)ZOrder.InfoCanvas);                
            }
        }

        /// <summary>
        /// 改行コードに対応☆（＾～＾）ただし 垂直タブ（めったに使わんだろ） は除去☆（＾～＾）
        /// (1) 垂直タブ は消す。
        /// (2) \\ は 垂直タブ にする☆
        /// (3) \n は 改行コード にする☆
        /// (4) 垂直タブは \ にする☆
        /// </summary>
        private static string SoluteNewline(string text)
        {
            var temp = text.Replace("\v", "", StringComparison.Ordinal);
            temp = temp.Replace("\\\\", "\v", StringComparison.Ordinal);
            temp = temp.Replace("\\n", "\n", StringComparison.Ordinal);
            temp = temp.Replace("\v", "\\", StringComparison.Ordinal);
            return temp;
        }

        public delegate void NodeCallback(double left, double top);

        /// <summary>
        /// 碁盤の線上の交点に何か置くぜ☆（＾～＾）
        /// 石１個置くたびに再計算するのは　無駄な気もするが、GUIでは、コーディングの楽さ優先だぜ☆（＾～＾）
        /// </summary>
        /// <param name="mainWindow"></param>
        /// <param name="index"></param>
        public static void PutAnythingOnNode(MainWindow mainWindow, int index, NodeCallback stoneCallback)
        {
            if (mainWindow == null)
            {
                throw new ArgumentNullException(nameof(mainWindow));
            }

            if (stoneCallback == null)
            {
                throw new ArgumentNullException(nameof(stoneCallback));
            }

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
            var row = index / mainWindow.BoardModel.ColumnSize;
            var column = index % mainWindow.BoardModel.ColumnSize + SignLen;
            var left = boardLeft + paddingLeft + columnInterval * column;
            var top = boardTop + paddingTop + rowInterval * row;
            stoneCallback(left, top);
        }
    }
}
