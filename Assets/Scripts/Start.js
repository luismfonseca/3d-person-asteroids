function OnGUI () {

    if (GUI.Button (Rect ((Screen.width/2-60), (Screen.height/2)+80, 120, 30), "START GAME")) {

        Application.LoadLevel("level1");

    }
}