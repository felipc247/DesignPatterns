
public interface IFood : ISpawnable
{
    void Serve(); // per servire il cibo
    float GetPrice(); // il prezzo
    float GetPreparationTime(); // tempo di preparazione
    float TimeUse();
}
