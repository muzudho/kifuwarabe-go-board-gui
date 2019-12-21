namespace UnitTestProject1
{
    using System;
    using KifuwarabeGoBoardGui.Controller;
    using KifuwarabeGoBoardGui.Controller.Parser;
    using KifuwarabeGoBoardGui.InputScript;
    using KifuwarabeGoBoardGui.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class InputLineParserTest
    {
        /// <summary>
        /// コメントのパース☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestComment()
        {
            var appModel = new ApplicationObjectDtoWrapper();

            InputLineParser.ParseByLine(
                "# This is a comment.",
                appModel,
                (aliasInstruction) => { Assert.Fail(); },
                (boardInstruction) => { Assert.Fail(); },
                (commentLine) =>
                {
                    Assert.AreEqual("# This is a comment.", commentLine);
                },
                (exitsInstruction) => { Assert.Fail(); },
                (infoInstruction) => { Assert.Fail(); },
                (jsonInstruction) => { Assert.Fail(); },
                (putsInstruction) => { Assert.Fail(); },
                (setsInstruction) => { Assert.Fail(); },
                (sleepsInstruction) => { Assert.Fail(); },
                () => { Assert.Fail(); }
                );
        }

        /// <summary>
        /// エイリアスのパース☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestAlias()
        {
            var appModel = new ApplicationObjectDtoWrapper();

            InputLineParser.ParseByLine(
                "alias top2 = ply sasite",
                appModel,
                (aliasInstruction) =>
                {
                    Assert.AreEqual("alias", aliasInstruction.Command);
                    Assert.IsTrue(aliasInstruction.Argument is AliasInstructionArgument);
                    Assert.AreEqual("top2 = ply sasite", ((AliasInstructionArgument)aliasInstruction.Argument).ToDisplay());
                },
                (boardInstruction) => { Assert.Fail(); },
                (commentLine) => { Assert.Fail(); },
                (exitsInstruction) => { Assert.Fail(); },
                (infoInstruction) => { Assert.Fail(); },
                (jsonInstruction) => { Assert.Fail(); },
                (putsInstruction) => { Assert.Fail(); },
                (setsInstruction) => { Assert.Fail(); },
                (sleepsInstruction) => { Assert.Fail(); },
                () => { Assert.Fail(); }
                );
        }

        /// <summary>
        /// ボードのパース☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestBoard()
        {
            var appModel = new ApplicationObjectDtoWrapper();

            {
                var text = @"
# 国際囲碁では I列は無いんだぜ☆（＾～＾）
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line, (inputLineModelController) => { });
                }
            }

            InputLineParser.ParseByLine(
                "board 19 ...................",
                appModel,
                (aliasInstruction) => { Assert.Fail(); },
                (boardInstruction) =>
                {
                    Assert.AreEqual("board", boardInstruction.Command);
                    Assert.IsTrue(boardInstruction.Argument is BoardInstructionArgument);
                    Assert.AreEqual("19 ...................", ((BoardInstructionArgument)boardInstruction.Argument).ToDisplay(appModel));
                },
                (commentLine) => { Assert.Fail(); },
                (exitsInstruction) => { Assert.Fail(); },
                (infoInstruction) => { Assert.Fail(); },
                (jsonInstruction) => { Assert.Fail(); },
                (putsInstruction) => { Assert.Fail(); },
                (setsInstruction) => { Assert.Fail(); },
                (sleepsInstruction) => { Assert.Fail(); },
                () => { Assert.Fail(); }
                );
        }

        /// <summary>
        /// エグジットのパース☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestExit()
        {
            var appModel = new ApplicationObjectDtoWrapper();

            InputLineParser.ParseByLine(
                "exit",
                appModel,
                (aliasInstruction) => { Assert.Fail(); },
                (boardInstruction) => { Assert.Fail(); },
                (commentLine) => { Assert.Fail(); },
                (exitsInstruction) =>
                {
                    Assert.AreEqual("exit", exitsInstruction.Command);
                    // 引数なし
                },
                (infoInstruction) => { Assert.Fail(); },
                (jsonInstruction) => { Assert.Fail(); },
                (putsInstruction) => { Assert.Fail(); },
                (setsInstruction) => { Assert.Fail(); },
                (sleepsInstruction) => { Assert.Fail(); },
                () => { Assert.Fail(); }
                );
        }

        /// <summary>
        /// インフォのパース☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestInfo()
        {
            var appModel = new ApplicationObjectDtoWrapper();

            InputLineParser.ParseByLine(
                "info This is a my banana!",
                appModel,
                (aliasInstruction) => { Assert.Fail(); },
                (boardInstruction) => { Assert.Fail(); },
                (commentLine) => { Assert.Fail(); },
                (exitsInstruction) => { Assert.Fail(); },
                (infoInstruction) =>
                {
                    Assert.AreEqual("info", infoInstruction.Command);
                    Assert.IsTrue(infoInstruction.Argument is InfoInstructionArgument);
                    Assert.AreEqual("This is a my banana!", ((InfoInstructionArgument)infoInstruction.Argument).ToDisplay());
                },
                (jsonInstruction) => { Assert.Fail(); },
                (putsInstruction) => { Assert.Fail(); },
                (setsInstruction) => { Assert.Fail(); },
                (sleepsInstruction) => { Assert.Fail(); },
                () => { Assert.Fail(); }
                );
        }

        /// <summary>
        /// Jsonのパース☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestJson()
        {
            var appModel = new ApplicationObjectDtoWrapper();

            InputLineParser.ParseByLine(
                @"json {""uso"":800}",
                appModel,
                (aliasInstruction) => { Assert.Fail(); },
                (boardInstruction) => { Assert.Fail(); },
                (commentLine) => { Assert.Fail(); },
                (exitsInstruction) => { Assert.Fail(); },
                (infoInstruction) => { Assert.Fail(); },
                (jsonInstruction) =>
                {
                    Assert.AreEqual("json", jsonInstruction.Command);
                    Assert.IsTrue(jsonInstruction.Argument is JsonInstructionArgument);
                    Assert.AreEqual(@"{""uso"":800}", ((JsonInstructionArgument)jsonInstruction.Argument).ToDisplay());
                },
                (putsInstruction) => { Assert.Fail(); },
                (setsInstruction) => { Assert.Fail(); },
                (sleepsInstruction) => { Assert.Fail(); },
                () => { Assert.Fail(); }
                );
        }

        /// <summary>
        /// Putのパース☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestPut()
        {
            var appModel = new ApplicationObjectDtoWrapper();

            {
                var text = @"
# 国際囲碁では I列は無いんだぜ☆（＾～＾）
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line, (inputLineModelController) => { });
                }
            }

            InputLineParser.ParseByLine(
                @"put black to K10",
                appModel,
                (aliasInstruction) => { Assert.Fail(); },
                (boardInstruction) => { Assert.Fail(); },
                (commentLine) => { Assert.Fail(); },
                (exitsInstruction) => { Assert.Fail(); },
                (infoInstruction) => { Assert.Fail(); },
                (jsonInstruction) => { Assert.Fail(); },
                (putsInstruction) =>
                {
                    Assert.AreEqual("put", putsInstruction.Command);
                    Assert.IsTrue(putsInstruction.Argument is PutsInstructionArgument);
                    Assert.AreEqual("black to K10", ((PutsInstructionArgument)putsInstruction.Argument).ToDisplay(appModel));
                },
                (setsInstruction) => { Assert.Fail(); },
                (sleepsInstruction) => { Assert.Fail(); },
                () => { Assert.Fail(); }
                );
        }

        /// <summary>
        /// Setのパース☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestSet()
        {
            var appModel = new ApplicationObjectDtoWrapper();

            {
                var text = @"
# 国際囲碁では I列は無いんだぜ☆（＾～＾）
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
";

                foreach (var line in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line, (inputLineModelController) => { });
                }
            }

            InputLineParser.ParseByLine(
                @"set b-name.visible = true",
                appModel,
                (aliasInstruction) => { Assert.Fail(); },
                (boardInstruction) => { Assert.Fail(); },
                (commentLine) => { Assert.Fail(); },
                (exitsInstruction) => { Assert.Fail(); },
                (infoInstruction) => { Assert.Fail(); },
                (jsonInstruction) => { Assert.Fail(); },
                (putsInstruction) => { Assert.Fail(); },
                (setsInstruction) =>
                {
                    Assert.AreEqual("set", setsInstruction.Command);
                    Assert.IsTrue(setsInstruction.Argument is SetsInstructionArgument);
                    Assert.AreEqual("b-name.visible = true", ((SetsInstructionArgument)setsInstruction.Argument).ToDisplay());
                },
                (sleepsInstruction) => { Assert.Fail(); },
                () => { Assert.Fail(); }
                );
        }

        /// <summary>
        /// 該当なしのパース☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestZzz()
        {
            var appModel = new ApplicationObjectDtoWrapper();

            InputLineParser.ParseByLine(
                @"わはは☆ｍ９（＾▽＾）！",
                appModel,
                (aliasInstruction) => { Assert.Fail(); },
                (boardInstruction) => { Assert.Fail(); },
                (commentLine) => { Assert.Fail(); },
                (exitsInstruction) => { Assert.Fail(); },
                (infoInstruction) => { Assert.Fail(); },
                (jsonInstruction) => { Assert.Fail(); },
                (putsInstruction) => { Assert.Fail(); },
                (setsInstruction) => { Assert.Fail(); },
                (sleepsInstruction) => { Assert.Fail(); },
                () =>
                {
                    // 成功☆（＾～＾）
                }
                );
        }
    }
}
