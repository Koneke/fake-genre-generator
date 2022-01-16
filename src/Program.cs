public class Program
{
    public static void Main(string[] args)
    {
        var queue = new Queue<Modifier>();

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
                .Aggregate((INode)genre, (s, m) => m.Apply(s));
            return result.Name;
        }

        Modifier.UtilityModifiers.Add(
            "drop-s",
            new Modifier(ModifierType.Misc, n => new PartialApplication(
                n.Name.Substring(0, n.Name.Length - 1),
                n.Rules.Except(new[] { GenreRule.DropsS })
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

        var random = new Random();
        var modifiers = Data.Modifiers.FisherYates();

        for (var i = 0; i < 20; i++)
        {
            var root = genres.ElementAt(random.Next(0, genres.Count()));
            Console.WriteLine(Generate(root, modifiers, random));
        }
    }
}
