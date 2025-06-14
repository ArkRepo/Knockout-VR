/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using UnityEditor;
using System.IO;

public class AndroidVideoEditorUtil
{
    private static readonly string videoPlayerFileName =
        "Assets/Oculus/SampleFramework/Core/Video/Plugins/Android/java/com/oculus/videoplayer/NativeVideoPlayer.java";

    private static readonly string audio360PluginPath =
        "Assets/Oculus/SampleFramework/Core/Video/Plugins/Android/Audio360/audio360.aar";

    private static readonly string audio360Exo218PluginPath =
        "Assets/Oculus/SampleFramework/Core/Video/Plugins/Android/Audio360/audio360-exo218.aar";

    [MenuItem("Oculus/Samples/Video/Enable Native Android Video Player")]
    public static void EnableNativeVideoPlayer()
    {
        // Enable native video player
        PluginImporter nativeVideoPlayer = (PluginImporter)AssetImporter.GetAtPath(videoPlayerFileName);
        if (nativeVideoPlayer != null)
        {
            nativeVideoPlayer.SetCompatibleWithPlatform(BuildTarget.Android, true);
            nativeVideoPlayer.SaveAndReimport();
        }

        // Enable audio plugins
        PluginImporter audio360 = (PluginImporter)AssetImporter.GetAtPath(audio360PluginPath);
        PluginImporter audio360exo218 = (PluginImporter)AssetImporter.GetAtPath(audio360Exo218PluginPath);

        if (audio360 != null && audio360exo218 != null)
        {
            audio360.SetCompatibleWithPlatform(BuildTarget.Android, true);
            audio360exo218.SetCompatibleWithPlatform(BuildTarget.Android, true);
            audio360.SaveAndReimport();
            audio360exo218.SaveAndReimport();
        }

        // Enable gradle build with exoplayer
        Gradle.Configuration.UseGradle();
        var template = Gradle.Configuration.OpenTemplate();
        template.AddDependency("com.google.android.exoplayer:exoplayer", "2.18.2");
        template.Save();

        var properties = Gradle.Configuration.OpenProperties();
        properties.SetProperty("android.useAndroidX", "true");
        properties.Save();

        // Set the API level to 31
        PlayerSettings.Android.targetSdkVersion = (AndroidSdkVersions)31;
    }

    [MenuItem("Oculus/Samples/Video/Disable Native Android Video Player")]
    public static void DisableNativeVideoPlayer()
    {
        // Disable native video player
        PluginImporter nativeVideoPlayer = (PluginImporter)AssetImporter.GetAtPath(videoPlayerFileName);
        if (nativeVideoPlayer != null)
        {
            nativeVideoPlayer.SetCompatibleWithPlatform(BuildTarget.Android, false);
            nativeVideoPlayer.SaveAndReimport();
        }

        // Disable audio plugins
        PluginImporter audio360 = (PluginImporter)AssetImporter.GetAtPath(audio360PluginPath);
        PluginImporter audio360exo218 = (PluginImporter)AssetImporter.GetAtPath(audio360Exo218PluginPath);

        if (audio360 != null && audio360exo218 != null)
        {
            audio360.SetCompatibleWithPlatform(BuildTarget.Android, false);
            audio360exo218.SetCompatibleWithPlatform(BuildTarget.Android, false);
            audio360.SaveAndReimport();
            audio360exo218.SaveAndReimport();
        }

        // remove exoplayer and sourcesets from gradle file (leave other parts since they are harmless).
        if (Gradle.Configuration.IsUsingGradle())
        {
            var template = Gradle.Configuration.OpenTemplate();
            template.RemoveDependency("com.google.android.exoplayer:exoplayer");
            template.RemoveAndroidSetting("sourceSets.main.java.srcDir");
            template.Save();
        }
    }
}
