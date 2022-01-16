public static class Data
{
    public static IEnumerable<Modifier> Modifiers = new List<Modifier>
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
    };
}