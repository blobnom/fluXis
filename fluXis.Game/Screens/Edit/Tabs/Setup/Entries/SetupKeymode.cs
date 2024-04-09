using System.Linq;
using fluXis.Game.Graphics.Drawables;
using fluXis.Game.Graphics.Sprites;
using fluXis.Game.Graphics.UserInterface.Color;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osuTK;

namespace fluXis.Game.Screens.Edit.Tabs.Setup.Entries;

public partial class SetupKeymode : CompositeDrawable
{
    private int[] counts { get; } = { 4, 5, 6, 7, 8 };

    [BackgroundDependencyLoader]
    private void load()
    {
        RelativeSizeAxes = Axes.X;
        Height = 60;
        CornerRadius = 10;
        Masking = true;

        InternalChildren = new Drawable[]
        {
            new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = FluXisColors.Background3
            },
            new FillFlowContainer
            {
                RelativeSizeAxes = Axes.Both,
                Direction = FillDirection.Horizontal,
                ChildrenEnumerable = counts.Select(count => new Entry(count) { Width = 1f / counts.Length })
            }
        };
    }

    private partial class Entry : BufferedContainer
    {
        [Resolved]
        private EditorMap map { get; set; }

        private int mode { get; }

        private const float blur_strength = 10;

        private FillFlowContainer flow;

        private float blurStrength
        {
            get => BlurSigma.X / blur_strength;
            set => BlurSigma = new Vector2(value * blur_strength);
        }

        public Entry(int mode)
        {
            this.mode = mode;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            RelativeSizeAxes = Axes.Both;
            EffectPlacement = EffectPlacement.Behind;
            EffectBlending = BlendingParameters.Additive;
            EffectPlacement = EffectPlacement.Behind;
            RedrawOnScale = false;
            DrawOriginal = true;

            InternalChildren = new Drawable[]
            {
                flow = new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(4),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Colour = FluXisColors.GetKeyColor(mode),
                    Children = new Drawable[]
                    {
                        new FillFlowContainer
                        {
                            AutoSizeAxes = Axes.Both,
                            Direction = FillDirection.Horizontal,
                            Spacing = new Vector2(3),
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            ChildrenEnumerable = Enumerable.Range(0, mode).Select(_ => new TicTac(16))
                        },
                        new FluXisSpriteText
                        {
                            Text = $"{mode} Keys",
                            WebFontSize = 14,
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Margin = new MarginPadding { Bottom = -3 }
                        }
                    }
                }
            };
        }

        protected override bool OnClick(ClickEvent e)
        {
            return map.SetKeyMode(mode);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            map.KeyModeChanged += keyModeChanged;
            keyModeChanged(map.RealmMap.KeyCount);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            map.KeyModeChanged -= keyModeChanged;
        }

        private void keyModeChanged(int mode)
        {
            if (mode == this.mode)
            {
                blurTo(1, 400);
                flow.FadeTo(1, 200);
            }
            else
            {
                blurTo(0, 400);
                flow.FadeTo(.4f, 200);
            }
        }

        private void blurTo(float strength, int duration)
        {
            this.TransformTo(nameof(blurStrength), strength, duration, Easing.OutQuint);
        }
    }
}
