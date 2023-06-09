using UnityEngine;

public class CameraCntr : MonoBehaviour
{
    [SerializeField] private float dumping = 1.5f;
    [SerializeField] private Vector2 offset = new Vector2(2f, 1f);
    private bool isLeft;
    [SerializeField]private Transform player;
    private int lastX;

    private void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindPlayer(isLeft);
    }

    public void FindPlayer(bool playerIsLeft)
    {
        lastX = Mathf.RoundToInt(player.position.x);
        if (playerIsLeft)
        {
            CameraMoov(-1);
        }
        else
        {
            CameraMoov(1);
        }
    }

    private void CameraMoov(int d)
    {
        transform.position = new Vector3(player.position.x + d * offset.x, player.position.y + d * offset.y, transform.position.z);
    }

    private void Update()
    {
        if (player)
        {
            int currentX = Mathf.RoundToInt(player.position.x);
            if (currentX > lastX) isLeft = false; else if (currentX < lastX) isLeft = true;
            lastX = Mathf.RoundToInt(player.position.x);

            Vector3 target;
            if (isLeft)
            {
                target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
            }
            else
            {
                target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
            }

            Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;
        }
    }
}
