namespace UnitTestProject1
{
    using System;
    using KifuwarabeUec11Gui.Controller;
    using KifuwarabeUec11Gui.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class InputLineModelTest
    {
        [TestMethod]
        public void TestAliasModel()
        {
            var appModel = new ApplicationObjectModelWrapper();

            var line = @"alias top2 = ply sasite";

            InputLineModelController.ParseLine(appModel, line).ThenAlias(
                (aliasInstruction) =>
                {
                    Assert.IsTrue(appModel.ContainsKeyOfObjectRealName(new AliasName("ply")));
                    Assert.IsTrue(appModel.ContainsKeyOfObjectRealName(new AliasName("sasite")));
                    Assert.IsFalse(appModel.ContainsKeyOfObjectRealName(new AliasName("dog-food")));
                },
                () => { Assert.Fail(); }
                ).ThenComment(
                    (commentLine) => { Assert.Fail(); },
                    () => { }
                ).ThenInfo(
                    (infoText) => { Assert.Fail(); },
                    () => { }
                ).ThenJson(
                    (jsonAppModel) => { Assert.Fail(); },
                    () => { }
                ).ThenPut(
                    (putsArgs) => { Assert.Fail(); },
                    () => { }
                ).ThenSet(
                    (setsArgs) => { Assert.Fail(); },
                    () => { }
                );
        }

        [TestMethod]
        public void TestCommentModel()
        {
            var appModel = new ApplicationObjectModelWrapper();

            var line = @"# This is a comment line.";

            InputLineModelController.ParseLine(appModel, line).ThenAlias(
                    (aliasInstruction) => { Assert.Fail(); },
                    () => { }
                ).ThenComment(
                    (commentLine) =>
                    {
                        Assert.AreEqual(line, commentLine);
                    },
                    () => { Assert.Fail(); }
                ).ThenInfo(
                    (infoText) => { Assert.Fail(); },
                    () => { }
                ).ThenJson(
                    (jsonAppModel) => { Assert.Fail(); },
                    () => { }
                ).ThenPut(
                    (putsArgs) => { Assert.Fail(); },
                    () => { }
                ).ThenSet(
                    (setsArgs) => { Assert.Fail(); },
                    () => { }
                );
        }

        [TestMethod]
        public void TestInfoModel()
        {
            var appModel = new ApplicationObjectModelWrapper();

            var line = "info This is a information.";

            InputLineModelController.ParseLine(appModel, line).ThenAlias(
                    (aliasInstruction) => { Assert.Fail(); },
                    () => { }
                ).ThenComment(
                    (commentLine) => { Assert.Fail(); },
                    () => { }
                ).ThenInfo(
                    (infoText) =>
                    {
                        Assert.AreEqual("This is a information.", infoText);
                    },
                    () => { Assert.Fail(); }
                ).ThenJson(
                    (jsonAppModel) => { Assert.Fail(); },
                    () => { }
                ).ThenPut(
                    (putsArgs) => { Assert.Fail(); },
                    () => { }
                ).ThenSet(
                    (setsArgs) => { Assert.Fail(); },
                    () => { }
                );
        }

        [TestMethod]
        public void TestJsonModel()
        {
            var appModel = new ApplicationObjectModelWrapper();

            var line = @"json {""uso"":800}";

            InputLineModelController.ParseLine(appModel, line).ThenAlias(
                    (aliasInstruction) => { Assert.Fail(); },
                    () => { }
                ).ThenComment(
                    (commentLine) => { Assert.Fail(); },
                    () => { }
                ).ThenInfo(
                    (infoText) => { Assert.Fail(); },
                    () => { }
                ).ThenJson(
                    (jsonAppModel) =>
                    {
                        // TODO テストしにくいぜ☆（＾～＾）ダブルクォーテーションいっぱいあるし☆（＾～＾）
                    },
                    () => { Assert.Fail(); }
                ).ThenPut(
                    (putsArgs) => { Assert.Fail(); },
                    () => { }
                ).ThenSet(
                    (setsArgs) => { Assert.Fail(); },
                    () => { }
                );
        }

        [TestMethod]
        public void TestPutsModel()
        {
            var appModel = new ApplicationObjectModelWrapper();

            var text = @"
# 国際囲碁では I列は無いんだぜ☆（＾～＾）
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
";

            foreach (var line1 in text.Split(Environment.NewLine))
            {
                InputLineModelController.ParseLine(appModel, line1);
            }

            var line = @"put black to K10";

            InputLineModelController.ParseLine(appModel, line).ThenAlias(
                    (aliasInstruction) => { Assert.Fail(); },
                    () => { }
                ).ThenComment(
                    (commentLine) => { Assert.Fail(); },
                    () => { }
                ).ThenInfo(
                    (infoText) => { Assert.Fail(); },
                    () => { }
                ).ThenJson(
                    (jsonAppModel) => { Assert.Fail(); },
                    () => { }
                ).ThenPut(
                    (putsArgs) =>
                    {
                        Assert.AreEqual("K10", putsArgs.Destination.ToDisplay(appModel));
                    },
                    () => { Assert.Fail(); }
                ).ThenSet(
                    (setsArgs) => { Assert.Fail(); },
                    () => { }
                );
        }

        [TestMethod]
        public void TestSetsModel()
        {
            var appModel = new ApplicationObjectModelWrapper();

            {
                var text = @"
# 国際囲碁では I列は無いんだぜ☆（＾～＾）
set column-numbers = ""A"", ""B"", ""C"", ""D"", ""E"", ""F"", ""G"", ""H"", ""J"", ""K"", ""L"", ""M"", ""N"", ""O"", ""P"", ""Q"", ""R"", ""S"", ""T""
set row-numbers = ""19"", ""18"", ""17"", ""16"", ""15"", ""14"", ""13"", ""12"", ""11"", ""10"", ""  9"", ""  8"", ""  7"", ""  6"", ""  5"", ""  4"", ""  3"", ""  2"", ""  1""
";

                foreach (var line1 in text.Split(Environment.NewLine))
                {
                    InputLineModelController.ParseLine(appModel, line1);
                }
            }

            var line = @"set top2.title = バナナ";

            InputLineModelController.ParseLine(appModel, line).ThenAlias(
                    (aliasInstruction) => { Assert.Fail(); },
                    () => { }
                ).ThenComment(
                    (commentLine) => { Assert.Fail(); },
                    () => { }
                ).ThenInfo(
                    (infoText) => { Assert.Fail(); },
                    () => { }
                ).ThenJson(
                    (jsonAppModel) => { Assert.Fail(); },
                    () => { }
                ).ThenPut(
                    (putsArgs) =>
                    {
                        Assert.Fail();
                    },
                    () => { }
                ).ThenSet(
                    (setsArgs) => {
                        Assert.AreEqual("バナナ", setsArgs.Value);
                    },
                    () => { Assert.Fail(); }
                );
        }
    }
}
