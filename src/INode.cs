public interface INode
{
    public string Name { get; }
    public IEnumerable<GenreRule> Rules { get; }
}