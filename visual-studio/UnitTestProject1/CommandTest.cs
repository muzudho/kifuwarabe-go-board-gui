﻿namespace UnitTestProject1
{
    using System;
    using KifuwarabeUec11Gui.Controller;
    using KifuwarabeUec11Gui.Controller.Parser;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CommandTest
    {
        /// <summary>
        /// 国際式囲碁のセル番地表記をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestBlackA1()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"
# 国際囲碁では I列は無いんだぜ☆（＾～＾）
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
black K10
";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputController.ParseByLine(
                        appModel,
                        line,
                        (infoText) =>
                        {
                        },
                        (newAppModel) =>
                        {
                        },
                        (args) =>
                        {
                            // Puts.
                        },
                        (args) =>
                        {
                            // Sets.
                        });
                }
            }

            // TODO 番地を指定して石を取るメソッドがない？
        }

        /// <summary>
        /// Putsコマンドのテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestPutsCommand()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"
# 国際囲碁では I列は無いんだぜ☆（＾～＾）
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
put black to A10
";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputController.ParseByLine(
                        appModel,
                        line,
                        (infoText) =>
                        {
                        },
                        (newAppModel) =>
                        {
                        },
                        (args) =>
                        {
                            // Puts.
                            Assert.IsTrue(args is PutsInstructionArgument);
                        },
                        (args) =>
                        {
                            // Sets.
                        });
                }
            }

            PutsInstructionArgumentParser.Parse(
                "put black to A19",
                3,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("black to A19", matched?.ToDisplay(appModel));
                    return curr;
                });
            PutsInstructionArgumentParser.Parse(
                "put white to A1:B4 K11 S16:T19",
                3,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("white to A1:B4 K11 S16:T19", matched?.ToDisplay(appModel));
                    return curr;
                });
            PutsInstructionArgumentParser.Parse(
                "put space to K11",
                3,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("space to K11", matched?.ToDisplay(appModel));
                    return curr;
                });
        }

        /// <summary>
        /// setコマンドの引数をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestSetsInstructionArgument()
        {
            SetsInstructionArgumentParser.Parse(
                "set b-name.value = Kifuwarabe",
                3,
                (matched, curr) =>
                {
                    Assert.AreEqual("b-name.value = Kifuwarabe", matched?.ToDisplay());
                    return curr;
                });
            SetsInstructionArgumentParser.Parse(
                "set b-name = Kifuwarabe",
                3,
                (matched, curr) =>
                {
                    Assert.AreEqual("b-name.value = Kifuwarabe", matched?.ToDisplay());
                    return curr;
                });
            SetsInstructionArgumentParser.Parse(
                "set  b-time  =  10:00  ",
                3,
                (matched, curr) =>
                {
                    Assert.AreEqual("b-time.value = 10:00", matched?.ToDisplay());
                    return curr;
                });
            SetsInstructionArgumentParser.Parse(
                "set b-hama =",
                3,
                (matched, curr) =>
                {
                    Assert.AreEqual("b-hama.value = ", matched?.ToDisplay());
                    return curr;
                });
        }

        /// <summary>
        /// 国際式囲碁のセル番地表記をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestColorInstructionArgumentTest()
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
                    InputController.ParseByLine(
                        appModel,
                        line,
                        (infoText) =>
                        {
                        },
                        (newAppModel) =>
                        {
                        },
                        (args) =>
                        {
                            // Puts.
                        },
                        (args) =>
                        {
                            // Sets.
                        });
                }
            }

            CellRangeListArgumentParser.Parse(
                "black A19 K1 T1",
                5,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("A19 K1 T1", matched?.ToDisplay(appModel));
                    return curr;
                });
            CellRangeListArgumentParser.Parse(
                "white B19 K2 S1",
                5,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("B19 K2 S1", matched?.ToDisplay(appModel));
                    return curr;
                });
            CellRangeListArgumentParser.Parse(
                "space C19 K3 R1",
                5,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("C19 K3 R1", matched?.ToDisplay(appModel));
                    return curr;
                });

            // 混合型☆（＾～＾）
            CellRangeListArgumentParser.Parse(
                "space A2:B1 C4:D3 E5",
                5,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("A2:B1 C4:D3 E5", matched?.ToDisplay(appModel));
                    return curr;
                });

            // 大文字・小文字は区別するぜ☆（＾～＾）
            CellRangeListArgumentParser.Parse(
                "black a19 k1 t1",
                5,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreNotEqual("A19 K1 T1", matched?.ToDisplay(appModel));
                    return curr;
                });
        }
    }
}
