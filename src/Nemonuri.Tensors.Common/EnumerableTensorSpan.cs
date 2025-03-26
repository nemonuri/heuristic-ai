namespace Nemonuri.Tensors;

[Experimental(Experimentals.TensorTDiagId, UrlFormat = Experimentals.SharedUrlFormat)]
public readonly ref struct EnumerableTensorSpan<T>
{
    private readonly Span<T> _destination;
}