#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace JPMC.MSDK.Configurator
{
    public class Mask
    {
        public string name;
        public TextJustification justification = TextJustification.None;
        public int expose;
        public Mask()
        {
        }
        public Mask( string name, TextJustification just, int expose )
        {
            this.name = name;
            this.justification = just;
            this.expose = expose;
        }
    }

}
