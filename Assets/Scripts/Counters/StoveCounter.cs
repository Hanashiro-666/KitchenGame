using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingResipeSO[] fryingResipeArray;

    public override void  Interact(Player player){
        if(!HasKitchenObject()){
            //There is no KitchrnObject here
            if(player.HasKitchenObject()){
                //Player  is carrying something
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
                    //Player carruing something thatcan be Fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
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

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO){
     FryingResipeSO fryingResipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingResipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO){
        FryingResipeSO fryingResipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if(fryingResipeSO != null){
            return fryingResipeSO.output;
        }else{
            return null;
        }
    } 

    private FryingResipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO){
        foreach (FryingResipeSO FryingResipeSO in fryingResipeArray){
            if(FryingResipeSO.input == inputKitchenObjectSO){
                return FryingResipeSO;
            }
            
        }
        return null;
    }

}
