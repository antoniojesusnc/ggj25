using UnityEngine;

public class SoundTest : MonoBehaviour
{
    private void Update()
    {
        // SFX Tests
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.Instance.PlaySFX("Jump");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            SoundManager.Instance.PlaySFX("Footsteps");
        }

        // Loop Tests
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SoundManager.Instance.PlayLoop("Loop1");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SoundManager.Instance.PlayLoop("Loop2");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SoundManager.Instance.StopLoop();
        }
    }
}

