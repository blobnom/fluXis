using fluXis.Game.Configuration;
using fluXis.Game.Graphics.Sprites;
using fluXis.Game.Overlay.Settings.UI;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace fluXis.Game.Overlay.Settings.Sections.UserInterface;

public partial class UserInterfaceMainMenuSection : SettingsSubSection
{
    public override string Title => "Main Menu";
    public override IconUsage Icon => FontAwesome6.Solid.House;

    [BackgroundDependencyLoader]
    private void load()
    {
        AddRange(new Drawable[]
        {
            new SettingsToggle
            {
                Label = "fluXis intro music",
                Description = "Play the fluXis intro music on startup. Disabling this will play a random song from your library instead.",
                Bindable = Config.GetBindable<bool>(FluXisSetting.IntroTheme)
            },
            new SettingsToggle
            {
                Label = "Bubble Visualizer",
                Description = "Enable the bubble visualizer on the main menu.",
                Bindable = Config.GetBindable<bool>(FluXisSetting.MainMenuVisualizer)
            },
            new SettingsToggle
            {
                Label = "Bubble Sway",
                Description = "Moves the bubbles in a sine wave pattern.",
                Bindable = Config.GetBindable<bool>(FluXisSetting.MainMenuVisualizerSway)
            },
        });
    }
}
