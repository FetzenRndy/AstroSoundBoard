﻿// ****************************** Module Header ****************************** //
//
//
// Last Modified: 18:11:2017 / 15:06
// Creation: 18:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="KeybindView.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Views.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using AstroSoundBoard.Models.DataModels;
    using AstroSoundBoard.Objects.Interfaces;
    using AstroSoundBoard.Services;
    using AstroSoundBoard.Views.Windows;

    using log4net;

    public partial class KeybindView : UserControl, IAddableView
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public KeybindViewModel Model { get; set; }
        public IAddableViewModel SoundModel
        {
            get => Model;
            set => Model = (KeybindViewModel)value;
        }

        public KeybindView(SoundModel soundModel)
        {
            Model = new KeybindViewModel(soundModel);

            InitializeComponent();
            DataContext = this;

            Log.Debug(Model.Sound.Name + " " + Model.Sound.HotKey.HasAssignedKeybind);

            Model.Sound.HotKey.PropertyChanged += (sender, args) => { CurrentKeybindPanel.Visibility = Model.Sound.HotKey.HasAssignedKeybind ? Visibility.Visible : Visibility.Hidden; };
            Model.Sound.HotKey.NotifyOfPropertyChange();
        }

        public void ConfigureKeybind(object sender, RoutedEventArgs e)
        {
            if (!KeybindConfiguratorWindow.HasOpenInstance)
            {
                new KeybindConfiguratorWindow(Model.Sound).Show();
            }
            else
            {
                MessageBox.Show("Only one instance of the Configuration Window allowed at a given Time... \nPlease close the other one.", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void RemoveKeybind(object sender, RoutedEventArgs e)
        {
            int index = SettingsManager.Cache.FindIndex(cacheSound => cacheSound.Name == Model.Sound.Name);
            SettingsManager.Cache[index].HotKey = new KeyBind();

            KeybindManager.SetKeybinds();
        }
    }
}