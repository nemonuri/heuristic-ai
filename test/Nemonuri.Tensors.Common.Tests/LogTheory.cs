namespace Nemonuri.Tensors.Common.Tests;

internal static class LogTheory
{
    public static string ConvertSpanToLogString<T>(T[] source)
    {
        return $"[{string.Join(", ", source)}]";
    }
    
    public static string ConvertSpanToLogString<T>(Span<T> source) => ConvertSpanToLogString((ReadOnlySpan<T>)source);

    public static string ConvertSpanToLogString<T>(ReadOnlySpan<T> source) => ConvertSpanToLogString(source.ToArray());
}