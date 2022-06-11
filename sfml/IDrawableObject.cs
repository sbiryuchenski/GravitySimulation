using SFML.Graphics;

namespace sfml
{
    interface IDrawableObject
    {
        /// <summary>
        /// Get object to draw
        /// </summary>
        /// <returns></returns>
        public Drawable GetDrawable();
    }
}
