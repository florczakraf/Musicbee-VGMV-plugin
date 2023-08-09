namespace System.Windows.Forms {
    internal class BufferedPanel: Panel {
        public BufferedPanel() {
            this.DoubleBuffered = true;         //to avoid flickering of the panel
        }
    }
}