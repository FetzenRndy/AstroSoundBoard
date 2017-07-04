﻿// ****************************** Module Header ****************************** //
//
//
// Last Modified: 04:07:2017 / 20:30
// Creation: 01:07:2017
// Project: AstroSoundBoard
//
//
// <copyright file="ItemManager.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Components
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;

    using AstroSoundBoard.Core.Objects.Interfaces;
    using AstroSoundBoard.Core.Objects.Models;

    using Newtonsoft.Json;

    public class ItemManager<TView>
        where TView : class, IAddableView
    {
        private List<SoundModel> models = new List<SoundModel>();
        private readonly List<TView> views = new List<TView>();
        private readonly Func<SoundModel, TView> creationDelegate;
        private bool CurrentFavoriteStatus { get; set; }

        public ItemManager(Func<SoundModel, TView> creationDelegate)
        {
            this.creationDelegate = creationDelegate;
            Reload();
        }

        private void Reload()
        {
            LoadModels();
            LoadViews();
        }

        private void LoadModels()
        {
            models = SettingsManager.GetSounds();
        }

        private void LoadViews()
        {
            views.Clear();
            foreach (SoundModel model in models)
            {
                views.Add(creationDelegate(model));
            }
        }

        public void Search(ref ItemsControl itemControl, string element)
        {
            itemControl.Items.Filter = item => Filter(item as TView);

            bool Filter(IAddableView model)
            {
                if (model == null)
                {
                    return false;
                }

                return model.SoundModel.Sound.Name.ToLower().Contains(element.ToLower());
            }
        }

        public void ToogleFavorites(ref ItemsControl itemControl)
        {
            CurrentFavoriteStatus = !CurrentFavoriteStatus;

            itemControl.Items.Filter = item => Filter(item as TView);

            bool Filter(IAddableView model)
            {
                if (model == null)
                {
                    return false;
                }

                bool isFavorite = model.SoundModel.Sound.IsFavorite == JsonConvert.True;
                if (CurrentFavoriteStatus)
                {
                    return isFavorite;
                }
                else
                {
                    return !isFavorite;
                }
            }
        }

        public void SetAll(ref ItemsControl itemControl)
        {
            Reload();
            try
            {
                itemControl.Items.Clear();
                foreach (TView view in views)
                {
                    itemControl.Items.Add(view);
                }
            }
            catch
            {
                // Eat
            }
        }
    }
}