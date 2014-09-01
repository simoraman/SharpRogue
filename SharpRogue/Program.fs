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

    let move direction hero = 
        let tryNew newCoordinates = 
            let found = List.find (Utils.findTile newCoordinates) generateCoordinates  
            match found.tile with
            | '#' -> hero
            | '+' -> hero         
            | _ -> {hero with currentPosition = newCoordinates; oldPosition = hero.currentPosition} 
        match direction with
            | Right -> tryNew { x = hero.currentPosition.x + 1; y = hero.currentPosition.y; }
            | Down -> tryNew { x = hero.currentPosition.x; y = hero.currentPosition.y + 1; }
            | Left -> tryNew { x = hero.currentPosition.x - 1; y = hero.currentPosition.y; }
            | Up -> tryNew { x = hero.currentPosition.x; y = hero.currentPosition.y - 1; } 
            | _ -> hero

    let rec gameLoop(hero) =
        drawHero (hero, generateCoordinates)
        let input = getInput()
        match input with
            | Exit -> ()
            | _ -> move input hero |> gameLoop  


    [<EntryPoint>]
    let main argv = 
        generateCoordinates |> drawWorld
        gameLoop { oldPosition = {x = 1; y = 1;}; currentPosition = {x = 1; y = 1;}; }
        0 // return an integer exit code