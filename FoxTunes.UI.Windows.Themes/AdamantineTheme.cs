﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace FoxTunes
{
    [Component(ID)]
    [WindowsUserInterfaceDependency]
    [ComponentPreference(ReleaseType.Default)]
    public class AdamantineTheme : ThemeBase
    {
        public const string ID = "06464CF4-118F-47EA-9597-303D305EF847";

        public AdamantineTheme()
            : base(ID, Strings.AdamantineTheme_Name, Strings.AdamantineTheme_Description, GetColorPalettes())
        {

        }

        public override int CornerRadius
        {
            get
            {
                return 0;
            }
        }

        public override ResourceDictionary GetResourceDictionary()
        {
            return new ResourceDictionary()
            {
                Source = new Uri("/FoxTunes.UI.Windows.Themes;component/Themes/Adamantine.xaml", UriKind.Relative)
            };
        }

        public override Stream GetArtworkPlaceholder()
        {
            return typeof(AdamantineTheme).Assembly.GetManifestResourceStream("FoxTunes.UI.Windows.Themes.Images.Adamantine_Artwork.png");
        }

        public static IEnumerable<IColorPalette> GetColorPalettes()
        {
            return new[]
            {
                new ColorPalette(
                    ID + "_AAAA",
                    ColorPaletteRole.Visualization,
                    Strings.AdamantineTheme_ColorPalette_Default_Name,
                    Strings.AdamantineTheme_ColorPalette_Default_Description,
                    Resources.White
                ),
                new ColorPalette(
                    ID + "_BBBB",
                    ColorPaletteRole.Visualization,
                    Strings.AdamantineTheme_ColorPalette_Gradient1_Name,
                    Strings.AdamantineTheme_ColorPalette_Gradient1_Description,
                    Resources.Transparent_White
                ),
            };
        }
    }
}
