namespace UnitTestProject1
{
    using System;
    using KifuwarabeGoBoardGui.Controller;
    using KifuwarabeGoBoardGui.Controller.Parser;
    using KifuwarabeGoBoardGui.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CommandTest
    {
        /// <summary>
        /// 国際式囲碁のセル番地表記をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestSetRowNumbers()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"set row-numbers.type = string-list
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
        }

        /// <summary>
        /// Putsコマンドのテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestPutBlackToA19()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"# 国際囲碁では I列は無いんだぜ☆（＾～＾）
set column-size.type = number;
set column-size = 19

set column-numbers.type = string-list
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""

set row-size.type = number;
set row-size = 19

# set row-numbers.type = string-list
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line);
                }
            }

            var start = "put".Length;
            PutsInstructionArgumentParser.Parse(
                "put black to A1",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("black to A1", matched?.ToDisplay(appModel));
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            start = "put".Length;
            PutsInstructionArgumentParser.Parse(
                "put black to A19",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("black to A19", matched?.ToDisplay(appModel));
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            start = "put".Length;
            PutsInstructionArgumentParser.Parse(
                "put white to A1:B4 K11 S16:T19",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("white to A1:B4 K11 S16:T19", matched?.ToDisplay(appModel));
                    return curr;
                },
                () =>
                {
                    Assert.Fail();
                    return start;
                });

            start = 3;
            PutsInstructionArgumentParser.Parse(
                "put space to K11",
                start,
                appModel,
                (matched, curr) =>
                {
                    Assert.AreEqual("space to K11", matched?.ToDisplay(appModel));
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
