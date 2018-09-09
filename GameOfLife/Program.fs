open System
open System
open System.Threading
open GameOfLife.Game

let inline AliveCount grid =
    List.sumBy (fun x -> if x = Live then 1 else 0) grid

let inline printGameState game loop =
    let symbol cell = match cell with
                        | Live -> "X"
                        | Dead -> " "
    let alive = AliveCount game.Grid
    printfn "#Loop: %i        Living: %i" loop alive
    printfn ""
    List.iteri (fun index cell ->
        match index % game.width with
        | a when a+1 = game.width ->  symbol cell |> printfn "%s"
        | _ ->  symbol cell |> printf "%s") game.Grid

let rec GameLoop game loopNumber =
    do Console.Clear()
    do printGameState game loopNumber
    do Thread.Sleep(100)

    match AliveCount game.Grid with
    | 0 -> printf "Game Over"
    | _ -> game |> Cycle |> GameLoop <| 1+loopNumber

let MakeGameBoard height width =
    let random = Random()
    List.init (height * width) (fun index -> if random.NextDouble() > 0.41 then Live else Dead)

[<EntryPoint>]
let main argv =
    printfn "Inputs: %A" argv
    let height = Array.get argv 0 |> Int32.Parse
    let width = Array.get argv 1 |> Int32.Parse

    let initalGameState = {Grid = MakeGameBoard height width; height = height; width = width}
    GameLoop initalGameState 0
    0 // return an integer exit code
