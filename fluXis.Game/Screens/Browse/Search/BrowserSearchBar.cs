using fluXis.Game.Graphics;
using fluXis.Game.Graphics.Sprites;
using fluXis.Game.Graphics.UserInterface.Color;
using fluXis.Game.Graphics.UserInterface.Text;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace fluXis.Game.Screens.Browse.Search;

public partial class BrowserSearchBar : Container
{
    public MapBrowser MapBrowser { get; init; }

    private FluXisTextBox textBox;

    [BackgroundDependencyLoader]
    private void load()
    {
        RelativeSizeAxes = Axes.X;
        Height = 60;
        CornerRadius = 10;
        Masking = true;
        EdgeEffect = FluXisStyles.ShadowSmall;

        InternalChildren = new Drawable[]
        {
            new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = FluXisColors.Background2
            },
            new Container
            {
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding { Horizontal = 60 },
                Child = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    CornerRadius = 10,
                    Masking = true,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = FluXisColors.Background3
                        },
                        new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            Padding = new MarginPadding(10),
                            Child = textBox = new FluXisTextBox
                            {
                                RelativeSizeAxes = Axes.Both,
                                PlaceholderText = "Search...",
                                BackgroundInactive = FluXisColors.Background3,
                                BackgroundActive = FluXisColors.Background3
                            }
                        }
                    }
                }
            },
            new SpriteIcon
            {
                Size = new Vector2(25),
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.Centre,
                X = 30,
                Icon = FontAwesome6.Solid.MagnifyingGlass
            },
            new ClickableSpriteIcon
            {
                Size = new Vector2(25),
                Anchor = Anchor.CentreRight,
                Origin = Anchor.Centre,
                X = -30,
                Icon = FontAwesome6.Solid.AngleDoubleRight,
                Action = search
            }
        };
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();
        textBox.OnCommit += (_, _) => search();
    }

    private void search()
    {
        MapBrowser.Search(textBox.Text);
    }
}
