using System.Collections.Generic;

[System.Serializable]
public class SubList
{
    public int Level;
    public List<bool> IsBig = new List<bool>();
    public List<int> SpawnIndex = new List<int>();
}
