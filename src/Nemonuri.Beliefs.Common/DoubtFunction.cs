namespace Nemonuri.Beliefs;

public delegate double DoubtFunction(uint index);

public delegate double Doubt2DFunction(uint currentIndex, uint nextIndex);

public delegate double DoubtFunctional
(
#region Prev doubt functions
    DoubtFunction[] prevDoubtFunctions, // n
    uint[] prevIndexes, // n
#endregion Prev doubt functions

#region Current doubt function
    DoubtFunction currentDoubtFunction,
    uint currentIndex,
#endregion Current doubt function

#region Next doubt function union
    DoubtFunction? nextDoubtFunction,
    Doubt2DFunction? nextDoubt2DFunction,
#endregion Next doubt function union
    uint nextIndex
);
