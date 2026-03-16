using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Trion.Desktop.Models;

/// <summary>
/// A ranked group of supporters for one tier, pre-loaded with WPF brushes
/// so the view needs no converters.
/// </summary>
public sealed class SupporterTierGroup
{
    public string  Tier      { get; }
    public string  Label     { get; }
    public string  IconKey   { get; }
    public bool    IsRoyalty { get; }   // legend / champion → glow effect
    public Color   GlowColor { get; }   // for DropShadowEffect.Color
    public Brush   TierBrush { get; }   // border / text accent
    public Brush   CardBackground { get; }  // subtle tinted fill for royalty cards
    public ObservableCollection<SupporterEntry> Members { get; } = [];

    public SupporterTierGroup(string tier)
    {
        Tier = tier;
        (Label, IconKey, IsRoyalty, GlowColor, TierBrush, CardBackground) = tier switch
        {
            "legend" => (
                "Legend",
                "Icon.Crown",
                true,
                Color.FromRgb(0xFF, 0xD7, 0x00),
                (Brush)new LinearGradientBrush(
                    new GradientStopCollection
                    {
                        new GradientStop(Color.FromRgb(0xFF, 0xD7, 0x00), 0.0),
                        new GradientStop(Color.FromRgb(0xFF, 0xEC, 0x6A), 0.5),
                        new GradientStop(Color.FromRgb(0xFF, 0xD7, 0x00), 1.0),
                    }, 0),
                (Brush)new SolidColorBrush(Color.FromArgb(0x18, 0xFF, 0xD7, 0x00))),

            "champion" => (
                "Champion",
                "Icon.Gem",
                true,
                Color.FromRgb(0xC0, 0x84, 0xFC),
                new SolidColorBrush(Color.FromRgb(0xC0, 0x84, 0xFC)),
                new SolidColorBrush(Color.FromArgb(0x18, 0xC0, 0x84, 0xFC))),

            "guardian" => (
                "Guardian",
                "Icon.Shield",
                false,
                Color.FromRgb(0x60, 0xA5, 0xFA),
                new SolidColorBrush(Color.FromRgb(0x60, 0xA5, 0xFA)),
                new SolidColorBrush(Color.FromArgb(0x0A, 0x60, 0xA5, 0xFA))),

            _ => (
                "Support",
                "Icon.Star",
                false,
                Color.FromRgb(0x34, 0xD3, 0x99),
                new SolidColorBrush(Color.FromRgb(0x34, 0xD3, 0x99)),
                new SolidColorBrush(Color.FromArgb(0x0A, 0x34, 0xD3, 0x99))),
        };
    }
}
