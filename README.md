# Volcanoids GMod
This is a mod to configure a few various things in-game.

The first Volcanoids is run with this mod installed, it will create a file called `GMod.json` in `%LOCALAPPDATA%Low\Volcanoid\Volcanoids\Mods`. (Assuming the file doesn't exist in the first place.)

# Config
Option | Description
--- | ---
coreSlotMultiplier | Default: 1.0<br>Multiplier on the max core slots available.
revealFullMap | Default: false<br>If true, will reveal the whole map when you load the game.
disablePowerPlantStopThreshold | Default: false<br>Power plants by default shut off at 100% energy, and don't turn on till 90% energy. This saves coal.<br>If you want then on all the time, set this to true.
powerPerSecondMultiplier | Default: 1.0<br>Affects the power output of things. e.g. if a plant outputs +10 power, then a 2.0 value results in +20 power.
shipClaimHealthLevel | Default: 0.25<br>This is the health level the ship must be at to be claimable.
radarRangeMultiplier | Default: 1.0<br>Multiplier on the radar range of ships. Higher and you can see more deposits/items around on the map.
infiniteStamina | Default: false<br>Infinite stamina.
infiniteAmmo | Default: false<br>Infinite ammo.
inventorySizeMultiplier | Default: 1.0<br>Multiplier on the inventory size. Affects everything with an inventory. (Players, storage modules, chests, etc.)
segmentSizeMultiplier | Default: 1.0<br>Multiplier on the number of segments an engine will support.<br>**BEWARE** Once added, you can't remove a segment, and you can make them too long for the landing spot.
disableAimSway | Default: false<br>Disables weapon sway when aiming.
hideHelpIconNearCrosshair | Default: false<br>When true, completely hides the question mark that appears near the crosshair.
oreAndIngotStackSizeMultiplier | Default: 1.0<br>Multiplier on ores & ingots, and alloys.<br>Effect is accumulative with stackSizeMultiplier.
stackSizeMultiplier | Default: 1.0<br>General stack size multiplier. (For items with stack sizes > 1.)<br>Effect is accumulative with oreAndIngotStackSizeMultiplier.
changeTurretRecipes | Default: false<br>Changes the default turret recipes to use an equivalent number of Copper Gun Components instead of the guns themselves.

# Requirements
- Volcanoids obviously.
- [Harmony](https://github.com/pardeike/Harmony). The dll is included with the release zip for convenience.

# Installation
- Extract the release to the Volcanoids mod folder (`%LOCALAPPDATA%Low\Volcanoid\Volcanoids\Mods`) so `GMod.dll` is placed like this: `%LOCALAPPDATA%Low\Volcanoid\Volcanoids\Mods\GMod.dll`.

# Un-installation
- Just remove the dll from `%LOCALAPPDATA%Low\Volcanoid\Volcanoids\Mods`.