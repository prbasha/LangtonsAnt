using LangtonsAnt.Common;
using System;

namespace LangtonsAnt.Model
{
    /// <summary>
    /// The Cell class represents a single cell.
    /// </summary>
    public class Cell : NotificationBase
    {
        #region Fields

        private CellColour _cellColour;
        private bool _containsAnt;
        private Direction _antDirection;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Cell()
        {
            _cellColour = CellColour.White;
            _containsAnt = false;
            _antDirection = Direction.Up;
        }

        /// <summary>
        /// Constructor.
        /// Creates a cell with the provided colour.
        /// </summary>
        public Cell(CellColour cellColour)
        {
            _cellColour = cellColour;
            _containsAnt = false;
            _antDirection = Direction.Up;
        }
        
        #endregion

        #region Events
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the cell state.
        /// </summary>
        public CellColour CellColour
        {
            get
            {
                return _cellColour;
            }
            set
            {
                _cellColour = value;
                RaisePropertyChanged("CellColour");
            }
        }

        /// <summary>
        /// Gets or sets a boolean flag indicating if the ant is in this cell.
        /// </summary>
        public bool ContainsAnt
        {
            get
            {
                return _containsAnt;
            }
            set
            {
                _containsAnt = value;
                RaisePropertyChanged("ContainsAnt");
            }
        }

        /// <summary>
        /// Gets or sets the ant's direction.
        /// </summary>
        public Direction AntDirection
        {
            get
            {
                return _antDirection;
            }
            set
            {
                _antDirection = value;
                RaisePropertyChanged("AntDirection");
            }
        }

        #endregion

        #region Methods
        #endregion
    }
}
