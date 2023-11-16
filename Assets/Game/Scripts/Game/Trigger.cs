using UnityEngine;

namespace Game
{
    public class Trigger : MonoBehaviour
    {
        public GameObject Push;

        private GameObject player;

        private void Update()
        {
            if (player != null)
            {
                if (player.GetComponent<Rigidbody>().velocity.x == 0)
                {
                    Push.SendMessage("DoIt");
                    gameObject.SetActive(false);
                }
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                player = other.gameObject;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                player = null;
            }
        }
    }
}
