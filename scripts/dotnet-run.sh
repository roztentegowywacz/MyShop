#!/bin/bash
sudo service mongod start
cd ..
dotnet build
cd MyShop.Server/src/MyShop.Api
dotnet run