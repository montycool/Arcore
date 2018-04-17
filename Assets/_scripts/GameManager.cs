

namespace GoogleARCore.HelloAR
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
	using System.Runtime.Serialization.Formatters.Binary;
	using System.IO;
    public class GameManager : MonoBehaviour
    {

        // Use this for initialization
        int score;
        public Text _currentScoreText;
        public Text _TopScoreText;

        public Joystick _joystick;
        public Transform _t;
        public HelloARController _HC;
		string filePath;    //for save jason or binary
		public Save gameData;  // serelize class

        void Start()
        {
			filePath = Application.persistentDataPath + "/data.json";
			//LoadGame ();  // call for binary data load
			LoadGameData();  //call for json dat load
        }

        // Update is called once per frame
        void Update()
        {
            if (_HC._tempTrans != null)
            {

                _HC._tempTrans.Translate(-_joystick.Horizontal * 0.008f, 0, -_joystick.Vertical * 0.008f);
            }
            //_t.Translate(_joystick.Horizontal/10,0,_joystick.Vertical/10);
        }

        public void startRespawn()
        {

            _HC._isSelectable = false;
            scoreUpdate();
        }

        void scoreUpdate()
        {
            score += 1;
            _currentScoreText.text = "Current Score :" + score.ToString();

			//SaveGame(); // for binary data save
			SaveGameData ();  // for jason data save
        }

		private Save CreateSaveGameObject()
		{
			Save save = new Save();
		

			save.HighScore = score;


			return save;
		}


		//this funcio call for binary data save

		public void SaveGame()
		{
			
			Save save = CreateSaveGameObject();


			BinaryFormatter bformate = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
			bformate.Serialize(file, save);
			file.Close();

	
		}


		//this funcio call for binary data load
		public void LoadGame()
		{ 
			
			if (File.Exists (Application.persistentDataPath + "/gamesave.save")) {
				
			
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (Application.persistentDataPath + "/gamesave.save", FileMode.Open);
				Save save = (Save)bf.Deserialize (file);
				file.Close ();



				_TopScoreText.text = "Top score: " + save.HighScore;


			}
		}


		private void LoadGameData()
		{
			

			if (File.Exists (filePath)) {
				string dataAsJson = File.ReadAllText (filePath);
				Save gameData = JsonUtility.FromJson<Save> (dataAsJson);
				_TopScoreText.text = "Top score: " + gameData.HighScore;

			} 
		}

		private void SaveGameData()
		{
			gameData = new Save ();
			gameData.HighScore = score;
			string dataAsJson = JsonUtility.ToJson (gameData);
			File.WriteAllText (filePath, dataAsJson);

		}
    }
}
