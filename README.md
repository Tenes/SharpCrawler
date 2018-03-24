# SharpCrawler
Dungeon crawler | Rogue Like made with Monogame (C#)

Nothing too fancy, just a fun little project made to explore the possibility of the core version of Monogame.

## /!\ Install the font /!\
The game uses a custom font named Bitty. Be sure to install it before doing anything.


## To Run:
Clone all the repo into a folder and run the following commands:
```DIGITAL Command Language
dotnet restore
dotnet build
dotnet run
```

## To Compile:
Depending on your platform, you can run one of those commands to create an executable:
```DIGITAL Command Language
dotnet publish -c Release -r win7-x64
dotnet publish -c Release -r win10-x64
dotnet publish -c Release -r osx.10.12-x64
dotnet publish -c Release -r linux-x64
```
The .exe will be in /bin/Release/

If you want to change the output directory you can use this command:
```DIGITAL Command Language
dotnet publish -c Release -o FolderExample -r win10-x64
```

## What the game looks like:
![Imgur](https://i.imgur.com/tKglWgF.png)

![Imgur](https://i.imgur.com/wrYCEp2.png)

![Imgur](https://i.imgur.com/aZZcLFE.png)

