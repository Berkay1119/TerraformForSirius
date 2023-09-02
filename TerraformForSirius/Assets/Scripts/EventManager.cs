using System;

public static class EventManager
{
    public delegate void CardSelectedEvent(HexagonalCard x);
    public delegate void ConsumeResourceEvent(Resources x);
    public static event CardSelectedEvent CardSelected;
    public static event ConsumeResourceEvent ConsumeResource;


    public static void OnCardSelected(HexagonalCard x)
    {
        CardSelected?.Invoke(x);
    }

    public static void OnConsumeResource(Resources x)
    {
        ConsumeResource?.Invoke(x);
    }
}
