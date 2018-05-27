# Natural Locomotion Getting Started
This document goes over getting started using our plugin and does assume you have followed the [installation guide](installation.md), if not you should do that first.

## Prefabs
The quickest way to get started is by using one of the provided prefabs inside the `Prefabs` folder.
Each prefab is very basic and will most likely need to be extended but can be used as a starting point.

Currently there are two prefabs that only differ by having physics enabled or disabled:
* Translate Camera Rig does not use physics
* Rigidbody Camera Rig does use physics

The prefabs are setup to use our standard input method activated by the grip buttons.

After determining your prefab you can just drag and drop the prefab into your scene like any other.
You will need to remove the `Main Camera` if you are using an empty scene as the Camera Rig has its own camera.
You will also probably need to include some sort of ground or environment.

## Manually
In order to build a proper locomotion system you need a few pieces. 
1. If you have an existing VR camera rig you can use that, if not the SteamVR Camera Rig prefab is a good place to start.
   * The SteamVR prefab is located  at `SteamVR > Prefabs > [CameraRig]`.
1. On the root of your camera rig attach the `LocomotionHub` script.
1. Also on the root you will need some sort of `Movement` script to handle the input and move the player.
   * Look in the `Natural Locomotion > Movements` folder for provided scripts.
1. Finally you will need to provide input to the system, this is usually handled by attaching an `Input` script to each hand.
   * Look in the `Natural Locomotion > Inputs` folder for provided scripts.
1. You can optionally include modifiers such as sprinting, there is no requirement for where these are placed as long as they can get to the locmotion hub to listen and register for events.
1. Note that not all movement scripts will work with all inputs and that your game will most likely need to make your own. Don't worry though, it is pretty easy and will be explained in depth in other tutorials.

