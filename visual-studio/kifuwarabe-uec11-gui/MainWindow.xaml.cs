namespace KifuwarabeGoBoardGui
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using System.Windows.Threading;
    using KifuwarabeGoBoardGui.Controller;
    using KifuwarabeGoBoardGui.Model;

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
        public ApplicationObjectModelWrapper Model { get; private set; }

        private List<Line> VerticalLines { get; set; }
        private List<Line> HorizontalLines { get; set; }

        public List<Ellipse> Stones { get; private set; }
        public List<Shape> Marks { get; private set; }

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
            this.Model = new ApplicationObjectModelWrapper();

            // このアプリケーションの他に、ファイルにアクセスしないなら真で☆（＾～＾）
            this.Model.ModelChangeLogWriter.Enable = true;

            this.VerticalLines = new List<Line>();
            this.HorizontalLines = new List<Line>();

            this.Stones = new List<Ellipse>();
            this.Marks = new List<Shape>();

            this.Stars = new List<Ellipse>();
            this.RowLabels = new List<Label>();
            this.ColumnLabels = new List<Label>();

            // 乱数のタネは固定しておいた方がデバッグしやすいぜ☆（＾～＾）
            this.Random = new Random(0);

            InitializeComponent();
        }

        public void SetModel(ApplicationObjectModelWrapper model)
        {
            this.Model = model;
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
            var moveMarker = this.moveMarker;

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


            var columnInterval = board.Width / this.Model.Board.GetColumnDiv();
            var rowInterval = board.Height / this.Model.Board.GetRowDiv();

            // タテ線をヨコに並べるぜ☆（＾～＾）
            for (var column = 0; column < HyperParameter.MaxColumnSize; column++)
            {
                var line = this.VerticalLines[column];
                if (column < this.Model.Board.ColumnSize)
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
                    line.Y2 = line.Y1 + rowInterval * this.Model.Board.GetRowLastO0();
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
                if (row < this.Model.Board.RowSize)
                {
                    line.Visibility = Visibility.Visible;
                    Canvas.SetLeft(line, 0);
                    Canvas.SetTop(line, 0);
                    line.Width = grid.RenderSize.Width;
                    line.Height = grid.RenderSize.Height;
                    line.StrokeThickness = 1.5;
                    Panel.SetZIndex(line, (int)ZOrder.Line);
                    // 盤☆（＾～＾）
                    line.X1 = boardLeft + board.Width * 0.05 + columnInterval * SignLen;
                    line.Y1 = boardTop + board.Height * 0.05 + rowInterval * row;
                    line.X2 = line.X1 + columnInterval * this.Model.Board.GetColumnLastO0();
                    line.Y2 = line.Y1;
                }
                else
                {
                    line.Visibility = Visibility.Hidden;
                }
            }
            // Trace.WriteLine($"verticalLine0 ({verticalLine0.X1}, {verticalLine0.Y1})  ({verticalLine0.X2}, {verticalLine0.Y2})");

            // 石をウィンドウ・サイズに合わせようぜ☆（＾～＾）？
            StoneViewController.FitSizeToWindow(this.Model, this);

            // 列の符号を描こうぜ☆（＾～＾）？
            ColumnNumberViewController.Repaint(this.Model, this);

            // 行の番号を描こうぜ☆（＾～＾）？
            RowNumberViewController.Repaint(this.Model, this);

            // １９路盤の星を描こうぜ☆（＾～＾）？
            StarViewController.Repaint(this.Model, this);

            // 最後の着手点を描こうぜ☆（＾～＾）？
            MoveMarkerViewController.Repaint(this.Model, this);
        }

        private void Window_Initialized(object sender, System.EventArgs e)
        {
            // 通信ログを書き込むやつ☆（＾～＾）
            {
                this.CommunicationLogWriter = new CommunicationLogWriter("communication.log");
                this.CommunicationLogWriter.WriteLine("> I am a KifuwarabeGoBoardGui!");
                this.CommunicationLogWriter.Flush();
            }

            // 入力を読み取るやつ☆（＾～＾）
            {
                this.InputTextReader = new InputTextReader("input.txt");
            }

            // UIスレッドで動くタイマー☆（＾～＾）
            {
                this.DispatchTimer = new DispatcherTimer();

                // 何ミリ秒ごとに `input.txt` を書くにするか☆（＾～＾）これは初期値☆（＾～＾）
                this.DispatchTimer.Interval = TimeSpan.FromMilliseconds(this.Model.GetNumber(ApplicationObjectModel.IntervalMsecRealName).Value);

                this.DispatchTimer.Tick += (s, e) =>
                {
                    // input.txt読取。
                    InputLineModelController.Read(this.Model, this, (text) =>
                    {
                    // 1行ずつ解析☆（＾～＾）
                    InputLineModelController.ParseLine(this.Model, text).ThenAlias(
                        (aliasInstruction) =>
                        {
                        },
                        () =>
                        {

                        }).ThenComment(
                        (commentLine) =>
                        {
                            Trace.WriteLine($"Info            | Comment=[{commentLine}].");
                        },
                        () =>
                        {

                        }).ThenInfo(
                        (infoText) =>
                        {
                                // infoなら☆（＾～＾）
                                this.infoValue.Content = infoText;
                        },
                        () =>
                        {

                        }).ThenJson(
                        (jsonAppModel) =>
                        {
                                // モデルの差し替えなら☆（＾～＾）
                                this.SetModel(jsonAppModel);
                        },
                        () =>
                        {

                        }).ThenPut(
                        (putsArgs) =>
                        {
                                // put コマンド☆（＾～＾）
                            },
                        () =>
                        {

                        }).ThenSet(
                        (setsArgs) =>
                        {
                        },
                        () =>
                        {
                        });

                        // すべてのコマンドの実行が終わったらまとめて再描画だぜ☆（＾～＾）
                        ApplicationViewController.RepaintAllViews(this.Model, this);

                        // 全ての入力から　モデルの変更に対応したぜ☆（＾～＾）！
                        // あとは　モデルに合わせてビューを更新するだけだな☆（＾～＾）！
                        {
                            // GUI出力 を書き込むやつ☆（＾～＾）
                            // Tickイベントでファイルの入出力するのも度胸があるよな☆（＾～＾）
                            // using文を使えば、開いたファイルは 終わったらすぐ閉じるぜ☆（＾～＾）
                            using (var outputJsonWriter = new OutputJsonWriter("output.json"))
                            {
                                outputJsonWriter.WriteLine(this.Model.ApplicationObjectModel.ToJson());
                                outputJsonWriter.Flush();
                            }
                        }
                    });
                };

                // さっそく常駐☆（＾～＾）
                this.DispatchTimer.Start();
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
            var columnInterval = board.Width / this.Model.Board.GetColumnDiv();
            var rowInterval = board.Height / this.Model.Board.GetRowDiv();

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
            StarViewController.Initialize(this.Model.Board, this);

            // 黒石を描いて非表示にして持っておこうぜ☆（＾～＾）？
            StoneViewController.Initialize(this.Model, this);
            MarkViewController.Initialize(this.Model, this);

            // 列の符号を描こうぜ☆（＾～＾）？
            ColumnNumberViewController.Initialize(this);

            // 行の番号を描こうぜ☆（＾～＾）？
            RowNumberViewController.Initialize(this);

            // 着手のマーカー☆（＾～＾）
            Panel.SetZIndex(moveMarker, (int)ZOrder.MoveMarker);

            // UI表示物☆（＾～＾）
            {
                Panel.SetZIndex(top1Canvas, (int)ZOrder.UI);
                Panel.SetZIndex(top2Canvas, (int)ZOrder.UI);
                Panel.SetZIndex(right1Canvas, (int)ZOrder.UI);
                Panel.SetZIndex(right3Canvas, (int)ZOrder.UI);
                Panel.SetZIndex(right2Canvas, (int)ZOrder.UI);
                Panel.SetZIndex(left1Canvas, (int)ZOrder.UI);
                Panel.SetZIndex(left2Canvas, (int)ZOrder.UI);
                Panel.SetZIndex(left3Canvas, (int)ZOrder.UI);
                Panel.SetZIndex(left4Canvas, (int)ZOrder.UI);
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
        /// <param name="index"></param>
        public void PutAnythingOnNode(int index, NodeCallback stoneCallback)
        {
            if (stoneCallback == null)
            {
                throw new ArgumentNullException(nameof(stoneCallback));
            }

            // 盤☆（＾～＾）
            var board = this.board;
            var grid = this.grid;
            var centerX = grid.RenderSize.Width / 2;
            var centerY = grid.RenderSize.Height / 2;

            // 昔でいう呼び方で Client area は WPF では grid.RenderSize らしい（＾ｑ＾）
            // 短い方の一辺を求めようぜ☆（＾～＾）ぴったり枠にくっつくと窮屈なんで 0.95 掛けで☆（＾～＾）
            var shortenEdge = System.Math.Min(grid.RenderSize.Width, grid.RenderSize.Height) * 0.95;
            var boardLeft = centerX - shortenEdge / 2;
            var boardTop = centerY - shortenEdge / 2;
            var paddingLeft = board.Width * 0.05;
            var paddingTop = board.Height * 0.05;
            var columnInterval = board.Width / this.Model.Board.GetColumnDiv();
            var rowInterval = board.Height / this.Model.Board.GetRowDiv();

            var row = index / this.Model.Board.ColumnSize;
            var column = index % this.Model.Board.ColumnSize + SignLen;

            var left = boardLeft + paddingLeft + columnInterval * column;
            var top = boardTop + paddingTop + rowInterval * row;
            stoneCallback(left, top);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ApplicationViewController.RepaintAllViews(this.Model, this);
        }
    }
}
