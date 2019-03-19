using LangtonsAnt.Common;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace LangtonsAnt.Model
{
    /// <summary>
    /// The Grid class represents the grid for the Langton's Ant implementation.
    /// </summary>
    public class Grid : NotificationBase
    {
        #region Fields

        private ObservableCollection<Cell> _gridCells;
        private int _stepIntervalMilliSeconds = 1;
        private DispatcherTimer _stepTimer;
        private bool _isGameRunning = false;
        private static object _updateLock = new object();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Grid()
        {
            try
            {
                BuildGrid();
            }
            catch (Exception ex)
            {
                throw new Exception("Grid(): " + ex.ToString());
            }
        }

        #endregion

        #region Events
        #endregion

        #region Properties

        /// <summary>
        /// Gets the width of the grid (in cells).
        /// </summary>
        public int GridWidthCells
        {
            get
            {
                return Constants.GridWidth;
            }
        }

        /// <summary>
        /// Gets the height of the grid (in cells).
        /// </summary>
        public int GridHeightCells
        {
            get
            {
                return Constants.GridHeight;
            }
        }

        /// <summary>
        /// Gets the collection of grid cells.
        /// </summary>
        public ObservableCollection<Cell> GridCells
        {
            get
            {
                if (_gridCells == null)
                {
                    _gridCells = new ObservableCollection<Cell>();
                }

                return _gridCells;
            }
            private set
            {
                if (value != null)
                {
                    _gridCells = new ObservableCollection<Cell>(value);
                    RaisePropertyChanged("GridCells");
                }
            }
        }

        /// <summary>
        /// Gets or sets the ant's direction.
        /// </summary>
        public Direction AntDirection
        {
            get
            {
                Direction antDirection = Direction.Up;

                lock (_updateLock)
                {
                    // Locate the cell containing the ant.
                    foreach (Cell gridCell in _gridCells)
                    {
                        if (!gridCell.ContainsAnt)
                        {
                            // The ant is not in this cell - move to the next cell.
                            continue;
                        }

                        // The cell containing the ant has been located - retrieve the ant's current direction.
                        antDirection = gridCell.AntDirection;

                        break;  // Exit the loop.
                    }
                }

                return antDirection;
            }
            set
            {
                if (!IsGameRunning)
                {
                    lock (_updateLock)
                    {
                        // Locate the cell containing the ant.
                        foreach (Cell gridCell in _gridCells)
                        {
                            if (!gridCell.ContainsAnt)
                            {
                                // The ant is not in this cell - move to the next cell.
                                continue;
                            }

                            // The cell containing the ant has been located - set the ant's current direction.
                            gridCell.AntDirection = value;
                            RaisePropertyChanged("AntDirection");

                            break;  // Exit the loop.
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the step interval is milli-seconds.
        /// </summary>
        public int StepIntervalMilliSeconds
        {
            get
            {
                return _stepIntervalMilliSeconds;
            }
            set
            {
                if (value >= Constants.MinimumStepIntervalMilliSeconds && value <= Constants.MaximumStepIntervalMilliSeconds)
                {
                    _stepIntervalMilliSeconds = value;
                    RaisePropertyChanged("StepIntervalMilliSeconds");

                    if (_stepTimer != null && _stepTimer.IsEnabled)
                    {
                        // Update the step timer interval.
                        _stepTimer.Interval = TimeSpan.FromMilliseconds(_stepIntervalMilliSeconds);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the game running boolean flag.
        /// </summary>
        public bool IsGameRunning
        {
            get
            {
                return _isGameRunning;
            }
            private set
            {
                _isGameRunning = value;
                RaisePropertyChanged("IsGameRunning");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The BuildGrid method is called to build (initialise) the grid.
        /// </summary>
        private void BuildGrid()
        {
            try
            {
                if (!IsGameRunning)
                {
                    _gridCells = new ObservableCollection<Cell>();
                    while (_gridCells.Count != Constants.GridWidth * Constants.GridHeight)
                    {
                        _gridCells.Add(new Cell(CellColour.White));
                    }

                    // Place the ant at/near the centre of the grid.
                    int xPosition = Constants.GridWidth / 2;
                    int yPosition = Constants.GridHeight / 2;
                    int cellIndex = xPosition + (yPosition * Constants.GridWidth);
                    _gridCells.ElementAt(cellIndex).ContainsAnt = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Grid.StartGame(): " + ex.ToString());
            }
        }

        /// <summary>
        /// The BuildGrid method is called to reset the grid.
        /// All grid cells are set to white.
        /// The ant is placed at/near the centre of the grid.
        /// </summary>
        private void ResetGrid()
        {
            try
            {
                if (!IsGameRunning)
                {
                    // Set all cells to white.
                    foreach (Cell gridCell in _gridCells)
                    {
                        gridCell.CellColour = CellColour.White;

                        // If the cell contains the ant, remove the ant.
                        if (gridCell.ContainsAnt)
                        {
                            gridCell.ContainsAnt = false;
                        }
                    }

                    // Place the ant at/near the centre of the grid.
                    int xPosition = Constants.GridWidth / 2;
                    int yPosition = Constants.GridHeight / 2;
                    int cellIndex = xPosition + (yPosition * Constants.GridWidth);
                    _gridCells.ElementAt(cellIndex).ContainsAnt = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Grid.StartGame(): " + ex.ToString());
            }
        }

        /// <summary>
        /// The StartGame method is called to start the game.
        /// </summary>
        public void StartGame()
        {
            try
            {
                if (!IsGameRunning)
                {
                    // Start the step timer.
                    _stepTimer = new DispatcherTimer();
                    _stepTimer.Interval = TimeSpan.FromMilliseconds(_stepIntervalMilliSeconds);
                    _stepTimer.Tick += new EventHandler(StepTimerEventHandler);
                    _stepTimer.Start();

                    IsGameRunning = true; // Set the simulation running flag.
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Grid.StartGame(): " + ex.ToString());
            }
        }

        /// <summary>
        /// The StopGame method is called to stop the game.
        /// </summary>
        public void StopGame()
        {
            try
            {
                // Stop the step timer.
                if (_stepTimer != null && _stepTimer.IsEnabled)
                {
                    _stepTimer.Stop();
                }

                // Clear the simulation running flag.
                if (IsGameRunning)
                {
                    IsGameRunning = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Grid.StopGame(): " + ex.ToString());
            }
        }

        /// <summary>
        /// The StepGame method is called to step through one iteration of the game.
        /// </summary>
        public async Task StepGame()
        {
            try
            {
                // Clear the simulation running flag.
                if (!IsGameRunning)
                {
                    await UpdateGrid();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Grid.StepGame(): " + ex.ToString());
            }
        }

        /// <summary>
        /// The ResetGame method is called to reset the game.
        /// </summary>
        public void ResetGame()
        {
            try
            {
                StopGame();     // Stop the game (if it is running).  
                ResetGrid();    // Reset the grid.
            }
            catch (Exception ex)
            {
                throw new Exception("Grid.ResetGame(): " + ex.ToString());
            }
        }
        
        /// <summary>
        /// The StepTimerEventHandler method is called when the step timer expires.
        /// It updates the current state of the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void StepTimerEventHandler(object sender, EventArgs e)
        {
            try
            {
                if (IsGameRunning)
                {
                    await UpdateGrid();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Grid.StepTimerEventHandler(object sender, EventArgs e): " + ex.ToString());
            }
        }

        /// <summary>
        /// The UpdateGrid method is called to update the grid.
        /// Each cell is updated as per the rules for Langton's Ant.
        /// For more information: https://rosettacode.org/wiki/Langton%27s_ant
        /// </summary>
        private async Task UpdateGrid()
        {
            try
            {
                bool stopGame = false;  // Stop game flag.

                await Task.Run(() =>
                {
                    lock (_updateLock)
                    {
                        // Update the grid cells, one cell at a time.
                        foreach (Cell gridCell in _gridCells)
                        {
                            if (!gridCell.ContainsAnt)
                            {
                                // The ant is not in this cell - move to the next cell.
                                continue;
                            }

                            // The cell containing the ant has been located.

                            int cellIndex = _gridCells.IndexOf(gridCell);   // Retrieve the index of the cell.

                            // Determine if the cell is on the top/right/bottom/left edge of the grid.
                            bool topEdge = cellIndex < Constants.GridWidth ? true : false;
                            bool rightEdge = ((cellIndex + 1) % Constants.GridWidth) == 0 ? true : false;
                            bool leftEdge = (cellIndex % Constants.GridWidth) == 0 ? true : false;
                            bool bottomEdge = (cellIndex + Constants.GridWidth) >= (Constants.GridWidth * Constants.GridHeight) ? true : false;

                            // If the ant has reached the edge of the grid, the game is over.
                            if (topEdge || rightEdge || leftEdge || bottomEdge)
                            {
                                stopGame = true;
                                break;
                            }

                            // Determine the indexes for all of the cell neighbours.
                            int topNeighbourIndex = cellIndex - Constants.GridWidth;
                            int rightNeighbourIndex = cellIndex + 1;
                            int bottomNeighbourIndex = cellIndex + Constants.GridWidth;
                            int leftNeighbourIndex = cellIndex - 1;

                            // Change the cell colour and turn the ant.
                            if (gridCell.CellColour == CellColour.Black)
                            {
                                // Black changes to white.
                                gridCell.CellColour = CellColour.White;
                                // Turn the ant left.
                                switch (gridCell.AntDirection)
                                {
                                    case Direction.Up:
                                        gridCell.AntDirection = Direction.Left;
                                        break;
                                    case Direction.Right:
                                        gridCell.AntDirection = Direction.Up;
                                        break;
                                    case Direction.Down:
                                        gridCell.AntDirection = Direction.Right;
                                        break;
                                    case Direction.Left:
                                        gridCell.AntDirection = Direction.Down;
                                        break;
                                }
                            }
                            else if (gridCell.CellColour == CellColour.White)
                            {
                                // White changes to black.
                                gridCell.CellColour = CellColour.Black;
                                // Turn the ant right.
                                switch (gridCell.AntDirection)
                                {
                                    case Direction.Up:
                                        gridCell.AntDirection = Direction.Right;
                                        break;
                                    case Direction.Right:
                                        gridCell.AntDirection = Direction.Down;
                                        break;
                                    case Direction.Down:
                                        gridCell.AntDirection = Direction.Left;
                                        break;
                                    case Direction.Left:
                                        gridCell.AntDirection = Direction.Up;
                                        break;
                                }
                            }

                            // Move the ant forward.
                            gridCell.ContainsAnt = false;
                            switch (gridCell.AntDirection)
                            {
                                case Direction.Up:
                                    _gridCells[topNeighbourIndex].ContainsAnt = true;
                                    _gridCells[topNeighbourIndex].AntDirection = gridCell.AntDirection;
                                    break;
                                case Direction.Right:
                                    _gridCells[rightNeighbourIndex].ContainsAnt = true;
                                    _gridCells[rightNeighbourIndex].AntDirection = gridCell.AntDirection;
                                    break;
                                case Direction.Down:
                                    _gridCells[bottomNeighbourIndex].ContainsAnt = true;
                                    _gridCells[bottomNeighbourIndex].AntDirection = gridCell.AntDirection;
                                    break;
                                case Direction.Left:
                                    _gridCells[leftNeighbourIndex].ContainsAnt = true;
                                    _gridCells[leftNeighbourIndex].AntDirection = gridCell.AntDirection;
                                    break;
                            }

                            break;  // The ant has been located and updated - this iteration is complete.
                        }
                    }
                });

                if (stopGame)
                {
                    StopGame();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Grid.UpdateGrid(): " + ex.ToString());
            }
        }

        /// <summary>
        /// The IsCellIndexValid method is called to determine if a provided cell index is within range of the grid cell collection.
        /// </summary>
        /// <param name="cellIndex"></param>
        /// <returns></returns>
        private bool IsCellIndexValid(int cellIndex)
        {
            return cellIndex >= 0 && cellIndex < _gridCells.Count;
        }
        
        #endregion
    }
}
