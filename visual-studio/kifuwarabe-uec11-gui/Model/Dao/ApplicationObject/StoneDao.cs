namespace KifuwarabeGoBoardGui.Model.Dao
{
    using System;
    using KifuwarabeGoBoardGui.Model.Dto;

    /// <summary>
    /// 石を操作するぜ☆（＾～＾）
    /// 
    /// こっちを設定して、あっちを設定して、また　こっちに戻ってきて設定して、というような
    /// 無限ループしないようにセットするのもコントローラーのメリットだぜ☆（＾～＾）
    /// </summary>
    public static class StoneDao
    {
        /// <summary>
        /// 黒石に変えようぜ☆（＾～＾）
        /// </summary>
        public static void ChangeModel(ApplicationObjectDtoWrapper appModel, ColorDto newValue, int zShapedIndex)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            var oldValue = appModel.Board.GetColor(zShapedIndex);
            // var newValue = ColorDto.Black; // ColorDto.White; // ColorDto.Transparent
            // TODO: ログが大量になってしまう☆（＾～＾）
            // appModel.ModelChangeLogWriter.WriteLine($"Stones[{zShapedIndex}]", oldValue.ToString(), newValue.ToString());
            appModel.Board.SetColor(zShapedIndex, newValue);
        }
    }
}
