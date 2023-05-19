using System.Linq;
using UnityEngine;

public class MovementTutorial : MonoBehaviour
{

    private ChessPieceMovement _chessPieceMovement;
    
    private void Start()
    {
        TutorialStateManager.OnTutorialStateChanged += HighlightAvailableMoves;
        TutorialStateManager.OnTutorialStateChanged += Eat;
    }

    private void OnDestroy()
    {
        TutorialStateManager.OnTutorialStateChanged -= HighlightAvailableMoves;
        TutorialStateManager.OnTutorialStateChanged -= Eat;
    }
    
    private void HighlightAvailableMoves(TutorialState tutorialState)
    {
        if (tutorialState != TutorialState.Movement)
            return;

        _chessPieceMovement = FindObjectOfType<ChessPieceMovement>();
        _chessPieceMovement.StartMoving();
    }

    private void Eat(TutorialState tutorialState)
    {
        if (tutorialState != TutorialState.Eat)
            return;

        var enemy = FindObjectsOfType<ChessPieceData>().First(e => e.GetTeam() == Team.White);
        _chessPieceMovement.MoveAndEat(enemy.GetInWhichTileIAm());
    }
}
