/**
Reference:
https://github.com/dotnet/runtime/blob/1d1bf92fcf43aa6981804dc53c5174445069c9e4/src/libraries/System.Private.CoreLib/src/System/ArraySegment.cs
*/

using System.Collections;
using CommunityToolkit.Diagnostics;

namespace Nemonuri.Monoids.FreeMonoids;

internal readonly struct ReadOnlyListSegment<T> : IReadOnlyList<T>
{
    private readonly IReadOnlyList<T>? _list;
    private readonly int _offset;
    private readonly int _count;

    public ReadOnlyListSegment(IReadOnlyList<T> list)
    {
        Guard.IsNotNull(list);

        _list = list;
        _offset = 0;
        _count = list.Count;
    }

    public ReadOnlyListSegment(IReadOnlyList<T> list, int offset, int count)
    {
        Guard.IsNotNull(list);
        Guard.IsGreaterThanOrEqualTo((uint)list.Count, (uint)offset);
        Guard.IsGreaterThanOrEqualTo((uint)count, (uint)(list.Count - offset));

        _list = list;
        _offset = offset;
        _count = count;
    }

    public IReadOnlyList<T>? List => _list;

    public int Offset => _offset;

    public int Count => _count;

    public T this[int index]
    {
        get
        {
            Guard.IsLessThan((uint)index, (uint)_count);

            return _list![_offset + index];
        }
    }

    public IEnumerator<T> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Enumerator : IEnumerator<T>
    {
        private readonly IReadOnlyList<T> _list;
        private readonly int _start;
        private readonly int _end;
        private int _current;

        internal Enumerator(ReadOnlyListSegment<T> readOnlyListSegment)
        {
            var list1 = readOnlyListSegment.List;
            Guard.IsNotNull(list1, "ReadOnlyListSegment<T>.List");

            _list = list1;
            _start = readOnlyListSegment.Offset;
            _end = readOnlyListSegment.Offset + readOnlyListSegment.Count;
            _current = readOnlyListSegment.Offset - 1;
        }

        public T Current
        {
            get
            {
                Guard.IsGreaterThanOrEqualTo(_current, _start);
                Guard.IsLessThan(_end, _current);
                return _list[_current];
            }
        }

        public bool MoveNext()
        {
            if (_current < _end)
            {
                _current++;
                return _current < _end;
            }
            return false;
        }

        public void Reset()
        {
            _current = _start - 1;
        }

        object? IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}