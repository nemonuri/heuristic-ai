namespace Nemonuri.Tensors;

public delegate T TexturePixelValueFactory<out T>(ReadOnlySpan<nint> pixelIndexes) 
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
;