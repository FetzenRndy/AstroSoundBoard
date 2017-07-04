﻿// ****************************** Module Header ****************************** //
//
//
// Last Modified: 04:07:2017 / 17:05
// Creation: 01:07:2017
// Project: AstroSoundBoard
//
//
// <copyright file="KeybindViewModel.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Controls.Keybind
{
    using AstroSoundBoard.Core.Objects.Interfaces;
    using AstroSoundBoard.Core.Objects.Models;

    public class KeybindViewModel : IAddableViewModel
    {
        public SoundModel Sound { get; set; }

        public KeybindViewModel(SoundModel modelDef)
        {
            Sound = modelDef;
        }
    }
}