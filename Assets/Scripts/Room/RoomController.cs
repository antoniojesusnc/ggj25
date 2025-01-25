using System.Collections;
using deVoid.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace ggj25
{
    public class RoomController : MonoBehaviour
    {
        private const float UPDATE_RATE = 0.1f;

        [SerializeField] private SpriteRenderer _dust;

        private HeroController _hero;

        private Vector3 _previousPosition;
        private Color32[] _currentColor;
        private DustCleaner _dustCleaner;
        private int _pixelSize;

        private float _timeStamp;

        private void Start()
        {
            _hero = GameObject.FindObjectOfType<HeroController>();
            _previousPosition = _hero.transform.position;

            _dustCleaner = new DustCleaner(_dust);

            _pixelSize = _dust.sprite.texture.GetPixels().Length;
        }

        private void Update()
        {
            _dustCleaner.TryClear();

            _timeStamp -= Time.deltaTime;
            if (_timeStamp <= 0)
            {
                StartCoroutine(CalculateCleaningRateCo());
                _timeStamp = UPDATE_RATE;
            }
        }

        private IEnumerator CalculateCleaningRateCo()
        {
            int iterations = 4;
            int x = 0;
            int y = 0;
            int width = Mathf.FloorToInt(_dust.sprite.texture.width/(float)iterations*2);
            int height = Mathf.FloorToInt(_dust.sprite.texture.height/(float)iterations*2);
            int pixelColored = 0;
            for (int i = 0; i < iterations * 0.5f; i++)
            {
                y = 0;
                for (int j = 0; j < iterations * 0.5f; j++)
                {
                    pixelColored += CalculateCleaningRate(x, y, width, height);
                    y += height;

                }

                x += width;
                yield return 0;
            }
            Signals.Get<OnRoomCleaningRateChanged>().Dispatch(pixelColored / (float)_pixelSize);
        }

        private int CalculateCleaningRate(int x, int y, int width, int height)
        {
            var pixels = _dust.sprite.texture.GetPixels(x, y, width, height);
            var pixelColored = 0;
            for (int i = 0; i < pixels.Length; i++)
            {
                if (pixels[i].a == 0)
                {
                    pixelColored++;
                }
            }

            return pixelColored;
        }
    }
}