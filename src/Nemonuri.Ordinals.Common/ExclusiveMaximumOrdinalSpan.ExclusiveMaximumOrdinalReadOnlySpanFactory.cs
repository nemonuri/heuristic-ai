namespace Nemonuri.Ordinals;

public readonly ref partial struct ExclusiveMaximumOrdinalSpan
{
    public readonly ref struct ExclusiveMaximumOrdinalReadOnlySpanFactory
    {
        public ExclusiveMaximumOrdinalSpan ExclusiveMaximumOrdinalSpan {get;}

        internal ExclusiveMaximumOrdinalReadOnlySpanFactory(ExclusiveMaximumOrdinalSpan exclusiveMaximumOrdinalSpan)
        {
            ExclusiveMaximumOrdinalSpan = exclusiveMaximumOrdinalSpan;
        }

        public int ExclusiveMaximumOrdinalSpanLength => ExclusiveMaximumOrdinalSpan.Length;

        public ExclusiveMaximumOrdinalReadOnlySpan Create(Span<ExclusiveMaximumOrdinal> exclusiveMaximumOrdinalSpan)
        {
            Guard.IsEqualTo(ExclusiveMaximumOrdinalSpanLength, exclusiveMaximumOrdinalSpan.Length);

            ExclusiveMaximumOrdinalSpan.InnerSpan.CopyTo(exclusiveMaximumOrdinalSpan);
            return new ExclusiveMaximumOrdinalReadOnlySpan(exclusiveMaximumOrdinalSpan);
        }
    }
}
