namespace KifuwarabeUec11Gui.Model
{
    public interface IPropertyValue
    {
        public bool Visible { get; set; }

        public void SetValue(object value);
    }
}
