using System;

public static class EventManager
{
    public delegate void CardSelectedEvent(HexagonalCard x);
    public delegate void CardPlayedEvent(HexagonalCard x);
    public delegate void SoundEffectEvent(SoundEffectTypes soundEffect);
    public delegate void ConsumeResourceEvent(Resources x);
    public static event CardSelectedEvent CardSelected;
    public static event CardPlayedEvent CardPlayed;
    public static event ConsumeResourceEvent ConsumeResource;
    public static event SoundEffectEvent SoundEffect;

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

}

public enum SoundEffectTypes
{
    HexPlacement,
    HexChoose
}