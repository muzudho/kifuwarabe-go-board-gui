namespace UnitTestProject1
{
    using System;
    using KifuwarabeUec11Gui.Controller;
    using KifuwarabeUec11Gui.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ApplicationObjectModelTest
    {
        /// <summary>
        /// 数値型プロパティの ply の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void PlyTestByCommand2()
        {
            var appModel = new ApplicationObjectModelWrapper(new ApplicationObjectModel());
            var plyRealName = new RealName("top2");

            Assert.IsFalse(appModel.ContainsKeyOfNumbers(plyRealName));

            var text = @"
alias top2 = ply
set top2.type = number
set top2.title = 手目
set top2.value = 2
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
                    });
            }

            Assert.IsTrue(appModel.ContainsKeyOfNumbers(plyRealName));

            appModel.MatchPropertyOption(
                plyRealName,
                (type, value) =>
                {
                    Assert.AreEqual(PropertyType.Number, type);
                    Assert.AreEqual("手目", value.Title);
                    Assert.AreEqual("0", value.ValueAsText());
                },
                () =>
                {
                });
        }

        /// <summary>
        /// 数値型プロパティの ply の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void PlyTestByCommand1()
        {
            var appModel = new ApplicationObjectModelWrapper(new ApplicationObjectModel());
            var plyRealName = new RealName("top2");

            Assert.IsFalse(appModel.ContainsKeyOfNumbers(plyRealName));

            var line = "alias top2 = ply";

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
                });

            Assert.IsTrue(appModel.ContainsKeyOfNumbers(plyRealName));

            appModel.MatchPropertyOption(
                plyRealName,
                (type, value) =>
                {
                    Assert.AreEqual(PropertyType.Number, type);
                    Assert.AreEqual("", value.Title);
                    Assert.AreEqual("0", value.ValueAsText());
                },
                () =>
                {
                });
        }

        /// <summary>
        /// 数値型プロパティの ply の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void PlyTest()
        {
            var appModel = new ApplicationObjectModelWrapper(new ApplicationObjectModel());
            var plyRealName = new RealName("top2");
            Assert.IsFalse(appModel.ContainsKeyOfNumbers(plyRealName));

            appModel.AddProperty(plyRealName, new PropertyNumber("手目"));
            Assert.IsTrue(appModel.ContainsKeyOfNumbers(plyRealName));

            var (type, value) = appModel.RemoveProperty(plyRealName);
            Assert.AreEqual(PropertyType.Number, type);
            Assert.AreEqual("手目", value.Title);
            Assert.AreEqual("0", value.ValueAsText());
        }

        /// <summary>
        /// 文字列型プロパティの info の追加☆（＾～＾）
        /// </summary>
        [TestMethod]
        public void InfoTest()
        {
            var appModel = new ApplicationObjectModelWrapper(new ApplicationObjectModel());
            var infoRealName = new RealName("info");
            Assert.IsFalse(appModel.ContainsKeyOfStrings(infoRealName));

            appModel.AddProperty(infoRealName, new PropertyString("#info", "Hello, world!"));
            Assert.IsTrue(appModel.ContainsKeyOfStrings(infoRealName));

            var (type, value) = appModel.RemoveProperty(infoRealName);
            Assert.AreEqual(PropertyType.StringType, type);
            Assert.AreEqual("#info", value.Title);
            Assert.AreEqual("Hello, world!", value.ValueAsText());
        }
    }
}
