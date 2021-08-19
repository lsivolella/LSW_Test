using UnityEngine;

public class IdleNpcState : BaseCharacterState
{
    private ClothesSellerBase clotherSeller;

    public IdleNpcState(ClothesSellerBase clotherSeller)
    {
        this.clotherSeller = clotherSeller;
    }

    public void GiftPlayer()
    {
        //TODO: sort a piece of each clothing and add it to player inventory.
    }
}
