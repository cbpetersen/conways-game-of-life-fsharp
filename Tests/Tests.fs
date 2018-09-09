module Tests

open System
open Xunit
open GameOfLife.Game

[<Fact>]
let ``a cell without neighbors stays dead`` () =
    let grid = {Grid = [Dead;]; height = 1; width = 1} |> Cycle
    Assert.Equal(Dead, liveAt grid (0,0))

[<Fact>]
let ``a cell without neighbors dies`` () =
    let grid = {Grid = [Live;]; height = 1; width = 1} |> Cycle
    Assert.Equal(Dead, liveAt grid (0,0))

[<Fact>]
let ``two vertical live cells dies from under population`` () =
    let grid = {Grid = [Live;Live;]; height = 2; width = 1} |> Cycle
    Assert.Equal(Dead, liveAt grid (0,0))
    Assert.Equal(Dead, liveAt grid (0,1))

[<Fact>]
let ``two horizontal live cells dies from under population`` () =
    let grid = {Grid = [Live;Live;]; height = 1; width = 2} |> Cycle
    Assert.Equal(Dead, liveAt grid (0,0))
    Assert.Equal(Dead, liveAt grid (1,0))

[<Fact>]
let ``tree live cells lives on to the next generation and a new is reproduced in 4x4`` () =
    let grid = {Grid = [Live;Live;Live;Dead]; height = 2; width = 2} |> Cycle
    Assert.Equal(Live, liveAt grid (0,0))
    Assert.Equal(Live, liveAt grid (0,1))
    Assert.Equal(Live, liveAt grid (1,0))
    Assert.Equal(Live, liveAt grid (1,1))

[<Fact>]
let ``four horizontal lives on to the next generation`` () =
    let grid = {Grid = [Live;Live;Live;Live;]; height = 2; width = 2} |> Cycle
    Assert.Equal(Live, liveAt grid (0,0))
    Assert.Equal(Live, liveAt grid (0,1))
    Assert.Equal(Live, liveAt grid (1,0))
    Assert.Equal(Live, liveAt grid (1,1))

[<Fact>]
let ``Block stays block`` () =
    let grid = {
        Grid = [
            Dead;Dead;Dead;Dead;
            Dead;Live;Live;Dead;
            Dead;Live;Live;Dead;
            Dead;Dead;Dead;Dead;
        ]
        height = 4
        width = 4
    }
    let nextGen = Cycle grid

    Assert.Equal(grid, nextGen)

[<Fact>]
let ``Beehive stays Beehive`` () =
    let grid = {
        Grid = [
            Dead;Dead;Dead;Dead;Dead;Dead;
            Dead;Dead;Live;Live;Dead;Dead;
            Dead;Live;Dead;Dead;Live;Dead;
            Dead;Dead;Live;Live;Dead;Dead;
            Dead;Dead;Dead;Dead;Dead;Dead;
        ]
        height = 5
        width = 6
    }
    let nextGen = Cycle grid

    Assert.Equal(grid, nextGen)

[<Fact>]
let ``Toad becomes toad after two cycles`` () =
    let grid = {
        Grid = [
            Dead;Dead;Dead;Dead;Dead;Dead;
            Dead;Dead;Dead;Dead;Dead;Dead;
            Dead;Dead;Live;Live;Live;Dead;
            Dead;Live;Live;Live;Dead;Dead;
            Dead;Dead;Dead;Dead;Dead;Dead;
            Dead;Dead;Dead;Dead;Dead;Dead;
        ]
        height = 5
        width = 6
    }
    let nextGen = Cycle grid |> Cycle

    Assert.Equal(grid, nextGen)
