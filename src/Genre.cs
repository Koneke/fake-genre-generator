public enum GenreRule
{
    NeedsSuffix,
    DropsS
}

public class Genre : INode
{
    public string Name { get; init; }
    public IEnumerable<GenreRule> Rules { get; init; }

    public Genre(string name, params GenreRule[] rules)
    {
        Name = name;
        Rules = rules;
    }
}
