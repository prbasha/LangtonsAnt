using LangtonsAnt.Common;
using LangtonsAnt.Model;
using System;
using System.ComponentModel;

namespace LangtonsAnt.ViewModel
{
    /// <summary>
    /// The LangtonsAntViewModel class represents the View Model for the Langton's Ant implementation.
    /// </summary>
    public class LangtonsAntViewModel : NotificationBase
    {
        #region Fields
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LangtonsAntViewModel()
        {
            try
            {
                Grid = new Grid();  // Initialise the model class.
                Grid.PropertyChanged += OnGridPropertyChanged;

                // Initialise commands.
                StartGameCommand = new DelegateCommand(OnStartGame, CanStartGame);
                StopGameCommand = new DelegateCommand(OnStopGame, CanStopGame);
                StepGameCommand = new DelegateCommand(OnStepGame, CanStepGame);
                ResetGameCommand = new DelegateCommand(OnResetGame);
            }
            catch (Exception ex)
            {
                throw new Exception("LangtonsAntViewModel(): " + ex.ToString());
            }
        }

        #endregion

        #region Events
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the grid.
        /// </summary>
        public Grid Grid { get; }

        /// <summary>
        /// Gets or sets the start game command.
        /// </summary>
        public DelegateCommand StartGameCommand { get; private set; }

        /// <summary>
        /// Gets or sets the stop game command.
        /// </summary>
        public DelegateCommand StopGameCommand { get; private set; }

        /// <summary>
        /// Gets or sets the step game command.
        /// </summary>
        public DelegateCommand StepGameCommand { get; private set; }

        /// <summary>
        /// Gets or sets the reset game command.
        /// </summary>
        public DelegateCommand ResetGameCommand { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// The OnStartGame method is called to start the game.
        /// </summary>
        /// <param name="arg"></param>
        public void OnStartGame(object arg)
        {
            try
            {
                if (Grid != null)
                {
                    Grid.StartGame();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("LangtonsAntViewModel.OnStartGame(object arg): " + ex.ToString());
            }
        }

        /// <summary>
        /// The CanStartGame method is callled to determine if the game can start.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public bool CanStartGame(object arg)
        {
            return Grid != null && !Grid.IsGameRunning;
        }

        /// <summary>
        /// The OnStopGame method is called to stop the game.
        /// </summary>
        /// <param name="arg"></param>
        public void OnStopGame(object arg)
        {
            try
            {
                if (Grid != null)
                {
                    Grid.StopGame();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("LangtonsAntViewModel.OnStopGame(object arg): " + ex.ToString());
            }
        }

        /// <summary>
        /// The CanStopGame method is callled to determine if the game can stop.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public bool CanStopGame(object arg)
        {
            return Grid != null && Grid.IsGameRunning;
        }

        /// <summary>
        /// The OnStepGame method is called to step the game.
        /// </summary>
        /// <param name="arg"></param>
        public async void OnStepGame(object arg)
        {
            try
            {
                if (Grid != null)
                {
                    await Grid.StepGame();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("LangtonsAntViewModel.OnStepGame(object arg): " + ex.ToString());
            }
        }

        /// <summary>
        /// The CanStepGame method is callled to determine if the game can step.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public bool CanStepGame(object arg)
        {
            return Grid != null && !Grid.IsGameRunning;
        }

        /// <summary>
        /// The OnResetGame method is called to reset the game.
        /// </summary>
        /// <param name="arg"></param>
        public void OnResetGame(object arg)
        {
            try
            {
                if (Grid != null)
                {
                    Grid.ResetGame();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("LangtonsAntViewModel.OnResetGame(object arg): " + ex.ToString());
            }
        }

        /// <summary>
        /// The OnGridPropertyChanged method is called when a property in the Grid model class changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGridPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                StartGameCommand.RaiseCanExecuteChanged();
                StopGameCommand.RaiseCanExecuteChanged();
                StepGameCommand.RaiseCanExecuteChanged();
                ResetGameCommand.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                throw new Exception("LangtonsAntViewModel.OnGridPropertyChanged(object sender, PropertyChangedEventArgs e): " + ex.ToString());
            }
        }

        #endregion
    }
}
