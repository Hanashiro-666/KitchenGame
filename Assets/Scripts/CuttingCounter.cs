using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public override void Interact(Player player)
    {
       if(!HasKitchenObject()){
            //There is no KitchrnObject here
            if(player.HasKitchenObject()){
                //Player  is carrying something
                    //Player carruing something thatcan be Cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                //Player not carrying anything 
            }
        } else {
            //There is a KitchenObject here
            if(player.HasKitchenObject()){
                //Player is carrying somthing
            } else {
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player){
       if(HasKitchenObject()){
        //There is a Kitchenbject here and it can be cut
        KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

        GetKitchenObject().DestroySelf();

        KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);

       }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO){
        foreach (CuttingRecipeSO cuttingResipeSO in cuttingRecipeSOArray){
            if(cuttingResipeSO.input == inputKitchenObjectSO){
                return cuttingResipeSO.output;
            }
            
        }
        return null;
    } 
}
