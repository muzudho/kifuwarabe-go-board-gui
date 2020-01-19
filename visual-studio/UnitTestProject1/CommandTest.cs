namespace UnitTestProject1
{
    using System;
    using KifuwarabeGoBoardGui.Controller.Parser;
    using KifuwarabeGoBoardGui.Model.Dao;
    using KifuwarabeGoBoardGui.Model.Dto;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CommandTest
    {
        /*
        /// <summary>
        /// 指定ミリ秒待つテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestSleep()
        {
            var appModel = new ApplicationObjectModelWrapper();
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            {
                // WaitFor3Seconds
                var text = @"sleep 3000";
                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line, (inputLineModelController) => { });
                }
            }

            var elapsedMs = stopwatch.ElapsedMilliseconds;
            Assert.IsTrue(3000 <= elapsedMs);
        }
        */

        /// <summary>
        /// 国際式囲碁のセル番地表記をテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestSetRowNumbers()
        {
            var appModel = new ApplicationObjectDtoWrapper();

            {
                var text = @"new row-numbers : string-list
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
";
                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineParserLv2.ParseLine(appModel, line, (inputLineModelController) => { });
                }
            }

            Assert.AreEqual(
                @"""19"",""18"",""17"",""16"",""15"",""14"",""13"",""12"",""11"",""10"",""  9"",""  8"",""  7"",""  6"",""  5"",""  4"",""  3"",""  2"",""  1""",
                appModel.GetStringList(ApplicationDto.RowNumbersRealName).ValueAsText());
        }

        /// <summary>
        /// Putsコマンドのテスト☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestPutBlackToA19()
        {
            var appModel = new ApplicationObjectDtoWrapper();

            {
                var text = @"# 国際囲碁では I列は無いんだぜ☆（＾～＾）
new column-size : number;
set column-size = 19

new column-numbers : string-list
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""

new row-size : number;
set row-size = 19

new row-numbers : string-list
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineParserLv2.ParseLine(appModel, line, (inputLineModelController) => { });
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
