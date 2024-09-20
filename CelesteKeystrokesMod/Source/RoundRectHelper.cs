using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.CelesteKeystrokesMod {
    internal class RoundRectHelper {
        public static Texture2D elipseTexture;

        public static void UploadElipseTexture(GraphicsDevice device) {
            if (elipseTexture == null) {
                using (var mStream = new MemoryStream(ImageBytes.rawData)) {
                    elipseTexture = Texture2D.FromStream(device, mStream);
                }
            }
        }

        public static void FillRoundRect(Rectangle area, int radius, Color color) {
                // upload the texture if not already there
                UploadElipseTexture(Monocle.Draw.SpriteBatch.GraphicsDevice);
            
                // adapt the radius to make sure it isnt bigger than the rect
                radius = Math.Min(Math.Min(area.Width / 2, radius), Math.Min(area.Height / 2, radius));

                if (elipseTexture != null) {
                    // top left
                    Monocle.Draw.SpriteBatch.Draw(RoundRectHelper.elipseTexture,
                        new Rectangle(area.X, area.Y, radius, radius),
                        new Rectangle(0, 0, 250, 250),
                        color);

                    // top right
                    Monocle.Draw.SpriteBatch.Draw(RoundRectHelper.elipseTexture,
                        new Rectangle(area.Right - radius, area.Y, radius, radius),
                        new Rectangle(250, 0, 250, 250),
                        color);

                    // bottom left
                    Monocle.Draw.SpriteBatch.Draw(RoundRectHelper.elipseTexture,
                        new Rectangle(area.X, area.Bottom - radius, radius, radius),
                        new Rectangle( 0, 250, 250, 250),
                        color);

                    // bottom right
                    Monocle.Draw.SpriteBatch.Draw(RoundRectHelper.elipseTexture,
                        new Rectangle(area.Right - radius, area.Bottom - radius, radius, radius),
                        new Rectangle(250, 250, 250, 250),
                        color);

                    // top filler rect
                    Monocle.Draw.Rect(new Rectangle(area.X + radius, area.Y, area.Width - radius - radius, radius), color);
                    // bottom filler rect
                    Monocle.Draw.Rect(new Rectangle(area.X + radius, area.Y + area.Height - radius, area.Width - radius - radius, radius), color);
                    // middle filler rect
                    Monocle.Draw.Rect(new Rectangle(area.X, area.Y + radius, area.Width, area.Height - radius - radius), color);
                } else {
                    // be a normal rect when no elipse texture for round rect
                    Monocle.Draw.Rect(area, color);
                }
        }
        public static void FillRoundRect(float x, float y, float width, float height, float radius, Color color) {
            FillRoundRect(new Rectangle((int)x, (int)y, (int)width, (int)height), (int)radius, color);
        }

    }
}
