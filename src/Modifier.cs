public enum UniqueGroup
{
    Era,
    Size,
}

public enum ModifierType
{
    Misc,
    Prefix,
    Suffix,
    Adjective,
}

public class Modifier
{
    public static Dictionary<string, Modifier> UtilityModifiers = new();

    public ModifierType Type { get; }
    public UniqueGroup? Group { get; }
    private Func<INode, INode> Fn;

    public Modifier(
        ModifierType type,
        Func<INode, INode> apply
    )
    {
        Type = type;
        Fn = apply;
    }

    public Modifier(
        ModifierType type,
        UniqueGroup group,
        Func<INode, INode> apply
    ) : this(type, apply)
    {
        Group = group;
    }

    public INode Apply(INode node)
    {
        return Type == ModifierType.Suffix && node.Rules.Contains(GenreRule.DropsS)
            ? Fn(UtilityModifiers["drop-s"].Apply(node))
            : Fn(node);
    }
}
