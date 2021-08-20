using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHudManager : MonoBehaviour
{
    private MainCanvasManager mainCanvasManager;
    private SoundManager soundManager;

    private void Awake()
    {
        mainCanvasManager = GetComponentInParent<MainCanvasManager>();
        soundManager = SoundManager.Instance;
    }

    public void OpenInventory()
    {
        mainCanvasManager.OnInventoryCall();
    }

    public void PlaySoundClip()
    {
        soundManager.PlayButtonsSoundEffect();
    }
}
