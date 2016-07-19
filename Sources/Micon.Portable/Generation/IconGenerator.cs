namespace Micon.Portable.Generation
{
    using Graphics;
    using NGraphics;
    using System;
    public class IconGenerator : IIconGenerator
    {

        public IconGenerator(IPlatform platform)
		{
            this.platform = platform;
        }

        private readonly IPlatform platform;

        public IImageCanvas GenerateIcon(IImage hdImage, Icon icon)
		{
			var result = this.platform.CreateImageCanvas(new Size(icon.Width,icon.Height));

            if (icon.BackgroundColor != null)
            {
                var backgroundBrush = new LinearGradientBrush(Point.Zero, Point.OneY, icon.BackgroundColor, icon.BackgroundEndColor);
                var strokeBrush = icon.BackgroundEndColor.WithBrightness(0.7);
                var w = result.Size.Width - 1;
                var h = result.Size.Height - 1;
                var stroke = Math.Max(1, Math.Min(w, h) * 0.02);
                
                switch (icon.BackgroundShape)
                {
                    case Shape.RoundedRectangle:
                        var corner = Math.Min(w,h) * 0.15;

                        var frame = new Rect(stroke/2, stroke/2, w - stroke * 1, h - stroke * 1);

                        var ops = new PathOp[] {
                            new MoveTo (frame.X + corner,frame.Y),
                            new LineTo (frame.X + frame.Width - corner, frame.Y + 0),
                            new CurveTo(new Point(frame.X + frame.Width,frame.Y + 0),new Point(frame.X + frame.Width,frame.Y + corner), new Point(frame.X + frame.Width,frame.Y + corner)),
                            new LineTo (frame.X + frame.Width, frame.Y + frame.Height  - corner),
                            new CurveTo(new Point(frame.X + frame.Width,frame.Y + frame.Height),new Point(frame.X + frame.Width-corner,frame.Y + frame.Height), new Point(frame.X + frame.Width-corner,frame.Y + frame.Height)),
                            new LineTo (frame.X + corner, frame.Y + frame.Height),
                            new CurveTo(new Point(frame.X + 0,frame.Y + frame.Height),new Point(frame.X + 0,frame.Y + frame.Height-corner), new Point(frame.X + 0,frame.Y + frame.Height-corner)),
                            new LineTo (frame.X + 0, corner),
                            new CurveTo(new Point(frame.X + 0,frame.Y + 0),new Point(frame.X + corner,frame.Y + 0), new Point(frame.X + corner,frame.Y + 0)),
                            new ClosePath ()
                        };

                        result.FillPath(ops, backgroundBrush);
                        if(icon.HasBorder) result.DrawPath(ops, new Pen(strokeBrush, stroke));
                        
                        break;
                    case Shape.Circle:
                        result.DrawEllipse (Point.Zero, result.Size, null, backgroundBrush);
                        if (icon.HasBorder) result.StrokeEllipse(new Rect(stroke / 2, stroke / 2, w - stroke,h - stroke), strokeBrush,stroke);
                        break;
                    default:
                        result.DrawRectangle(-1,-1, result.Size.Width + 2, result.Size.Height + 2, null, backgroundBrush);
                        if (icon.HasBorder) result.StrokeRectangle(new Rect(stroke / 2, stroke / 2, w - stroke, h - stroke), strokeBrush, stroke);
                        break;
                }
            }


                if (hdImage != null)
            {
                var a = icon.ImageArea;

                var cx = a.X + a.Width / 2;
                var cy = a.Y + a.Height / 2;

                var area = new Rect(a.X, a.Y, a.Width, a.Height);
                area.Width = (int)(area.Width * icon.Scale);
                area.Height = (int)(area.Height * icon.Scale);
                area.X = cx - (area.Width / 2);
                area.Y = cy - (area.Height / 2);

                result.DrawImage(hdImage, area);
            }

			return result;
		}
	}
}