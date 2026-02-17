
# Pokémon Battle Microservice

A small .NET Web API that lets players choose a Pokémon type (**Fire**, **Water**, **Grass**) and battle a randomly selected opponent.  
The service also tracks in‑memory statistics for wins, losses, and draws.

## Endpoints
- **POST /play** – Play a battle  
- **GET /stats** – View battle statistics  

## Scripts
PowerShell script Run-PokemonBattles.ps1 is included in the **Scripts** folder to run 10 random plays and print results. Use below commands to run the scripts.
- **1.**  Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass  
- **2.** \<physical-path-to-script-file\>\Run-PokemonBattles.ps1 

## Swagger UI:
```
http://localhost:5080/swagger
```
