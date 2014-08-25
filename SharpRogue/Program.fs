// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

type Coordinate = { x:int; y:int; }

type Input = 
    Up 
    | Down
    | Left
    | Right
    | Exit

let rec getInput() = 
    let input = System.Console.ReadKey(true) 
    match input.Key with
        | System.ConsoleKey.RightArrow -> Right
        | System.ConsoleKey.DownArrow -> Down
        | System.ConsoleKey.LeftArrow -> Left
        | System.ConsoleKey.UpArrow -> Up
        | System.ConsoleKey.Q -> Exit
        | _ -> getInput()

let drawHero (coordinate:Coordinate) = 
    System.Console.Clear()
    System.Console.SetCursorPosition(coordinate.x, coordinate.y)
    System.Console.Write '@'

let rec gameLoop(coordinate) =
    drawHero coordinate
    let input = getInput()
    match input with
        | Right -> gameLoop { x = coordinate.x + 1; y = coordinate.y; }
        | Down -> gameLoop { x = coordinate.x; y = coordinate.y + 1; }
        | Left -> gameLoop { x = coordinate.x - 1; y = coordinate.y; }
        | Up -> gameLoop { x = coordinate.x; y = coordinate.y - 1; }
        | Exit -> ()

[<EntryPoint>]
let main argv = 
    gameLoop {x = 0; y = 0;}
    0 // return an integer exit code


