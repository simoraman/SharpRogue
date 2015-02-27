namespace SharpRogue
module main =
    open Microsoft.FSharp.Collections
    open Types
    open Graphics

    let rec getInput() = 
        let input = System.Console.ReadKey(true) 
        match input.Key with
            | System.ConsoleKey.RightArrow -> Right
            | System.ConsoleKey.DownArrow -> Down
            | System.ConsoleKey.LeftArrow -> Left
            | System.ConsoleKey.UpArrow -> Up
            | System.ConsoleKey.O -> Open
            | System.ConsoleKey.Q -> Exit
            | _ -> getInput()

    let map1 = [ 
            "##############";
            "#>           #          ######";
            "#            ############    #";
            "#            -          +    #";
            "#    ~~      ############    #";
            "#     ~~     #          #    #";
            "#      ~~    #          # <  #";
            "##############          ######" ]

    //mutable crap
    let generateCoordinates =
        let mutable y' = 0
        let mutable coord = List.empty
        for row in Utils.tokenizeMap map1 do
            let mutable x' = 0
            for col in row do
                coord <- List.append coord [{ coordinate = { x = x'; y = y'; }; tile = col; }]
                x' <- x' + 1
            y' <- y' + 1
        coord

    let getNextCoordinate hero direction = 
        match direction with
            | Right -> { x = hero.currentPosition.x + 1; y = hero.currentPosition.y; }
            | Down -> { x = hero.currentPosition.x; y = hero.currentPosition.y + 1; }
            | Left -> { x = hero.currentPosition.x - 1; y = hero.currentPosition.y; }
            | Up -> { x = hero.currentPosition.x; y = hero.currentPosition.y - 1; } 
            | _ -> hero.currentPosition

    let move direction world = 
        let hero = world.hero
        let newCoordinates = getNextCoordinate hero direction
        if newCoordinates = world.monster.currentPosition 
            then hero
            else
                let found = List.find (Utils.findTile newCoordinates) world.tiles   
                match found.tile with
                    | '#' -> hero
                    | '+' -> hero
                    | 'e' -> hero
                    | _ -> {hero with currentPosition = newCoordinates; oldPosition = hero.currentPosition} 

    let openDoor hero world = 
        let direction = getInput()
        let coordinate = getNextCoordinate hero direction
        let tile = List.find (Utils.findTile coordinate) world.tiles
        if tile.tile = '+' then drawOpenDoor tile.coordinate
        let newTiles = List.map (fun x -> if x.coordinate = coordinate then { x with tile = '-'} else x) world.tiles

        {world with hero = hero; tiles = newTiles}

    let moveMonster world =
        let monster = world.monster
        let hero = world.hero
        let deltaX = hero.currentPosition.x - monster.currentPosition.x
        let deltaY = hero.currentPosition.y - monster.currentPosition.y
        let direction = 
            if abs(deltaX) > abs(deltaY) then 
                if deltaX < 0 then Left else Right
            else 
                if deltaY < 0 then Up else Down
        let newPosition = getNextCoordinate monster direction
        {monster with oldPosition = monster.currentPosition; currentPosition = newPosition}

    let rec gameLoop world =
        drawHero (world.hero, world.tiles)
        drawHero (world.monster, world.tiles)
        let input = getInput()
        let monster = moveMonster world
        match input with
            | Exit -> ()
            | Open -> openDoor world.hero world |> gameLoop
            | _ -> {world with hero = (move input world); monster = monster} |> gameLoop  

    [<EntryPoint>]
    let main argv = 
        generateCoordinates |> drawWorld
        let hero = { avatar = '@'; oldPosition = {x = 0; y = 0;}; currentPosition = {x = 1; y = 1;}; }
        let monster = { avatar = 'e'; oldPosition = {x = 0; y = 0;}; currentPosition = {x = 10; y = 3;}; }
        let world = {hero = hero; tiles = generateCoordinates; monster = monster}
        gameLoop world
        0 // return an integer exit code