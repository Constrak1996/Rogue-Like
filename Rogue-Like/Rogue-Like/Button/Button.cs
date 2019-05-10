using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rogue_Like
{
    public class Button : Component
    {
        #region Fields
        private MouseState _curretMouse;
        private SpriteFont _font;
        private bool _isHovering;
        private MouseState _previousMouse;
        private Texture2D _texture;
        #endregion
        #region Propeties
        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Color PenColor { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }
        public string Text { get; set; }

        #endregion
        #region methods
        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;
            PenColor = Color.Black;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBach)
        {
            var colour = Color.White;
            if (_isHovering)
            {
                colour = Color.Gray;
            }
            spriteBach.Draw(_texture, Rectangle, colour);
            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBach.DrawString(_font, Text, new Vector2(x, y), PenColor);
            }
        }
        public override void Update(GameTime gameTime)
        {
            _previousMouse = _curretMouse;
            _curretMouse = Mouse.GetState();
            var mouseRectangle = new Rectangle(_curretMouse.X, _curretMouse.Y, 1, 1);
            _isHovering = false;
            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;
                if (_curretMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
        #endregion
    }
}