namespace System.Collections.Generic;

public interface ITreeNode
{
    public IList<ITreeNode> Children { get; set; }
}

public static partial class EnumerableExtensions
{
    public static IEnumerable<TSource> ToTreeNode<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TKey> relationKeySelector) where TSource : ITreeNode where TKey : notnull
    {
        var dic = source.ToDictionary(keySelector, v => v);

        foreach (var item in dic.Values)
        {
            if (dic.TryGetValue(relationKeySelector(item), out var value))
            {
                value.Children.Add(item);
            }
        }

        return dic.Values.Where(w => !dic.Values.Any(a => Equals(relationKeySelector(w), keySelector(a))));
    }

    public static IEnumerable<TSource> ToTreeNode<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TKey> relationKeySelector, TKey rootValue) where TSource : ITreeNode where TKey : notnull
    {
        var dic = source.ToDictionary(keySelector, v => v);

        foreach (var item in dic.Values)
        {
            if (dic.TryGetValue(relationKeySelector(item), out var value))
            {
                value.Children.Add(item);
            }
        }

        return dic.Values.Where(w => Equals(rootValue, relationKeySelector(w)));
    }

    public static IEnumerable<TSource> FilterNode<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) where TSource : ITreeNode
    {
        foreach (var item in source)
        {
            if (0 != item.Children.Count)
            {
                FilterNode(item.Children.OfType<TSource>(), predicate);
            }
            item.Children = item.Children?.Where(w => predicate((TSource)w) || 0 != w.Children.Count).ToList() ?? [];
        }

        return source.Where(w => predicate(w) || 0 != w.Children.Count);
    }
}