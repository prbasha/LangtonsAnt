using LangtonsAnt.ViewModel;
using System.Windows;

namespace LangtonsAnt.View
{
    /// <summary>
    /// The LangtonsAntView class represents the View for the Langton's Ant implementation.
    /// </summary>
    public partial class LangtonsAntView : Window
    {
        #region Fields
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LangtonsAntView()
        {
            InitializeComponent();

            // Create the View Model.
            LangtonsAntViewModel viewModel = new LangtonsAntViewModel();
            DataContext = viewModel;    // Set the data context for all data binding operations.
        }

        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Methods
        #endregion
    }
}
