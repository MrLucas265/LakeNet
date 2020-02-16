using UnityEngine;
using System.Collections;
using System.IO;

public class ScreenShot : MonoBehaviour
{
	public bool Bug;
	public string path;
	void Update()
	{
		if (Input.GetKeyDown("k"))
			TakeShot();
	}

	public void TakeShot()
	{
		StartCoroutine(ScreenshotEncode());
	}

	public static string ScreenShotName(int width, int height) {
		return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png", 
			Application.dataPath, 
			width, height, 
			System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
	}

	public IEnumerator ScreenshotEncode()
	{
		// wait for graphics to render
		yield return new WaitForEndOfFrame();

		// create a texture to pass to encoding
		Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

		// put buffer into texture
		texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		texture.Apply();

		// split the process up--ReadPixels() and the GetPixels() call inside of the encoder are both pretty heavy
		yield return 0;

		byte[] bytes = texture.EncodeToPNG();

		// save our test image (could also upload to WWW)
		string filename = ScreenShotName(Screen.width, Screen.height);
		if (Bug == true)
		{
			File.WriteAllBytes(path + "/" + System.DateTime.Now.ToString("yy-MM-dd_HH-mm-ss") + ".png", bytes);
			Bug = false;
		}
		else
		{
			File.WriteAllBytes(Application.dataPath + "/screenshots/" + System.DateTime.Now.ToString("yy-MM-dd_HH-mm-ss") + ".png", bytes);
		}

		// Added by Karl. - Tell unity to delete the texture, by default it seems to keep hold of it and memory crashes will occur after too many screenshots.
		DestroyObject( texture );

		//Debug.Log( Application.dataPath + "/../testscreen-" + count + ".png" );
	}
}