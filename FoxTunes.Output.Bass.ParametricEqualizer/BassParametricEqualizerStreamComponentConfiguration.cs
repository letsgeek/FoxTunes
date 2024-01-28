﻿using System.Collections.Generic;

namespace FoxTunes
{
    public static class BassParametricEqualizerStreamComponentConfiguration
    {
        public const string ENABLED = "AAAAF34F-A090-4AEE-BD65-128561960C92";

        public const string BANDWIDTH = "AAAB688D-9CA0-41B3-8E11-AC252DF03BE4";

        public const string BAND_32 = "BBBBC109-B99A-49C1-909D-16A862EFF344";

        public const string BAND_64 = "CCCCDD9E-7430-4123-B7CA-FDDD7680EF2C";

        public const string BAND_125 = "DDDD4934-28A4-4841-BC31-9BB895B13A14";

        public const string BAND_250 = "EEEE45E7-0B32-4E73-B394-2D510A34FB05";

        public const string BAND_500 = "FFFFD2A0-40A7-4772-9829-48D7C8CDCF52";

        public const string BAND_1000 = "GGGGEFB4-F65D-444F-A021-5B8F0B643AC5";

        public const string BAND_2000 = "HHHHB649-B83F-48D0-AF02-FA127FFC9F04";

        public const string BAND_4000 = "IIIIE3B6-1026-4505-8C77-294185F61452";

        public const string BAND_8000 = "JJJJAADF-5286-46BA-AC4F-80CFB1115215";

        public const string BAND_16000 = "KKKKD105-FC44-40F4-AF85-972CF7F328E9";

        public const string PRESET = "LLLL07F8-D580-4CAE-9BC2-B7EF6FB6D3AD";

        public const string PRESET_NONE = "AAAA7223-44DD-4D73-852D-1745DAD8646E";

        public const string PRESET_BASS = "BBBB7126-D4B5-49A5-8EA4-5E891551EA46";

        public const string PRESET_FLAT = "CCCC6D56-FA6E-4C02-A12E-1A99C49F3DFE";

        public const string PRESET_POP = "DDDD655E-8B97-4ED9-AA25-4E0198405FE9";

        public const string PRESET_ROCK = "EEEE8A92-CD4E-4785-A033-2625A2098232";

        public static IEnumerable<ConfigurationSection> GetConfigurationSections()
        {
            var section = new ConfigurationSection(BassOutputConfiguration.SECTION, "Output")
                .WithElement(
                    new BooleanConfigurationElement(ENABLED, "Enabled", path: "Parametric Equalizer")
                        .WithValue(false))
                .WithElement(
                    new DoubleConfigurationElement(BANDWIDTH, "Bandwidth", path: "Parametric Equalizer")
                        .WithValue(2.5)
                        .DependsOn(BassOutputConfiguration.SECTION, ENABLED)
                        .WithValidationRule(
                            new DoubleValidationRule(
                                PeakEQ.MIN_BANDWIDTH,
                                PeakEQ.MAX_BANDWIDTH,
                                0.1
                            )
                        ));

            foreach (var band in Bands)
            {
                section.WithElement(
                    new DoubleConfigurationElement(
                            band.Key,
                            band.Value < 1000 ? band.Value.ToString() + "Hz" : band.Value.ToString() + "kHz",
                            path: "Parametric Equalizer")
                        .WithValue(0)
                        .DependsOn(BassOutputConfiguration.SECTION, ENABLED)
                        .WithValidationRule(
                            new DoubleValidationRule(
                                PeakEQ.MIN_GAIN,
                                PeakEQ.MAX_GAIN
                            )
                        )
                );
            }

            section.WithElement(
                new SelectionConfigurationElement(PRESET, "Preset", path: "Parametric Equalizer")
                    .WithOptions(GetPresetOptions())
                    .DependsOn(BassOutputConfiguration.SECTION, ENABLED)
            );

            yield return section;

            StandardComponents.Instance.Configuration.GetElement<SelectionConfigurationElement>(
                BassOutputConfiguration.SECTION,
                PRESET
            ).ConnectValue(value => LoadPreset(value));
        }

        private static IEnumerable<SelectionConfigurationOption> GetPresetOptions()
        {
            yield return new SelectionConfigurationOption(PRESET_NONE, "None").Default();
            yield return new SelectionConfigurationOption(PRESET_BASS, "Bass");
            yield return new SelectionConfigurationOption(PRESET_FLAT, "Flat");
            yield return new SelectionConfigurationOption(PRESET_POP, "Pop");
            yield return new SelectionConfigurationOption(PRESET_ROCK, "Rock");
        }
        
