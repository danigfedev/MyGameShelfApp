# MyGameShelfApp

# Description
My Game Shelf App is an Android application for keeping track of your personal videogame collection. It allows the user to search for a game and add it to its collection or to a whishlist. The app is connected to IGDB (Internet Game Data Base), where it gets all game details. In addition to this, there are optional fields that the user can fill manually such as the game's price or the format (physical/digital). My Game Shelf app also allows to register the time played for a specific game in real time, so every time the user starts playing a game, it can track the amount of time played by session or total by pressing the "Register Play Time" button.

//Summary of game info tracked and plans for the future

# Contents

## 1- Unity project: U_GameShelfApp

The app's source code. At the moment of writing only the searching fucntionality is implemented.

- Unity version: 2021.1.3f1

### Dependencies

* **Text Mesh Pro**
To import Text Mesh Pro in project, follow Unity's instructions:
https://learn.unity.com/tutorial/textmesh-pro-importing-the-package#


## 2- Web Services

A PHP script for handling app authentication. At the moment of writing, it just connects to IGDB retrieving some hardcoded data.


