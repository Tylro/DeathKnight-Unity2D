using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;


[System.Serializable]
public struct SaveData
{
    public static SaveData Instance;

    //map stuff
    public HashSet<string> sceneNames;

    //bench stuff
    public string benchSceneName;
    public Vector2 benchPos;

    //player stuff
    public int playerHealth;
    public int playerMaxHealth;
    public int playerHeartShards;
    public float playerMana;
    public int playerManaOrbs;
    public int playerOrbShard;
    public float playerOrb0fill, playerOrb1fill, playerOrb2fill;
    public bool playerHalfMana;
    public Vector2 playerPosition;
    public string lastScene;

    public bool playerUnlockedWallJump, playerUnlockedDash, playerUnlockedVarJump;
    public bool playerUnlockedSideCast, playerUnlockedUpCast, playerUnlockedDownCast;

    //enemies stuff
    //shade
    public Vector2 shadePos;
    public string sceneWithShade;
    public Quaternion shadeRot;



    public void Initialize()
    {
        if(!File.Exists(Application.persistentDataPath + "/save.bench.data")) //if file doesnt exist, well create the file
        {
            BinaryWriter writer = new BinaryWriter(File.Create(Application.persistentDataPath + "/save.bench.data"));
        }
        if (!File.Exists(Application.persistentDataPath + "/save.player.data")) //if file doesnt exist, well create the file
        {
            BinaryWriter writer = new BinaryWriter(File.Create(Application.persistentDataPath + "/save.player.data"));
        }
        if (!File.Exists(Application.persistentDataPath + "/save.shade.data")) //if file doesnt exist, well create the file
        {
            BinaryWriter writer = new BinaryWriter(File.Create(Application.persistentDataPath + "/save.shade.data"));
        }

        if (sceneNames == null)
        {
            sceneNames = new HashSet<string>();
        }
    }
    #region Bench Stuff
    public void SaveBench()
    {
        using(BinaryWriter writer = new BinaryWriter(File.OpenWrite(Application.persistentDataPath + "/save.bench.data")))
        {
            writer.Write(benchSceneName);
            writer.Write(benchPos.x);
            writer.Write(benchPos.y);
        }
    }
    public void LoadBench()
    {
        string savePath = Application.persistentDataPath + "/save.bench.data";
        if(File.Exists(savePath) && new FileInfo(savePath).Length > 0)
        {
            using(BinaryReader reader = new BinaryReader(File.OpenRead(Application.persistentDataPath + "/save.bench.data")))
            {
                benchSceneName = reader.ReadString();
                benchPos.x = reader.ReadSingle();
                benchPos.y = reader.ReadSingle();
            }
        }
        else
        {
            Debug.Log("Bench doesnt exist");
        }
    }
    #endregion

