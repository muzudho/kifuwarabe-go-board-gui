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
            var boardModel = new BoardModelWrapper(new BoardModel());

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
            Assert.AreEqual("b-name.value = Kifuwarabe", SetsInstructionArgument.Parse("set b-name.value = Kifuwarabe", 3).Item1?.ToDisplay());
            Assert.AreEqual("b-name.value = Kifuwarabe", SetsInstructionArgument.Parse("set b-name = Kifuwarabe", 3).Item1?.ToDisplay());
            Assert.AreEqual("b-time.value = 10:00", SetsInstructionArgument.Parse("set  b-time  =  10:00  ", 3).Item1?.ToDisplay());
            Assert.AreEqual("b-hama.value = ", SetsInstructionArgument.Parse("set b-hama =", 3).Item1?.ToDisplay());
        }

        /// <summary>
        /// 国際式囲碁のセル番地表記をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestColorInstructionArgumentTest()
        {
            var model = new ApplicationObjectModelWrapper();

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
            var model = new ApplicationObjectModelWrapper();

            // とりあえずこのテストのスタートは0に揃えておこう☆（＾～＾）
            var start = 0;

            Assert.AreEqual(5, CellRange.Parse("C7:E9", start, model, (matched, curr) =>
            {
                Assert.AreEqual("C7:E9", matched?.ToDisplay(model));
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));

            Assert.AreEqual(5, CellRange.Parse("E9:C7", start, model, (matched, curr) =>
            {
                Assert.AreEqual("E9:C7", matched?.ToDisplay(model));
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));

            // 短縮表記☆（＾～＾）
            Assert.AreEqual(5, CellRange.Parse("F5:F5", start, model, (matched, curr) =>
            {
                Assert.AreEqual("F5", matched?.ToDisplay(model));
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));

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
                CellRange.Parse("H7:K9", start, model, (matched, curr) =>
                {
                    Assert.AreEqual("H7:K9", matched?.ToDisplay(model));
                    if (matched == null)
                    {
                        return start;
                    }

                    matched.Foreach(model, (index) =>
                    {
                        indexes.Add(index);
                    });

                    return curr;
                });

                // H7 J7 K7 H8 J8 K8 H9 J9 K9
                Assert.AreEqual("235 236 237 216 217 218 197 198 199", string.Join(' ', indexes));
            }

            {
                var indexes = new List<int>();

                // I列は無いことに注意☆（＾～＾）！
                CellRange.Parse("K9:H7", start, model, (matched, curr) =>
                {
                    Assert.AreEqual("K9:H7", matched?.ToDisplay(model));
                    if (matched == null)
                    {
                        return start;
                    }

                    matched.Foreach(model, (index) =>
                    {
                        indexes.Add(index);
                    });

                    return curr;
                });

                // K9 J9 H9 K8 J8 H8 K7 J7 H7
                Assert.AreEqual("199 198 197 218 217 216 237 236 235", string.Join(' ', indexes));
            }

            // 表記確認☆（＾～＾）
            {
                var signs = new List<string>();

                // I列は無いことに注意☆（＾～＾）！
                CellRange.Parse("H7:K9", start, model, (matched, curr) =>
                {
                    Assert.AreEqual("H7:K9", matched?.ToDisplay(model));
                    if (matched == null)
                    {
                        return start;
                    }

                    matched.Foreach(model, (indexO0) =>
                    {
                        signs.Add(CellAddress.FromIndex(indexO0, model).ToDisplayTrimed(model));
                    });

                    return curr;
                });

                Assert.AreEqual("H7 J7 K7 H8 J8 K8 H9 J9 K9", string.Join(' ', signs));
            }

            {
                var signs = new List<string>();

                // I列は無いことに注意☆（＾～＾）！
                CellRange.Parse("K9:H7", start, model, (matched, curr) =>
                {
                    Assert.AreEqual("K9:H7", matched?.ToDisplay(model));
                    if (matched == null)
                    {
                        return start;
                    }

                    matched.Foreach(model, (indexO0) =>
                    {
                        signs.Add(CellAddress.FromIndex(indexO0, model).ToDisplayTrimed(model));
                    });

                    return curr;
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
            var appModel = new ApplicationObjectModelWrapper();

            // とりあえずこのテストのスタートは0に揃えておこう☆（＾～＾）
            var start = 0;

            // 2桁☆（＾～＾）
            var list1 = new List<string>()
            {
                "A1","B2","C3",                          "D4",                          "E5",                          "F6",                          "G7",                          "H8",                          "J9"
            };

            foreach (var item in list1)
            {
                Assert.AreEqual(2, CellAddress.Parse(item, start, appModel, (matched, curr) =>
                {
                    Assert.AreEqual(item, matched?.ToDisplayTrimed(appModel));
                    if (matched != null)
                    {
                        return curr;
                    }
                    else
                    {
                        return start;
                    }
                }));
            }

            // 3桁☆（＾～＾）
            var list2 = new List<string>()
            {
                "K10",
                "L11",
                "M12",
                "N13",
                "O14",
                "P15",
                "Q16",
                "R17",
                "S18",
                "T19"
            };

            foreach (var item in list2)
            {
                Assert.AreEqual(3, CellAddress.Parse(item, start, appModel, (matched, curr) =>
                {
                    Assert.AreEqual(item, matched?.ToDisplayTrimed(appModel));
                    if (matched == null)
                    {
                        return start;
                    }

                    return curr;
                }));
            }

            // 大文字・小文字は区別するぜ☆（＾～＾）初期セットの列番号に小文字は無いぜ☆（＾～＾）
            Assert.AreEqual(start, CellAddress.Parse("a1", 0, appModel, (matched, curr) =>
            {
                Assert.IsNull(matched?.ToDisplayTrimed(appModel));
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));

            // 大文字・小文字は区別するぜ☆（＾～＾）
            Assert.AreEqual(3, CellAddress.Parse("T19", 0, appModel, (matched, curr) =>
            {
                Assert.AreNotEqual("t19", matched?.ToDisplayTrimed(appModel));
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));
        }

        /// <summary>
        /// 国際式囲碁の列番号のテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestInternationalColumnAddress()
        {
            var appModel = new ApplicationObjectModelWrapper();

            var start = 0;
            Assert.AreEqual(1, ColumnAddress.Parse("A", start, appModel, (matched, curr) =>
            {
                Assert.AreEqual("A", matched?.ToDisplay(appModel));
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));

            start = 3;
            Assert.AreEqual(4, ColumnAddress.Parse("ABCDEFGHIJKLMNOPQRST", start, appModel, (matched, curr) =>
            {
                Assert.AreEqual("D", matched?.ToDisplay(appModel));
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));

            // I is a null.
            start = 0;
            Assert.AreEqual(start, ColumnAddress.Parse("I", start, appModel, (matched, curr) =>
            {
                Assert.IsNull(matched);
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));
        }

        /// <summary>
        /// 国際式囲碁の行番号をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestRowAddress()
        {
            var appModel = new ApplicationObjectModelWrapper();
            int start;

            // インデックス確認☆（＾～＾）内部的には行番号は　ひっくり返っているぜ☆（＾～＾）"19" が 0行目、 "1" が 18行目だぜ☆（＾～＾）
            start = 0;
            Assert.AreEqual(start + 1, RowAddress.Parse("1", start, appModel, (matched, curr) =>
              {
                  Assert.AreEqual(18, matched?.NumberO0);
                  if (matched == null)
                  {
                      return start;
                  }

                  return curr;
              }));

            // インデックス確認☆（＾～＾）内部的には行番号は　ひっくり返っているぜ☆（＾～＾） "15" は 4行目だぜ☆（＾～＾）
            start = 14;
            Assert.AreEqual(start + 2, RowAddress.Parse("1234567890123415", start, appModel, (matched, curr) =>
              {
                  Assert.AreEqual(4, matched?.NumberO0);
                  if (matched == null)
                  {
                      return start;
                  }

                  return curr;
              }));

            // 表記確認☆（＾～＾）
            start = 0;
            Assert.AreEqual(start + 1, RowAddress.Parse("1", start, appModel, (matched, curr) =>
              {
                  Assert.AreEqual("1", matched?.ToDisplayTrimed(appModel));
                  if (matched == null)
                  {
                      return start;
                  }

                  return curr;
              }));

            start = 14;
            Assert.AreEqual(start + 2, RowAddress.Parse("1234567890123415", start, appModel, (matched, curr) =>
              {
                  Assert.AreEqual("15", matched?.ToDisplayTrimed(appModel));
                  if (matched == null)
                  {
                      return start;
                  }

                  return curr;
              }));
        }

        /// <summary>
        /// 単語完全一致のテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestExactlyKeyword()
        {
            // とりあえずこのテストのスタートは0に揃えておこう☆（＾～＾）
            var start = 0;

            // 単語完全一致のテスト☆（＾～＾）
            Assert.AreEqual(5, StartsWithKeyword.Parse("black", "black", start, (matched, curr) =>
            {
                Assert.AreEqual("black", matched?.ToDisplay());
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));

            Assert.AreEqual(5, StartsWithKeyword.Parse("white", "white", start, (matched, curr) =>
            {
                Assert.AreEqual("white", matched?.ToDisplay());
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));

            Assert.AreEqual(5, StartsWithKeyword.Parse("start", "start", start, (matched, curr) =>
            {
                Assert.AreEqual("start", matched?.ToDisplay());
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));

            // ホワイトスペースのテスト☆（*＾～＾*）
            Assert.AreEqual(5, WhiteSpace.Parse("     ", start, (matched, curr) =>
            {
                Assert.AreEqual("     ", matched?.ToDisplay());
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));

            // 最初にマッチする単語のテスト☆（＾～＾）
            Assert.AreEqual(5, Word.Parse("black a19 k10 t1", 0, (matched, curr) =>
            {
                Assert.AreEqual("black", matched?.ToDisplay());
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));

            Assert.AreEqual(12, WordUpToDelimiter.Parse("!", "Hello, world!", 0, (matched, curr) =>
            {
                Assert.AreEqual("Hello, world", matched?.ToDisplay());
                if (matched == null)
                {
                    return start;
                }

                return curr;
            }));
        }

        /// <summary>
        /// JSONの読込テスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestReadJson()
        {
            var appModel = ApplicationObjectModel.Parse("{\"board\":{\"rowSize\":15,\"columnSize\":13}}");
            Assert.AreEqual(15, appModel.Board.RowSize);
            Assert.AreEqual(13, appModel.Board.ColumnSize);
        }

        /// <summary>
        /// JSONの読込テスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestReadStarsJson()
        {
            var appModel = ApplicationObjectModel.Parse("{\"stringLists\":{\"stars\":{\"value\":[\"A1\",\"B2\",\"C3\"]}}}");
            Assert.AreEqual("A1,B2,C3", appModel.StringLists[StarsController.OutsideName].ToText());
        }

        /// <summary>
        /// セルのインデックスのテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestIndexOfCell()
        {
            var appModel = new ApplicationObjectModelWrapper();

            // インデックスは 左上を 0 とした Z字順。

            // 19路盤
            appModel.Board.RowSize = 19;
            appModel.Board.ColumnSize = 19;
            Assert.AreEqual(0, CellAddress.ToIndex(0, 0, appModel));
            Assert.AreEqual(19 - 1, CellAddress.ToIndex(0, 19 - 1, appModel));
            Assert.AreEqual(19, CellAddress.ToIndex(1, 0, appModel));
            Assert.AreEqual(19 * (19 - 1), CellAddress.ToIndex(19 - 1, 0, appModel));
            Assert.AreEqual(20 * (19 - 1), CellAddress.ToIndex(19 - 1, 19 - 1, appModel));

            // 15路盤
            appModel.Board.RowSize = 15;
            appModel.Board.ColumnSize = 15;
            Assert.AreEqual(0, CellAddress.ToIndex(0, 0, appModel));
            Assert.AreEqual(15 - 1, CellAddress.ToIndex(0, 15 - 1, appModel));
            Assert.AreEqual(15, CellAddress.ToIndex(1, 0, appModel));
            Assert.AreEqual(15 * (15 - 1), CellAddress.ToIndex(15 - 1, 0, appModel));
            Assert.AreEqual(16 * (15 - 1), CellAddress.ToIndex(15 - 1, 15 - 1, appModel));

            // 13路盤
            appModel.Board.RowSize = 13;
            appModel.Board.ColumnSize = 13;
            Assert.AreEqual(0, CellAddress.ToIndex(0, 0, appModel));
            Assert.AreEqual(13 - 1, CellAddress.ToIndex(0, 13 - 1, appModel));
            Assert.AreEqual(13, CellAddress.ToIndex(1, 0, appModel));
            Assert.AreEqual(13 * (13 - 1), CellAddress.ToIndex(13 - 1, 0, appModel));
            Assert.AreEqual(14 * (13 - 1), CellAddress.ToIndex(13 - 1, 13 - 1, appModel));

            // 9路盤
            appModel.Board.RowSize = 9;
            appModel.Board.ColumnSize = 9;
            Assert.AreEqual(0, CellAddress.ToIndex(0, 0, appModel));
            Assert.AreEqual(9 - 1, CellAddress.ToIndex(0, 9 - 1, appModel));
            Assert.AreEqual(9, CellAddress.ToIndex(1, 0, appModel));
            Assert.AreEqual(9 * (9 - 1), CellAddress.ToIndex(9 - 1, 0, appModel));
            Assert.AreEqual(10 * (9 - 1), CellAddress.ToIndex(9 - 1, 9 - 1, appModel));
        }

        /// <summary>
        /// 列番号のテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestColumnNumbers()
        {
            var model = new ApplicationObjectModelWrapper();
            var columnNumbers = model.StringLists[ColumnNumbersController.OutsideName].ToTextList();

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
