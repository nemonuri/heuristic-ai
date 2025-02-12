namespace Nemonuri.Ordinals;

public readonly ref partial struct EnumerableFromZeroOrdinalSegmentSpan
{
    public ref struct Enumerator
    {
        private readonly EnumerableFromZeroOrdinalSegmentSpan _span;

        private bool _initialized;

        internal Enumerator(EnumerableFromZeroOrdinalSegmentSpan span)
        {
            _initialized = false;
            _span = span;
        }

        public bool MoveNext() => OrdinalTheory.MoveNext(ref _initialized, _span._innerOrdinalSpan, _span._cardinalitySpan);

        public readonly ReadOnlySpan<nint> Current => _span._innerOrdinalSpan;
    }
}