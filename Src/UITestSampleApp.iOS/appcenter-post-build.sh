#!/usr/bin/env bash
if [ "$APPCENTER_XAMARIN_CONFIGURATION" == "Debug" ];then

    echo "Post Build Script Started"
    
    SolutionFile=`find "$APPCENTER_SOURCE_DIRECTORY" -name UITestSampleApp.sln`
    SolutionFileFolder=`dirname $SolutionFile`

    UITestProject=`find "$APPCENTER_SOURCE_DIRECTORY" -name UITestSampleApp.UITests.csproj`

    echo SolutionFile: $SolutionFile
    echo SolutionFileFolder: $SolutionFileFolder
    echo UITestProject: $UITestProject

    chmod -R 777 $SolutionFileFolder

    msbuild "$UITestProject" /property:Configuration=$APPCENTER_XAMARIN_CONFIGURATION

    UITestDLL=`find "$APPCENTER_SOURCE_DIRECTORY" -name "UITestSampleApp.UITests.dll" | grep bin`
    echo UITestDLL: $UITestDLL

    UITestBuildDir=`dirname $UITestDLL`
    echo UITestBuildDir: $UITestBuildDir

    UITestVersionNumber=`grep '[0-9]' $UITestProject | grep Xamarin.UITest|grep -o '[0-9]\{1,3\}\.[0-9]\{1,3\}\.[0-9]\{1,3\}\.[0-9]\{1,10\}\-'dev`
    echo UITestPrereleaseVersionNumber: $UITestVersionNumber

    UITestVersionNumberSize=${#UITestVersionNumber} 
    echo UITestVersionNumberSize: $UITestVersionNumberSize

    if [ $UITestVersionNumberSize == 0 ]
    then
        UITestVersionNumber=`grep '[0-9]' $UITestProject | grep Xamarin.UITest|grep -o '[0-9]\{1,3\}\.[0-9]\{1,3\}\.[0-9]\{1,3\}'`
        echo UITestVersionNumber: $UITestVersionNumber
    fi

    TestCloudExe=`find ~/.nuget | grep test-cloud.exe | grep $UITestVersionNumber | head -1`
    echo TestCloudExe: $TestCloudExe

    TestCloudExeDirectory=`dirname $TestCloudExe`
    echo TestCloudExeDirectory: $TestCloudExeDirectory

    IPAFile=`find "$APPCENTER_SOURCE_DIRECTORY" -name *.ipa | head -1`
    echo IPAFile: $IPAFile

    DSYMFile=`find "$APPCENTER_SOURCE_DIRECTORY" -name *.dSYM | head -1`
    echo DSYMFile: $DSYMFile

    npm install -g appcenter-cli

    appcenter login --token $AppCenterAPIToken

    appcenter test run uitest --app "bminnick/UITestSampleApp-1" --devices "bminnick/ios10-plus" --app-path $IPAFile --test-series "master" --locale "en_US" --build-dir $UITestBuildDir --dsym-dir $DSYMFile --uitest-tools-dir $TestCloudExeDirectory --async
fi