        public static void LoadPreset(string name)
        {
            var element = StandardComponents.Instance.Configuration.GetElement<SelectionConfigurationElement>(
                BassOutputConfiguration.SECTION,
                PRESET
            );
            foreach (var option in element.Options)
            {
                if (string.Equals(option.Name, name, System.StringComparison.OrdinalIgnoreCase))
                {
                    LoadPreset(option);
                    return;
                }
            }
        }

        private static void LoadPreset(SelectionConfigurationOption option)
        {
            var bands = default(Dictionary<string, int>);
            switch (option.Id)
            {
                case PRESET_NONE:
                    bands = new Dictionary<string, int>()
                    {
                        { BAND_32, 0 },
                        { BAND_64, 0 },
                        { BAND_125, 0 },
                        { BAND_250, 0 },
                        { BAND_500, 0 },
                        { BAND_1000, 0 },
                        { BAND_2000, 0 },
                        { BAND_4000, 0 },
                        { BAND_8000, 0 },
                        { BAND_16000, 0 }
                    };
                    break;
                case PRESET_BASS:
                    bands = new Dictionary<string, int>()
                    {
                        { BAND_32, 9 },
                        { BAND_64, 6 },
                        { BAND_125, 2 },
                        { BAND_250, 0 },
                        { BAND_500, -1 },
                        { BAND_1000, -1 },
                        { BAND_2000, 0 },
                        { BAND_4000, 0 },
                        { BAND_8000, 0 },
                        { BAND_16000, 0 }
                    };
                    break;
                case PRESET_FLAT:
                    bands = new Dictionary<string, int>()
                    {
                        { BAND_32, -5 },
                        { BAND_64, -5 },
                        { BAND_125, -2 },
                        { BAND_250, 0 },
                        { BAND_500, 2 },
                        { BAND_1000, 3 },
                        { BAND_2000, 3 },
                        { BAND_4000, 3 },
                        { BAND_8000, 3 },
                        { BAND_16000, 3 }
                    };
                    break;
                case PRESET_POP:
                    bands = new Dictionary<string, int>()
                    {
                        { BAND_32, -1 },
                        { BAND_64, 1 },
                        { BAND_125, 3 },
                        { BAND_250, 4 },
                        { BAND_500, 4 },
                        { BAND_1000, 3 },
                        { BAND_2000, 2 },
                        { BAND_4000, 1 },
                        { BAND_8000, 2 },
                        { BAND_16000, 4 }
                    };
                    break;
                case PRESET_ROCK:
                    bands = new Dictionary<string, int>()
                    {
                        { BAND_32, 5 },
                        { BAND_64, 3 },
                        { BAND_125, 0 },
                        { BAND_250, 0 },
                        { BAND_500, 0 },
                        { BAND_1000, 3 },
                        { BAND_2000, 4 },
                        { BAND_4000, 4 },
                        { BAND_8000, 3 },
                        { BAND_16000, 3 }
                    };
                    break;
            }
            foreach (var band in Bands)
            {
                var element = StandardComponents.Instance.Configuration.GetElement<DoubleConfigurationElement>(
                    BassOutputConfiguration.SECTION,
                    band.Key
                );
                if (element == null)
                {
                    continue;
                }
                element.Value = bands[band.Key];
            }
            StandardComponents.Instance.Configuration.Save();
        }

        public static IEnumerable<KeyValuePair<string, int>> Bands
        {
            get
            {
                yield return new KeyValuePair<string, int>(BAND_32, 32);
                yield return new KeyValuePair<string, int>(BAND_64, 64);
                yield return new KeyValuePair<string, int>(BAND_125, 125);
                yield return new KeyValuePair<string, int>(BAND_250, 250);
                yield return new KeyValuePair<string, int>(BAND_500, 500);
                yield return new KeyValuePair<string, int>(BAND_1000, 1000);
                yield return new KeyValuePair<string, int>(BAND_2000, 2000);
                yield return new KeyValuePair<string, int>(BAND_4000, 4000);
                yield return new KeyValuePair<string, int>(BAND_8000, 8000);
                yield return new KeyValuePair<string, int>(BAND_16000, 16000);
            }
        }

        public static IEnumerable<KeyValuePair<string, string>> Presets
        {
            get
            {
                yield return new KeyValuePair<string, string>(PRESET_NONE, "None");
                yield return new KeyValuePair<string, string>(PRESET_BASS, "Bass");
                yield return new KeyValuePair<string, string>(PRESET_FLAT, "Flat");
                yield return new KeyValuePair<string, string>(PRESET_POP, "Pop");
                yield return new KeyValuePair<string, string>(PRESET_ROCK, "Rock");
            }
        }
    }
}
