namespace UnitTestProject1
{
    using KifuwarabeUec11Gui;
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
            // var appView = new MainWindow();
            var text = @"
alias top2 = ply
set top2.type = number
set top2.title = 手目
set top2.value = 2
";

            InputController.ParseByLine(
                appModel,
                text,
                (infoText) =>
                {

                },
                (newAppModel)=>
                {
                },
                (textOfMove) =>
                {
                    // this.top1Value.Content = textOfMove;
                },
                (args)=>
                {
                });
            Assert.IsTrue(appModel.Numbers.ContainsKey("ply"));

            var (type, value) = appModel.GetProperty("ply");
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
            Assert.IsFalse(appModel.Numbers.ContainsKey("ply"));

            appModel.AddProperty("ply", new PropertyNumber("手目"));
            Assert.IsTrue(appModel.Numbers.ContainsKey("ply"));

            var (type, value) = appModel.RemoveProperty("ply");
            Assert.AreEqual(PropertyType.Number, type);
            Assert.AreEqual("手目", value.Title);
            Assert.AreEqual("0", value.ValueAsText());
        }
    }
}
