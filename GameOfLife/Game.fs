module GameOfLife.Game

type Dead = Dead of bool
type Live = Live of bool

type Cell = 
    |Dead
    |Live

type Game = {
    height: int
    width: int
    Grid: Cell list}

let inline toXY game index =
    let y = index / game.width
    let x = index % game.width
    x,y

let inline toIndex game x y =
    y * game.width + x

let inline isCellLive cell =
    match cell with
    | Dead -> 0
    | Live -> 1

let inline liveAt grid (x,y) =
    let index = toIndex grid x y
    match index with
    | z when x >= grid.width -> Dead
    | z when x < 0 -> Dead
    | z when y < 0 -> Dead
    | z when y >= grid.height -> Dead
    | z -> List.item index grid.Grid   

let inline calcNeighborsRange game index =
    let (x,y) = toXY game index
    
    [x-1,y+1; x,y+1;x+1,y+1;
    x-1,y; x+1,y;
    x-1,y-1; x,y-1; x+1,y-1]

let inline cycleElement game index element =
    let neighborRange = calcNeighborsRange game index
    let neighbors = List.mapi (fun x y -> liveAt game y) neighborRange
    let neighborsLive = List.sumBy (fun x -> isCellLive x) neighbors
    match neighborsLive with 
    | x when element = Live && (x = 2 || x = 3) -> Live
    | x when element = Dead && x = 3 -> Live
    | _ -> Dead
 
let inline Cycle game =
    let cycledGrid = List.mapi (fun x y -> cycleElement game x y) game.Grid
    {game with Grid = cycledGrid} 
