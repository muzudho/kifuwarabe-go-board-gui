namespace KifuwarabeGoBoardGui.Model.Dto
{
    /// <summary>
    /// 次のようなコマンド☆（＾～＾）
    /// 
    /// `new b-name : string`
    /// 
    /// 構造としては
    /// 
    /// `new {instance-name} : {type-name}`
    /// 
    /// だぜ☆（＾～＾）
    /// </summary>
    public class NewsInstructionArgumentDto
    {
        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public string InstanceName { get; private set; }

        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public string TypeName { get; private set; }

        public NewsInstructionArgumentDto(string instanceName, string typeName)
        {
            this.InstanceName = instanceName;
            this.TypeName = typeName;
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            return $"{this.InstanceName} = {this.TypeName}";
        }
    }
}
