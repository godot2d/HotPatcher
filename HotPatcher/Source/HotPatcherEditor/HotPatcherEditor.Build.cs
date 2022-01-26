// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;
using System;
using System.Collections.Generic;
using System.IO;

public class HotPatcherEditor : ModuleRules
{
	public HotPatcherEditor(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = ModuleRules.PCHUsageMode.UseExplicitOrSharedPCHs;
		bLegacyPublicIncludePaths = false;
		OptimizeCode = CodeOptimization.InShippingBuildsOnly;
		if (Target.Version.MajorVersion < 5 && Target.Version.MinorVersion <= 21)
		{
			bUseRTTI = true;
		}
		
		PublicIncludePaths.AddRange(
			new string[] {
				// ... add public include paths required here ...
			}
			);
				
		
		PrivateIncludePaths.AddRange(
			new string[] {
				// ... add other private include paths required here ...
			}
			);
			
		
		PublicDependencyModuleNames.AddRange(
			new string[]
			{
				"UnrealEd",
				"UMG",
				"UMGEditor",
				"Core",
				"Json",
				"ContentBrowser",
				"SandboxFile",
				"JsonUtilities",
				"TargetPlatform",
				"DesktopPlatform",
				"Projects",
				"Settings",
				"HTTP",
				"RHI",
				"EngineSettings",
				"AssetRegistry",
				"PakFileUtilities",
				"HotPatcherRuntime",
				"BinariesPatchFeature",
                "HotPatcherCore"
                // ... add other public dependencies that you statically link with here ...
			}
			);
		
		PrivateDependencyModuleNames.AddRange(
			new string[]
			{
				"Core",
				"UnrealEd",
				"Projects",
				"DesktopPlatform",
				"InputCore",
				"EditorStyle",
				"LevelEditor",
				"CoreUObject",
				"Engine",
				"Slate",
				"SlateCore",
				"RenderCore"
				// ... add private dependencies that you statically link with here ...	
			}
		);

		if (Target.Version.MajorVersion > 4 || Target.Version.MinorVersion > 23)
		{
			PublicDependencyModuleNames.AddRange(new string[]{
				"ToolMenus",
				"TraceLog"
			});
		}

		if (Target.Version.MajorVersion > 4 || Target.Version.MinorVersion > 23)
		{
			
		}
		System.Func<string, bool,bool> AddPublicDefinitions = (string MacroName,bool bEnable) =>
		{
			PublicDefinitions.Add(string.Format("{0}={1}",MacroName, bEnable ? 1 : 0));
			return true;
		};
		
		AddPublicDefinitions("ENABLE_COOK_ENGINE_MAP", false);
		AddPublicDefinitions("ENABLE_COOK_PLUGIN_MAP", false);
		BuildVersion Version;
		BuildVersion.TryRead(BuildVersion.GetDefaultFileName(), out Version);
		AddPublicDefinitions("WITH_EDITOR_SECTION", Version.MajorVersion > 4 || Version.MinorVersion > 24);
		// Game feature
		bool bEnableGameFeature = false;
		AddPublicDefinitions("ENGINE_GAME_FEATURE", bEnableGameFeature || (Target.Version.MajorVersion > 4 || Target.Version.MinorVersion > 26));
		
		System.Console.WriteLine("MajorVersion {0} MinorVersion: {1} PatchVersion {2}",Target.Version.MajorVersion,Target.Version.MinorVersion,Target.Version.PatchVersion);

		PublicDefinitions.AddRange(new string[]
		{
			"ENABLE_UPDATER_CHECK=1",
			"ENABLE_MULTI_COOKER=0",
			"TOOL_NAME=\"HotPatcher\"",
			"CURRENT_VERSION_ID=75",
			"CURRENT_PATCH_ID=0",
			"REMOTE_VERSION_FILE=\"https://imzlp.com/opensource/version.json\""
		});

		bool bEnablePackageContext = true;
		AddPublicDefinitions("WITH_PACKAGE_CONTEXT", (Version.MajorVersion > 4 || Version.MinorVersion > 23) && bEnablePackageContext);
		if (Version.MajorVersion > 4 || Version.MajorVersion > 26)
		{
			PublicDependencyModuleNames.AddRange(new string[]
			{
				"IoStoreUtilities",
				"UnrealEd"
			});
		}
	}
}
