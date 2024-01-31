using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player){
        if(!HasKitchenObject()){
            //There is no KitchrnObject here
            if(player.HasKitchenObject()){
                //Player  is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                //Player not carrying anything 
            }
        } else {
            //There is a KitchenObject here
            if(player.HasKitchenObject()){
                //Player is carrying somthing
                if(player.GetKitchenObject() is PlateKitchenObject){
                    //Player hold a Plate
                    PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                        GetKitchenObject().DestroySelf();
                    }  
                }
            } else {
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    
    
}
