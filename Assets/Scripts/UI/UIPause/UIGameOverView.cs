using TMPro;
using UnityEngine;

namespace ggj25
{
    public class UIGameOverView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _headerText;
        
        public void Open(bool isWin)
        {
            UpdateData(isWin);
            gameObject.SetActive(true);
        }
        
        public void Close()
        {
            gameObject.SetActive(false);
        }

        private void UpdateData(bool isWin)
        {
            _headerText.text = isWin ? "Victory" : "Defeat";
        }

        public void ClickOnRetry()
        {
            GameManager.Instance.ToGame();
        }
        
        public void ClickOnMainMenu()
        {
            GameManager.Instance.ToMainMenu();
        }
    }
}
