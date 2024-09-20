using System.Linq;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.CelesteKeystrokesMod {
    internal class KeystrokesEntity : Entity {
        public KeystrokesEntity() {
            Tag = Tags.HUD;
        }

        Vector2 ComputeSize() {
            var keySize = KeyRenderer.GetKeySize();
            keySize *= 3;
            float padding = KeyRenderer.GetKeyPadding()*2;
            return new Vector2(padding + keySize.X, padding + keySize.Y);
        }

        public override void Render() {
            var settings = CelesteKeystrokesModModule.Settings;
            if (!settings.Visible) return;

            KeyRenderer.BeginFrame();


            var graphicsDevice = Monocle.Draw.SpriteBatch.GraphicsDevice;
            Vector2 fullSize = ComputeSize();
            Vector2 keySize = KeyRenderer.GetKeySize();
            float keyPadding = KeyRenderer.GetKeyPadding();
            Vector2 edgeOffset = new Vector2((float)settings.EdgeOffsetX, (float)settings.EdgeOffsetY);
            bool originateFromRight = settings.UseRightEdge;
            bool originateFromBottom = settings.UseBottomEdge;
            Vector2 renderOrigin = new Vector2(
                originateFromRight ? ((float)graphicsDevice.PresentationParameters.Bounds.Width - edgeOffset.X - fullSize.X) : edgeOffset.X,
                originateFromBottom ? ((float)graphicsDevice.PresentationParameters.Bounds.Height - edgeOffset.Y - fullSize.Y) : edgeOffset.Y);

            // Function Keys
            var dashKey = Input.Dash.Binding.Keyboard.FirstOrDefault();
            var grabKey = Input.Grab.Binding.Keyboard.FirstOrDefault();
            var jumpKey = Input.Jump.Binding.Keyboard.FirstOrDefault();

            // Movement Keys
            var upKey = Input.MoveY.Negative.Keyboard.FirstOrDefault();
            var downKey = Input.MoveY.Positive.Keyboard.FirstOrDefault();
            var leftKey = Input.MoveX.Negative.Keyboard.FirstOrDefault();
            var rightKey = Input.MoveX.Positive.Keyboard.FirstOrDefault();

            // Render the Keys
            float cursorX = renderOrigin.X;
            float cursorY = renderOrigin.Y;
            KeyRenderer.RenderKey(new Vector2(cursorX, cursorY), grabKey);
            cursorX += keySize.X + keyPadding;
            KeyRenderer.RenderKey(new Vector2(cursorX, cursorY), upKey);
            cursorX += keySize.X + keyPadding;
            KeyRenderer.RenderKey(new Vector2(cursorX, cursorY), dashKey);

            cursorX = renderOrigin.X;
            cursorY += keySize.Y + keyPadding;
            KeyRenderer.RenderKey(new Vector2(cursorX, cursorY), leftKey);
            cursorX += keySize.X + keyPadding;
            KeyRenderer.RenderKey(new Vector2(cursorX, cursorY), downKey);
            cursorX += keySize.X + keyPadding;
            KeyRenderer.RenderKey(new Vector2(cursorX, cursorY), rightKey);

            cursorX = renderOrigin.X;
            cursorY += keySize.Y + keyPadding;
            KeyRenderer.RenderSpacebar(new Vector2(cursorX, cursorY), jumpKey);

            KeyRenderer.EndFrame();
        }
    }
}
