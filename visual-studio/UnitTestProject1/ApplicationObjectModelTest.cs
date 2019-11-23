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
        public void PlyTestByCommand()
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
                    (textOfMove) =>
                    {
                    },
                    (args) =>
                    {
                    });
            }

            Assert.IsTrue(appModel.ContainsKeyOfNumbers(plyRealName));

            var (type, value) = appModel.GetProperty(plyRealName);
            Assert.AreEqual(PropertyType.Number, type);
            Assert.AreEqual("手目", value.Title);
            Assert.AreEqual("0", value.ValueAsText());
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
    }
}
