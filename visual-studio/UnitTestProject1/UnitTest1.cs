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
            var boardModel = new BoardModel();

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
            for (int i = 0; i < boardModel.ColumnSize; i++)
            {
                builder.Append($"{i * boardModel.ColumnSize},");
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
            var boardModel = new BoardModel();

            Assert.AreEqual("A19 K1 T1", ColorInstructionArgument.Parse("black A19 K1 T1", 5, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("B19 K2 S1", ColorInstructionArgument.Parse("white B19 K2 S1", 5, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("C19 K3 R1", ColorInstructionArgument.Parse("space C19 K3 R1", 5, boardModel).Item1?.ToDisplay(boardModel));

            // 混合型☆（＾～＾）
            Assert.AreEqual("A2:B1 C4:D3 E5", ColorInstructionArgument.Parse("space A2:B1 C4:D3 E5", 5, boardModel).Item1?.ToDisplay(boardModel));

            // 大文字・小文字は区別するぜ☆（＾～＾）
            Assert.AreNotEqual("A19 K1 T1", ColorInstructionArgument.Parse("black a19 k1 t1", 5, boardModel).Item1?.ToDisplay(boardModel));
        }

        /// <summary>
        /// 国際式囲碁のセル番地表記をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestInternationalCellRange()
        {
            var boardModel = new BoardModel();
            Assert.AreEqual("C7:E9", InternationalCellRange.Parse("C7:E9", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("E9:C7", InternationalCellRange.Parse("E9:C7", 0, boardModel).Item1?.ToDisplay(boardModel));

            // 短縮表記☆（＾～＾）
            Assert.AreEqual("F5", InternationalCellRange.Parse("F5:F5", 0, boardModel).Item1?.ToDisplay(boardModel));

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
                InternationalCellRange.Parse("H7:K9", 0, boardModel).Item1?.Foreach(boardModel, (index) =>
                {
                    indexes.Add(index);
                });

                // H7 J7 K7 H8 J8 K8 H9 J9 K9
                Assert.AreEqual("235 236 237 216 217 218 197 198 199", string.Join(' ', indexes));
            }

            {
                var indexes = new List<int>();

                // I列は無いことに注意☆（＾～＾）！
                InternationalCellRange.Parse("K9:H7", 0, boardModel).Item1?.Foreach(boardModel, (index) =>
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
                InternationalCellRange.Parse("H7:K9", 0, boardModel).Item1?.Foreach(boardModel, (indexO0) =>
                {
                    var boardModel = new BoardModel();
                    signs.Add(InternationalCellAddress.FromIndex(indexO0, boardModel).ToDisplay(boardModel));
                });

                Assert.AreEqual("H7 J7 K7 H8 J8 K8 H9 J9 K9", string.Join(' ', signs));
            }

            {
                var signs = new List<string>();

                // I列は無いことに注意☆（＾～＾）！
                InternationalCellRange.Parse("K9:H7", 0, boardModel).Item1?.Foreach(boardModel, (indexO0) =>
                {
                    var boardModel = new BoardModel();
                    signs.Add(InternationalCellAddress.FromIndex(indexO0, boardModel).ToDisplay(boardModel));
                });

                Assert.AreEqual("K9 J9 H9 K8 J8 H8 K7 J7 H7", string.Join(' ', signs));
            }
        }

        /// <summary>
        /// 国際式囲碁のセル番地表記をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestInternationalCellAddress()
        {
            var boardModel = new BoardModel();
            Assert.AreEqual("A1", InternationalCellAddress.Parse("A1", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("B2", InternationalCellAddress.Parse("B2", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("C3", InternationalCellAddress.Parse("C3", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("D4", InternationalCellAddress.Parse("D4", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("E5", InternationalCellAddress.Parse("E5", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("F6", InternationalCellAddress.Parse("F6", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("G7", InternationalCellAddress.Parse("G7", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("H8", InternationalCellAddress.Parse("H8", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("J9", InternationalCellAddress.Parse("J9", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("K10", InternationalCellAddress.Parse("K10", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("L11", InternationalCellAddress.Parse("L11", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("M12", InternationalCellAddress.Parse("M12", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("N13", InternationalCellAddress.Parse("N13", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("O14", InternationalCellAddress.Parse("O14", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("P15", InternationalCellAddress.Parse("P15", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("Q16", InternationalCellAddress.Parse("Q16", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("R17", InternationalCellAddress.Parse("R17", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("S18", InternationalCellAddress.Parse("S18", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("T19", InternationalCellAddress.Parse("T19", 0, boardModel).Item1?.ToDisplay(boardModel));

            // 大文字・小文字は区別するぜ☆（＾～＾）
            Assert.AreNotEqual("A19", InternationalCellAddress.Parse("a19", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreNotEqual("B19", InternationalCellAddress.Parse("b19", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreNotEqual("s19", InternationalCellAddress.Parse("S19", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreNotEqual("t19", InternationalCellAddress.Parse("T19", 0, boardModel).Item1?.ToDisplay(boardModel));
        }

        /// <summary>
        /// 国際式囲碁の列番号のテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestInternationalColumnAddress()
        {
            var boardModel = new BoardModel();

            Assert.AreEqual("A", ColumnAddress.Parse("A", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("B", ColumnAddress.Parse("B", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("C", ColumnAddress.Parse("C", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("D", ColumnAddress.Parse("ABCDEFGHIJKLMNOPQRST", 3, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("E", ColumnAddress.Parse("ABCDEFGHIJKLMNOPQRST", 4, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("F", ColumnAddress.Parse("ABCDEFGHIJKLMNOPQRST", 5, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("G", ColumnAddress.Parse("G", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("H", ColumnAddress.Parse("H", 0, boardModel).Item1?.ToDisplay(boardModel));

            // I is a null.
            Assert.IsNull(ColumnAddress.Parse("I", 0, boardModel).Item1?.ToDisplay(boardModel));

            Assert.AreEqual("J", ColumnAddress.Parse("J", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("K", ColumnAddress.Parse("K", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("L", ColumnAddress.Parse("L", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("M", ColumnAddress.Parse("M", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("N", ColumnAddress.Parse("N", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("O", ColumnAddress.Parse("O", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("P", ColumnAddress.Parse("P", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("Q", ColumnAddress.Parse("Q", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("R", ColumnAddress.Parse("R", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("S", ColumnAddress.Parse("S", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("T", ColumnAddress.Parse("T", 0, boardModel).Item1?.ToDisplay(boardModel));
        }

        /// <summary>
        /// 国際式囲碁の行番号をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestInternationalRowAddress()
        {
            var boardModel = new BoardModel();

            // インデックス確認☆（＾～＾）内部的には行番号は　ひっくり返っているぜ☆（＾～＾）
            Assert.AreEqual(18, InternationalRowAddress.Parse("1", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(17, InternationalRowAddress.Parse("2", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(16, InternationalRowAddress.Parse("3", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(15, InternationalRowAddress.Parse("1234", 3, boardModel).Item1?.NumberO0);
            Assert.AreEqual(14, InternationalRowAddress.Parse("12345", 4, boardModel).Item1?.NumberO0);
            Assert.AreEqual(13, InternationalRowAddress.Parse("123456", 5, boardModel).Item1?.NumberO0);
            Assert.AreEqual(12, InternationalRowAddress.Parse("7", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(11, InternationalRowAddress.Parse("8", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(10, InternationalRowAddress.Parse("9", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(9, InternationalRowAddress.Parse("10", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(8, InternationalRowAddress.Parse("11", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(7, InternationalRowAddress.Parse("12", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(6, InternationalRowAddress.Parse("12345678901213", 12, boardModel).Item1?.NumberO0);
            Assert.AreEqual(5, InternationalRowAddress.Parse("123456789012314", 13, boardModel).Item1?.NumberO0);
            Assert.AreEqual(4, InternationalRowAddress.Parse("1234567890123415", 14, boardModel).Item1?.NumberO0);
            Assert.AreEqual(3, InternationalRowAddress.Parse("16", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(2, InternationalRowAddress.Parse("17", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(1, InternationalRowAddress.Parse("18", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(0, InternationalRowAddress.Parse("19", 0, boardModel).Item1?.NumberO0);

            // 表記確認☆（＾～＾）
            Assert.AreEqual("1", InternationalRowAddress.Parse("1", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("2", InternationalRowAddress.Parse("2", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("3", InternationalRowAddress.Parse("3", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("4", InternationalRowAddress.Parse("1234", 3, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("5", InternationalRowAddress.Parse("12345", 4, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("6", InternationalRowAddress.Parse("123456", 5, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("7", InternationalRowAddress.Parse("7", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("8", InternationalRowAddress.Parse("8", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("9", InternationalRowAddress.Parse("9", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("10", InternationalRowAddress.Parse("10", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("11", InternationalRowAddress.Parse("11", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("12", InternationalRowAddress.Parse("12", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("13", InternationalRowAddress.Parse("12345678901213", 12, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("14", InternationalRowAddress.Parse("123456789012314", 13, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("15", InternationalRowAddress.Parse("1234567890123415", 14, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("16", InternationalRowAddress.Parse("16", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("17", InternationalRowAddress.Parse("17", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("18", InternationalRowAddress.Parse("18", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("19", InternationalRowAddress.Parse("19", 0, boardModel).Item1?.ToDisplay(boardModel));
        }

        /// <summary>
        /// Z字方向式の行番号をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestZShapedRowAddress()
        {
            var boardModel = new BoardModel();

            // インデックス確認☆（＾～＾）
            Assert.AreEqual(0, RowAddress.Parse("1", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(1, RowAddress.Parse("2", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(2, RowAddress.Parse("3", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(3, RowAddress.Parse("1234", 3, boardModel).Item1?.NumberO0);
            Assert.AreEqual(4, RowAddress.Parse("12345", 4, boardModel).Item1?.NumberO0);
            Assert.AreEqual(5, RowAddress.Parse("123456", 5, boardModel).Item1?.NumberO0);
            Assert.AreEqual(6, RowAddress.Parse("7", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(7, RowAddress.Parse("8", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(8, RowAddress.Parse("9", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(9, RowAddress.Parse("10", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(10, RowAddress.Parse("11", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(11, RowAddress.Parse("12", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(12, RowAddress.Parse("12345678901213", 12, boardModel).Item1?.NumberO0);
            Assert.AreEqual(13, RowAddress.Parse("123456789012314", 13, boardModel).Item1?.NumberO0);
            Assert.AreEqual(14, RowAddress.Parse("1234567890123415", 14, boardModel).Item1?.NumberO0);
            Assert.AreEqual(15, RowAddress.Parse("16", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(16, RowAddress.Parse("17", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(17, RowAddress.Parse("18", 0, boardModel).Item1?.NumberO0);
            Assert.AreEqual(18, RowAddress.Parse("19", 0, boardModel).Item1?.NumberO0);

            // 表記確認☆（＾～＾）
            Assert.AreEqual("1", RowAddress.Parse("1", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("2", RowAddress.Parse("2", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("3", RowAddress.Parse("3", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("4", RowAddress.Parse("1234", 3, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("5", RowAddress.Parse("12345", 4, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("6", RowAddress.Parse("123456", 5, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("7", RowAddress.Parse("7", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("8", RowAddress.Parse("8", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("9", RowAddress.Parse("9", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("10", RowAddress.Parse("10", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("11", RowAddress.Parse("11", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("12", RowAddress.Parse("12", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("13", RowAddress.Parse("12345678901213", 12, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("14", RowAddress.Parse("123456789012314", 13, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("15", RowAddress.Parse("1234567890123415", 14, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("16", RowAddress.Parse("16", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("17", RowAddress.Parse("17", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("18", RowAddress.Parse("18", 0, boardModel).Item1?.ToDisplay(boardModel));
            Assert.AreEqual("19", RowAddress.Parse("19", 0, boardModel).Item1?.ToDisplay(boardModel));
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

        /// <summary>
        /// 列番号のテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestColumnNumbers()
        {
            var boardModel = new BoardModel();

            Assert.AreEqual(0, boardModel.ColumnNumbers.IndexOf("A"));
            Assert.AreEqual(1, boardModel.ColumnNumbers.IndexOf("B"));
            Assert.AreEqual(2, boardModel.ColumnNumbers.IndexOf("C"));
            Assert.AreEqual(3, boardModel.ColumnNumbers.IndexOf("D"));
            Assert.AreEqual(4, boardModel.ColumnNumbers.IndexOf("E"));
            Assert.AreEqual(5, boardModel.ColumnNumbers.IndexOf("F"));
            Assert.AreEqual(6, boardModel.ColumnNumbers.IndexOf("G"));
            Assert.AreEqual(7, boardModel.ColumnNumbers.IndexOf("H"));
            Assert.AreNotEqual(8, boardModel.ColumnNumbers.IndexOf("I"));
            Assert.AreEqual(8, boardModel.ColumnNumbers.IndexOf("J"));
            Assert.AreEqual(9, boardModel.ColumnNumbers.IndexOf("K"));
            Assert.AreEqual(10, boardModel.ColumnNumbers.IndexOf("L"));
            Assert.AreEqual(11, boardModel.ColumnNumbers.IndexOf("M"));
            Assert.AreEqual(12, boardModel.ColumnNumbers.IndexOf("N"));
            Assert.AreEqual(13, boardModel.ColumnNumbers.IndexOf("O"));
            Assert.AreEqual(14, boardModel.ColumnNumbers.IndexOf("P"));
            Assert.AreEqual(15, boardModel.ColumnNumbers.IndexOf("Q"));
            Assert.AreEqual(16, boardModel.ColumnNumbers.IndexOf("R"));
            Assert.AreEqual(17, boardModel.ColumnNumbers.IndexOf("S"));
            Assert.AreEqual(18, boardModel.ColumnNumbers.IndexOf("T"));
        }
    }
}
