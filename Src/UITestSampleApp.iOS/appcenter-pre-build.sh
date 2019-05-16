#!/usr/bin/env bash
if [ "$APPCENTER_XAMARIN_CONFIGURATION" == "Debug" ];then
    set -e
    
    UnitTestsCSProj=`find "$APPCENTER_SOURCE_DIRECTORY" -name "UITestSampleApp.UnitTests.csproj"`
    echo UnitTestsCSProj: $UnitTestsCSProj

    dotnet test $UnitTestsCSProj -c "UnitTest"
fi