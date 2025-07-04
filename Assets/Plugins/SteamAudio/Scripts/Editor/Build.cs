﻿//
// Copyright 2017 Valve Corporation. All rights reserved. Subject to the following license:
// https://valvesoftware.github.io/steam-audio/license.html
//

using System;
using UnityEditor;
#if UNITY_2021_2_OR_NEWER
using UnityEditor.Build;
#endif

namespace SteamAudio
{
    public static class Build
    {
        public static void BuildSteamAudio()
        {
            var args = Environment.GetCommandLineArgs();
            var lastArg = args[args.Length - 1];

            var fileName = "SteamAudio.unitypackage";
            if (lastArg != "SteamAudio.Build.BuildSteamAudio")
            {
                fileName = lastArg + "/" + fileName;
            }

            var assets = new string[] { "Assets/Plugins" };

            AssetDatabase.ExportPackage(assets, fileName, ExportPackageOptions.Recurse);
        }
    }

    [InitializeOnLoad]
    public static class Defines
    {
        // Define the constant STEAMAUDIO_ENABLED for all platforms that are supported by
        // Steam Audio. User scripts should check if this constant is defined
        // (using #if STEAMAUDIO_ENABLED) before using any of the Steam Audio C# classes.
        static Defines()
        {
#if UNITY_2021_2_OR_NEWER
            NamedBuildTarget[] supportedPlatforms = {
                NamedBuildTarget.Standalone,
                NamedBuildTarget.Android,
            };

            foreach (var supportedPlatform in supportedPlatforms)
            {
                PlayerSettings.SetScriptingDefineSymbols(supportedPlatform, "STEAMAUDIO_ENABLED");
            }
#endif
        }
    }
}
