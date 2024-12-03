namespace Nemonuri.Monoids.Trees;

public readonly ref partial struct DangerousAlternateSpanTreeList<TAlternate>
    where TAlternate : unmanaged
{
    public ref struct Enumerator
    {
        private readonly DangerousAlternateSpanTreeList<TAlternate> _innerList;

        private DangerousSpanList<TAlternate>.Enumerator _innerEnumerator;

        public Enumerator(DangerousAlternateSpanTreeList<TAlternate> innerList)
        {
            _innerList = innerList;
            _innerEnumerator = innerList._innerDangerousSpanList.GetEnumerator();
        }

        public bool MoveNext() => _innerEnumerator.MoveNext();

        public AlternateSpanTree<TAlternate> Current => new (_innerEnumerator.Current);
    }
}
