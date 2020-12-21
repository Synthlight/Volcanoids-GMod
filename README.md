# Volcanoids GMod
This is a mod to configure a few various things in-game.

The first Volcanoids is run with this mod installed, it will create a file called `gmod.json` in `Volcanoids\BepInEx\config`. (Assuming the file didn't existin the first place.)

# Config
Option | Description
--- | ---
coreSlotMultiplier | Default: 1.0<br>Multiplier on the max core slots available.
revealFullMapOnTravel | Default: false<br>If true, will reveal thw whole map when you use the pilot seat to travel.
disablePowerPlantStopThreshold | Default: false<br>Power plants by default shut off at 100% energy, and don't turn on till 90% energy. This saves coal.<br>If you want then on all the time, set this to true.
powerPerSecondMultiplier | Default: 1.0<br>Affects the power output of things. e.g. if a plant outputs +10 power, then a 2.0 value results in +20 power.
shipClaimHealthLevel | Default: 0.25<br>This is the health level the ship must be at to be claimable.
radarRangeMultiplier | Default: 1.0<br>Multiplier on the radar range of ships. Higher and you can see more deposits/items around on the map.
infiniteStamina | Default: false<br>Infinite stamina.
infiniteAmmo | Default: false<br>Infinite ammo.
inventorySizeMultiplier | Default: 1.0<br>Multiplier on the inventory size. Affects everything with an inventory. (Players, storage modules, chests, etc.)
segmentSizeMultiplier | Default: 1<br>Multiplier on the number of segments an engine will support.<br>**BEWARE** Once added, you can't remove a segment, and you can make them too long for the landing spot.
disableAimSway | Default: false<br>Disables weapon sway when aiming.

# Requirements
- Volcanoids obviously.
- BepInEx: https://github.com/BepInEx/BepInEx (You want the x64 version.)

# Installation
- Extract BepInEx to the root Volcanoids folder so `winhttp.dll` is in the same folder as `Volcanoids.exe`.
- Extract this mod so `GMod.dll` is placed like this: `Volcanoids\BepInEx\plugins\GMod.dll`.

# Un-installation
BepInEx uses `winhttp.dll` as an injector/loader. Renaming or deleting this file is enough to disable both my mod and the loader.