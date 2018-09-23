# LangtonsAnt
An implementation of Langton's ant. Implemented in C# .NET WPF.

The play area is a square grid of cells. At any one time, a cell can be black or white.

The ant begins in one cell (ideally at/near the center of the grid), facing one of four directions:
* Up
* Down
* Left
* Right

During execution, the ant will move based on the cell it is currently in.

The execution loop:
1. Identify the colour of the cell the ant is in:
   1. If the cell is black, the cell becomes white, and the ant turns left.
   2. If the cell is white, the cell becomes black, and the ant turns right.
2. The ant moves forward to the next cell.
3. Determine if the ant has hit the edge of the grid:
   1. If the ant has hit the edge of the grid, exit the loop.
   2. If the ant has not hit the edge of the grid, go to step 1.

For more information: https://rosettacode.org/wiki/Langton%27s_ant
