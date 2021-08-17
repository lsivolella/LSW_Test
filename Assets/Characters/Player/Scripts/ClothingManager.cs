using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer head;
    [SerializeField] SpriteRenderer body;
    [SerializeField] SpriteRenderer legs;
    [SerializeField] SpriteRenderer feet;
    [SerializeField] ClothingSO currentShirt;
    [SerializeField] ClothingSO currentPants;
    [SerializeField] ClothingSO currentShoes;

    public ClothingSO CurrentShirt { get { return currentShirt; } }
    public ClothingSO CurrentPants { get { return currentPants; } }
    public ClothingSO CurrentShoes { get { return currentShoes; } }

    private void Awake()
    {
        ClothingSetup();
    }

    private void ClothingSetup()
    {
        body.sprite = currentShirt.Sprite;
        legs.sprite = currentPants.Sprite;
        feet.sprite = currentShoes.Sprite;
    }

    public void ChangeClothes(ClothingSO newShirt, ClothingSO newPants, ClothingSO newShoes)
    {
        currentShirt = newShirt;
        currentPants = newPants;
        currentShoes = newShoes;

        ClothingSetup();
    }
}
