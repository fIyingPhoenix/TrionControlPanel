using System.Windows;
using System.Windows.Media;

namespace Trion.Desktop.Controls;

/// <summary>
/// Lightweight filled-area sparkline chart rendered via WPF DrawingContext.
/// Assign a new <c>double[]</c> to <see cref="Values"/> on each data update —
/// the control invalidates automatically via the DependencyProperty framework.
/// </summary>
public sealed class SparklineChart : FrameworkElement
{
    // ── Dependency Properties ─────────────────────────────────────────────────

    public static readonly DependencyProperty ValuesProperty =
        DependencyProperty.Register(nameof(Values), typeof(double[]),
            typeof(SparklineChart),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty StrokeProperty =
        DependencyProperty.Register(nameof(Stroke), typeof(Brush),
            typeof(SparklineChart),
            new FrameworkPropertyMetadata(Brushes.CornflowerBlue,
                FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty FillProperty =
        DependencyProperty.Register(nameof(Fill), typeof(Brush),
            typeof(SparklineChart),
            new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Fixed Y-axis maximum. If ≤ 0 the chart auto-scales to the window's peak value.
    /// </summary>
    public static readonly DependencyProperty MaxValueProperty =
        DependencyProperty.Register(nameof(MaxValue), typeof(double),
            typeof(SparklineChart),
            new FrameworkPropertyMetadata(0.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

    // ── CLR wrappers ──────────────────────────────────────────────────────────

    public double[]? Values
    {
        get => (double[]?)GetValue(ValuesProperty);
        set => SetValue(ValuesProperty, value);
    }

    public Brush Stroke
    {
        get => (Brush)GetValue(StrokeProperty);
        set => SetValue(StrokeProperty, value);
    }

    public Brush Fill
    {
        get => (Brush)GetValue(FillProperty);
        set => SetValue(FillProperty, value);
    }

    public double MaxValue
    {
        get => (double)GetValue(MaxValueProperty);
        set => SetValue(MaxValueProperty, value);
    }

    // ── Rendering ─────────────────────────────────────────────────────────────

    protected override void OnRender(DrawingContext dc)
    {
        double w = ActualWidth;
        double h = ActualHeight;
        if (w <= 1 || h <= 1) return;

        var values = Values;

        // No data yet — just draw a subtle baseline
        if (values == null || values.Length < 2)
        {
            var baselinePen = new Pen(Stroke, 1.0);
            baselinePen.DashStyle = DashStyles.Dash;
            baselinePen.Freeze();
            dc.DrawLine(baselinePen, new Point(0, h - 0.5), new Point(w, h - 0.5));
            return;
        }

        // ── Y-axis scale ──────────────────────────────────────────────────────

        double max = MaxValue > 0 ? MaxValue : 0;
        if (max <= 0)
            foreach (var v in values) if (v > max) max = v;
        if (max <= 0) max = 1.0;

        int    count  = values.Length;
        double xStep  = (w - 1.0) / Math.Max(count - 1, 1);
        double yScale = h - 2.0;   // leave 1 px top/bottom breathing room

        // ── Helper: Y coordinate for a value ─────────────────────────────────

        double Y(double v) => h - 1.0 - Math.Clamp(v / max, 0.0, 1.0) * yScale;

        // ── Optional horizontal grid lines (25 %, 50 %, 75 %) ─────────────────

        if (Fill is SolidColorBrush || Fill != Brushes.Transparent)
        {
            var gridPen = new Pen(Stroke, 0.4) { DashStyle = DashStyles.Dot };
            gridPen.Freeze();
            foreach (double pct in new[] { 0.25, 0.50, 0.75 })
            {
                double yGrid = h - 1.0 - pct * yScale;
                dc.DrawLine(gridPen, new Point(0, yGrid), new Point(w, yGrid));
            }
        }

        // ── Filled area geometry ──────────────────────────────────────────────

        var fillGeo = new StreamGeometry();
        using (var ctx = fillGeo.Open())
        {
            ctx.BeginFigure(new Point(0, h), isFilled: true, isClosed: true);
            ctx.LineTo(new Point(0, Y(values[0])), isStroked: false, isSmoothJoin: false);

            for (int i = 1; i < count; i++)
                ctx.LineTo(new Point(i * xStep, Y(values[i])), isStroked: false, isSmoothJoin: false);

            ctx.LineTo(new Point((count - 1) * xStep, h), isStroked: false, isSmoothJoin: false);
        }
        fillGeo.Freeze();
        dc.DrawGeometry(Fill, null, fillGeo);

        // ── Stroke line ───────────────────────────────────────────────────────

        var lineGeo = new StreamGeometry();
        using (var ctx = lineGeo.Open())
        {
            ctx.BeginFigure(new Point(0, Y(values[0])), isFilled: false, isClosed: false);
            for (int i = 1; i < count; i++)
                ctx.LineTo(new Point(i * xStep, Y(values[i])), isStroked: true, isSmoothJoin: false);
        }
        lineGeo.Freeze();

        var pen = new Pen(Stroke, 1.5) { LineJoin = PenLineJoin.Round };
        pen.Freeze();
        dc.DrawGeometry(null, pen, lineGeo);

        // ── Current-value dot at the rightmost point ──────────────────────────

        double dotX = (count - 1) * xStep;
        double dotY = Y(values[count - 1]);
        dc.DrawEllipse(Stroke, null, new Point(dotX, dotY), 2.5, 2.5);
    }
}
