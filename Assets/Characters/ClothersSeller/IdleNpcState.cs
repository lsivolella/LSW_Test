using UnityEngine;

public class IdleNpcState : BaseCharacterState
{
    private ShopkeeperBase clotherSeller;

    public IdleNpcState(ShopkeeperBase clotherSeller)
    {
        this.clotherSeller = clotherSeller;
    }
}
