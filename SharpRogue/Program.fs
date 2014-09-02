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
        let found = List.find (Utils.findTile newCoordinates) world.tiles   
        match found.tile with
            | '#' -> hero
            | '+' -> hero         
            | _ -> {hero with currentPosition = newCoordinates; oldPosition = hero.currentPosition} 

    let openDoor hero world = 
        let direction = getInput()
        let coordinate = getNextCoordinate hero direction
        let tile = List.find (Utils.findTile coordinate) world.tiles
        if tile.tile = '+' then drawOpenDoor tile.coordinate
        let newTiles = List.map (fun x -> if x.coordinate = coordinate then { x with tile = '-'} else x) world.tiles

        {world with hero = hero; tiles = newTiles}

    let rec gameLoop world =
        drawHero (world.hero, world.tiles)
        let input = getInput()
        match input with
            | Exit -> ()
            | Open -> openDoor world.hero world |> gameLoop
            | _ -> {world with hero = (move input world);} |> gameLoop  

    [<EntryPoint>]
    let main argv = 
        generateCoordinates |> drawWorld
        let world = {hero = { oldPosition = {x = 1; y = 1;}; currentPosition = {x = 1; y = 1;}; }; tiles = generateCoordinates}
        gameLoop world
        0 // return an integer exit code