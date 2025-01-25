
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    private void Update()
    {
        // SFX Tests
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.Instance.PlaySFX(AudioType.SFX.Jump);
            Debug.Log("Playing: Jump SFX");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            SoundManager.Instance.PlaySFX(AudioType.SFX.Footsteps);
            Debug.Log("Playing: Footsteps SFX");
        }

        // Loop Tests
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SoundManager.Instance.PlayLoop(AudioType.Loop.PeacefulMusic);
            Debug.Log("Playing: Peaceful Music Loop");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SoundManager.Instance.PlayLoop(AudioType.Loop.ActionMusic);
            Debug.Log("Playing: Action Music Loop");
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            SoundManager.Instance.StopAllSounds();
            Debug.Log("Playing: Action Music Loop");
        }
    }
}

