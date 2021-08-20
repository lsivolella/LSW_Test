using UnityEngine;

/// <summary>
/// Controls intro button
/// </summary>
public class IntroScreenManager : MonoBehaviour
{
    private MainCanvasManager mainCanvasManager;
    private SoundManager soundManager;

    private void Awake()
    {
        mainCanvasManager = GetComponentInParent<MainCanvasManager>();
        soundManager = SoundManager.Instance;
    }

    public void BeginGame()
    {
        mainCanvasManager.DisableIntroScreen();
    }

    public void PlaySoundClip()
    {
        soundManager.PlayButtonsSoundEffect();
    }
}
