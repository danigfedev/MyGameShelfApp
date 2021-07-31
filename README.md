# MyGameShelfApp

# Description
My Game Shelf App is an Android application for keeping track of your personal videogame collection. It allows the user to search for a game and add it to its collection or to a whishlist. The app is connected to IGDB (Internet Game Data Base), where it gets all game details. In addition to this, there are optional fields that the user can fill manually such as the game's price or the format (physical/digital). My Game Shelf app also allows to register the time played for a specific game in real time, so every time the user starts playing a game, it can track the amount of time played by session or total by pressing the "Register Play Time" button.

//Summary of game info tracked and plans for the future

# Contents

## 1- Unity project: U_GameShelfApp

The app's source code. At the moment of writing only the searching functionality is implemented.

- Unity version: 2021.1.3f1

### Dependencies

* **Text Mesh Pro**: To import Text Mesh Pro in project, follow Unity's instructions:
https://learn.unity.com/tutorial/textmesh-pro-importing-the-package#


## 2- Web Services

A gateway for handling app authentication. As IGDB uses app tokens and not user tokens, the idea behind this gateway is to store the app token and manage its retrieval and update, avoiding the app from managing this sensible information direcctly. **At the moment of writing**, it is conformed by a single PHP script that just connects to IGDB and retrieves some hardcoded data.


