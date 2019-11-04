namespace UnitTestProject1
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.InputScript.InternationalGo;
    using KifuwarabeUec11Gui.Output;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// 簡単な実行☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void Test1()
        {
            /*
            // 簡単な実行☆（＾～＾）
            var builder = new StringBuilder();
            builder.Append("[");
            for (int i=0; i<100; i++)
            {
                builder.Append($"{i},");
            }
            builder.Append("]");
            Trace.WriteLine(builder.ToString());
            // */

            //*
            // 簡単な実行☆（＾～＾）
            var builder = new StringBuilder();
            builder.Append("[");
            for (int i = 0; i < BoardModel.ColumnSize; i++)
            {
                builder.Append($"{i * BoardModel.ColumnSize},");
            }
            builder.Append("]");
            Trace.WriteLine(builder.ToString());
            // */
        }

        /// <summary>
        /// プロパティ値の設定をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestSetsInstructionArgumentTest()
        {
            Assert.AreEqual("b-name = Kifuwarabe", SetsInstructionArgument.Parse("set b-name = Kifuwarabe", 3).Item1?.ToDisplay());
            Assert.AreEqual("b-time = 10:00", SetsInstructionArgument.Parse("set  b-time  =  10:00  ", 3).Item1?.ToDisplay());
            Assert.AreEqual("b-hama = ", SetsInstructionArgument.Parse("set b-hama =", 3).Item1?.ToDisplay());
        }

        /// <summary>
        /// 国際式囲碁のセル番地表記をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestColorInstructionArgumentTest()
        {
            Assert.AreEqual("A19 K1 T1", ColorInstructionArgument.Parse("black a19 k1 t1", 5).Item1?.ToDisplay());
            Assert.AreEqual("B19 K2 S1", ColorInstructionArgument.Parse("white b19 k2 s1", 5).Item1?.ToDisplay());
            Assert.AreEqual("C19 K3 R1", ColorInstructionArgument.Parse("space c19 k3 r1", 5).Item1?.ToDisplay());

            // 混合型☆（＾～＾）
            Assert.AreEqual("A2:B1 C4:D3 E5", ColorInstructionArgument.Parse("space a2:b1 c4:d3 e5", 5).Item1?.ToDisplay());
        }

        /// <summary>
        /// 国際式囲碁のセル番地表記をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestInternationalCellRange()
        {
            Assert.AreEqual("C7:E9", InternationalCellRange.Parse("c7:e9", 0).Item1?.ToDisplay());
            Assert.AreEqual("E9:C7", InternationalCellRange.Parse("e9:c7", 0).Item1?.ToDisplay());

            // 短縮表記☆（＾～＾）
            Assert.AreEqual("F5", InternationalCellRange.Parse("F5:F5", 0).Item1?.ToDisplay());

            // 内部インデックスも確認☆（＾～＾）
            /*
|Row |Index|Column|Index|
|----|---|---|---|
| "1"|342|A| 0|
| "2"|323|B| 1|
| "3"|304|C| 2|
| "4"|285|D| 3|
| "5"|266|E| 4|
| "6"|247|F| 5|
| "7"|228|G| 6|
| "8"|209|H| 7|
| "9"|190|J| 8|
|"10"|171|K| 9|
|"11"|152|L|10|
|"12"|133|M|11|
|"13"|114|N|12|
|"14"| 95|O|13|
|"15"| 76|P|14|
|"16"| 57|Q|15|
|"17"| 38|R|16|
|"18"| 19|S|17|
|"19"|  0|T|18|
             */
            {
                var indexes = new List<int>();

                // I列は無いことに注意☆（＾～＾）！
                // 右肩上がり☆（＾～＾）
                InternationalCellRange.Parse("H7:K9", 0).Item1?.Foreach((index, r, c) =>
                {
                    indexes.Add(index);
                });

                // H7 J7 K7 H8 J8 K8 H9 J9 K9
                Assert.AreEqual("235 236 237 216 217 218 197 198 199", string.Join(' ', indexes));
            }

            {
                var indexes = new List<int>();

                // I列は無いことに注意☆（＾～＾）！
                InternationalCellRange.Parse("K9:H7", 0).Item1?.Foreach((index, r, c) =>
                {
                    indexes.Add(index);
                });

                // K9 J9 H9 K8 J8 H8 K7 J7 H7
                Assert.AreEqual("199 198 197 218 217 216 237 236 235", string.Join(' ', indexes));
            }

            // 表記確認☆（＾～＾）
            {
                var signs = new List<string>();

                // I列は無いことに注意☆（＾～＾）！
                InternationalCellRange.Parse("H7:K9", 0).Item1?.Foreach((indexO0, r, c) =>
                {
                    signs.Add(InternationalCellAddress.FromIndex(indexO0).ToDisplay());
                });

                Assert.AreEqual("H7 J7 K7 H8 J8 K8 H9 J9 K9", string.Join(' ', signs));
            }

            {
                var signs = new List<string>();

                // I列は無いことに注意☆（＾～＾）！
                InternationalCellRange.Parse("K9:H7", 0).Item1?.Foreach((indexO0, r, c) =>
                {
                    signs.Add(InternationalCellAddress.FromIndex(indexO0).ToDisplay());
                });

                Assert.AreEqual("K9 J9 H9 K8 J8 H8 K7 J7 H7", string.Join(' ', signs));
            }
        }

        /// <summary>
        /// Z字方向式のセル番地表記をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestZShapedCellRange()
        {
            Assert.AreEqual("C7:E9", CellRange.Parse("c7:e9", 0).Item1?.ToDisplay());
            Assert.AreEqual("E9:C7", CellRange.Parse("e9:c7", 0).Item1?.ToDisplay());

            // 短縮表記☆（＾～＾）
            Assert.AreEqual("F5", CellRange.Parse("F5:F5", 0).Item1?.ToDisplay());

            // 内部インデックスも確認☆（＾～＾）
            /*
|Row |Index|Column|Index|
|----|---|---|---|
| "1"|  0|A| 0|
| "2"| 19|B| 1|
| "3"| 38|C| 2|
| "4"| 57|D| 3|
| "5"| 76|E| 4|
| "6"| 95|F| 5|
| "7"|114|G| 6|
| "8"|133|H| 7|
| "9"|152|I| 8|
|"10"|171|J| 9|
|"11"|190|K|10|
|"12"|209|L|11|
|"13"|228|M|12|
|"14"|247|N|13|
|"15"|266|O|14|
|"16"|285|P|15|
|"17"|304|Q|16|
|"18"|323|R|17|
|"19"|342|S|18|
             */
            {
                var indexes = new List<int>();

                var msg = new StringBuilder();
                var (cellRange, next) = CellRange.Parse("h7:j9", 0);
                if (cellRange != null)
                {
                    msg.AppendLine($"cellRange | {cellRange.ToDisplay()}");
                    cellRange.Foreach((index, r, c) =>
                    {
                        indexes.Add(index);
                        msg.AppendLine($"index={index} r={r} c={c}");
                    });

                    // H7 I7 J7 H8 I8 J8 H9 I9 J9
                    Assert.AreEqual("121 122 123 140 141 142 159 160 161", string.Join(' ', indexes), msg.ToString());
                }
                else
                {
                    Assert.Fail();
                }
            }

            {
                var indexes = new List<int>();

                var msg = new StringBuilder();
                var (cellRange, next) = CellRange.Parse("j9:h7", 0);
                if (cellRange != null)
                {
                    cellRange.Foreach((index, r, c) =>
                    {
                        indexes.Add(index);
                        msg.AppendLine($"index={index} r={r} c={c}");
                    });

                    // J9 I9 H9 J8 I8 H8 J7 I7 H7
                    Assert.AreEqual("161 160 159 142 141 140 123 122 121", string.Join(' ', indexes), msg.ToString());
                }
                else
                {
                    Assert.Fail();
                }
            }

            // 表記確認☆（＾～＾）
            {
                var signs = new List<string>();

                var msg = new StringBuilder();
                var (cellRange, next) = CellRange.Parse("h7:j9", 0);
                if (cellRange != null)
                {
                    cellRange.Foreach((indexO0, r, c) =>
                    {
                        var cellAddress = CellAddress.FromIndex(indexO0);
                        msg.AppendLine($"indexO0={indexO0} {cellAddress.ToIndex()}");
                        signs.Add(cellAddress.ToDisplay());
                    });

                    Assert.AreEqual("H7 I7 J7 H8 I8 J8 H9 I9 J9", string.Join(' ', signs), msg.ToString());
                }
                else
                {
                    Assert.Fail();
                }
            }

            {
                var signs = new List<string>();

                var msg = new StringBuilder();
                var (cellRange, next) = CellRange.Parse("j9:h7", 0);
                if (cellRange != null)
                {
                    cellRange.Foreach((indexO0, r, c) =>
                    {
                        var cellAddress = CellAddress.FromIndex(indexO0);
                        msg.AppendLine($"indexO0={indexO0} {cellAddress.ToIndex()}");
                        signs.Add(cellAddress.ToDisplay());
                    });

                    Assert.AreEqual("J9 I9 H9 J8 I8 H8 J7 I7 H7", string.Join(' ', signs), msg.ToString());
                }
                else
                {
                    Assert.Fail();
                }
            }
        }

        /// <summary>
        /// 国際式囲碁のセル番地表記をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestInternationalCellAddress()
        {
            Assert.AreEqual("A1", InternationalCellAddress.Parse("A1", 0).Item1?.ToDisplay());
            Assert.AreEqual("B2", InternationalCellAddress.Parse("B2", 0).Item1?.ToDisplay());
            Assert.AreEqual("C3", InternationalCellAddress.Parse("C3", 0).Item1?.ToDisplay());
            Assert.AreEqual("D4", InternationalCellAddress.Parse("D4", 0).Item1?.ToDisplay());
            Assert.AreEqual("E5", InternationalCellAddress.Parse("E5", 0).Item1?.ToDisplay());
            Assert.AreEqual("F6", InternationalCellAddress.Parse("F6", 0).Item1?.ToDisplay());
            Assert.AreEqual("G7", InternationalCellAddress.Parse("G7", 0).Item1?.ToDisplay());
            Assert.AreEqual("H8", InternationalCellAddress.Parse("H8", 0).Item1?.ToDisplay());
            Assert.AreEqual("J9", InternationalCellAddress.Parse("J9", 0).Item1?.ToDisplay());
            Assert.AreEqual("K10", InternationalCellAddress.Parse("K10", 0).Item1?.ToDisplay());
            Assert.AreEqual("L11", InternationalCellAddress.Parse("L11", 0).Item1?.ToDisplay());
            Assert.AreEqual("M12", InternationalCellAddress.Parse("M12", 0).Item1?.ToDisplay());
            Assert.AreEqual("N13", InternationalCellAddress.Parse("N13", 0).Item1?.ToDisplay());
            Assert.AreEqual("O14", InternationalCellAddress.Parse("O14", 0).Item1?.ToDisplay());
            Assert.AreEqual("P15", InternationalCellAddress.Parse("P15", 0).Item1?.ToDisplay());
            Assert.AreEqual("Q16", InternationalCellAddress.Parse("Q16", 0).Item1?.ToDisplay());
            Assert.AreEqual("R17", InternationalCellAddress.Parse("R17", 0).Item1?.ToDisplay());
            Assert.AreEqual("S18", InternationalCellAddress.Parse("S18", 0).Item1?.ToDisplay());
            Assert.AreEqual("T19", InternationalCellAddress.Parse("T19", 0).Item1?.ToDisplay());

            // 小文字は、大文字に変換。
            Assert.AreEqual("A19", InternationalCellAddress.Parse("a19", 0).Item1?.ToDisplay());
            Assert.AreEqual("B19", InternationalCellAddress.Parse("b19", 0).Item1?.ToDisplay());
            Assert.AreEqual("S19", InternationalCellAddress.Parse("s19", 0).Item1?.ToDisplay());
            Assert.AreEqual("T19", InternationalCellAddress.Parse("t19", 0).Item1?.ToDisplay());
        }

        /// <summary>
        /// 国際式囲碁の列番号のテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestInternationalColumnAddress()
        {
            Assert.AreEqual("A", InternationalColumnAddress.Parse("A", 0).Item1?.ToDisplay());
            Assert.AreEqual("B", InternationalColumnAddress.Parse("B", 0).Item1?.ToDisplay());
            Assert.AreEqual("C", InternationalColumnAddress.Parse("C", 0).Item1?.ToDisplay());
            Assert.AreEqual("D", InternationalColumnAddress.Parse("ABCDEFGHIJKLMNOPQRST", 3).Item1?.ToDisplay());
            Assert.AreEqual("E", InternationalColumnAddress.Parse("ABCDEFGHIJKLMNOPQRST", 4).Item1?.ToDisplay());
            Assert.AreEqual("F", InternationalColumnAddress.Parse("ABCDEFGHIJKLMNOPQRST", 5).Item1?.ToDisplay());
            Assert.AreEqual("G", InternationalColumnAddress.Parse("G", 0).Item1?.ToDisplay());
            Assert.AreEqual("H", InternationalColumnAddress.Parse("H", 0).Item1?.ToDisplay());

            // I is a null.
            Assert.IsNull(InternationalColumnAddress.Parse("I", 0).Item1?.ToDisplay());

            Assert.AreEqual("J", InternationalColumnAddress.Parse("J", 0).Item1?.ToDisplay());
            Assert.AreEqual("K", InternationalColumnAddress.Parse("K", 0).Item1?.ToDisplay());
            Assert.AreEqual("L", InternationalColumnAddress.Parse("L", 0).Item1?.ToDisplay());
            Assert.AreEqual("M", InternationalColumnAddress.Parse("M", 0).Item1?.ToDisplay());
            Assert.AreEqual("N", InternationalColumnAddress.Parse("N", 0).Item1?.ToDisplay());
            Assert.AreEqual("O", InternationalColumnAddress.Parse("O", 0).Item1?.ToDisplay());
            Assert.AreEqual("P", InternationalColumnAddress.Parse("P", 0).Item1?.ToDisplay());
            Assert.AreEqual("Q", InternationalColumnAddress.Parse("Q", 0).Item1?.ToDisplay());
            Assert.AreEqual("R", InternationalColumnAddress.Parse("R", 0).Item1?.ToDisplay());
            Assert.AreEqual("S", InternationalColumnAddress.Parse("S", 0).Item1?.ToDisplay());
            Assert.AreEqual("T", InternationalColumnAddress.Parse("T", 0).Item1?.ToDisplay());
        }

        /// <summary>
        /// 国際式囲碁の行番号をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestInternationalRowAddress()
        {
            // インデックス確認☆（＾～＾）内部的には行番号は　ひっくり返っているぜ☆（＾～＾）
            Assert.AreEqual(18, InternationalRowAddress.Parse("1", 0).Item1?.NumberO0);
            Assert.AreEqual(17, InternationalRowAddress.Parse("2", 0).Item1?.NumberO0);
            Assert.AreEqual(16, InternationalRowAddress.Parse("3", 0).Item1?.NumberO0);
            Assert.AreEqual(15, InternationalRowAddress.Parse("1234", 3).Item1?.NumberO0);
            Assert.AreEqual(14, InternationalRowAddress.Parse("12345", 4).Item1?.NumberO0);
            Assert.AreEqual(13, InternationalRowAddress.Parse("123456", 5).Item1?.NumberO0);
            Assert.AreEqual(12, InternationalRowAddress.Parse("7", 0).Item1?.NumberO0);
            Assert.AreEqual(11, InternationalRowAddress.Parse("8", 0).Item1?.NumberO0);
            Assert.AreEqual(10, InternationalRowAddress.Parse("9", 0).Item1?.NumberO0);
            Assert.AreEqual(9, InternationalRowAddress.Parse("10", 0).Item1?.NumberO0);
            Assert.AreEqual(8, InternationalRowAddress.Parse("11", 0).Item1?.NumberO0);
            Assert.AreEqual(7, InternationalRowAddress.Parse("12", 0).Item1?.NumberO0);
            Assert.AreEqual(6, InternationalRowAddress.Parse("12345678901213", 12).Item1?.NumberO0);
            Assert.AreEqual(5, InternationalRowAddress.Parse("123456789012314", 13).Item1?.NumberO0);
            Assert.AreEqual(4, InternationalRowAddress.Parse("1234567890123415", 14).Item1?.NumberO0);
            Assert.AreEqual(3, InternationalRowAddress.Parse("16", 0).Item1?.NumberO0);
            Assert.AreEqual(2, InternationalRowAddress.Parse("17", 0).Item1?.NumberO0);
            Assert.AreEqual(1, InternationalRowAddress.Parse("18", 0).Item1?.NumberO0);
            Assert.AreEqual(0, InternationalRowAddress.Parse("19", 0).Item1?.NumberO0);

            // 表記確認☆（＾～＾）
            Assert.AreEqual("1", InternationalRowAddress.Parse("1", 0).Item1?.ToDisplay());
            Assert.AreEqual("2", InternationalRowAddress.Parse("2", 0).Item1?.ToDisplay());
            Assert.AreEqual("3", InternationalRowAddress.Parse("3", 0).Item1?.ToDisplay());
            Assert.AreEqual("4", InternationalRowAddress.Parse("1234", 3).Item1?.ToDisplay());
            Assert.AreEqual("5", InternationalRowAddress.Parse("12345", 4).Item1?.ToDisplay());
            Assert.AreEqual("6", InternationalRowAddress.Parse("123456", 5).Item1?.ToDisplay());
            Assert.AreEqual("7", InternationalRowAddress.Parse("7", 0).Item1?.ToDisplay());
            Assert.AreEqual("8", InternationalRowAddress.Parse("8", 0).Item1?.ToDisplay());
            Assert.AreEqual("9", InternationalRowAddress.Parse("9", 0).Item1?.ToDisplay());
            Assert.AreEqual("10", InternationalRowAddress.Parse("10", 0).Item1?.ToDisplay());
            Assert.AreEqual("11", InternationalRowAddress.Parse("11", 0).Item1?.ToDisplay());
            Assert.AreEqual("12", InternationalRowAddress.Parse("12", 0).Item1?.ToDisplay());
            Assert.AreEqual("13", InternationalRowAddress.Parse("12345678901213", 12).Item1?.ToDisplay());
            Assert.AreEqual("14", InternationalRowAddress.Parse("123456789012314", 13).Item1?.ToDisplay());
            Assert.AreEqual("15", InternationalRowAddress.Parse("1234567890123415", 14).Item1?.ToDisplay());
            Assert.AreEqual("16", InternationalRowAddress.Parse("16", 0).Item1?.ToDisplay());
            Assert.AreEqual("17", InternationalRowAddress.Parse("17", 0).Item1?.ToDisplay());
            Assert.AreEqual("18", InternationalRowAddress.Parse("18", 0).Item1?.ToDisplay());
            Assert.AreEqual("19", InternationalRowAddress.Parse("19", 0).Item1?.ToDisplay());
        }

        /// <summary>
        /// Z字方向式の行番号をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestZShapedRowAddress()
        {
            // インデックス確認☆（＾～＾）
            Assert.AreEqual(0, RowAddress.Parse("1", 0).Item1?.NumberO0);
            Assert.AreEqual(1, RowAddress.Parse("2", 0).Item1?.NumberO0);
            Assert.AreEqual(2, RowAddress.Parse("3", 0).Item1?.NumberO0);
            Assert.AreEqual(3, RowAddress.Parse("1234", 3).Item1?.NumberO0);
            Assert.AreEqual(4, RowAddress.Parse("12345", 4).Item1?.NumberO0);
            Assert.AreEqual(5, RowAddress.Parse("123456", 5).Item1?.NumberO0);
            Assert.AreEqual(6, RowAddress.Parse("7", 0).Item1?.NumberO0);
            Assert.AreEqual(7, RowAddress.Parse("8", 0).Item1?.NumberO0);
            Assert.AreEqual(8, RowAddress.Parse("9", 0).Item1?.NumberO0);
            Assert.AreEqual(9, RowAddress.Parse("10", 0).Item1?.NumberO0);
            Assert.AreEqual(10, RowAddress.Parse("11", 0).Item1?.NumberO0);
            Assert.AreEqual(11, RowAddress.Parse("12", 0).Item1?.NumberO0);
            Assert.AreEqual(12, RowAddress.Parse("12345678901213", 12).Item1?.NumberO0);
            Assert.AreEqual(13, RowAddress.Parse("123456789012314", 13).Item1?.NumberO0);
            Assert.AreEqual(14, RowAddress.Parse("1234567890123415", 14).Item1?.NumberO0);
            Assert.AreEqual(15, RowAddress.Parse("16", 0).Item1?.NumberO0);
            Assert.AreEqual(16, RowAddress.Parse("17", 0).Item1?.NumberO0);
            Assert.AreEqual(17, RowAddress.Parse("18", 0).Item1?.NumberO0);
            Assert.AreEqual(18, RowAddress.Parse("19", 0).Item1?.NumberO0);

            // 表記確認☆（＾～＾）
            Assert.AreEqual("1", RowAddress.Parse("1", 0).Item1?.ToDisplay());
            Assert.AreEqual("2", RowAddress.Parse("2", 0).Item1?.ToDisplay());
            Assert.AreEqual("3", RowAddress.Parse("3", 0).Item1?.ToDisplay());
            Assert.AreEqual("4", RowAddress.Parse("1234", 3).Item1?.ToDisplay());
            Assert.AreEqual("5", RowAddress.Parse("12345", 4).Item1?.ToDisplay());
            Assert.AreEqual("6", RowAddress.Parse("123456", 5).Item1?.ToDisplay());
            Assert.AreEqual("7", RowAddress.Parse("7", 0).Item1?.ToDisplay());
            Assert.AreEqual("8", RowAddress.Parse("8", 0).Item1?.ToDisplay());
            Assert.AreEqual("9", RowAddress.Parse("9", 0).Item1?.ToDisplay());
            Assert.AreEqual("10", RowAddress.Parse("10", 0).Item1?.ToDisplay());
            Assert.AreEqual("11", RowAddress.Parse("11", 0).Item1?.ToDisplay());
            Assert.AreEqual("12", RowAddress.Parse("12", 0).Item1?.ToDisplay());
            Assert.AreEqual("13", RowAddress.Parse("12345678901213", 12).Item1?.ToDisplay());
            Assert.AreEqual("14", RowAddress.Parse("123456789012314", 13).Item1?.ToDisplay());
            Assert.AreEqual("15", RowAddress.Parse("1234567890123415", 14).Item1?.ToDisplay());
            Assert.AreEqual("16", RowAddress.Parse("16", 0).Item1?.ToDisplay());
            Assert.AreEqual("17", RowAddress.Parse("17", 0).Item1?.ToDisplay());
            Assert.AreEqual("18", RowAddress.Parse("18", 0).Item1?.ToDisplay());
            Assert.AreEqual("19", RowAddress.Parse("19", 0).Item1?.ToDisplay());
        }

        /// <summary>
        /// 単語完全一致のテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestExactlyKeyword()
        {
            // 単語完全一致のテスト☆（＾～＾）
            Assert.AreEqual("black", ExactlyKeyword.Parse("black", "black", 0).Item1?.ToDisplay());
            Assert.AreEqual("white", ExactlyKeyword.Parse("white", "white", 0).Item1?.ToDisplay());
            Assert.AreEqual("start", ExactlyKeyword.Parse("start", "start", 0).Item1?.ToDisplay());

            // ホワイトスペースのテスト☆（*＾～＾*）
            Assert.AreEqual("     ", WhiteSpace.Parse("     ", 0).Item1?.ToDisplay());

            // 最初にマッチする単語のテスト☆（＾～＾）
            Assert.AreEqual("black", Word.Parse("black a19 k10 t1", 0).Item1?.ToDisplay());

            Assert.AreEqual("Hello, world", WordUpToDelimiter.Parse("!", "Hello, world!", 0).Item1?.ToDisplay());
        }
    }
}
