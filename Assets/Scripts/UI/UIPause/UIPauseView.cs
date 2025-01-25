using UnityEngine;

namespace ggj25
{
    public class UIPauseView : MonoBehaviour
    {
        public void Open()
        {
            gameObject.SetActive(true);
        }
        
        public void Close()
        {
            gameObject.SetActive(false);
        }
        
        public void ClickOnContinue()
        {
            GameManager.Instance.ContinueGame();
        }
        
        public void ClickOnMainMenu()
        {
            GameManager.Instance.ToMainMenu();
        }
    }
}
