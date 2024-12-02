namespace Nemonuri.Ordinals;

public readonly ref partial struct EnumerableOrdinalSegmentSpan
{
    public readonly ref struct Enumerator
    {
        private readonly EnumerableOrdinalSegmentSpan _span;

        private readonly EnumerableFromZeroOrdinalSegmentSpan.Enumerator _fromZeroEnumerator;

        internal Enumerator(EnumerableOrdinalSegmentSpan span)
        {
            _span = span;
            _fromZeroEnumerator = _span._enumerableFromZeroOrdinalSegmentSpan.GetEnumerator();
        }

        public bool MoveNext() => _fromZeroEnumerator.MoveNext();

        public ReadOnlySpan<nint> Current
        {
            get
            {
                ReadOnlySpan<nint> currentFromZeroOrdinalSpan = _fromZeroEnumerator.Current;
                for (int i = 0; i < _span.Length; i++)
                {
                    _span._innerOrdinalSpan[i] = currentFromZeroOrdinalSpan[i] + _span._minimumOrdinalSpan[i];
                }
                return _span._innerOrdinalSpan;
            }
        }
    }
}