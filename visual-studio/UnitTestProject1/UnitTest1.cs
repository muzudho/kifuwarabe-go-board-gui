namespace UnitTestProject1
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using KifuwarabeUec11Gui;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using KifuwarabeUec11Gui.Controller;

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
            var model = new ApplicationObjectModel();

            Assert.AreEqual("A19 K1 T1", ColorInstructionArgument.Parse("black A19 K1 T1", 5, model).Item1?.ToDisplay(model));
            Assert.AreEqual("B19 K2 S1", ColorInstructionArgument.Parse("white B19 K2 S1", 5, model).Item1?.ToDisplay(model));
            Assert.AreEqual("C19 K3 R1", ColorInstructionArgument.Parse("space C19 K3 R1", 5, model).Item1?.ToDisplay(model));

            // 混合型☆（＾～＾）
            Assert.AreEqual("A2:B1 C4:D3 E5", ColorInstructionArgument.Parse("space A2:B1 C4:D3 E5", 5, model).Item1?.ToDisplay(model));

            // 大文字・小文字は区別するぜ☆（＾～＾）
            Assert.AreNotEqual("A19 K1 T1", ColorInstructionArgument.Parse("black a19 k1 t1", 5, model).Item1?.ToDisplay(model));
        }

        /// <summary>
        /// 国際式囲碁のセル番地表記をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestInternationalCellRange()
        {
            var model = new ApplicationObjectModel();
            Assert.AreEqual("C7:E9", CellRange.Parse("C7:E9", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("E9:C7", CellRange.Parse("E9:C7", 0, model).Item1?.ToDisplay(model));

            // 短縮表記☆（＾～＾）
            Assert.AreEqual("F5", CellRange.Parse("F5:F5", 0, model).Item1?.ToDisplay(model));

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
                CellRange.Parse("H7:K9", 0, model).Item1?.Foreach(model, (index) =>
                {
                    indexes.Add(index);
                });

                // H7 J7 K7 H8 J8 K8 H9 J9 K9
                Assert.AreEqual("235 236 237 216 217 218 197 198 199", string.Join(' ', indexes));
            }

            {
                var indexes = new List<int>();

                // I列は無いことに注意☆（＾～＾）！
                CellRange.Parse("K9:H7", 0, model).Item1?.Foreach(model, (index) =>
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
                CellRange.Parse("H7:K9", 0, model).Item1?.Foreach(model, (indexO0) =>
                {
                    signs.Add(CellAddress.FromIndex(indexO0, model).ToDisplayTrimed(model));
                });

                Assert.AreEqual("H7 J7 K7 H8 J8 K8 H9 J9 K9", string.Join(' ', signs));
            }

            {
                var signs = new List<string>();

                // I列は無いことに注意☆（＾～＾）！
                CellRange.Parse("K9:H7", 0, model).Item1?.Foreach(model, (indexO0) =>
                {
                    signs.Add(CellAddress.FromIndex(indexO0, model).ToDisplayTrimed(model));
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
            var model = new ApplicationObjectModel();
            Assert.AreEqual("A1", CellAddress.Parse("A1", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("B2", CellAddress.Parse("B2", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("C3", CellAddress.Parse("C3", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("D4", CellAddress.Parse("D4", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("E5", CellAddress.Parse("E5", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("F6", CellAddress.Parse("F6", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("G7", CellAddress.Parse("G7", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("H8", CellAddress.Parse("H8", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("J9", CellAddress.Parse("J9", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("K10", CellAddress.Parse("K10", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("L11", CellAddress.Parse("L11", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("M12", CellAddress.Parse("M12", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("N13", CellAddress.Parse("N13", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("O14", CellAddress.Parse("O14", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("P15", CellAddress.Parse("P15", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("Q16", CellAddress.Parse("Q16", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("R17", CellAddress.Parse("R17", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("S18", CellAddress.Parse("S18", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("T19", CellAddress.Parse("T19", 0, model).Item1?.ToDisplayTrimed(model));

            // 大文字・小文字は区別するぜ☆（＾～＾）
            Assert.AreNotEqual("A19", CellAddress.Parse("a19", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreNotEqual("B19", CellAddress.Parse("b19", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreNotEqual("s19", CellAddress.Parse("S19", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreNotEqual("t19", CellAddress.Parse("T19", 0, model).Item1?.ToDisplayTrimed(model));
        }

        /// <summary>
        /// 国際式囲碁の列番号のテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestInternationalColumnAddress()
        {
            var model = new ApplicationObjectModel();

            Assert.AreEqual("A", ColumnAddress.Parse("A", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("B", ColumnAddress.Parse("B", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("C", ColumnAddress.Parse("C", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("D", ColumnAddress.Parse("ABCDEFGHIJKLMNOPQRST", 3, model).Item1?.ToDisplay(model));
            Assert.AreEqual("E", ColumnAddress.Parse("ABCDEFGHIJKLMNOPQRST", 4, model).Item1?.ToDisplay(model));
            Assert.AreEqual("F", ColumnAddress.Parse("ABCDEFGHIJKLMNOPQRST", 5, model).Item1?.ToDisplay(model));
            Assert.AreEqual("G", ColumnAddress.Parse("G", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("H", ColumnAddress.Parse("H", 0, model).Item1?.ToDisplay(model));

            // I is a null.
            Assert.IsNull(ColumnAddress.Parse("I", 0, model).Item1?.ToDisplay(model));

            Assert.AreEqual("J", ColumnAddress.Parse("J", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("K", ColumnAddress.Parse("K", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("L", ColumnAddress.Parse("L", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("M", ColumnAddress.Parse("M", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("N", ColumnAddress.Parse("N", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("O", ColumnAddress.Parse("O", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("P", ColumnAddress.Parse("P", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("Q", ColumnAddress.Parse("Q", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("R", ColumnAddress.Parse("R", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("S", ColumnAddress.Parse("S", 0, model).Item1?.ToDisplay(model));
            Assert.AreEqual("T", ColumnAddress.Parse("T", 0, model).Item1?.ToDisplay(model));
        }

        /// <summary>
        /// 国際式囲碁の行番号をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestRowAddress()
        {
            var model = new ApplicationObjectModel();

            // インデックス確認☆（＾～＾）内部的には行番号は　ひっくり返っているぜ☆（＾～＾）
            Assert.AreEqual(18, RowAddress.Parse("1", 0, model).Item1?.NumberO0);
            Assert.AreEqual(17, RowAddress.Parse("2", 0, model).Item1?.NumberO0);
            Assert.AreEqual(16, RowAddress.Parse("3", 0, model).Item1?.NumberO0);
            Assert.AreEqual(15, RowAddress.Parse("1234", 3, model).Item1?.NumberO0);
            Assert.AreEqual(14, RowAddress.Parse("12345", 4, model).Item1?.NumberO0);
            Assert.AreEqual(13, RowAddress.Parse("123456", 5, model).Item1?.NumberO0);
            Assert.AreEqual(12, RowAddress.Parse("7", 0, model).Item1?.NumberO0);
            Assert.AreEqual(11, RowAddress.Parse("8", 0, model).Item1?.NumberO0);
            Assert.AreEqual(10, RowAddress.Parse("9", 0, model).Item1?.NumberO0);
            Assert.AreEqual(9, RowAddress.Parse("10", 0, model).Item1?.NumberO0);
            Assert.AreEqual(8, RowAddress.Parse("11", 0, model).Item1?.NumberO0);
            Assert.AreEqual(7, RowAddress.Parse("12", 0, model).Item1?.NumberO0);
            Assert.AreEqual(6, RowAddress.Parse("12345678901213", 12, model).Item1?.NumberO0);
            Assert.AreEqual(5, RowAddress.Parse("123456789012314", 13, model).Item1?.NumberO0);
            Assert.AreEqual(4, RowAddress.Parse("1234567890123415", 14, model).Item1?.NumberO0);
            Assert.AreEqual(3, RowAddress.Parse("16", 0, model).Item1?.NumberO0);
            Assert.AreEqual(2, RowAddress.Parse("17", 0, model).Item1?.NumberO0);
            Assert.AreEqual(1, RowAddress.Parse("18", 0, model).Item1?.NumberO0);
            Assert.AreEqual(0, RowAddress.Parse("19", 0, model).Item1?.NumberO0);

            // 表記確認☆（＾～＾）
            Assert.AreEqual("1", RowAddress.Parse("1", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("2", RowAddress.Parse("2", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("3", RowAddress.Parse("3", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("4", RowAddress.Parse("1234", 3, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("5", RowAddress.Parse("12345", 4, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("6", RowAddress.Parse("123456", 5, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("7", RowAddress.Parse("7", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("8", RowAddress.Parse("8", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("9", RowAddress.Parse("9", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("10", RowAddress.Parse("10", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("11", RowAddress.Parse("11", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("12", RowAddress.Parse("12", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("13", RowAddress.Parse("12345678901213", 12, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("14", RowAddress.Parse("123456789012314", 13, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("15", RowAddress.Parse("1234567890123415", 14, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("16", RowAddress.Parse("16", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("17", RowAddress.Parse("17", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("18", RowAddress.Parse("18", 0, model).Item1?.ToDisplayTrimed(model));
            Assert.AreEqual("19", RowAddress.Parse("19", 0, model).Item1?.ToDisplayTrimed(model));
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
            var start = 0;
            // Assert.AreEqual("     ", WhiteSpace.Parse("     ", start).Item1?.ToDisplay());
            Assert.AreEqual(5, WhiteSpace.Parse2("     ", start, (whiteSpace, curr) =>
            {
                Assert.AreEqual("     ", whiteSpace?.ToDisplay());
                if (whiteSpace != null)
                {
                    return curr + whiteSpace.Text.Length;
                }
                else
                {
                    return start;
                }
            }));

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
            var model = new ApplicationObjectModel();
            var columnNumbers = model.Properties[ColumnNumbersController.OutsideName].ToTextList();

            Assert.AreEqual(0, columnNumbers.IndexOf("A"));
            Assert.AreEqual(1, columnNumbers.IndexOf("B"));
            Assert.AreEqual(2, columnNumbers.IndexOf("C"));
            Assert.AreEqual(3, columnNumbers.IndexOf("D"));
            Assert.AreEqual(4, columnNumbers.IndexOf("E"));
            Assert.AreEqual(5, columnNumbers.IndexOf("F"));
            Assert.AreEqual(6, columnNumbers.IndexOf("G"));
            Assert.AreEqual(7, columnNumbers.IndexOf("H"));
            Assert.AreNotEqual(8, columnNumbers.IndexOf("I"));
            Assert.AreEqual(8, columnNumbers.IndexOf("J"));
            Assert.AreEqual(9, columnNumbers.IndexOf("K"));
            Assert.AreEqual(10, columnNumbers.IndexOf("L"));
            Assert.AreEqual(11, columnNumbers.IndexOf("M"));
            Assert.AreEqual(12, columnNumbers.IndexOf("N"));
            Assert.AreEqual(13, columnNumbers.IndexOf("O"));
            Assert.AreEqual(14, columnNumbers.IndexOf("P"));
            Assert.AreEqual(15, columnNumbers.IndexOf("Q"));
            Assert.AreEqual(16, columnNumbers.IndexOf("R"));
            Assert.AreEqual(17, columnNumbers.IndexOf("S"));
            Assert.AreEqual(18, columnNumbers.IndexOf("T"));
        }
    }
}
