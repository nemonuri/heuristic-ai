namespace Nemonuri.Ordinals;

public readonly ref partial struct EnumerableOrdinalSegmentSpan
{
    public readonly ref struct Enumerator
    {
        private readonly EnumerableOrdinalSegmentSpan _span;

        private readonly EnumerableMinorizedOrdinalSegmentSpan.Enumerator _innerEnumerator;

        internal Enumerator(EnumerableOrdinalSegmentSpan span)
        {
            _span = span;
            _innerEnumerator = _span._enumerableMinorizedOrdinalSegmentSpan.GetEnumerator();
        }

        public bool MoveNext() => _innerEnumerator.MoveNext();

        public ReadOnlySpan<nint> Current
        {
            get
            {
                ReadOnlySpan<nint> currentMinorizedOrdinal = _innerEnumerator.Current;
                for (int i = 0; i < _span.Length; i++)
                {
                    _span._innerOrdinalSpan[i] = currentMinorizedOrdinal[i] + _span._minimumOrdinalSpan[i];
                }
                return _span._innerOrdinalSpan;
            }
        }
    }
}