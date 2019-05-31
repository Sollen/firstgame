using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerKeyboardController : MonoBehaviour
    {
        public Player player;

        private void Start()
        {
            player = player != null ? GetComponent<Player>() : player;
            if (player == null)
                Debug.Log("Player don't set to controller");
        }

        private void Update()
        {
            if (player != null)
            {
                if (Input.GetKey(KeyCode.D)) player.MoveRight();
                if (Input.GetKey(KeyCode.A)) player.MoveLeft();
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) player.Jump();
            }
        }
    }
}
