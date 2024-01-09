using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HalfMove 
{
    public readonly chess chessPiece;
    public readonly Movement Move;
    public readonly bool CapturedPiece;
    public readonly bool CausedCheck;
    public bool CausedStalemate { get; private set; }
    public bool CausedCheckmate { get; private set; }
    
    private static readonly Dictionary<chess.chessType, string> pieceTypeToANSymbolMap = new Dictionary<chess.chessType, string> {
        { chess.chessType.Pawn, "" },
        { chess.chessType.Knight, "N" },
        { chess.chessType.Bishop, "B" },
        { chess.chessType.Rook, "R" },
        { chess.chessType.Queen, "Q" },
        { chess.chessType.King, "K" },		
    };

    public HalfMove(chess piece, Movement move, bool capturedPiece, bool causedCheck) {
        chessPiece = piece;
        Move = move;
        CapturedPiece = capturedPiece;
        CausedCheck = causedCheck;
        CausedCheckmate = default;
        CausedStalemate = default;
    }

    public void SetGameEndBools(bool causedStalemate, bool causedCheckmate) {
        CausedCheckmate = causedCheckmate;
        CausedStalemate = causedStalemate;
    }
    
    // TODO handle ambiguous piece moves.
    public string ToAlgebraicNotation() {
        string pieceSymbol = (chessPiece.mChessType == chess.chessType.Pawn) && CapturedPiece
            ? chessBoard.getBoardCodeString(Move.Start.x)
            : pieceTypeToANSymbolMap[chessPiece.mChessType];

        string capture = CapturedPiece ? "x" : string.Empty;
        string endSquare = chessBoard.getBoardGridString(Move.End);
        string suffix = CausedCheckmate
            ? "#"
            : CausedCheck
                ? "+"
                : string.Empty;

        string moveText;
        switch (chessPiece.mChessType) {
            // CastlingMove
            // case chess.chessType.King when Move is CastlingMove: {
            //     moveText = Move.End.x == 3 ? $"O-O-O{suffix}" : $"O-O{suffix}";
            //     break;
            // }
            // PromotionMove
            // case chess.chessType.Pawn: {
            //     string promotionPiece = Move is PromotionMove promotionMove
            //         ? $"={pieceTypeToANSymbolMap[promotionMove.PromotionPiece.mChessType]}"
            //         : string.Empty;

            //     moveText = $"{pieceSymbol}{capture}{endSquare}{promotionPiece}{suffix}";
            //     break;
            // }
            default: {
                moveText = $"{pieceSymbol}{capture}{endSquare}{suffix}";
                break;
            }
        }

        return moveText;
    }
}