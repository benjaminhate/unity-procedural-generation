# Unity Procedural Generation

## Project

The project is a procedural generation of a terrain.
The terrain meats certains specifications :

- their must be a hill in the middle of the map
- a building must be placed on top of the hill
- roads are randomly generated with a logical algorithm
- next to the roads, smaller buildings and houses are placed to create small town

## Game

The game using this generation is a replay game.
You control a character that needs to reach the building on top of the hill in under 60 seconds.
When you reach it, you start again with a new character. Meanwhile, AI-controled characters will replay the paths you took with the previous characters.

The goal of the game is to get the maximum character to the building.
