using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UdpMistro;

namespace Broccoli.Engine.Input
{
    public class LocalPlayer : Player
    {
        public LocalPlayer(Texture2D texture, Rectangle size, InputHandler input) : base(texture, size, input, Globals.NoId)
        {
        }
        
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var rect = new Rectangle(HitBox.X-(Texture.Width/2-HitBox.Width/2), HitBox.Y - (Texture.Height - HitBox.Height), Texture.Width, Texture.Height);
            spriteBatch.Draw(Texture, rect, Color.White);

            var texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { new Color(255,0,0,10) });
            //red is hitbox, black is texture
            spriteBatch.Draw(texture, HitBox, Color.White);
        }
    }
}