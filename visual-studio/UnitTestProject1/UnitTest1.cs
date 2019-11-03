namespace UnitTestProject1
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using KifuwarabeUec11Gui.Script;
    using KifuwarabeUec11Gui.Script.ExcelGo;
    using KifuwarabeUec11Gui.Script.InternationalGo;
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
            */
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

            {
                var signs = new List<string>();

                // I列は無いことに注意☆（＾～＾）！
                InternationalCellRange.Parse("H7:K9", 0).Item1?.Foreach((index) =>
                {
                    signs.Add(InternationalCellAddress.FromIndex(index).ToDisplay());
                });

                Assert.AreEqual("H7 J7 K7 H8 J8 K8 H9 J9 K9", string.Join(' ', signs));
            }

            {
                var signs = new List<string>();

                // I列は無いことに注意☆（＾～＾）！
                InternationalCellRange.Parse("K9:H7", 0).Item1?.Foreach((index) =>
                {
                    signs.Add(InternationalCellAddress.FromIndex(index).ToDisplay());
                });

                Assert.AreEqual("K9 J9 H9 K8 J8 H8 K7 J7 H7", string.Join(' ', signs));
            }
        }

        /// <summary>
        /// Excel式のセル番地表記をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestExcelCellRange()
        {
            Assert.AreEqual("C7:E9", CellRange.Parse("c7:e9", 0).Item1?.ToDisplay());
            Assert.AreEqual("E9:C7", CellRange.Parse("e9:c7", 0).Item1?.ToDisplay());

            // 短縮表記☆（＾～＾）
            Assert.AreEqual("F5", CellRange.Parse("F5:F5", 0).Item1?.ToDisplay());

            {
                var signs = new List<string>();

                CellRange.Parse("h7:j9", 0).Item1?.Foreach((index) =>
                {
                    signs.Add(CellAddress.FromIndex(index).ToDisplay());
                });

                Assert.AreEqual("H7 I7 J7 H8 I8 J8 H9 I9 J9", string.Join(' ', signs));
            }

            {
                var signs = new List<string>();

                CellRange.Parse("j9:h7", 0).Item1?.Foreach((index) =>
                {
                    signs.Add(CellAddress.FromIndex(index).ToDisplay());
                });

                Assert.AreEqual("J9 I9 H9 J8 I8 H8 J7 I7 H7", string.Join(' ', signs));
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
        }
    }
}
