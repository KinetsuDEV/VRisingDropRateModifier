# DropRateModifier
`Server side only` mod to change drop rate.

What is the difference between `DropTableModifier_General` native server configuration and `DropRateModifier` mod configuration?

![alt text](https://github.com/KinetsuDEV/VRisingDropRateModifier/blob/main/Thunderstore/drop-settings-comparison.png?raw=true)

## Installation
* Install [BepInEx](https://v-rising.thunderstore.io/package/BepInEx/BepInExPack_V_Rising/)
* Install [Wetstone](https://v-rising.thunderstore.io/package/molenzwiebel/Wetstone/)
* Extract _DropRateModifier.dll_ into _(VRising server folder)/BepInEx/plugins_

## Configurable Values
```ini
[DropRateConfig]

## Drop rate modifier value
# Setting type: Single
# Default value: 1
DropRateModifier = 1
```