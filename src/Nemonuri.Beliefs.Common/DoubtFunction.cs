namespace Nemonuri.Beliefs;

public delegate double DoubtFunction(uint index);

public delegate double DoubtFunctional
(
    DoubtFunction[] prevDoubtFunctions, // n
    uint[] prevIndexes, // n
    DoubtFunction currentDoubtFunction,
    uint currentIndex,
    DoubtFunction? nextDoubtFunction,
    uint nextIndex
);
