# FermenterTimer

Client-side BepInEx plugin for Valheim: a fermenter's hover text shows how long until it is ready
(`Ready in 23m 41s`, or `1h 05m` over an hour).

It patches `Fermenter.GetHoverText` and computes the time left from the fermentation start time the
fermenter already stores (`GetFermentationTime`) and its `m_fermentationDuration`, so it needs
nothing on the server and is exact for every player. Nothing shows for an empty or finished
fermenter, or inside a ward the player cannot access.

Settings: `BepInEx/config/Tie.FermenterTimer.cfg` (`ShowSeconds`, default on).

Install with a mod manager: [Tie-FermenterTimer on Thunderstore](https://thunderstore.io/c/valheim/p/Tie/FermenterTimer/).
`thunderstore/build.sh <folder>` builds that package (mod page, icon from `thunderstore/make_icon.py`,
changelog and manifest in `thunderstore/`; the version is `Version` in the plugin).
