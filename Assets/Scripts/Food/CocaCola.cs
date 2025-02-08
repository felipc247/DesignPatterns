using UnityEngine;

public class CocaCola : FoodBase
{
    public override void Serve()
    {
        Debug.Log($"{foodData.Name} è stato servito al prezzo di {GetPrice()} in {GetPreparationTime()} secondi");
    }
}
