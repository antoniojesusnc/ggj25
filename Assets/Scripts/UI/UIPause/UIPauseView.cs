using UnityEngine;

namespace ggj25
{
    public class UIPauseView : MonoBehaviour
    {
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