    #region Player stuff
    public void SavePlayerData()
    {
        using(BinaryWriter writer = new BinaryWriter(File.OpenWrite(Application.persistentDataPath + "/save.player.data")))
        {
            playerHealth = PlayerController.Instance.Health;
            writer.Write(playerHealth);
            playerMaxHealth = PlayerController.Instance.maxHealth;
            writer.Write(playerMaxHealth);
            playerHeartShards = PlayerController.Instance.heartShards;
            writer.Write(playerHeartShards);

            playerMana = PlayerController.Instance.Mana;
            writer.Write(playerMana);
            playerHalfMana = PlayerController.Instance.halfMana;
            writer.Write(playerHalfMana);
            playerManaOrbs = PlayerController.Instance.manaOrbs;
            writer.Write(playerManaOrbs);
            playerOrbShard = PlayerController.Instance.orbShard;
            writer.Write(playerOrbShard);
            playerOrb0fill = PlayerController.Instance.manaOrbsHandler.orbFills[0].fillAmount;
            writer.Write(playerOrb0fill);
            playerOrb1fill = PlayerController.Instance.manaOrbsHandler.orbFills[1].fillAmount;
            writer.Write(playerOrb1fill);
            playerOrb2fill = PlayerController.Instance.manaOrbsHandler.orbFills[2].fillAmount;
            writer.Write(playerOrb2fill);

            playerUnlockedWallJump = PlayerController.Instance.unlockedWallJump;
            writer.Write(playerUnlockedWallJump);
            playerUnlockedDash = PlayerController.Instance.unlockedDash;
            writer.Write(playerUnlockedDash);
            playerUnlockedVarJump = PlayerController.Instance.unlockedVarJump;
            writer.Write(playerUnlockedVarJump);

            playerUnlockedSideCast = PlayerController.Instance.unlockedSideCast;
            writer.Write(playerUnlockedSideCast);
            playerUnlockedUpCast = PlayerController.Instance.unlockedUpCast;
            writer.Write(playerUnlockedUpCast);
            playerUnlockedDownCast = PlayerController.Instance.unlockedDownCast;
            writer.Write(playerUnlockedDownCast);

            playerPosition = PlayerController.Instance.transform.position;
            writer.Write(playerPosition.x);
            writer.Write(playerPosition.y);

            lastScene = SceneManager.GetActiveScene().name;
            writer.Write(lastScene);
        }
        Debug.Log("saved player data");
        

    }
    public void LoadPlayerData()
    {
        string savePath = Application.persistentDataPath + "/save.player.data";
        if(File.Exists(savePath) && new FileInfo(savePath).Length > 0)
        {
            using(BinaryReader reader = new BinaryReader(File.OpenRead(Application.persistentDataPath + "/save.player.data")))
            {
                playerHealth = reader.ReadInt32();
                playerMaxHealth = reader.ReadInt32();
                playerHeartShards = reader.ReadInt32();
                playerMana = reader.ReadSingle();
                playerHalfMana = reader.ReadBoolean();
                playerManaOrbs = reader.ReadInt32();
                playerOrbShard = reader.ReadInt32();
                playerOrb0fill = reader.ReadSingle();
                playerOrb1fill = reader.ReadSingle();
                playerOrb2fill = reader.ReadSingle();

                playerUnlockedWallJump = reader.ReadBoolean();
                playerUnlockedDash = reader.ReadBoolean();
                playerUnlockedVarJump = reader.ReadBoolean();

                playerUnlockedSideCast = reader.ReadBoolean();
                playerUnlockedUpCast = reader.ReadBoolean();
                playerUnlockedDownCast = reader.ReadBoolean();

                playerPosition.x = reader.ReadSingle();
                playerPosition.y = reader.ReadSingle();

                lastScene = reader.ReadString();

                SceneManager.LoadScene(lastScene);
                PlayerController.Instance.transform.position = playerPosition;
                PlayerController.Instance.halfMana = playerHalfMana;
                PlayerController.Instance.Health = playerHealth;
                PlayerController.Instance.maxHealth = playerMaxHealth;
                PlayerController.Instance.heartShards = playerHeartShards;
                PlayerController.Instance.Mana = playerMana;
                PlayerController.Instance.manaOrbs = playerManaOrbs;
                PlayerController.Instance.orbShard = playerOrbShard;
                PlayerController.Instance.manaOrbsHandler.orbFills[0].fillAmount = playerOrb0fill;
                PlayerController.Instance.manaOrbsHandler.orbFills[1].fillAmount = playerOrb1fill;
                PlayerController.Instance.manaOrbsHandler.orbFills[2].fillAmount = playerOrb2fill;

                PlayerController.Instance.unlockedWallJump = playerUnlockedWallJump;
                PlayerController.Instance.unlockedDash = playerUnlockedDash;
                PlayerController.Instance.unlockedVarJump = playerUnlockedVarJump;

                PlayerController.Instance.unlockedSideCast = playerUnlockedSideCast;
                PlayerController.Instance.unlockedUpCast = playerUnlockedUpCast;
                PlayerController.Instance.unlockedDownCast = playerUnlockedDownCast;
            }
            Debug.Log("load player data");
            Debug.Log(playerHalfMana);
        }
        else
        {
            Debug.Log("File doesnt exist");
            PlayerController.Instance.halfMana = false;
            PlayerController.Instance.Health = PlayerController.Instance.maxHealth;
            PlayerController.Instance.Mana = 0.5f;
            PlayerController.Instance.heartShards = 0;

            PlayerController.Instance.unlockedWallJump = false;
            PlayerController.Instance.unlockedDash = false;
            PlayerController.Instance.unlockedVarJump = false;
        }
    }

    #endregion

    #region enemy stuff
    public void SaveShadeData()
    {
        using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(Application.persistentDataPath + "/save.shade.data")))
        {
            sceneWithShade = SceneManager.GetActiveScene().name;
            shadePos = Shade.Instance.transform.position;
            shadeRot = Shade.Instance.transform.rotation;

            writer.Write(sceneWithShade);

            writer.Write(shadePos.x);
            writer.Write(shadePos.y);

            writer.Write(shadeRot.x);
            writer.Write(shadeRot.y);
            writer.Write(shadeRot.z);
            writer.Write(shadeRot.w);
        }
    }
    public void LoadShadeData()
    {
        string savePath = Application.persistentDataPath + "/save.shade.data";
        if(File.Exists(savePath) && new FileInfo(savePath).Length > 0)
        {
            using(BinaryReader reader = new BinaryReader(File.OpenRead(Application.persistentDataPath + "/save.shade.data")))
            {
                sceneWithShade = reader.ReadString();
                shadePos.x = reader.ReadSingle();
                shadePos.y = reader.ReadSingle();

                float rotationX = reader.ReadSingle();
                float rotationY = reader.ReadSingle();
                float rotationZ = reader.ReadSingle();
                float rotationW = reader.ReadSingle();
                shadeRot = new Quaternion(rotationX, rotationY, rotationZ, rotationW);
            }
            Debug.Log("Load shade data");
        }
        else
        {
            Debug.Log("Shade doesnt exist");
        }
    }
    #endregion
}