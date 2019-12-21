namespace UnitTestProject1
{
    using System;
    using System.Collections.Generic;
    using KifuwarabeGoBoardGui.Controller;
    using KifuwarabeGoBoardGui.Controller.Parser;
    using KifuwarabeGoBoardGui.InputScript;
    using KifuwarabeGoBoardGui.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ParserInstruction
    {
        /// <summary>
        /// aliasコマンドの引数をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestAliasInstructionArgumentParser()
        {
            var start = 5;
            AliasInstructionArgumentParser.Parse(
                "alias top2 = ply",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("top2 = ply", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            start = 5;
            AliasInstructionArgumentParser.Parse(
                "alias right3 = b-name black-name player1-name",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("right3 = b-name black-name player1-name", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            AliasInstructionArgumentParser.Parse(
                "alias  right3  =  b-name  black-name  player1-name  ",
                5,
                (matched, curr) =>
                {
                    Assert.AreEqual("right3 = b-name black-name player1-name", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });
        }

        /// <summary>
        /// aliasコマンドの引数をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestBoardInstructionArgumentParser()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"
# 国際囲碁では I列は無いんだぜ☆（＾～＾）
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            var start = 5;
            BoardInstructionArgumentParser.Parse(
                "board 19 ...................",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("19 ...................", matched?.ToDisplay(appModel));
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });
        }

        /// <summary>
        /// 国際式囲碁のセル番地表記をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestCellRangeListArgumentParser()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"
# 国際囲碁では I列は無いんだぜ☆（＾～＾）
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            var start = 0;
            CellRangeListArgumentParser.Parse(
                "A19 K1 T1",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("A19 K1 T1", matched.ToDisplay(appModel));
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            // 混合型☆（＾～＾）
            start = 0;
            CellRangeListArgumentParser.Parse(
                "A2:B1 C4:D3 E5",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("A2:B1 C4:D3 E5", matched.ToDisplay(appModel));
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            // 大文字・小文字は区別するぜ☆（＾～＾）
            start = 0;
            CellRangeListArgumentParser.Parse(
                "a19 k1 t1",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.Fail();
                    return curr;
                },
                () =>
                {
                    // ここを通る。
                    return start;
                });
        }

        [TestMethod]
        public void TestInfoInstructionArgumentParser()
        {
            var appModel = new ApplicationObjectModelWrapper();

            var infoRealName = new RealName("info");
            Assert.IsFalse(appModel.ContainsKeyOfStrings(infoRealName));

            {
                var text = @"set info.type = string
info バナナ食うか☆（＾～＾）？";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            Assert.IsTrue(appModel.ContainsKeyOfStrings(infoRealName));

            Assert.AreEqual(
                "バナナ食うか☆（＾～＾）？",
                appModel.GetString(infoRealName).Value
                );

            // 消してもいいけど困るだけだぜ☆（＾～＾）
            appModel.RemoveProperty(
                infoRealName,
                (value) =>
                {
                    Assert.IsTrue(value is PropertyString);
                    Assert.AreEqual("", value.Title);
                    Assert.AreEqual("バナナ食うか☆（＾～＾）？", value.ValueAsText());
                },
                () =>
                {
                });
        }

        /// <summary>
        /// TODO JSONのテスト大変☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestJsonInstructionArgumentParser()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestPutsInstructionArgumentParser()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"
# 国際囲碁では I列は無いんだぜ☆（＾～＾）
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            var start = "put".Length;
            PutsInstructionArgumentParser.Parse(
                "put black to K10",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("black to K10", matched.ToDisplay(appModel));
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });
        }

        /// <summary>
        /// setコマンドの引数をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestSetsInstructionArgument()
        {
            var start = "set".Length;
            SetsInstructionArgumentParser.Parse(
                "set b-name.value = Kifuwarabe",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("b-name.value = Kifuwarabe", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            start = 3;
            SetsInstructionArgumentParser.Parse(
                "set b-name = Kifuwarabe",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("b-name.value = Kifuwarabe", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            start = 3;
            SetsInstructionArgumentParser.Parse(
                "set  b-time  =  10:00  ",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("b-time.value = 10:00", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            start = 3;
            SetsInstructionArgumentParser.Parse(
                "set b-hama =",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("b-hama.value = ", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });
        }
    }

    [TestClass]
    public class ParserToken
    {
        /// <summary>
        /// 単語完全一致のテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestStartsWithKeywordParser()
        {
            var start = 0;
            StartsWithKeywordParser.Parse(
                "banana",
                "banana1",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("banana", matched.ToDisplay());
                    Assert.AreEqual(6, curr);
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            // ホワイトスペースのテスト☆（*＾～＾*）
            start = 0;
            WhiteSpaceParser.Parse(
                "     ",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("     ", matched.ToDisplay());
                    Assert.AreEqual(5, curr);
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });
        }

        /// <summary>
        /// aliasコマンドの引数をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestWhiteSpaceParser()
        {
            var start = 0;
            WhiteSpaceParser.Parse(
                "",
                start,
                (matched, curr) =>
                {
                    Assert.Fail();
                    return curr;
                },
                () =>
                {
                    // Success.
                    return start;
                });

            WhiteSpaceParser.Parse(
                " ",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual(" ", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            WhiteSpaceParser.Parse(
                "  ",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("  ", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestWordParser()
        {
            var start = 0;
            WordParser.Parse(
                "apple banana chery.",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("apple", matched.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestWordUpToDelimiterParser()
        {
            var start = 0;
            WordUpToDelimiterParser.Parse(
                ":",
                "apple,banana:chery.daikon;",
                start,
                (matched, curr) =>
                {
                    Assert.AreEqual("apple,banana", matched?.ToDisplay());
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });
        }
    }

    [TestClass]
    public class ParserOthers
    {
        [TestMethod]
        public void TestCellAddressParser()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"
# 国際囲碁では I列は無いんだぜ☆（＾～＾）
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            var start = 0;
            CellAddressParser.Parse(
                "K10",
                start,
                appModel,
                (CellAddress matched, int curr) =>
                {
                    Assert.AreEqual(9, matched.ColumnAddress.NumberO0);
                    Assert.AreEqual(9, matched.RowAddress.NumberO0);
                    return curr;
                },
                () =>
                {
                    return start;
                });
        }

        [TestMethod]
        public void TestCellRangeParser()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"
# 国際囲碁では I列は無いんだぜ☆（＾～＾）
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            var start = 0;
            CellRangeParser.Parse(
                "C7:E9",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("C7:E9", matched.ToDisplay(appModel));
                    Assert.AreEqual(5, curr);
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            start = 0;
            CellRangeParser.Parse(
                "E9:C7",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("E9:C7", matched.ToDisplay(appModel));
                    Assert.AreEqual(5, curr);
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            // 短縮表記☆（＾～＾）
            start = 0;
            CellRangeParser.Parse(
                "F5:F5",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("F5", matched.ToDisplay(appModel));
                    Assert.AreEqual(5, curr);
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

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
                start = 0;
                CellRangeParser.Parse(
                    "H7:K9",
                    start,
                    appModel,
                    (matched, curr) =>
                    {
                        Assert.AreEqual("H7:K9", matched?.ToDisplay(appModel));
                        if (matched == null)
                        {
                            return start;
                        }

                        matched.Foreach(appModel, (index) =>
                        {
                            indexes.Add(index);
                        });

                        return curr;
                    },
                    () =>
                    {
                        Assert.Fail();
                        return start;
                    });

                // H7 J7 K7 H8 J8 K8 H9 J9 K9
                Assert.AreEqual("235 236 237 216 217 218 197 198 199", string.Join(' ', indexes));
            }

            {
                var indexes = new List<int>();

                // I列は無いことに注意☆（＾～＾）！
                start = 0;
                CellRangeParser.Parse(
                    "K9:H7",
                    start,
                    appModel,
                    (matched, curr) =>
                    {
                        Assert.AreEqual("K9:H7", matched?.ToDisplay(appModel));
                        if (matched == null)
                        {
                            return start;
                        }

                        matched.Foreach(appModel, (index) =>
                        {
                            indexes.Add(index);
                        });

                        return curr;
                    },
                    () =>
                    {
                        Assert.Fail();
                        return start;
                    });

                // K9 J9 H9 K8 J8 H8 K7 J7 H7
                Assert.AreEqual("199 198 197 218 217 216 237 236 235", string.Join(' ', indexes));
            }

            // 表記確認☆（＾～＾）
            {
                var signs = new List<string>();

                // I列は無いことに注意☆（＾～＾）！
                start = 0;
                CellRangeParser.Parse(
                    "H7:K9",
                    start,
                    appModel,
                    (matched, curr) =>
                    {
                        Assert.AreEqual("H7:K9", matched?.ToDisplay(appModel));
                        if (matched == null)
                        {
                            return start;
                        }

                        matched.Foreach(appModel, (indexO0) =>
                        {
                            signs.Add(CellAddress.FromIndex(indexO0, appModel).ToDisplayTrimed(appModel));
                        });

                        return curr;
                    },
                    () =>
                    {
                        Assert.Fail();
                        return start;
                    });

                Assert.AreEqual("H7 J7 K7 H8 J8 K8 H9 J9 K9", string.Join(' ', signs));
            }

            {
                var signs = new List<string>();

                // I列は無いことに注意☆（＾～＾）！
                start = 0;
                CellRangeParser.Parse(
                    "K9:H7",
                    start,
                    appModel,
                    (matched, curr) =>
                    {
                        Assert.AreEqual("K9:H7", matched?.ToDisplay(appModel));
                        if (matched == null)
                        {
                            return start;
                        }

                        matched.Foreach(appModel, (indexO0) =>
                        {
                            signs.Add(CellAddress.FromIndex(indexO0, appModel).ToDisplayTrimed(appModel));
                        });

                        return curr;
                    },
                    () =>
                    {
                        Assert.Fail();
                        return start;
                    });

                Assert.AreEqual("K9 J9 H9 K8 J8 H8 K7 J7 H7", string.Join(' ', signs));
            }
        }

        [TestMethod]
        public void TestColumnAddressParser()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"
# 国際囲碁では I列は無いんだぜ☆（＾～＾）
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""
";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            Assert.AreEqual(
                @"""A"",""B"",""C"",""D"",""E"",""F"",""G"",""H"",""J"",""K"",""L"",""M"",""N"",""O"",""P"",""Q"",""R"",""S"",""T""",
                appModel.GetStringList(ApplicationObjectModel.ColumnNumbersRealName).ValueAsText());
        }

        /// <summary>
        /// TODO Inputは でかくて大変☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestInputLineParser()
        {
            Assert.Fail();
        }

        /// <summary>
        /// 国際式囲碁の行番号をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestRowAddressParser()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            Assert.AreEqual(
                @"""19"",""18"",""17"",""16"",""15"",""14"",""13"",""12"",""11"",""10"",""  9"",""  8"",""  7"",""  6"",""  5"",""  4"",""  3"",""  2"",""  1""",
                appModel.GetStringList(ApplicationObjectModel.RowNumbersRealName).ValueAsText());

            int start;

            // インデックス確認☆（＾～＾）内部的には行番号は　ひっくり返っているぜ☆（＾～＾）"19" が 0行目、 "1" が 18行目だぜ☆（＾～＾）
            start = 0;
            RowAddressParser.Parse(
                "1",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual(18, matched?.NumberO0);
                    Assert.AreEqual(start + 1, curr);
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            // インデックス確認☆（＾～＾）内部的には行番号は　ひっくり返っているぜ☆（＾～＾） "15" は 4行目だぜ☆（＾～＾）
            start = 14;
            RowAddressParser.Parse(
                "1234567890123415",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual(4, matched?.NumberO0);
                    Assert.AreEqual(start + 2, curr);
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            // 表記確認☆（＾～＾）
            start = 0;
            RowAddressParser.Parse(
                "1",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("1", matched?.ToDisplayTrimed(appModel));
                    Assert.AreEqual(start + 1, curr);
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            start = 14;
            RowAddressParser.Parse(
               "1234567890123415",
               start,
               appModel,
               (matched, curr) =>
               {
                   Assert.AreEqual("15", matched?.ToDisplayTrimed(appModel));
                   Assert.AreEqual(start + 2, curr);
                   return curr;
               },
               () =>
               {
                   Assert.Fail();
                   return start;
               });
        }
    }
}
