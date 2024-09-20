using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Celeste.Mod.CelesteKeystrokesMod {
    internal class KeyRenderer {
        public static float GetFadeTimeSeconds() {
            return (float)CelesteKeystrokesModModule.Settings.FadeTime / 100.0F;
        }
        public static float GetKeyPadding() {
            return (float)CelesteKeystrokesModModule.Settings.KeyPadding * GetScale();
        }
        public static Color GetUpKeyColor() {
            var settings = CelesteKeystrokesModModule.Settings;
            return new Color(settings.ReleasedColorRed, settings.ReleasedColorGreen, settings.ReleasedColorBlue);
        }
        public static Color GetDownKeyColor() {
            var settings = CelesteKeystrokesModModule.Settings;
            return new Color(settings.PressedColorRed, settings.PressedColorGreen, settings.PressedColorBlue);
        }
        public static float GetScale() {
            return (float)CelesteKeystrokesModModule.Settings.Scale / 10.0F;
        }
        public static Vector2 GetKeySize() {
            var icon = Input.GuiKey(Keys.L);
            return new Vector2(icon.Width, icon.Height) * GetScale();
        }
        public static Vector2 GetSpacebarSize() {
            var size = GetKeySize();
            size.X = size.X * 3.0F + GetKeyPadding() * 2.0F;
            return size;
        }
        private static SortedDictionary<Keys, float> keyPressAnims = new SortedDictionary<Keys, float>();
        private static KeyboardState currentKeyboardState;
        private static KeyboardState previousKeyboardState;
        


        public static void BeginFrame() {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (previousKeyboardState == null) {
                previousKeyboardState = currentKeyboardState;
            }
        }
        public static void EndFrame() { }

        private static float UpdateAndGetAnimationScalarFor(Keys key) {
            bool isPressed = currentKeyboardState.IsKeyDown(key);
            bool wasPressed = previousKeyboardState.IsKeyDown(key);

            if (!keyPressAnims.ContainsKey(key)) {
                keyPressAnims[key] = 0;
            }
            if (isPressed != wasPressed) {
                keyPressAnims[key] = GetFadeTimeSeconds() - keyPressAnims[key];
            }

            float keyAnimTime = keyPressAnims[key];
            keyAnimTime = Math.Max(0, keyAnimTime - Monocle.Engine.DeltaTime);
            keyPressAnims[key] = keyAnimTime;

            float animationProgress = 1.0F - (keyAnimTime / GetFadeTimeSeconds());
            return animationProgress;
        }
        private static void RenderKeyIcon(Vector2 position, float scale, Color color, Keys key) {
            var icon = Input.GuiKey(key);

            icon.Draw(position, new Vector2(0, 0), color, GetScale());
        }

        public static void RenderKey(Vector2 position, Keys key) {
            bool isPressed = currentKeyboardState.IsKeyDown(key);
            float animationProgress = UpdateAndGetAnimationScalarFor(key);
            Color keyColor;
            if (isPressed) {
                keyColor = Color.Lerp(GetUpKeyColor(), GetDownKeyColor(), animationProgress);
            } else {
                keyColor = Color.Lerp(GetDownKeyColor(), GetUpKeyColor(), animationProgress);
            }
            RenderKeyIcon(position, GetScale(), keyColor, key);
        }

        public static void RenderSpacebar(Vector2 position, Keys key) {
            bool isPressed = currentKeyboardState.IsKeyDown(key);
            float animationProgress = UpdateAndGetAnimationScalarFor(key);
            Color keyColor;
            if (isPressed) {
                keyColor = Color.Lerp(GetUpKeyColor(), GetDownKeyColor(), animationProgress);
            } else {
                keyColor = Color.Lerp(GetDownKeyColor(), GetUpKeyColor(), animationProgress);
            }

            var size = GetSpacebarSize();

            // Spacebar Background
            RoundRectHelper.FillRoundRect(position.X, position.Y, size.X, size.Y, size.Y / 3.0F, keyColor);

            // Spacebar Line
            float lineHeight = size.Y * 0.1f;
            float lineY = position.Y + (size.Y - lineHeight) / 2.0F;
            RoundRectHelper.FillRoundRect(position.X + size.X * 0.1f, lineY, size.X * 0.8f, lineHeight, lineY / 3.0F, Color.Black);
        }

    }
}