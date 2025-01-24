using deVoid.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace ggj25
{
    public class RoomController : MonoBehaviour
    {
        private const float UPDATE_RATE = 0.1f;
        
        [SerializeField]
        private SpriteRenderer _dust;

        private HeroController _hero;

        private Vector3 _previousPosition;
        private Color32[] _currentColor;
        private DustController _dustController;
        private DustCleaner _dustCleaner;
        private int _pixelSize;

        private float _timeStamp;
        
        private void Start()
        {
            _hero = GameObject.FindObjectOfType<HeroController>();
            _previousPosition = _hero.transform.position;
            _dustController = new DustController(_dust);

            _dustCleaner = new DustCleaner(_dust);
            
            _pixelSize = _dust.sprite.texture.GetPixels().Length;
        }

        private void Update()
        {
            //_dustController.ClearRoom();
            _dustCleaner.TryClear();

            _timeStamp -= Time.deltaTime;
            if(_timeStamp <= 0)
            {
                CalculateCleaningRate();
                _timeStamp = UPDATE_RATE;
            }
        }

        private void CalculateCleaningRate()
        {
            var pixels = _dust.sprite.texture.GetPixels();
            var pixelColored = 0;
            for (int i = 0; i < pixels.Length; i++)
            {
                if (pixels[i].a == 0)
                {
                    pixelColored++;
                }
            }
            Signals.Get<OnRoomCleaningRateChanged>().Dispatch(pixelColored/(float)_pixelSize);
        }
    }
}
