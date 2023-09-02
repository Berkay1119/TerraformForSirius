
using System;

[Serializable]
public class Resources
{
    public int Food;
    public int Water;
    public int Mine;
    public int Population;
    public int PlanetHealth;
    public float Kadir;

    public void Consume(Resources resources)
    {
        Food -= resources.Food;
        Water -= resources.Water;
        Mine -= resources.Mine;
        PlanetHealth -= resources.PlanetHealth;
    }

    public void Generate(Resources resources)
    {
        Food += resources.Food;
        Water += resources.Water;
        Mine += resources.Mine;
        Population += resources.Population;
        PlanetHealth += resources.PlanetHealth;
    }

    public bool IsGreaterThan(Resources resources)
    {
        if (resources.Food>Food)
        {
            return false;
        }
        
        if (resources.Water>Water)
        {
            return false;
        }
        
        if (resources.Mine>Mine)
        {
            return false;
        }
        
        //if (resources.Population>Population)
        //{
        //    return false;
        //}
        
        if (resources.PlanetHealth>PlanetHealth)
        {
            return false;
        }
        
        //if (resources.Kadir>Kadir)
        //{
        //    return false;
        //}
        
        return true;
    }
}
