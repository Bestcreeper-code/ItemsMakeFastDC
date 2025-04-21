# 🎮 My BepInEx Mod

A custom mod for **Dungeon Clawler** built using the BepInEx framework.

---

## 📦 Requirements

- [BepInEx (latest version)](https://github.com/BepInEx/BepInEx/releases)
- Compatible with any game version(until settings or speed system gets revamped)

---

## 🧰 Installation

### 1. Install BepInEx

- Download the latest BepInEx release [here](https://github.com/BepInEx/BepInEx/releases).
- Extract the contents into your game folder (where the main executable is).
- Run the game once to generate required folders.

### 2. Install this Mod

- Download the dll file from [Here](https://github.com/Bestcreeper-code/ItemsMakeFastDC/releases/tag/Releases) or compile the [source](https://github.com/Bestcreeper-code/ItemsMakeFastDC/releases/tag/Sources) version yourself (Visual Studio).
- Place the `.dll` file into the `BepInEx/plugins/` folder.

### 3. Launch the Game

- BepInEx will automatically load the mod on startup.

---

## 🧪 Features

- Every item in your deck slightly increases game speed (configurable in-game).
- Base game speed is adjustable from **0 to 3.5** (configurable in-game).
- You can bind a key to display your current speed (only configurable via `config.txt` for now).

---

## 🔧 Configuration

A config file will be created in the mod folder(C:\Program Files (x86)\Steam\steamapps\common\Dungeon Clawler\Windows\BepInEx\pluginsItemsMakeFast\config.txt by default)
As said above, you can modify your settings in this file or use the ingame settings(exept the speed showing key that can for now only bechanged in the config file)
