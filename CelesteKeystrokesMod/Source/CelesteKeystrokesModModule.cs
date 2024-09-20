using System;

namespace Celeste.Mod.CelesteKeystrokesMod;

public class CelesteKeystrokesModModule : EverestModule {
    public static CelesteKeystrokesModModule Instance { get; private set; }
    public override Type SettingsType => typeof(CelesteKeystrokesModModuleSettings);
    public static CelesteKeystrokesModModuleSettings Settings => (CelesteKeystrokesModModuleSettings) Instance._Settings;

    public override Type SessionType => typeof(CelesteKeystrokesModModuleSession);
    public static CelesteKeystrokesModModuleSession Session => (CelesteKeystrokesModModuleSession) Instance._Session;

    public override Type SaveDataType => typeof(CelesteKeystrokesModModuleSaveData);
    public static CelesteKeystrokesModModuleSaveData SaveData => (CelesteKeystrokesModModuleSaveData) Instance._SaveData;

    public CelesteKeystrokesModModule() {
        Instance = this;
#if DEBUG
        // debug builds use verbose logging
        Logger.SetLogLevel(nameof(CelesteKeystrokesModModule), LogLevel.Verbose);
#else
        // release builds use info logging to reduce spam in log files
        Logger.SetLogLevel(nameof(CelesteKeystrokesModModule), LogLevel.Info);
#endif
    }


    private static void OnLoadLevel(Level level, Player.IntroTypes playerIntro, bool isFromLoader) {
        level.Add(new KeystrokesEntity());
    }

    public override void Load() {
        Everest.Events.Level.OnLoadLevel += OnLoadLevel;
    }

    public override void Unload() {
        Everest.Events.Level.OnLoadLevel -= OnLoadLevel;
    }
}