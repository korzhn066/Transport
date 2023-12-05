namespace Transport.Models
{
    internal class RectangleProps
    {
        public RectangleProps(
            string color, 
            double x, 
            double y, 
            double width, 
            double height)
        {
            Color = color;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public RectangleProps(
            string color,
            double x,
            double y,
            double width,
            double height,
            string text)
        {
            Color = color;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Text = text;
        }

        public string Color { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
