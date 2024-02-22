using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Graphic : ContentPage
    {
        public Graphic()
        {
            InitializeComponent();
            SKCanvasView canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = canvasView;
        }
        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            var info = args.Info;
            var surface = args.Surface;
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.Black);
            int scaleFactor = 2;
            var bodyPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.YellowGreen,
            };
            canvas.DrawCircle(info.Width / 2, info.Height / 2, 100 * scaleFactor, bodyPaint);
            var headPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.ForestGreen,
            };
            canvas.DrawCircle(info.Width / 2, (info.Height / 2) - 110 * scaleFactor, 50 * scaleFactor, headPaint);
            var maskPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Purple,
            };
            canvas.DrawRect(new SKRect((info.Width / 2) - 50 * scaleFactor, (info.Height / 2) - 130 * scaleFactor, (info.Width / 2) + 50 * scaleFactor, (info.Height / 2) - 90 * scaleFactor), maskPaint);
            var eyePaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.White,
            };
            canvas.DrawCircle((info.Width / 2) - 20 * scaleFactor, (info.Height / 2) - 110 * scaleFactor, 10 * scaleFactor, eyePaint);
            canvas.DrawCircle((info.Width / 2) + 20 * scaleFactor, (info.Height / 2) - 110 * scaleFactor, 10 * scaleFactor, eyePaint);
            var weaponPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.SaddleBrown,
                StrokeWidth = 10 * scaleFactor
            };
            canvas.DrawLine((info.Width / 2) - 70 * scaleFactor, (info.Height / 2) - 50 * scaleFactor, (info.Width / 2) - 70 * scaleFactor, (info.Height / 2) + 50 * scaleFactor, weaponPaint);
            var limbPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.ForestGreen,
                StrokeWidth = 20 * scaleFactor
            };
            canvas.DrawLine((info.Width / 2) - 100 * scaleFactor, (info.Height / 2), (info.Width / 2) - 150 * scaleFactor, (info.Height / 2) + 50 * scaleFactor, limbPaint);
            canvas.DrawLine((info.Width / 2) + 100 * scaleFactor, (info.Height / 2), (info.Width / 2) + 150 * scaleFactor, (info.Height / 2) + 50 * scaleFactor, limbPaint);
            canvas.DrawLine((info.Width / 2) - 50 * scaleFactor, (info.Height / 2) + 100 * scaleFactor, (info.Width / 2) - 50 * scaleFactor, (info.Height / 2) + 150 * scaleFactor, limbPaint);
            canvas.DrawLine((info.Width / 2) + 50 * scaleFactor, (info.Height / 2) + 100 * scaleFactor, (info.Width / 2) + 50 * scaleFactor, (info.Height / 2) + 150 * scaleFactor, limbPaint);
        }
    }
}