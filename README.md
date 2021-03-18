# OnionFruit™ Geolocation Database
GeoIP CDN for OnionFruit™

[![Latest Release](https://img.shields.io/github/v/release/dragonfruitnetwork/OnionFruit)](https://github.com/dragonfruitnetwork/OnionFruit/releases)
[![Total Downloads](https://img.shields.io/github/downloads/dragonfruitnetwork/OnionFruit/total)](https://github.com/dragonfruitnetwork/OnionFruit/releases)
[![Crowdin](https://badges.crowdin.net/onionfruit/localized.svg)](https://crowdin.com/project/onionfruit)
[![DragonFruit Discord](https://img.shields.io/discord/482528405292843018?label=Discord&style=popout)](https://discord.gg/VA26u5Z)

## Overview
This is a project that aims to provide timed releases/updates to the databases OnionFruit™ Connect (Client) uses to provide country-related information.
The assets are then published publicly for anyone to use.

This essentially consists of a timed GitHub Action that builds some docker images with the required software which are then run and the outputs published as a static site.

## Licenses

This component is licensed under the MIT License.
This project makes reference to [GeoIP](https://www.maxmind.com/), but their database is no longer used. The [IPFire Location Database](https://location.ipfire.org/) is used instead.