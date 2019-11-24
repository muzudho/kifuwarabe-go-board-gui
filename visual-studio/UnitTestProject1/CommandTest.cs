namespace UnitTestProject1
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
        }

        /// <summary>
        /// setコマンドの引数をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestSetsInstructionArgument()
        {
            Assert.AreEqual("b-name.value = Kifuwarabe", SetsInstructionArgumentParser.Parse("set b-name.value = Kifuwarabe", 3).Item1?.ToDisplay());
            Assert.AreEqual("b-name.value = Kifuwarabe", SetsInstructionArgumentParser.Parse("set b-name = Kifuwarabe", 3).Item1?.ToDisplay());
            Assert.AreEqual("b-time.value = 10:00", SetsInstructionArgumentParser.Parse("set  b-time  =  10:00  ", 3).Item1?.ToDisplay());
            Assert.AreEqual("b-hama.value = ", SetsInstructionArgumentParser.Parse("set b-hama =", 3).Item1?.ToDisplay());
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

            Assert.AreEqual("A19 K1 T1", ColorInstructionArgumentParser.Parse("black A19 K1 T1", 5, appModel).Item1?.ToDisplay(appModel));
            Assert.AreEqual("B19 K2 S1", ColorInstructionArgumentParser.Parse("white B19 K2 S1", 5, appModel).Item1?.ToDisplay(appModel));
            Assert.AreEqual("C19 K3 R1", ColorInstructionArgumentParser.Parse("space C19 K3 R1", 5, appModel).Item1?.ToDisplay(appModel));

            // 混合型☆（＾～＾）
            Assert.AreEqual("A2:B1 C4:D3 E5", ColorInstructionArgumentParser.Parse("space A2:B1 C4:D3 E5", 5, appModel).Item1?.ToDisplay(appModel));

            // 大文字・小文字は区別するぜ☆（＾～＾）
            Assert.AreNotEqual("A19 K1 T1", ColorInstructionArgumentParser.Parse("black a19 k1 t1", 5, appModel).Item1?.ToDisplay(appModel));
        }
    }
}
