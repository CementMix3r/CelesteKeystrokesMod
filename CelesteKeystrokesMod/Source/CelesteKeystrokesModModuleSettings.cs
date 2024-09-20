namespace Celeste.Mod.CelesteKeystrokesMod;

public class CelesteKeystrokesModModuleSettings : EverestModuleSettings {
    public bool Visible { get; set; } = true;

    [SettingRange(0, 200, false)]
    [SettingSubText("Default value: 15")]
    public int FadeTime { get; set; } = 15;

    [SettingRange(0, 100, false)]
    [SettingSubText("Default value: 10")]
    public int KeyPadding { get; set; } = 10;

    [SettingRange(1, 30, false)]
    [SettingSubText("Default value: 1.0")]
    public int Scale { get; set; } = 10;

    // Pressed Color
    [SettingRange(0, 255, false)]
    [SettingSubText("Default value: 150")]
    public int PressedColorRed { get; set; } = 150;
    [SettingRange(0, 255, false)]
    [SettingSubText("Default value: 150")]
    public int PressedColorGreen { get; set; } = 150;
    [SettingRange(0, 255, false)]
    [SettingSubText("Default value: 150")]
    public int PressedColorBlue { get; set; } = 150;
    

    // Released Color
    [SettingRange(0, 255, false)]
    [SettingSubText("Default value: 255")]
    public int ReleasedColorRed { get; set; } = 255;
    [SettingRange(0, 255, false)]
    [SettingSubText("Default value: 255")]
    public int ReleasedColorGreen { get; set; } = 255;
    [SettingRange(0, 255, false)]
    [SettingSubText("Default value: 255")]
    public int ReleasedColorBlue { get; set; } = 255;

    [SettingRange(0, 1250, true)]
    [SettingSubText("Default value: 50")]
    public int EdgeOffsetX { get; set; } = 50;

    [SettingRange(0, 1250, true)]
    [SettingNumberInput(allowNegatives: false, maxLength: 1)]
    [SettingSubText("Default value: 50")]
    public int EdgeOffsetY { get; set; } = 50;

    [SettingSubText("Default value: true, if true the EdgeOffsetX is relative to the right of the screen.")]
    public bool UseRightEdge { get; set; } = true;
    [SettingSubText("Default value: false, if true the EdgeOffsetY is relative to the bottom of the screen.")]
    public bool UseBottomEdge { get; set; } = false;
}