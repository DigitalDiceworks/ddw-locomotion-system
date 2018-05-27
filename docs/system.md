# Natural Locomotion System
This document details each of the subsystems that make up the overall Natural Locmotion system.

## Locomotion Hub
The locomotion hub acts as a midway point between inputs and events.
As well as calculating modifiers values.

This is usually placed directly on your VR camera rig.

## Natural Input
Input components handle calculating the current direction and speed.
Typically you will probably have one on each hand.

You will need to make your component inherit from `NaturalInput` to make an input component.

In order to activate your input you have to tell the locomotion hub by calling `BeginInput` with your input as the parameter.
To deactivate call `EndInput` on the locomotion hub.

The `GetVector` function will only be called when the input is active so you do not need to check for that.

A good example is the Grip Input located in `Scripts / Inputs`.

## Movement
Movement components handle the actual movement of the play area.
Typically you will attach this to the VR camera rig and move yourself.

In order for your movemvent component to get input vectors you will need to subscribe to the `onInput` event from the locomotion hub.
This event is not exclusive to movemvent scripts and can be used for other things as well.

A good example is the Translation Movement located in `Scripts / Movements`.

## Modifiers
Modifier components allow you to adjust the raw normalized input vectors before they are sent to movement scripts.
Modifier scripts can be placed anywhere in your scene as long as they have access to the locomotion hub.

Controlling your modifier can be done by calling `AddModifier`, `RemoveModifier` and `ToggleModifier` on the locomotion hub.

A good example is the Toggle Sprint modifier  located in `Scripts / Modifiers`.

