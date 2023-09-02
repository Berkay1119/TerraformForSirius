
public class Resources
{
    public int Food;
    public int Water;
    public int Mine;
    public int Population;
    public int PlanetHealth;

    public void Consume(Resources resources)
    {
        Food -= resources.Food;
        Water -= resources.Water;
        Mine -= resources.Mine;
        Population -= resources.Population;
        PlanetHealth -= resources.PlanetHealth;
    }
}
