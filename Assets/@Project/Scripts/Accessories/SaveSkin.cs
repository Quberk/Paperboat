
[System.Serializable]
public class SaveSkin
{
    public string id;
    public bool pesawatUnlocked;

    public SaveSkin (string id, bool pesawatUnlocked)
    {
        this.id = id;
        this.pesawatUnlocked = pesawatUnlocked;
    }
}
