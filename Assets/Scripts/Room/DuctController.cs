using UnityEngine;

namespace ggj25
{
    public class DustController
    {
        private SpriteRenderer _dust;

        private HeroController _hero;

        private Vector3 _previousPosition;
        private Color32[] _currentColor;

        public DustController(SpriteRenderer dust)
        {
            _dust = dust;
            
            _hero = GameObject.FindObjectOfType<HeroController>();
            _previousPosition = _hero.transform.position;
        }
        
        public void ClearRoom()
        {
            var heroPosition = _hero.transform.position;
            
            Vector2 pixelPos = WorldToPixelCoordinates(heroPosition);
            _currentColor = _dust.sprite.texture.GetPixels32();

            ColourBetween(_previousPosition, heroPosition, _hero.Config.CleanSize ,Color.clear);
            ApplyMarkedPixelChanges();
            
            _previousPosition = _hero.transform.position;
        }

        private void ColourBetween(Vector3 previousPosition, Vector3 heroPosition, float width, Color color)
        {
            // Get the distance from start to finish
            float distance = Vector2.Distance(previousPosition, heroPosition);
            Vector2 direction = (previousPosition - heroPosition).normalized;

            Vector2 position = previousPosition;

            // Calculate how many times we should interpolate between start_point and end_point based on the amount of time that has passed since the last update
            float lerp_steps = 1 / distance;

            for (float lerp = 0; lerp <= 1; lerp += lerp_steps)
            {
                position = Vector2.Lerp(previousPosition, heroPosition, lerp);
                MarkPixelsToColour(position, Mathf.RoundToInt(width), color);
            }
        }
        
        public void MarkPixelsToColour(Vector2 pixel, int pen_thickness, Color color_of_pen)
        {
            // Figure out how many pixels we need to colour in each direction (x and y)
            var center_x = (int)pixel.x;
            var center_y = (int)pixel.y;
            //int extra_radius = Mathf.Min(0, pen_thickness - 2);

            for (int x = center_x - pen_thickness; x <= center_x + pen_thickness; x++)
            {
                // Check if the X wraps around the image, so we don't draw pixels on the other side of the image
                if (x >= (int)_dust.sprite.rect.width || x < 0)
                    continue;

                for (int y = center_y - pen_thickness; y <= center_y + pen_thickness; y++)
                {
                    MarkPixelToChange(x, y, color_of_pen);
                }
            }
        }
        
        public void MarkPixelToChange(int x, int y, Color color)
        {
            // Need to transform x and y coordinates to flat coordinates of array
            var array_pos = y * (int)_dust.sprite.rect.width + x;

            // Check if this is a valid position
            if (array_pos > _currentColor.Length || array_pos < 0)
                return;

            _currentColor[Mathf.RoundToInt(array_pos)] = color;
        }

        public void ApplyMarkedPixelChanges()
        {
            _dust.sprite.texture.SetPixels32(_currentColor);
            _dust.sprite.texture.Apply();
        }

        public Vector2 WorldToPixelCoordinates(Vector2 world_position)
        {
            // Change coordinates to local coordinates of this image
            Vector3 local_pos = _dust.transform.InverseTransformPoint(world_position);

            // Change these to coordinates of pixels
            float pixelWidth = _dust.sprite.rect.width;
            float pixelHeight = _dust.sprite.rect.height;
            float unitsToPixels = pixelWidth / _dust.sprite.bounds.size.x * _dust.transform.localScale.x;

            // Need to center our coordinates
            float centered_x = local_pos.x * unitsToPixels + pixelWidth / 2;
            float centered_y = local_pos.y * unitsToPixels + pixelHeight / 2;

            // Round current mouse position to nearest pixel
            Vector2 pixel_pos = new Vector2(Mathf.RoundToInt(centered_x), Mathf.RoundToInt(centered_y));

            return pixel_pos;
        }
    }
}
