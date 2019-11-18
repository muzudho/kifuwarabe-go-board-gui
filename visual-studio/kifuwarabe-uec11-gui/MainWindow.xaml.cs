namespace KifuwarabeUec11Gui
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using System.Windows.Threading;
    using KifuwarabeUec11Gui.Model;
    using KifuwarabeUec11Gui.Controller;
    using System.Diagnostics;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 通信ログ を書き込むやつ☆（＾～＾）
        /// </summary>
        public CommunicationLogWriter CommunicationLogWriter { get; private set; }

        /// <summary>
        /// GUI出力 を書き込むファイルの名前だぜ☆（＾～＾）
        /// </summary>
        private static string OutputJsonName => "./output.json";

        /// <summary>
        /// 入力を読み取るやつ☆（＾～＾）
        /// </summary>
        public InputTextReader InputTextReader { get; private set; }

        /// <summary>
        /// UIスレッドで動くタイマー☆（＾～＾）
        /// </summary>
        public DispatcherTimer DispatchTimer { get; private set; }

        /// <summary>
        /// このアプリケーションのデータ☆（＾～＾）
        /// </summary>
        public ApplicationObjectModel Model { get; private set; }

        private List<Line> VerticalLines { get; set; }
        private List<Line> HorizontalLines { get; set; }
        private List<Ellipse> Stones { get; set; }
        public List<Ellipse> Stars { get; private set; }
        public List<Label> RowLabels { get; private set; }
        public List<Label> ColumnLabels { get; private set; }
        private Random Random { get; set; }

        /// <summary>
        /// 符号の1列☆（＾～＾）
        /// </summary>
        public static int SignLen => 1;

        public MainWindow()
        {
            this.Model = new ApplicationObjectModel();

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

        public void SetModel(ApplicationObjectModel model)
        {
            this.Model = model;
        }

        public Ellipse GetStone(int zShapedIndex)
        {
            return this.Stones[zShapedIndex];
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.FitSizeToWindow();
        }

        /// <summary>
        /// TODO リサイズしてないなら　設定しなおさなくていいものも　ここに書いてあるな☆（＾～＾）減らせそう☆（＾～＾）
        /// </summary>
        public void FitSizeToWindow()
        {
            var grid = this.grid;
            var board = this.board;
            var lastMoveMarker = this.lastMoveMarker;

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
            // Trace.WriteLine($"board ({boardLeft}, {boardTop})");
            Canvas.SetLeft(board, boardLeft);
            Canvas.SetTop(board, boardTop);
            board.Width = shortenEdge;
            board.Height = shortenEdge;
            var paddingLeft = board.Width * 0.05;
            var paddingTop = board.Height * 0.05;
            var columnInterval = board.Width / this.Model.Board.GetColumnDiv(this.Model.Metrics);
            var rowInterval = board.Height / this.Model.Board.GetRowDiv(this.Model.Metrics);

            // タテ線をヨコに並べるぜ☆（＾～＾）
            for (var column = 0; column < HyperParameter.MaxColumnSize; column++)
            {
                var line = this.VerticalLines[column];
                if (column < this.Model.Metrics.ColumnSize)
                {
                    line.Visibility = Visibility.Visible;
                    Canvas.SetLeft(line, 0);
                    Canvas.SetTop(line, 0);
                    line.Width = grid.RenderSize.Width;
                    line.Height = grid.RenderSize.Height;
                    line.StrokeThickness = 1.5;
                    Panel.SetZIndex(line, (int)ZOrder.Line);
                    // 盤☆（＾～＾）
                    line.X1 = boardLeft + board.Width * 0.05 + columnInterval * (column + SignLen);
                    line.Y1 = boardTop + board.Height * 0.05;
                    line.X2 = line.X1;
                    line.Y2 = line.Y1 + rowInterval * this.Model.Board.GetRowLastO0(this.Model.Metrics);
                }
                else
                {
                    line.Visibility = Visibility.Hidden;
                }
            }

            // ヨコ線をタテに並べるぜ☆（＾～＾）
            for (var row = 0; row < HyperParameter.MaxRowSize; row++)
            {
                var line = this.HorizontalLines[row];
                if (row < this.Model.Metrics.RowSize)
                {
                    Canvas.SetLeft(line, 0);
                    Canvas.SetTop(line, 0);
                    line.Width = grid.RenderSize.Width;
                    line.Height = grid.RenderSize.Height;
                    line.StrokeThickness = 1.5;
                    Panel.SetZIndex(line, (int)ZOrder.Line);
                    // 盤☆（＾～＾）
                    line.X1 = boardLeft + board.Width * 0.05 + columnInterval * SignLen;
                    line.Y1 = boardTop + board.Height * 0.05 + rowInterval * row;
                    line.X2 = line.X1 + columnInterval * this.Model.Board.GetColumnLastO0(this.Model.Metrics);
                    line.Y2 = line.Y1;
                }
                else
                {
                    line.Visibility = Visibility.Hidden;
                }
            }
            // Trace.WriteLine($"verticalLine0 ({verticalLine0.X1}, {verticalLine0.Y1})  ({verticalLine0.X2}, {verticalLine0.Y2})");

            // １９路盤の星を描こうぜ☆（＾～＾）？
            StarController.Repaint(this.Model, this);

            // 石を描こうぜ☆（＾～＾）？
            for (var zShapedIndex = 0; zShapedIndex < HyperParameter.MaxCellCount; zShapedIndex++)
            {
                var stone = this.Stones[zShapedIndex];
                if (zShapedIndex < this.Model.Board.GetCellCount(this.Model.Metrics))
                {
                    PutAnythingOnNode(this, zShapedIndex, (left, top) =>
                    {
                        // 大きさ☆（＾～＾）
                        stone.Width = board.Width / this.Model.Board.GetColumnDiv(this.Model.Metrics) * 0.8;
                        stone.Height = board.Height / this.Model.Board.GetRowDiv(this.Model.Metrics) * 0.8;

                        Canvas.SetLeft(stone, left - stone.Width / 2);
                        Canvas.SetTop(stone, top - stone.Height / 2);
                    });
                }
                else
                {
                    StoneController.ChangeModelToSpace(this.Model, zShapedIndex);
                }
            }

            // 最後の着手点を描こうぜ☆（＾～＾）？
            LastMoveMarkerController.Repaint(this.Model, this);

            // 列の符号を描こうぜ☆（＾～＾）？
            ColumnNumberController.Repaint(this.Model, this);

            // 行の番号を描こうぜ☆（＾～＾）？
            RowNumberController.Repaint(this.Model, this);

            // 何手目か表示しようぜ☆（＾～＾）？
            {
                this.plyValue.Content = $"{this.Model.Numbers[ApplicationObjectModel.PlyOutsideName].ToText()}";
            }
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
                this.DispatchTimer.Interval = TimeSpan.FromMilliseconds(this.Model.Numbers[ApplicationObjectModel.IntervalMsecOutsideName].Value);

                this.DispatchTimer.Tick += (s, e) =>
                {
                    InputController.Read(this.Model, this, (text)=>
                    {
                        InputController.ParseByLine(this.Model, this, text, ()=>
                        {
                            // 1コマンド実行するたびに、しつこく再描画しようと思ったが、べつに……☆（＾～＾）
                        });

                        // すべてのコマンドの実行が終わったらまとめて再描画だぜ☆（＾～＾）
                        ApplicationController.RepaintAllViews(this.Model, this);

                        // 全ての入力から　モデルの変更に対応したぜ☆（＾～＾）！
                        // あとは　モデルに合わせてビューを更新するだけだな☆（＾～＾）！
                        {
                            // GUI出力 を書き込むやつ☆（＾～＾）
                            // Tickイベントでファイルの入出力するのも度胸があるよな☆（＾～＾）
                            // using文を使えば、開いたファイルは 終わったらすぐ閉じるぜ☆（＾～＾）
                            using (var outputJsonWriter = new OutputJsonWriter("output.json"))
                            {
                                outputJsonWriter.WriteLine(this.Model.ToJson());
                                outputJsonWriter.Flush();
                            }
                        }
                    });
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
            var columnInterval = board.Width / this.Model.Board.GetColumnDiv(this.Model.Metrics);
            var rowInterval = board.Height / this.Model.Board.GetRowDiv(this.Model.Metrics);

            // タテ線をヨコに並べるぜ☆（＾～＾）
            for (var column = 0; column < HyperParameter.MaxColumnSize; column++)
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
            for (var row = 0; row < HyperParameter.MaxRowSize; row++)
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
            StarController.Initialize(this.Model.Metrics, this.Model.Board, this);

            // 黒石を描いて非表示にして持っておこうぜ☆（＾～＾）？
            for (var i = 0; i < HyperParameter.MaxCellCount; i++)
            {
                var row = i / this.Model.Metrics.ColumnSize;
                var column = i % this.Model.Metrics.ColumnSize;

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
            ColumnNumberController.Initialize(this);

            // 行の番号を描こうぜ☆（＾～＾）？
            RowNumberController.Initialize(this.Model.Board, this);

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
        public static string SoluteNewline(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

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
        /// <param name="appView"></param>
        /// <param name="index"></param>
        public static void PutAnythingOnNode(MainWindow appView, int index, NodeCallback stoneCallback)
        {
            if (appView == null)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            if (stoneCallback == null)
            {
                throw new ArgumentNullException(nameof(stoneCallback));
            }

            // 盤☆（＾～＾）
            var board = appView.board;
            var grid = appView.grid;
            var centerX = grid.RenderSize.Width / 2;
            var centerY = grid.RenderSize.Height / 2;

            // 昔でいう呼び方で Client area は WPF では grid.RenderSize らしい（＾ｑ＾）
            // 短い方の一辺を求めようぜ☆（＾～＾）ぴったり枠にくっつくと窮屈なんで 0.95 掛けで☆（＾～＾）
            var shortenEdge = System.Math.Min(grid.RenderSize.Width, grid.RenderSize.Height) * 0.95;
            var boardLeft = centerX - shortenEdge / 2;
            var boardTop = centerY - shortenEdge / 2;
            var paddingLeft = board.Width * 0.05;
            var paddingTop = board.Height * 0.05;
            var columnInterval = board.Width / appView.Model.Board.GetColumnDiv(appView.Model.Metrics);
            var rowInterval = board.Height / appView.Model.Board.GetRowDiv(appView.Model.Metrics);
            var row = index / appView.Model.Metrics.ColumnSize;
            var column = index % appView.Model.Metrics.ColumnSize + SignLen;
            var left = boardLeft + paddingLeft + columnInterval * column;
            var top = boardTop + paddingTop + rowInterval * row;
            stoneCallback(left, top);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ApplicationController.RepaintAllViews(this.Model, this);
        }
    }
}
