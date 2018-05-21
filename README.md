# Natural Locomotion
Natural Locomotion is a VR movement system aimed at enabling a seemless unrestricted form of movement in VR.

## Social Media
[Website](http://www.digitaldiceworks.com) | 
[Twitter](https://twitter.com/DigiDiceworks) |
[Facebook](https://www.facebook.com/Digital-Diceworks-2001428196760669/) | 
[LinkedIn](https://www.linkedin.com/company/18320768/) |
[Discord](http://bit.ly/DDWDiscord)

## Overview
![High Level Overview](https://g.gravizo.com/svg?
  digraph G {
    Locomotion Hub [shape=box];
    Locomotion Hub -> Primary Input;
    Locomotion Hub -> Secondary Input;
    Primary Input -> Modifiers;
    Secondary Input -> Modifiers;
    Modifiers -> On Input Event [shape=box];
  }
)
Note that the system itself doesnt directly move the play zone, instead it just forwards it to the `onInput` event in the `Locomotion Hub`.
This provides a very simple and extensible way to use the system.
In fact the locomotion hub doesn't handle anything directly except for separating the primary and secondary inputs, everything else it has events for.
You can read into each subsystem in the documentation linked below.

## Documentation
All documentation is stored in the `docs` directory. In addition to guides and walkthroughs the code base is fully documented with XML docs.
* [Installation](docs/installation.md): Preparing, downloading, installing and trying the sample scenes.
* [Getting Started](docs/gettingstarted.md): How to get started using Natural Locomotion in your game.
* [Locomotion Hub](docs/locomotionhub.md): What does the locomotion hub do and why does it do it.
* [Natural Input](docs/naturalinput.md): How to gather player inputs, looking mainly at our recommended method.
* [Movement](docs/movement.md): How to actually move the player.
* [Modifiers](docs/modifiers.md): How to take raw input and apply modifications based on anything your game needs.

## Contributing
A more in depth guide will be written soon. But any of the following:
1. Spreading the word!
1. Pull requests for issues, concerns or features
1. Helping out others

## Contact Us
We are avid users of Discord and you can usually find at least one of us on the [Digital Diceworks discord](http://bit.ly/DDWDiscord).

