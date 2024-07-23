using System.Collections.Generic;

public interface IScoreStorage
{
    public void Save(List<int> scores);
    public List<int> Load();
}
