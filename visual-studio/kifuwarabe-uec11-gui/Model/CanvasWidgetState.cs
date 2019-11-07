namespace KifuwarabeUec11Gui.Model
{
    public class CanvasWidgetState
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Visible { get; set; }

        public CanvasWidgetState()
        {
            this.Name = string.Empty;
            this.Value = string.Empty;
        }
    }
}
