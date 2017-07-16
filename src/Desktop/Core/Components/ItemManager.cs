﻿// ****************************** Module Header ****************************** //
//
//
// Last Modified: 16:07:2017 / 18:45
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
    using System.Diagnostics;
    using System.Windows.Controls;

    using AstroSoundBoard.Core.Objects;
    using AstroSoundBoard.Core.Objects.Interfaces;
    using AstroSoundBoard.Core.Objects.Models;

    using FetzDeLib.Extensions;

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
            /* BEGIN EASTER EGG CODE */
            string[] links =
                {
                    "http://stackoverflow.com/admin.php",
                    "https://youtu.be/dWja_zn_ftc",
                    "https://youtu.be/LVGRAd8TuaQ",
                    "https://youtu.be/k7VcSMr3hbc",
                    "https://youtu.be/K5tVbVu9Mkg"
                };

            if (element.ToLower().EqualsAnyOf("static", "void", "main", "public"))
            {
                Process.Start(links[AppSettings.Rng.Next(0, links.Length)]);
            }

            /* END EASTER EGG CODE */
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