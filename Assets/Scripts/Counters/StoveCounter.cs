using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{

    private enum State{
        Idle,
        Frying,
        Fried,
        Burned,
    }
    [SerializeField] private FryingResipeSO[] fryingResipeArray;

    [SerializeField] private BurningResipeSO[] burningResipeArray;

    private State state;

    private float fryingTimer;

    private float burningTimer;

    private FryingResipeSO fryingResipeSO;

    private BurningResipeSO burningRecipeSO;

    private void Update(){

        if(HasKitchenObject()){
           
        
         switch(state){
            case State.Idle:
            break;

            case State.Frying:
            fryingTimer += Time.deltaTime;

            if(fryingTimer > fryingResipeSO.fryingTimerMax){
                //Fried
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(fryingResipeSO.output, this);

                Debug.Log("Object Fried!");

                state = State.Fried;
                burningTimer = 0f;
                burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            }
            break;

            case State.Fried:

            burningTimer += Time.deltaTime;

            if(burningTimer > burningRecipeSO.burningTimerMax){
                //Fried
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                Debug.Log("Object Burnned!");

                state = State.Burned;
            }
            break;

            case State.Burned:
            break;
          }
          Debug.Log(state);

        }
    }

    public override void  Interact(Player player){
        if(!HasKitchenObject()){
            //There is no KitchrnObject here
            if(player.HasKitchenObject()){
                //Player  is carrying something
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
                    //Player carruing something thatcan be Fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    fryingResipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;
                    fryingTimer = 0f;
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

                state = State.Idle;
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
        foreach (FryingResipeSO fryingResipeSO in fryingResipeArray){
            if(fryingResipeSO.input == inputKitchenObjectSO){
                return fryingResipeSO;
            }
            
        }
        return null;
    }

private BurningResipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO){
        foreach (BurningResipeSO burningRecipeSO in  burningResipeArray){
            if(burningRecipeSO.input == inputKitchenObjectSO){
                return burningRecipeSO;
            }
            
        }
        return null;
    }
    

}
