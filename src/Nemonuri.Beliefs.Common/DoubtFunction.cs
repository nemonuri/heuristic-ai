namespace Nemonuri.Beliefs;

public delegate double DoubtFunction(uint index);

public delegate double DoubtFunctional
(
    uint[] prevIndexes, // n
    DoubtFunction[] prevDoubtFunctions, // n
    uint currentIndex,
    DoubtFunction currentDoubtFunction,
    uint index
);
