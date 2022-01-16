public class PartialApplication : INode
{
    public string Name { get; init; }
    public IEnumerable<GenreRule> Rules { get; init; }

    public PartialApplication(string name, params GenreRule[] rules)
    {
        Name = name;
        Rules = rules;
    }

    public PartialApplication(string name, IEnumerable<GenreRule> rules)
    {
        Name = name;
        Rules = rules;
    }
}
