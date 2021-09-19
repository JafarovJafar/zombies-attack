using UnityEngine.Events;

public class EventsPool
{
    public static EventsPool Instance;

    public UnityAction PlayerDead;
    public UnityAction<ZombieModel> ZombieDead;

    public void Init()
    {
        Instance = this;
    }
}