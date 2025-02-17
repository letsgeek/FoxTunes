﻿using FoxTunes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoxTunes
{
    public static class WindowsUserInterfaceConfiguration
    {
        public const string SECTION = "0047011D-7C95-4EDE-A4DE-B839CF05E9AB";

        public const string THEME_ELEMENT = "AAAA9DEE-1168-4D96-9355-31ECC0666820";

        public const string LAYOUT_ELEMENT = "BBBB9A67-F909-49EA-A4D3-6E26659A5797";

        public const string MARQUEE_INTERVAL_ELEMENT = "JJJJ685A-4D15-4AE1-B7AD-3E5786CB8EDB";

        public const string MARQUEE_STEP_ELEMENT = "KKKKDCB3-69C3-4F73-966C-6A7738E359A1";

        public const string SHOW_CURSOR_ADORNERS_ELEMENT = "NNNN7E23-A1E4-4BB6-9291-B553F4F7AD12";

        public const string TIMER_FREQUENCY = "MMMMB95C-C67B-4D27-858A-225E2003503B";

        public const int DEFAULT_TIMER_FREQUENCY = 200;

        public const int MIN_TIMER_FREQUENCY = 10;

        public const int MAX_TIMER_FREQUENCY = 1000;

        public const string TRANSPARENCY = "NNNN0D57-6E03-4718-ACA3-AD8199F5AC75";

        public const string TRANSPARENCY_PROVIDER = "NNOOE002-E835-443B-9C30-6E6530215B13";

        public static IEnumerable<ConfigurationSection> GetConfigurationSections()
        {
            yield return new ConfigurationSection(SECTION, Strings.WindowsUserInterfaceConfiguration_Section)
                .WithElement(
                    new SelectionConfigurationElement(THEME_ELEMENT, Strings.WindowsUserInterfaceConfiguration_Theme).WithOptions(GetThemeOptions()))
                .WithElement(
                    new SelectionConfigurationElement(LAYOUT_ELEMENT, Strings.WindowsUserInterfaceConfiguration_Layout).WithOptions(GetLayoutOptions()))
                .WithElement(
                    new IntegerConfigurationElement(MARQUEE_INTERVAL_ELEMENT, Strings.WindowsUserInterfaceConfiguration_MarqueeInterval, path: Strings.General_Advanced).WithValue(50).WithValidationRule(new IntegerValidationRule(10, 1000)))
                .WithElement(
                    new DoubleConfigurationElement(MARQUEE_STEP_ELEMENT, Strings.WindowsUserInterfaceConfiguration_MarqueeStep, path: Strings.General_Advanced).WithValue(0.80).WithValidationRule(new DoubleValidationRule(0.80, 10, 0.4)))
                .WithElement(
                    new BooleanConfigurationElement(SHOW_CURSOR_ADORNERS_ELEMENT, Strings.WindowsUserInterfaceConfiguration_Cursors, path: Strings.General_Advanced).WithValue(Publication.ReleaseType == ReleaseType.Default))
                .WithElement(
                    new IntegerConfigurationElement(TIMER_FREQUENCY, Strings.WindowsUserInterfaceConfiguration_TimerFrequency, path: Strings.General_Advanced).WithValue(DEFAULT_TIMER_FREQUENCY).WithValidationRule(new IntegerValidationRule(MIN_TIMER_FREQUENCY, MAX_TIMER_FREQUENCY)))
                .WithElement(
                    new BooleanConfigurationElement(TRANSPARENCY, Strings.WindowsUserInterfaceConfiguration_Transparency))
                .WithElement(
                    new SelectionConfigurationElement(TRANSPARENCY_PROVIDER, Strings.WindowsUserInterfaceConfiguration_TransparencyProvider).DependsOn(SECTION, TRANSPARENCY)
            );
        }

        private static IEnumerable<SelectionConfigurationOption> GetThemeOptions()
        {
            var themes = ComponentRegistry.Instance.GetComponents<ITheme>();
            foreach (var theme in themes)
            {
                var option = new SelectionConfigurationOption(theme.Id, theme.Name, theme.Description);
                if (ComponentRegistry.Instance.IsDefault(theme))
                {
                    option.Default();
                }
                yield return option;
            }
        }

        public static ITheme GetTheme(SelectionConfigurationOption option)
        {
            var themes = ComponentRegistry.Instance.GetComponents<ITheme>();
            return themes.FirstOrDefault(theme => string.Equals(theme.Id, option.Id, StringComparison.OrdinalIgnoreCase));
        }

        public static IEnumerable<SelectionConfigurationOption> GetLayoutOptions()
        {
            var layoutProviders = ComponentRegistry.Instance.GetComponents<IUILayoutProvider>();
            foreach (var layoutProvider in layoutProviders)
            {
                var option = new SelectionConfigurationOption(layoutProvider.Id, layoutProvider.Name, layoutProvider.Description);
                if (ComponentRegistry.Instance.IsDefault(layoutProvider))
                {
                    option.Default();
                }
                yield return option;
            }
        }

        public static IUILayoutProvider GetLayout(SelectionConfigurationOption option)
        {
            var layoutProviders = ComponentRegistry.Instance.GetComponents<IUILayoutProvider>();
            return layoutProviders.FirstOrDefault(layoutProvider => string.Equals(layoutProvider.Id, option.Id, StringComparison.OrdinalIgnoreCase));
        }

        public static Type GetControl(SelectionConfigurationOption option)
        {
            var component = LayoutManager.Instance.GetComponent(option.Id);
            if (component == null)
            {
                return LayoutManager.PLACEHOLDER;
            }
            return component.Type;
        }

        public static bool GetIsPrimaryView(SelectionConfigurationOption option, string id)
        {
            return string.Equals(option.Id, id, StringComparison.OrdinalIgnoreCase);
        }
    }
}
