using Nemonuri.Ordinals.Tensors.Extensions;

namespace Nemonuri.Ordinals.Tensors;

public static partial class MinorizedOrdinalSegmentTheory
{
    public readonly ref struct NRangeSpanFactory
    {
        public MinorizedOrdinalSegmentSpan MinorizedOrdinalSegmentSpan {get;}

        internal NRangeSpanFactory(MinorizedOrdinalSegmentSpan minorizedOrdinalSegmentSpan)
        {
            MinorizedOrdinalSegmentSpan = minorizedOrdinalSegmentSpan;
        }

        public int NRangeSpanLength => MinorizedOrdinalSegmentSpan.Length;

        public Span<NRange> Create(Span<NRange> nRangeSpan)
        {
            Guard.IsEqualTo(NRangeSpanLength, nRangeSpan.Length);

            for (int i = 0; i < nRangeSpan.Length; i++)
            {
                nRangeSpan[i] = MinorizedOrdinalSegmentSpan[i].ToNRange();
            }

            return nRangeSpan;
        }
        
    }
}
