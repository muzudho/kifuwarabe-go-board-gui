namespace KifuwarabeUec11Gui.Script.ZShaped
{
    using System;
    using System.Collections.Generic;

    public delegate void IndexCallback(int index, int rowXXX, int columnXXX); // XXX

    /// <summary>
    /// セルの範囲指定だぜ☆（＾～＾）
    /// `i7:k9` なら `i7 j7 k7 i8 j8 k8 i9 j9 k9` な☆（＾～＾）
    /// `k7:i9` なら `k7 j7 i7 k8 j8 i8 k9 j9 i9` な☆（＾～＾）
    /// ただし要素をデータとして持ったらメモリを圧迫するんで、持たないぜ☆（＾～＾）
    /// 
    /// また、 `k10` は `k10:k10` の省略形として認めるぜ☆（＾～＾）
    /// 
    /// このオブジェクトは、国際式囲碁のことは知らなくていいように作れだぜ☆（＾～＾）
    /// </summary>
    public class CellRange
    {
        public CellAddress StartsCellAddress { get; private set; }
        public CellAddress EndsCellAddress { get; private set; }

        public CellRange(CellAddress startsCellAddress, CellAddress endsCellAddress)
        {
            this.StartsCellAddress = startsCellAddress;
            this.EndsCellAddress = endsCellAddress;
        }

        public static (CellRange, int) Parse(string text, int start)
        {
            var next = start;

            CellAddress startsCellAddress;
            (startsCellAddress, next) = CellAddress.Parse(text, next);
            if (startsCellAddress == null)
            {
                // 構文不一致☆（＾～＾）
                return (null, start);
            }
            // Trace.WriteLine($"startsCellAddres| {startsCellAddress.ToDisplay()}");

            ExactlyKeyword colon;
            (colon, next) = ExactlyKeyword.Parse(":", text, next);
            if (colon == null)
            {
                // 構文不一致☆（＾～＾）

                // ここまで一致していれば、短縮形として確定するぜ☆（＾～＾）
                // 例えば `k10` は、 `k10:k10` と一致したと判定するんだぜ☆（＾～＾）
                return (new CellRange(startsCellAddress, startsCellAddress), next);
            }

            CellAddress endsCellAddress;
            (endsCellAddress, next) = CellAddress.Parse(text, next);
            if (endsCellAddress == null)
            {
                // 構文不一致☆（＾～＾）コロンが付いていて尻切れトンボなら不一致、諦めろだぜ☆（＾～＾）
                return (null, start);
            }
            // Trace.WriteLine($"endsCellAddress | {endsCellAddress.ToDisplay()}");

            return (new CellRange(startsCellAddress, endsCellAddress), next);
        }

        public void Foreach(IndexCallback callback)
        {
            // Trace.WriteLine($"Foreach         | {this.ToDisplay()} | sr={this.StartsCellAddress.RowAddress.ToDisplay()} sc={this.StartsCellAddress.ColumnAddress.ToDisplay()} er={this.EndsCellAddress.RowAddress.ToDisplay()} ec={this.EndsCellAddress.ColumnAddress.ToDisplay()}");

            // 長さを求めようぜ☆（＾～＾）
            var horizontalLength = this.EndsCellAddress.ColumnAddress.NumberO0 - this.StartsCellAddress.ColumnAddress.NumberO0;
            var verticalLength = this.EndsCellAddress.RowAddress.NumberO0 - this.StartsCellAddress.RowAddress.NumberO0;
            // Trace.WriteLine($"Length          | horizontal={horizontalLength} vertical={verticalLength}");

            // 幅を求めようぜ☆（＾～＾）
            var widthEdges = Math.Abs(horizontalLength);
            var heightEdges = Math.Abs(verticalLength);
            // Trace.WriteLine($"Edges           | width={widthEdges} height={heightEdges}");

            // 向きを作ろうぜ☆（＾～＾） -1 とか 1 を作ってるんだぜ☆（＾～＾）
            int horizontalDirection;
            if (widthEdges==0)
            {
                horizontalDirection = 1;
            }
            else
            {
                horizontalDirection = horizontalLength / widthEdges;
            }

            int verticalDirection;
            if (heightEdges == 0)
            {
                verticalDirection = 1;
            }
            else
            {
                verticalDirection = verticalLength / heightEdges;
            }
            // Trace.WriteLine($"Direction       | horizontal={horizontalDirection} vertical={verticalDirection}");

            for (int verticalZoom = 0; verticalZoom < heightEdges + 1; verticalZoom += 1)
            {
                for (int horizontalZoom = 0; horizontalZoom < widthEdges + 1; horizontalZoom += 1)
                {
                    var rowNumberO0 = this.StartsCellAddress.RowAddress.NumberO0 + verticalZoom * verticalDirection;
                    var columnNumberO0 = this.StartsCellAddress.ColumnAddress.NumberO0 + horizontalZoom * horizontalDirection;
                    var index = rowNumberO0 * ScriptDocument.BoardSize + columnNumberO0;
                    // Trace.WriteLine($"Index           | sr={this.StartsCellAddress.RowAddress.Number} vz={verticalZoom} row={rowNumber} | sc={this.StartsCellAddress.ColumnAddress.Number} hz={horizontalZoom} col={columnNumber} | index={index}.");
                    callback(index, rowNumberO0, columnNumberO0);
                }
            }
        }

        public HashSet<int> ToIndexes()
        {
            var hash = new HashSet<int>();

            Foreach((index, r, c)=>
            {
                hash.Add(index);
            });

            return hash;
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public virtual string ToDisplay()
        {
            var starts = this.StartsCellAddress.ToDisplay();
            var ends = this.EndsCellAddress.ToDisplay();

            if (starts == ends)
            {
                return $"{starts}";
            }

            return $"{starts}:{ends}";
        }
    }
}
