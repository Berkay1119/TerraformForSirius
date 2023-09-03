using System;

public static class EventManager
{
    public delegate void CardEvent(HexagonalCard x);
    public delegate void ResourceEvent(Resources x);
    public delegate void AlienEvent(AlienInfo alienInfo);
    public delegate void IntEvent(int x);
    public delegate void FloatEvent(float x);
    public delegate void SoundEffectEvent(SoundEffectTypes soundEffect);
    
    
    public static event CardEvent CardSelected;
    public static event CardEvent CardPlayed;
    public static event ResourceEvent ConsumeResource;
    public static event IntEvent AvailablePopulationChanged;
    public static event FloatEvent AvailableKadirChanged;
    public static event ResourceEvent GenerateResources;
    public static event ResourceEvent AdjustTileControlUI;
    public static event Action NextTurn;
    public static event SoundEffectEvent SoundEffect;
    public static event IntEvent BarrierIncreased;
   public static event AlienEvent OnAlienInvasion;


    public static void OnCardSelected(HexagonalCard x)
    {
        CardSelected?.Invoke(x);
        SoundEffect?.Invoke(SoundEffectTypes.HexChoose);
    }

    public static void OnCardPlayed(HexagonalCard x)
    {
        CardPlayed?.Invoke(x);
        SoundEffect?.Invoke(SoundEffectTypes.HexPlacement);
    }

    public static void OnConsumeResource(Resources x)
    {
        ConsumeResource?.Invoke(x);
    }

    public static void OnAvailablePopulationChanged(int x)
    {
        AvailablePopulationChanged?.Invoke(x);
    }

    public static void OnAvailableKadirChanges(int x)
    {
        AvailableKadirChanged?.Invoke(x);
    }

    public static void OnGenerateResources(Resources x)
    {
        GenerateResources?.Invoke(x);
    }

    public static void OnAdjustTileControlUI(Resources x)
    {
        AdjustTileControlUI?.Invoke(x);
    }

    public static void OnNextTurn()
    {
        NextTurn?.Invoke();
    }

    public static void OnBarrierIncreased(int x)
    {
        BarrierIncreased?.Invoke(x);
    }

    public static void AliensAreComing(AlienInfo alienInfo)
    {
        OnAlienInvasion?.Invoke(alienInfo);
    }
}

public enum SoundEffectTypes
{
    HexPlacement,
    HexChoose
}
