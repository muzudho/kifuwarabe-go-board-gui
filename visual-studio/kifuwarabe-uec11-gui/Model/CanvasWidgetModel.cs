namespace KifuwarabeUec11Gui.Model
{
    public class CanvasWidgetModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Visible { get; set; }

        public CanvasWidgetModel()
        {
            this.Name = string.Empty;
            this.Value = string.Empty;
            this.Visible = true;
        }
    }
}
