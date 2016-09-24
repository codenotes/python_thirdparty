// Copyright 1998-2016 Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;
using System;

public class python : ModuleRules
{



    public void includeAdd(string env)
    {
        var items = Environment.GetEnvironmentVariable(env);
        foreach (string s in items.Split(';'))
        {
            Console.WriteLine("INCLUDE:" + s);
            PublicIncludePaths.Add(s);

        }

    }

    public void addPreproc(string ros_preproc)
    {
        foreach (string s in ros_preproc.Split(';'))
        {
            Console.WriteLine("DEFINE:" + s);
            Definitions.Add(s);

        }
    }

    public void includeLib(string env, string prefix = null)
    {
        var items = Environment.GetEnvironmentVariable(env);
        string slib;

        if (prefix != null)
        {//TODO:make for non windows case
            if (prefix.PadRight(1) != "\\")
                prefix += '\\';
        }

        foreach (string s in items.Split(';'))
        {


            slib = prefix + s;
            Console.WriteLine("LIB INCLUDE:" + slib);
            PublicAdditionalLibraries.Add(slib);

        }
    }


    public python(TargetInfo Target)
	{
		Type = ModuleType.External;
        addPreproc("BOOST_PYTHON_STATIC_LIB");
	
		if (Target.Platform == UnrealTargetPlatform.Win64)
		{
	
		bUseRTTI = true;
        // Enable C++ Exceptions for this module
        bEnableExceptions = true;
        //Build Config
        UEBuildConfiguration.bForceEnableExceptions = true;


        PublicIncludePaths.Add(ModuleDirectory +@"\python_2_7_11\include");
        PublicLibraryPaths.Add(ModuleDirectory +@"\python_2_7_11\Lib\Win64");
        includeAdd("BOOST_160_INCLUDE");
        var l=Environment.GetEnvironmentVariable("BOOST_160_64_LIB");
        PublicLibraryPaths.Add(l);

        //	PublicDependencyModuleNames.AddRange(new string[] { "Core", "CoreUObject", "Engine", "InputCore" });
        //  includeAdd("BOOST_160_INCLUDE");
        string PythonLibPath = UEBuildConfiguration.UEThirdPartySourceDirectory + "python/python_2_7_11/Lib/Win64";
        PublicLibraryPaths.Add(PythonLibPath);


        PublicAdditionalLibraries.Add(@"python27.lib");

		}

//        var ros_preproc = "GREG1;BOOST_REGEX_NO_EXTERNAL_TEMPLATES;OPENCV;_NO_FTDI;BOOST_TYPE_INDEX_FORCE_NO_RTTI_COMPATIBILITY;GREG1";// ;BOOST_LIB_DIAGNOSTIC";
     //   addPreproc(ros_preproc);




    }





}
