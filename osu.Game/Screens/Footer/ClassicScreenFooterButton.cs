// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Game.Input.Bindings;
using osu.Game.Skinning;
using osuTK;

namespace osu.Game.Screens.Footer
{
    public partial class ClassicScreenFooterButton : ScreenFooterButton
    {
        public new const int CORNER_RADIUS = 10;
        public new const int HEIGHT = 90;
        protected new const int BUTTON_WIDTH = 92;


        /// <summary>
        /// If this footer button controls the visibility of an overlay, it will be bound to this bindable.
        /// </summary>
        public new readonly Bindable<Visibility> OverlayState = new Bindable<Visibility>();
        [Resolved]
        private ISkinSource skin { get; set; } = null!;
        public new readonly OverlayContainer? Overlay;

        private readonly Container spriteContainer;

        public ClassicScreenFooterButton(string textureLookupName, OverlayContainer? overlay = null)
        {
            Overlay = overlay;

            Size = new Vector2(BUTTON_WIDTH, HEIGHT);

            Y = -5;
            Children = new Drawable[]
            {
                spriteContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both
                },
            };
        }
        protected override void LoadComplete()
        {
            if (Overlay != null)
                OverlayState.BindTo(Overlay.State);

            OverlayState.BindValueChanged(_ => UpdateDisplay());
            Enabled.BindValueChanged(_ => UpdateDisplay(), true);

            FinishTransforms(true);
        }

        public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) => spriteContainer.ReceivePositionalInputAt(screenSpacePos);
        public override bool OnPressed(KeyBindingPressEvent<GlobalAction> e)
        {
            if (e.Action != Hotkey || e.Repeat) return false;

            TriggerClick();
            return true;
        }

        public override void UpdateDisplay()
        {
            spriteContainer.Children = new Drawable[]
            {
                new Sprite
                {
                    Texture = skin.GetTexture("selection-mode")
                }
            };
        }
    }
}
