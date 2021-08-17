using UnityEngine;

public class IdleNpcState : BaseCharacterState
{
    private ClothesSellerBase clotherSeller;

    public IdleNpcState(ClothesSellerBase clotherSeller)
    {
        this.clotherSeller = clotherSeller;
    }
}
