﻿// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.PowerToys.Settings.UI.Library;
using Microsoft.PowerToys.Settings.UI.OOBE.Enums;
using Microsoft.PowerToys.Settings.UI.OOBE.ViewModel;
using Microsoft.PowerToys.Settings.UI.Views;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Microsoft.PowerToys.Settings.UI.OOBE.Views
{
    public sealed partial class OobeOverviewAlternate : Page
    {
        public OobePowerToysModule ViewModel { get; set; }

        public OobeOverviewAlternate()
        {
            this.InitializeComponent();
            ViewModel = new OobePowerToysModule(OobeShellPage.OobeShellHandler.Modules[(int)PowerToysModules.Overview]);
            DataContext = ViewModel;

            FancyZonesHotkeyControl.Keys = SettingsRepository<FancyZonesSettings>.GetInstance(new SettingsUtils()).SettingsConfig.Properties.FancyzonesEditorHotkey.Value.GetKeysList();
            RunHotkeyControl.Keys = SettingsRepository<PowerLauncherSettings>.GetInstance(new SettingsUtils()).SettingsConfig.Properties.OpenPowerLauncher.GetKeysList();
            ColorPickerHotkeyControl.Keys = SettingsRepository<ColorPickerSettings>.GetInstance(new SettingsUtils()).SettingsConfig.Properties.ActivationShortcut.GetKeysList();
            AlwaysOnTopHotkeyControl.Keys = SettingsRepository<AlwaysOnTopSettings>.GetInstance(new SettingsUtils()).SettingsConfig.Properties.Hotkey.Value.GetKeysList();
        }

        private void SettingsLaunchButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (OobeShellPage.OpenMainWindowCallback != null)
            {
                OobeShellPage.OpenMainWindowCallback(typeof(GeneralPage));
            }

            ViewModel.LogOpeningSettingsEvent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.LogOpeningModuleEvent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModel.LogClosingModuleEvent();
        }
    }
}
