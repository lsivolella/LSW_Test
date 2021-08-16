using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer head;
    [SerializeField] SpriteRenderer body;
    [SerializeField] SpriteRenderer legs;
    [SerializeField] SpriteRenderer feet;
    [SerializeField] ShirtSO currentShirt;

    public Sprite Head { get { return head.sprite; } }
    public Sprite Body { get { return body.sprite; } }
    public Sprite Legs { get { return legs.sprite; } }
    public Sprite Feet { get { return feet.sprite; } }

    public ShirtSO CurrentShirt { get { return currentShirt; }}

    private void Awake()
    {
        ClothingSetup();
    }

    private void ClothingSetup()
    {
        body.sprite = currentShirt.Sprite;
    }
}
