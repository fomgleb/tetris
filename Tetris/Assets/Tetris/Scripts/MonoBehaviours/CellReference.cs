using UnityEngine;

namespace Tetris.Scripts.MonoBehaviours
{
    public class CellReference : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer cell;

        public SpriteRenderer Cell => cell;
    }
}
