# FermenterTimer

Look at a fermenter that is working and its hover text tells you how long is left, for example
**Ready in 23m 41s**, or **Ready in 1h 05m** over an hour.

## What it does

- Adds one orange line, **Ready in …**, under the fermenter's own hover text while it ferments.
- Reads the start time the fermenter already stores, so the countdown is exact for everyone and
  stays right after you leave and come back.
- Nothing shows for an empty fermenter, one that is done, or one inside a ward you have no access to.

## Install

On **your game** (client side), with a mod manager or by copying `FermenterTimer.dll` into
`BepInEx/plugins`. The server and the other players need nothing: it works on any server, vanilla or
modded.

## Settings

`BepInEx/config/Tie.FermenterTimer.cfg`, section `[General]`:

| Setting | Default | Meaning |
|---|---|---|
| `ShowSeconds` | `true` | Show seconds under an hour (`23m 41s`); off shows whole minutes (`24m`). |

## Support

If this mod saved you some trouble:

[![Buy me a coffee](https://cdn.buymeacoffee.com/buttons/v2/default-yellow.png)](https://buymeacoffee.com/tieboo)
