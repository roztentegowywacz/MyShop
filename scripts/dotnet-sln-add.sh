#!/bin/bash
echo "--------------------------------------"
echo "/// Adding projects to solution... ///"
echo "--------------------------------------"
echo ''
cd ..
dotnet sln MyShop.sln add ./MyShop.Server/**/**/*.csproj

echo ''
dotnet sln MyShop.sln list