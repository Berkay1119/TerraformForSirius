public static class EventManager
{
    public delegate void CardSelectedEvent(HexagonalCard x);
    
    public static event CardSelectedEvent CardSelected;


    public static void OnCardSelected(HexagonalCard x)
    {
        CardSelected?.Invoke(x);
    }
}
