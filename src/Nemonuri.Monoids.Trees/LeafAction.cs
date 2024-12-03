namespace Nemonuri.Monoids.Trees;

public delegate void LeafAction<TUnmanagedAlternate, TLeaf, TExtraContext>
(
    TLeaf leaf,
    scoped ref readonly DecomposAndInvokeContext<TUnmanagedAlternate, TLeaf, TExtraContext> context
)
    where TUnmanagedAlternate : unmanaged
#if NET9_0_OR_GREATER
    where TLeaf : allows ref struct
    where TExtraContext : allows ref struct
#endif
;