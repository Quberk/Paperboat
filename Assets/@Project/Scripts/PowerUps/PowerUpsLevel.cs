[System.Serializable]
public class PowerUpsLevel
{
    public int levelPesawat;
    public int levelParasut;
    public int levelMagnet;
    public int levelLayar;

    public PowerUpsLevel(int levelPesawat, int levelParasut, int levelMagnet, int levelLayar)
    {
        this.levelPesawat = levelPesawat;
        this.levelParasut = levelParasut;
        this.levelMagnet = levelMagnet;
        this.levelLayar = levelLayar;
    }
}
