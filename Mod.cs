﻿using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;
using Game.Simulation;
using Unity.Entities;

namespace InstantBoarding
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger($"{nameof(InstantBoarding)}.{nameof(Mod)}").SetShowsErrorsInUI(false);
        private Setting m_Setting;

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");

            //m_Setting = new Setting(this);
            //m_Setting.RegisterInOptionsUI();
            //GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(m_Setting));


            //AssetDatabase.global.LoadSettings(nameof(InstantBoarding), m_Setting, new Setting(this));


            var oldSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<TransportTrainAISystem>();
            oldSystem.Enabled = false;

            updateSystem.UpdateAt<InstantBoardingTrainSystem>(SystemUpdatePhase.GameSimulation);
            //updateSystem.UpdateBefore<InstantBoardingSystem>(SystemUpdatePhase.GameSimulation);
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));
            if (m_Setting != null)
            {
                m_Setting.UnregisterInOptionsUI();
                m_Setting = null;
            }
        }
    }
}
