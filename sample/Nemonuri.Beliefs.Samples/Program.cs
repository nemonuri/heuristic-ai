using ScottPlot;
using ScottPlot.Plottables;
using Nemonuri.Beliefs;

Plot myPlot = new();

DoubtFunctionalUnion doubtFunctionalUnion = new (DistanceDoubtFunctional1D);
DoubtFunction doubtFunction = static index => index;

var v1 = doubtFunctionalUnion.CreateDoubtFunctions
(
    prevDoubtFunctionInfos: [],
    currentDoubtFunction: doubtFunction.Composit(d => d-5),
    currentDoubtFunctionCardinality: 10,
    nextDoubtFunctionUnion: new (doubtFunction.Composit(d => d-5)),
    nextDoubtFunctionCardinality: 10,
    out double[][] innerDoubtArrays
);

double[,] data = innerDoubtArrays.To2DArray();
Heatmap heatmap = myPlot.Add.Heatmap(data);
heatmap.Colormap = new ScottPlot.Colormaps.Turbo();

myPlot.Add.ColorBar(heatmap);

myPlot.SavePng("demo.png", 400, 300);


static double DistanceDoubtFunctional1D
(
    DoubtFunction[] prevDoubtFunctions,
    uint[] prevIndexes,
    DoubtFunction currentDoubtFunction,
    uint currentIndex,
    DoubtFunction nextDoubtFunction,
    uint nextIndex
)
{
    double d0 = currentDoubtFunction(currentIndex);
    double d1 = nextDoubtFunction(nextIndex);

    return Math.Sqrt(d0*d0 + d1*d1);
}