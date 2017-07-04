﻿// ****************************** Module Header ****************************** //
// 
// 
// Last Modified: 04:07:2017 / 20:27
// Creation: 01:07:2017
// Project: AstroSoundBoard
// 
// 
// <copyright file="BoardView.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //



namespace AstroSoundBoard.WPF.Pages.Board
{
    using System.Reflection;
    using System.Windows.Controls;

    using AstroSoundBoard.Core.Components;
    using AstroSoundBoard.WPF.Controls.Sound;

    using log4net;

    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public partial class BoardView : UserControl
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly ItemManager<SoundView> ItemManager = new ItemManager<SoundView>(model => new SoundView(model));

        public static BoardView BoardViewInstance { get; set; }

        public BoardView()
        {
            BoardViewInstance = this;

            InitializeComponent();
            DataContext = this;

            ItemManager.SetAll(ref ItemCtrl);
        }

        public void OnlyShowFavorites()
        {
            ItemManager.ToogleFavorites(ref ItemCtrl);
        }

        public void SearchForElement(string element)
        {
            ItemManager.Search(ref ItemCtrl, element);
        }
    }
}