namespace UnitTestProject1
{
    using System;
    using KifuwarabeGoBoardGui.Controller.Parser;
    using KifuwarabeGoBoardGui.Model.Dao;
    using KifuwarabeGoBoardGui.Model.Dto;
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

            new InputLineParser()
                .AppendCallbackOnAliasCommand((aliasInstruction) => { Assert.Fail(); })
                .AppendCallbackOnBoardCommand((boardInstruction) => { Assert.Fail(); })
                .AppendCallbackOnCommentCommand((commentLine) =>
                {
                    Assert.AreEqual("# This is a comment.", commentLine);
                })
                .AppendCallbackOnExitsCommand((exitsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnInfoCommand((infoInstruction) => { Assert.Fail(); })
                .AppendCallbackOnJsonCommand((jsonInstruction) => { Assert.Fail(); })
                .AppendCallbackOnNewsCommand((newsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnPutsCommand((putsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnSetsCommand((setsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnSleepsCommand((sleepsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnNoneCommand(() => { Assert.Fail(); })
                .ParseByLine("# This is a comment.", appModel);
        }

        /// <summary>
        /// エイリアスのパース☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestAlias()
        {
            var appModel = new ApplicationObjectDtoWrapper();

            new InputLineParser()
                .AppendCallbackOnAliasCommand((aliasInstruction) =>
                {
                    Assert.AreEqual("alias", aliasInstruction.Command);
                    Assert.IsTrue(aliasInstruction.Argument is AliasArgumentDto);
                    Assert.AreEqual("top2 = ply sasite", ((AliasArgumentDto)aliasInstruction.Argument).ToDisplay());
                })
                .AppendCallbackOnBoardCommand((boardInstruction) => { Assert.Fail(); })
                .AppendCallbackOnCommentCommand((commentLine) => { Assert.Fail(); })
                .AppendCallbackOnExitsCommand((exitsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnInfoCommand((infoInstruction) => { Assert.Fail(); })
                .AppendCallbackOnJsonCommand((jsonInstruction) => { Assert.Fail(); })
                .AppendCallbackOnNewsCommand((newsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnPutsCommand((putsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnSetsCommand((setsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnSleepsCommand((sleepsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnNoneCommand(() => { Assert.Fail(); })
                .ParseByLine("alias top2 = ply sasite", appModel);
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
                    InputLineDao.ParseLine(appModel, line, (inputLineModelController) => { });
                }
            }

            new InputLineParser()
                .AppendCallbackOnAliasCommand((aliasInstruction) => { Assert.Fail(); })
                .AppendCallbackOnBoardCommand((boardInstruction) =>
                {
                    Assert.AreEqual("board", boardInstruction.Command);
                    Assert.IsTrue(boardInstruction.Argument is BoardArgumentDto);
                    Assert.AreEqual("19 ...................", ((BoardArgumentDto)boardInstruction.Argument).ToDisplay(appModel));
                })
                .AppendCallbackOnCommentCommand((commentLine) => { Assert.Fail(); })
                .AppendCallbackOnExitsCommand((exitsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnInfoCommand((infoInstruction) => { Assert.Fail(); })
                .AppendCallbackOnJsonCommand((jsonInstruction) => { Assert.Fail(); })
                .AppendCallbackOnNewsCommand((newsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnPutsCommand((putsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnSetsCommand((setsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnSleepsCommand((sleepsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnNoneCommand(() => { Assert.Fail(); })
                .ParseByLine("board 19 ...................", appModel);
        }

        /// <summary>
        /// エグジットのパース☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestExit()
        {
            var appModel = new ApplicationObjectDtoWrapper();

            new InputLineParser()
                .AppendCallbackOnAliasCommand((aliasInstruction) => { Assert.Fail(); })
                .AppendCallbackOnBoardCommand((boardInstruction) => { Assert.Fail(); })
                .AppendCallbackOnCommentCommand((commentLine) => { Assert.Fail(); })
                .AppendCallbackOnExitsCommand((exitsInstruction) =>
                {
                    Assert.AreEqual("exit", exitsInstruction.Command);
                    // 引数なし
                })
                .AppendCallbackOnInfoCommand((infoInstruction) => { Assert.Fail(); })
                .AppendCallbackOnJsonCommand((jsonInstruction) => { Assert.Fail(); })
                .AppendCallbackOnNewsCommand((newsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnPutsCommand((putsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnSetsCommand((setsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnSleepsCommand((sleepsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnNoneCommand(() => { Assert.Fail(); })
                .ParseByLine("exit", appModel);
        }

        /// <summary>
        /// インフォのパース☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestInfo()
        {
            var appModel = new ApplicationObjectDtoWrapper();

            new InputLineParser()
                .AppendCallbackOnAliasCommand((aliasInstruction) => { Assert.Fail(); })
                .AppendCallbackOnBoardCommand((boardInstruction) => { Assert.Fail(); })
                .AppendCallbackOnCommentCommand((commentLine) => { Assert.Fail(); })
                .AppendCallbackOnExitsCommand((exitsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnInfoCommand((infoInstruction) =>
                {
                    Assert.AreEqual("info", infoInstruction.Command);
                    Assert.IsTrue(infoInstruction.Argument is InfoInstructionArgumentDto);
                    Assert.AreEqual("This is a my banana!", ((InfoInstructionArgumentDto)infoInstruction.Argument).ToDisplay());
                })
                .AppendCallbackOnJsonCommand((jsonInstruction) => { Assert.Fail(); })
                .AppendCallbackOnNewsCommand((newsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnPutsCommand((putsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnSetsCommand((setsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnSleepsCommand((sleepsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnNoneCommand(() => { Assert.Fail(); })
                .ParseByLine("info This is a my banana!", appModel);
        }

        /// <summary>
        /// Jsonのパース☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestJson()
        {
            var appModel = new ApplicationObjectDtoWrapper();

            new InputLineParser()
                .AppendCallbackOnAliasCommand((aliasInstruction) => { Assert.Fail(); })
                .AppendCallbackOnBoardCommand((boardInstruction) => { Assert.Fail(); })
                .AppendCallbackOnCommentCommand((commentLine) => { Assert.Fail(); })
                .AppendCallbackOnExitsCommand((exitsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnInfoCommand((infoInstruction) => { Assert.Fail(); })
                .AppendCallbackOnJsonCommand((jsonInstruction) =>
                {
                    Assert.AreEqual("json", jsonInstruction.Command);
                    Assert.IsTrue(jsonInstruction.Argument is JsonInstructionArgumentDto);
                    Assert.AreEqual(@"{""uso"":800}", ((JsonInstructionArgumentDto)jsonInstruction.Argument).ToDisplay());
                })
                .AppendCallbackOnNewsCommand((newsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnPutsCommand((putsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnSetsCommand((setsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnSleepsCommand((sleepsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnNoneCommand(() => { Assert.Fail(); })
                .ParseByLine(@"json {""uso"":800}", appModel);
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
                    InputLineDao.ParseLine(appModel, line, (inputLineModelController) => { });
                }
            }

            new InputLineParser()
                .AppendCallbackOnAliasCommand((aliasInstruction) => { Assert.Fail(); })
                .AppendCallbackOnBoardCommand((boardInstruction) => { Assert.Fail(); })
                .AppendCallbackOnCommentCommand((commentLine) => { Assert.Fail(); })
                .AppendCallbackOnExitsCommand((exitsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnInfoCommand((infoInstruction) => { Assert.Fail(); })
                .AppendCallbackOnJsonCommand((jsonInstruction) => { Assert.Fail(); })
                .AppendCallbackOnNewsCommand((newsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnPutsCommand((putsInstruction) =>
                {
                    Assert.AreEqual("put", putsInstruction.Command);
                    Assert.IsTrue(putsInstruction.Argument is PutsInstructionArgumentDto);
                    Assert.AreEqual("black to K10", ((PutsInstructionArgumentDto)putsInstruction.Argument).ToDisplay(appModel));
                })
                .AppendCallbackOnSetsCommand((setsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnSleepsCommand((sleepsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnNoneCommand(() => { Assert.Fail(); })
                .ParseByLine(@"put black to K10", appModel);
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
                    InputLineDao.ParseLine(appModel, line, (inputLineModelController) => { });
                }
            }

            new InputLineParser()
                .AppendCallbackOnAliasCommand((aliasInstruction) => { Assert.Fail(); })
                .AppendCallbackOnBoardCommand((boardInstruction) => { Assert.Fail(); })
                .AppendCallbackOnCommentCommand((commentLine) => { Assert.Fail(); })
                .AppendCallbackOnExitsCommand((exitsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnInfoCommand((infoInstruction) => { Assert.Fail(); })
                .AppendCallbackOnJsonCommand((jsonInstruction) => { Assert.Fail(); })
                .AppendCallbackOnNewsCommand((newsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnPutsCommand((putsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnSetsCommand((setsInstruction) =>
                    {
                        Assert.AreEqual("set", setsInstruction.Command);
                        Assert.IsTrue(setsInstruction.Argument is SetsInstructionArgumentDto);
                        Assert.AreEqual("b-name.visible = true", ((SetsInstructionArgumentDto)setsInstruction.Argument).ToDisplay());
                    })
                .AppendCallbackOnSleepsCommand((sleepsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnNoneCommand(() => { Assert.Fail(); })
                .ParseByLine(@"set b-name.visible = true", appModel);
        }

        /// <summary>
        /// 該当なしのパース☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void TestZzz()
        {
            var appModel = new ApplicationObjectDtoWrapper();

            new InputLineParser()
                .AppendCallbackOnAliasCommand((aliasInstruction) => { Assert.Fail(); })
                .AppendCallbackOnBoardCommand((boardInstruction) => { Assert.Fail(); })
                .AppendCallbackOnCommentCommand((commentLine) => { Assert.Fail(); })
                .AppendCallbackOnExitsCommand((exitsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnInfoCommand((infoInstruction) => { Assert.Fail(); })
                .AppendCallbackOnJsonCommand((jsonInstruction) => { Assert.Fail(); })
                .AppendCallbackOnNewsCommand((newsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnPutsCommand((putsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnSetsCommand((setsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnSleepsCommand((sleepsInstruction) => { Assert.Fail(); })
                .AppendCallbackOnNoneCommand(
                    () =>
                    {
                        // 成功☆（＾～＾）
                    })
                .ParseByLine(@"わはは☆ｍ９（＾▽＾）！", appModel);
        }
    }
}
