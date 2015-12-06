using Microsoft.Xna.Framework;

namespace Diablo_Wannabe.ImageProcessing
{
    public class ImageEffect
    {
        protected Image Image;

        public bool IsActive;

        public ImageEffect()
        {
            this.IsActive = false;
        }

        public virtual void LoadContent(ref Image Image)
        {
            this.Image = Image;
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}