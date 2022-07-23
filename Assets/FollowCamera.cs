using System.Collections;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject beer;
    public float speed;
    public GameObject fin;
    private bool Overview;


    Camera mainCamera;

    float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;

    Vector2 firstTouchPrevPos, secondTouchPrevPos;

    [SerializeField]
    float zoomModifierSpeed = 0.1f;

    [SerializeField]

    private void Start()
    {
        Overview = true;
        this.transform.position = new Vector3(fin.transform.position.x, fin.transform.position.y + 0.2f, fin.transform.position.z - 2.5f);
        StartCoroutine(Wait());


        // Use this for initialization
        mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (!Overview)
        {
            Vector3 target = new Vector3()
            {
                x = beer.transform.position.x,
                y = beer.transform.position.y + 0.2f,
                z = this.transform.position.z,
            };

            Vector3 pos = Vector3.Lerp(this.transform.position, target, speed * Time.deltaTime);
            this.transform.position = pos;
        }


        // Update is called once per frame
        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

            if (touchesPrevPosDifference > touchesCurPosDifference && this.transform.position.z > -11.5f)
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - zoomModifier);
            if (touchesPrevPosDifference < touchesCurPosDifference && this.transform.position.z < -9.5f)
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + zoomModifier);

        }
    }

    public void NewBeer(GameObject newBeer)
    {
        beer = newBeer;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        Overview = false;
    }
}
