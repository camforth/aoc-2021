using System.Collections;

namespace AOC;

public static class AocExtensions
{
    public static int CountWhile<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
    {
        var count = 0;
        foreach (var item in enumerable)
        {
            count++;
            if (!predicate(item)) break;
        }

        return Math.Max(1, count);
    }
    
    public static IEnumerable<int> GetRowValues(this int[,] grid, int row, int start, int end) 
        => Enumerable.Range(start, end - start).Select(i => grid[row, i]);

    public static IEnumerable<int> GetColumnValues(this int[,] grid, int column, int start, int end) 
        => Enumerable.Range(start, end - start).Select(i => grid[i, column]);

    public static CustomIntEnumerator GetEnumerator(this Range range)
        => new(range);
    
    public static IEnumerable<TResult> Select<TResult>(this Range range, Func<int, TResult> selector)
    {
        foreach (var item in range)
            yield return selector(item);
    }
}

public class CustomIntEnumerator 
{
    private int _current;
    private readonly int _end;

    public CustomIntEnumerator(Range range)
    {
        _current = range.Start.Value - 1;
        _end = range.End.Value;
    }

    public int Current => _current;

    public bool MoveNext()
    {
        _current++;
        return _current <= _end;
    }
}