﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenPiece : BasePiece
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerable<ChessSquare> GetAvailableMoves(BasePiece[][] pieces, int? element, int[][] elementalSquares)
    {
        var moves = new List<ChessSquare>();
        var column = currentSquare.Column - 65;
        var row = currentSquare.Row - 1;

        var fire = element.HasValue && element.Value == elementalSquares[row][column] && element.Value == 2;

        // Handle rook-like moves
        var topIncr = row;
        var topBurnout = fire ? 1 : 0;
        var bottomIncr = row;
        var bottomBurnout = fire ? 1 : 0;
        var leftIncr = column;
        var leftBurnout = fire ? 1 : 0;
        var rightIncr = column;
        var rightBurnout = fire ? 1 : 0;
        while (topIncr < 7 && (pieces[topIncr + 1][column] == null || pieces[topIncr + 1][column].Team != Team))
        {
            topIncr++;
            moves.Add(new ChessSquare(topIncr + 1, currentSquare.Column));
            if (pieces[topIncr][column] != null)
            {
                if (topBurnout == 0)
                {
                    break;
                }
                topBurnout--;
            }
        }

        while (bottomIncr > 0 && (pieces[bottomIncr - 1][column] == null || pieces[bottomIncr - 1][column].Team != Team))
        {
            bottomIncr--;
            moves.Add(new ChessSquare(bottomIncr + 1, currentSquare.Column));
            if (pieces[bottomIncr][column] != null)
            {
                if (bottomBurnout == 0)
                {
                    break;
                }
                bottomBurnout--;
            }
        }

        while (rightIncr < 7 && (pieces[row][rightIncr + 1] == null || pieces[row][rightIncr + 1].Team != Team))
        {
            rightIncr++;
            moves.Add(new ChessSquare(currentSquare.Row, (char)(rightIncr + 65)));
            if (pieces[row][rightIncr] != null)
            {
                if (rightBurnout == 0)
                {
                    break;
                }
                rightBurnout--;
            }
        }

        while (leftIncr > 0 && (pieces[row][leftIncr - 1] == null || pieces[row][leftIncr - 1].Team != Team))
        {
            leftIncr--;
            moves.Add(new ChessSquare(currentSquare.Row, (char)(leftIncr + 65)));
            if (pieces[row][leftIncr] != null)
            {
                if (leftBurnout == 0)
                {
                    break;
                }
                leftBurnout--;
            }
        }


        // Handle bishop-like moves
        var topLeftRowIncr = row;
        var topLeftColIncr = column;
        var topLeftBurnout = fire ? 1 : 0;
        while (topLeftRowIncr < 7 && topLeftColIncr > 0 && (pieces[topLeftRowIncr + 1][topLeftColIncr - 1] == null || pieces[topLeftRowIncr + 1][topLeftColIncr - 1].Team != Team))
        {
            topLeftRowIncr++;
            topLeftColIncr--;
            moves.Add(new ChessSquare(topLeftRowIncr + 1, (char)(topLeftColIncr + 65)));
            if (pieces[topLeftRowIncr][topLeftColIncr] != null)
            {
                if (topLeftBurnout == 0)
                {
                    break;
                }
                topLeftBurnout--;
            }
        }

        var topRightRowIncr = row;
        var topRightColIncr = column;
        var topRightBurnout = fire ? 1 : 0;

        while (topRightRowIncr < 7 && topRightColIncr < 7 && (pieces[topRightRowIncr + 1][topRightColIncr + 1] == null || pieces[topRightRowIncr + 1][topRightColIncr + 1].Team != Team))
        {
            topRightRowIncr++;
            topRightColIncr++;
            moves.Add(new ChessSquare(topRightRowIncr + 1, (char)(topRightColIncr + 65)));
            if (pieces[topRightRowIncr][topRightColIncr] != null)
            {
                if (topRightBurnout == 0)
                {
                    break;
                }
                topRightBurnout--;
            }
        }

        var bottomLeftRowIncr = row;
        var bottomLeftColIncr = column;
        var bottomLeftBurnout = fire ? 1 : 0;
        while (bottomLeftRowIncr > 0 && bottomLeftColIncr > 0 && (pieces[bottomLeftRowIncr - 1][bottomLeftColIncr - 1] == null || pieces[bottomLeftRowIncr - 1][bottomLeftColIncr - 1].Team != Team))
        {
            bottomLeftRowIncr--;
            bottomLeftColIncr--;
            moves.Add(new ChessSquare(bottomLeftRowIncr + 1, (char)(bottomLeftColIncr + 65)));
            if (pieces[bottomLeftRowIncr][bottomLeftColIncr] != null)
            {
                if (bottomLeftBurnout == 0)
                {
                    break;
                }
                bottomLeftBurnout--;
            }
        }

        var bottomRightRowIncr = row;
        var bottomRightColIncr = column;
        var bottomRightBurnout = fire ? 1 : 0;
        while (bottomRightRowIncr > 0 && bottomRightColIncr < 7 && (pieces[bottomRightRowIncr - 1][bottomRightColIncr + 1] == null || pieces[bottomRightRowIncr - 1][bottomRightColIncr + 1].Team != Team))
        {
            bottomRightRowIncr--;
            bottomRightColIncr++;
            moves.Add(new ChessSquare(bottomRightRowIncr + 1, (char)(bottomRightColIncr + 65)));
            if (pieces[bottomRightRowIncr][bottomRightColIncr] != null)
            {
                if (bottomRightBurnout == 0)
                {
                    break;
                }
                bottomRightBurnout--;
            }
        }

        if (element.HasValue && element.Value == elementalSquares[row][column])
        {
            switch (element.Value)
            {
                case 0:
                    for (var i = 0; i < pieces.Length; i++)
                    {
                        for (var j = 0; j < pieces[i].Length; j++)
                        {
                            var colDiff = Math.Abs(column - j);
                            var rowDiff = Math.Abs(row - i);

                            if (elementalSquares[i][j] == 0 && (pieces[i][j] == null || pieces[i][j].Team != Team) && (rowDiff == 0 || colDiff == 0 || (rowDiff == colDiff)))
                            {
                                moves.Add(new ChessSquare(i + 1, (char)(j + 65)));
                            }
                        }
                    }
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                default:
                    break;
            }
        }


        return moves;
    }
}
