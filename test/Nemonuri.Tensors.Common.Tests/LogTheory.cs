namespace Nemonuri.Tensors.Common.Tests;

internal static class LogTheory
{
    public static string ToLogString<T>(T[] source)
    {
        return $"[{string.Join(", ", source)}]";
    }
    
    public static string ToLogString<T>(Span<T> source) => ToLogString((ReadOnlySpan<T>)source);

    public static string ToLogString<T>(ReadOnlySpan<T> source) => ToLogString(source.ToArray());
}