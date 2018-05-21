# Natural Locomotion Installation
This document serves as a walkthrough to getting your project ready to go for natural locomotion.

## Steam VR
In order to use natural locomotion you will first need to download and import the steam VR plugin.
The easiest way is to download the plugin from the unity asset store and import it into your existing or new project.
[Unity asset store link](https://assetstore.unity.com/packages/templates/systems/steamvr-plugin-32647)

## Downloading Natural Locomotion
There are three methods of downloading the natural locomotion package.
1. Unity asset store here (Waiting to submit)
1. [Github releases page](https://github.com/DigitalDiceworks/natural-locomotion/releases)
1. You can of course just clone the repo and be on the bleeding edge or make your own changes in a forked repository.

Unity asset store will be easier to update but may be delayed for approvals.
Cloning the repo will require manual updates but may be required if you need to make local changes.

## Importing Natual Locomotion
After downloading the unity package you can open your project and then double click the downloaded package file to import it into your project.
You can also use the `Assets > Import Package > Custom Package...` menu option.
In the importing window selection import everything.

## Samples
We have a few example scenes allowing you to try out the system before implementing it yourself.
Below is a brief explanation of each sample scene.

### Basic Movement
This sample takes the preferred method of using the grip button and using the starting point input system to move the player.
Movement is handled by simply translating the play area, this means there are no physics in the scene and you will not collide with barriers.

### Physics Movement
Uses the same inputs as the basic movement sample except that instead of translating the player we use a rigidbody allowing for physics to take place.

