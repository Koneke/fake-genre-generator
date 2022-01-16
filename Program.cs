var random = new Random();

IEnumerable<T> FisherYates<T>(IEnumerable<T> source)
{
    var result = new List<T>();
    for (var i = 0; i < source.Count(); i++)
    {
        var pick = source
            .Except(result)
            .ElementAt(random.Next(0, source.Count() - i));
        result.Add(pick);
        yield return pick;
    }
}

var queue = new Queue<Modifier>();

var modifiers = FisherYates(new List<Modifier>
{
    // add in ordering somehow
    // first order by type, then order value?
    // see, english adj. order

    { new Modifier(ModifierType.Prefix, n => new PartialApplication($"psy{n.Name}", n.Rules)) },
    { new Modifier(ModifierType.Prefix, n => new PartialApplication($"bro{n.Name}", n.Rules)) },
    { new Modifier(ModifierType.Prefix, n => new PartialApplication($"hyper{n.Name}", n.Rules)) },
    { new Modifier(ModifierType.Prefix, n => new PartialApplication($"neuro{n.Name}", n.Rules)) },
    { new Modifier(ModifierType.Prefix, n => new PartialApplication($"vapor{n.Name}", n.Rules)) },

    // { new Node(NodeType.Suffix, n => $"{n.Name}style") },
    { new Modifier(ModifierType.Suffix, n => new PartialApplication($"{n.Name}core", n.Rules)) },
    { new Modifier(ModifierType.Suffix, n => new PartialApplication($"{n.Name}wave", n.Rules)) },
    { new Modifier(ModifierType.Suffix, n => new PartialApplication($"{n.Name}step", n.Rules)) },
    { new Modifier(ModifierType.Suffix, n => new PartialApplication($"{n.Name}hop", n.Rules)) },

    // { new Modifier(ModifierType.Adjective, n => $"tech {n.Name}") },
    { new Modifier(ModifierType.Adjective, n => new PartialApplication($"ambient {n.Name}", n.Rules)) },
    { new Modifier(ModifierType.Adjective, n => new PartialApplication($"progressive {n.Name}", n.Rules)) },
    { new Modifier(ModifierType.Adjective, n => new PartialApplication($"deep {n.Name}", n.Rules)) },
    { new Modifier(ModifierType.Adjective, n => new PartialApplication($"hard {n.Name}", n.Rules)) },
    { new Modifier(ModifierType.Adjective, n => new PartialApplication($"melodic {n.Name}", n.Rules)) },
    { new Modifier(ModifierType.Adjective, n => new PartialApplication($"dark {n.Name}", n.Rules)) },
    { new Modifier(ModifierType.Adjective, n => new PartialApplication($"psychedelic {n.Name}", n.Rules)) },
    { new Modifier(ModifierType.Adjective, n => new PartialApplication($"big room {n.Name}", n.Rules)) },
    { new Modifier(ModifierType.Adjective, n => new PartialApplication($"bubblegum {n.Name}", n.Rules)) },

    { new Modifier(ModifierType.Adjective, UniqueGroup.Size, n => new PartialApplication($"minimal {n.Name}", n.Rules)) },
    { new Modifier(ModifierType.Adjective, UniqueGroup.Size, n => new PartialApplication($"maximal {n.Name}", n.Rules)) },
    { new Modifier(ModifierType.Adjective, UniqueGroup.Size, n => new PartialApplication($"small {n.Name}", n.Rules)) },
    { new Modifier(ModifierType.Adjective, UniqueGroup.Size, n => new PartialApplication($"liquid {n.Name}", n.Rules)) },

    { new Modifier(ModifierType.Adjective, UniqueGroup.Era, n => new PartialApplication($"70s {n.Name}", n.Rules)) },
    { new Modifier(ModifierType.Adjective, UniqueGroup.Era, n => new PartialApplication($"80s {n.Name}", n.Rules)) },
    { new Modifier(ModifierType.Adjective, UniqueGroup.Era, n => new PartialApplication($"90s {n.Name}", n.Rules)) },
});

Modifier.UtilityModifiers.Add(
    "drop-s",
    new Modifier(ModifierType.Misc, n => new PartialApplication(
        n.Name.Substring(0, n.Name.Length - 1),
        n.Rules.Except(new [] { GenreRule.DropsS })
    )));

var genres = new List<Genre>
{
    // "rumba",
    new Genre("dub"),
    new Genre("trance"),
    new Genre("house"),
    new Genre("techno"),
    new Genre("dnb"),
    new Genre("disco"),
    new Genre("breaks", GenreRule.DropsS),
    new Genre("dance"),
    new Genre("pop"),
    new Genre("electro"),
    new Genre("jungle"),
    // new Genre("room"),
    new Genre("moombahton"),
    new Genre("twerk"),
    new Genre("drill"),
    new Genre("synth"),
    new Genre("complextro"),
    new Genre("riddim"),
    new Genre("rnb"),

    new Genre("hard", GenreRule.NeedsSuffix)
};

string Generate(
    Genre genre,
    IEnumerable<Modifier> modifiers,
    Random random
)
{
    void Pick(
        List<Modifier> pickedModifiers,
        List<UniqueGroup?> usedGroups,
        Func<Modifier, bool>? filter = null
    )
    {
        var pick = modifiers
            .Where(m => filter != null ? filter(m) : true)
            .Except(pickedModifiers)
            .Where(m => !usedGroups.Contains(m.Group))
            .ElementAt(0);

        pickedModifiers.Add(pick);
        if (pick.Group != null)
            usedGroups.Add(pick.Group);
    }

    var pickedModifiers = new List<Modifier>();
    var usedGroups = new List<UniqueGroup?>();

    if (genre.Rules.Contains(GenreRule.NeedsSuffix))
    {
        Pick(pickedModifiers, usedGroups, m => m.Type == ModifierType.Suffix);
    }

    for (var i = 0; i < random.Next(1, 4); i++)
    {
        Pick(pickedModifiers, usedGroups);
    }

    var result = pickedModifiers
        .Take(2)
        .OrderBy(m => m.Type)
        .Aggregate((INode) genre, (s, m) => m.Apply(s));
    return result.Name;
}

for (var i = 0; i < 20; i++)
{
    // var root = genres.ElementAt(random.Next(0, genres.Count()));
    var root = genres.Find(g => g.Name == "breaks");
    Console.WriteLine(Generate(root, modifiers, random));
}

public interface INode
{
    public string Name { get; }
    public IEnumerable<GenreRule> Rules { get; }
}

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
    public static Dictionary<string, Modifier> UtilityModifiers = new ();

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