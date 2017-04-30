﻿// ****************************** Module Header ****************************** //
//
//
// Last Modified: 01:05:2017 / 01:18
// Creation: 29:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="KeybindManagerWindow.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Windows
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    using AstroSoundBoard.Core.Components;
    using AstroSoundBoard.Core.Objects.DataObjects;
    using AstroSoundBoard.Core.Objects.DataObjects.SoundDefinitionJsonTypes;
    using AstroSoundBoard.WPF.Controls.Keybind;

    using PropertyChanged;

    [ImplementPropertyChanged]
    public partial class KeybindManagerWindow : Window
    {
        private List<KeybindView> AllViews { get; set; } = new List<KeybindView>();

        public KeybindManagerWindow()
        {
            InitializeComponent();
            DataContext = this;

            foreach (Definition definition in SoundManager.Information.GetSoundList())
            {
                var item = new Sound
                {
                    Description = definition.Info.Description,
                    IsFavorite = SettingsManager.GetSound(definition.Sound.Name).IsFavorite,
                    Name = definition.Sound.Name,
                    VideoLink = definition.Info.VideoLink
                };

                var view = new KeybindView(item) { MinWidth = Width - 40 };

                ItemCtrl.Items.Add(view);
                AllViews.Add(view);
            }
        }

        private void RemoveAllKeybinds(object sender, RoutedEventArgs e) => KeybindManager.RemoveAllKeybinds();

        public void SearchForElement(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchBox.Text))
            {
                ItemCtrl.Items.Clear();

                foreach (KeybindView view in AllViews)
                {
                    ItemCtrl.Items.Add(view);
                }
            }
            else
            {
                List<KeybindView> matchingItems = new List<KeybindView>();

                foreach (KeybindView view in ItemCtrl.Items)
                {
                    if (view.LocalDefinition.Name.ToLower().Contains(SearchBox.Text.ToLower()))
                    {
                        matchingItems.Add(view);
                    }
                }

                ItemCtrl.Items.Clear();

                foreach (KeybindView view in matchingItems)
                {
                    ItemCtrl.Items.Add(view);
                }
            }
        }
    }
}