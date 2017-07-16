﻿// ****************************** Module Header ****************************** //
//
//
// Last Modified: 16:07:2017 / 19:06
// Creation: 01:07:2017
// Project: AstroSoundBoard
//
//
// <copyright file="SoundView.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Controls.Sound
{
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;

    using AstroSoundBoard.Core.Components;
    using AstroSoundBoard.Core.Objects.Interfaces;
    using AstroSoundBoard.Core.Objects.Models;
    using AstroSoundBoard.Core.Utils.Extensions;
    using AstroSoundBoard.WPF.Windows;

    using log4net;

    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public partial class SoundView : UserControl, IAddableView
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public SoundViewModel Model { get; set; }

        public IAddableViewModel SoundModel
        {
            get => Model;
            set => Model = (SoundViewModel)value;
        }

        public SoundView(SoundModel def)
        {
            Log.Debug($"Creating Control for {def.Name}");

            Model = new SoundViewModel(def);
            Model.Sound.Name = Model.Sound.Name.ToDisplayName();

            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Toggles the Favorite Setting
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">E</param>
        private void ToggleFavorite(object sender, RoutedEventArgs e) => SettingsManager.Update(Model.Sound);

        /// <summary>
        /// Opens a new Window with Informations about the Sound
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">E</param>
        private void ShowInfo(object sender, RoutedEventArgs e)
        {
            var window = new InfoWindow(Model.Sound);
            window.Show();
        }

        /// <summary>
        /// Plays the Sound
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event</param>
        private void PlaySound(object sender, RoutedEventArgs e)
        {
            Model.Sound.PlaySound();
        }
    }
}