public static class IEnumerableExtensions
{
    public static IEnumerable<T> FisherYates<T>(this IEnumerable<T> source)
    {
        var random = new Random();
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
}