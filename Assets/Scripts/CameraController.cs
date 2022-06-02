using UnityEngine;

public class CameraController : MonoBehaviour {

	private bool movementIsActive = true;
	private float scrollCoefficient = 1000f;
	public float scrollSpeed = 5f;
	public float minYPosition = 15f;
	public float maxYPosition = 70f;


	public float panoramicSpeed = 30f;
	public float panoramicBoardThickness = 5f; // толщина полосы, где курсор регистрирует подведение к краю экрана
	
	void Update ()
	{
		if (GameManager.GameIsOver)
        {
			this.enabled = false;
			return;
        }

		if (Input.GetKey(KeyCode.Escape))
			movementIsActive = !movementIsActive;
		if (!movementIsActive)
			return;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panoramicBoardThickness)
        {
			transform.Translate(Vector3.forward * panoramicSpeed * Time.deltaTime, Space.World);
        }
		if (Input.GetKey("s") || Input.mousePosition.y <= panoramicBoardThickness)
		{
			transform.Translate(Vector3.back * panoramicSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panoramicBoardThickness)
		{
			transform.Translate(Vector3.right * panoramicSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("a") || Input.mousePosition.x <=  panoramicBoardThickness)
		{
			transform.Translate(Vector3.left * panoramicSpeed * Time.deltaTime, Space.World);
		}

		float scroll = Input.GetAxis("Mouse ScrollWheel");

		Vector3 position = transform.position;	

		position.y -= scroll * scrollSpeed * Time.deltaTime * scrollCoefficient;
		position.y = Mathf.Clamp(position.y, minYPosition, maxYPosition);          //зажимаем возможность прокрутки в фиксированных значениях

		transform.position = position;

	}
}
