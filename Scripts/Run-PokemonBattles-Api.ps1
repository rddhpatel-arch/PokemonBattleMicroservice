# ---------------------------------------------
# Pokémon Battle API – Automated Test Script
# Runs 10 random plays and prints results
# 
# Run below commands from Windows PowerShell command window
# 1.	Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
# 2.	<physical-path-to-script-file>\Run-PokemonBattles.ps1
# ---------------------------------------------

$baseUrl = "http://localhost:5080/api/v1/battle"

# Enum mapping for readability
$types = @{
    0 = "Fire"
    1 = "Water"
    2 = "Grass"
}

Write-Host "Launching 10 random Pokémon battles..."
Write-Host ""

for ($i = 1; $i -le 10; $i++) {

    # Pick a random Pokémon type (0,1,2)
    $playerChoice = Get-Random -Minimum 0 -Maximum 3

    $body = @{
        playerChoice = $playerChoice
    } | ConvertTo-Json

    # Call POST /play
    $response = Invoke-RestMethod -Uri "$baseUrl/play" `
                                  -Method Post `
                                  -Body $body `
                                  -ContentType "application/json"

    $playerType   = $types[$response.player]
    $opponentType = $types[$response.opponent]
    $outcome      = $response.outcome

    Write-Host "Play ${i}:"
    Write-Host "  Player:   $playerType"
    Write-Host "  Opponent: $opponentType"
    Write-Host "  Outcome:  $outcome"
    Write-Host ""
}

Write-Host "Fetching final statistics..."
Write-Host ""

$stats = Invoke-RestMethod -Uri "$baseUrl/stats" -Method Get

Write-Host "=== Final Battle Statistics ==="
$stats.PSObject.Properties | ForEach-Object {
    Write-Host "$($_.Name): $($_.Value)"
}