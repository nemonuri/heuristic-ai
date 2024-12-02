namespace Nemonuri.ManagedPointers.Tensors;

public readonly ref partial struct DangerousSlicedReadOnlyTensorSpanList<T>
{
    public ref struct Enumerator
    {
        private readonly DangerousSlicedReadOnlyTensorSpanList<T> _spanList;

        private DangerousSpanList<NRange>.Enumerator _innerEnumerator;

        internal Enumerator(DangerousSlicedReadOnlyTensorSpanList<T> spanList)
        {
            _spanList = spanList;
            _innerEnumerator = _spanList._innerNRangeDangerousSpanList.GetEnumerator();
        }
        
        public bool MoveNext() => _innerEnumerator.MoveNext();

        public ReadOnlyTensorSpan<T> Current => _spanList[_innerEnumerator.Current];
        
    }
}
