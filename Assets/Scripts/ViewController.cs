using System.Collections.Generic;
using Events;
using Model;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ViewController : MonoBehaviour
{
    private GameModel _gameModel;
    private Button _button;

    private void Awake()
    {
        _gameModel = new GameModel();
        _button = GameObject.Find("RestartButton").GetComponent<Button>();
        OnListChangeEvent.Register(ViewUptade);
    }

    private void Start()
    {
        InitList();
        _button.onClick.AddListener((() =>
        {
            InitList();
        }));
    }

    private void Update()
    {
        if (Input.GetKeyDown((KeyCode.A)))
        {
            AAction();
            if (AMerge())
            {
                RandomGenerate();
            }
            AAction();
        }
        if (Input.GetKeyDown((KeyCode.S)))
        {
            SAction();
            if (SMerge())
            {
                RandomGenerate();
            }
            SAction();
        }
        if (Input.GetKeyDown((KeyCode.D)))
        {
            DAction();
            if (DMerge())
            {
                RandomGenerate();
            }
            DAction();
        }
        if (Input.GetKeyDown((KeyCode.W)))
        {
            WAction();
            if (WMerge())
            {
                RandomGenerate();
            }
            WAction();
        }
    }

    private void OnDestroy()
    {
        OnListChangeEvent.UnRegister(ViewUptade);
    }

    private void InitList()
    {
        for (int i = 0; i < 16; i++) _gameModel[i] = 0;
        RandomGenerate();
        RandomGenerate();
    }

    private void ViewUptade()
    {
        for (int i = 0; i < 16; i++)
        {
            GameObject mBlock = transform.GetChild(i).gameObject;
            if (_gameModel[i] == 0)
            {
                mBlock.SetActive(false);
            }
            else
            {
                Texture2D img = Resources.Load("image/block_" + _gameModel[i]) as Texture2D;
                SpriteRenderer spr = mBlock.GetComponent<SpriteRenderer>();
                Sprite sp = Sprite.Create(img,spr.sprite.textureRect,new Vector2(0.5f,0.5f));
                spr.sprite = sp;
                mBlock.SetActive(true);
            }
        }
    }

    private void RandomGenerate()
    {
        List<int> emptyList = new List<int>();
        for (int i = 0; i < 16; i++)
        {
            if (_gameModel[i] == 0)
            {
                emptyList.Add(i);
            }
        }

        if (emptyList.Count <= 0)
        {
            GameOver();
            return;
        }

        if (Random.Range(0,4) < 3)
        {
            _gameModel[emptyList[Random.Range(0, emptyList.Count)]] = 2;
        }
        else
        {
            _gameModel[emptyList[Random.Range(0, emptyList.Count)]] = 4;
        }
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
    }

    private bool AMerge()
    {
        bool isMerged = false;
        for (int i = 0; i <= 12; i+=4)
        {
            for (int j = i; j <= i+2 ; j++)
            {
                if (_gameModel[j] == _gameModel[j+1] && _gameModel[j] != 0)
                {
                    _gameModel[j] *= 2;
                    _gameModel[j + 1] = 0;
                    if (!isMerged) isMerged = true;
                }
            }
        }
        return isMerged || AAction();;
    }
    private bool DMerge()
    {
        bool isMerged = false;
        for (int i = 3; i <= 15; i+=4)
        {
            for (int j = i; j >= i-2 ; j--)
            {
                if (_gameModel[j] == _gameModel[j - 1] && _gameModel[j] != 0)
                {
                    _gameModel[j] *= 2;
                    _gameModel[j - 1] = 0;
                    if (!isMerged) isMerged = true;
                }
            }
        }
        return isMerged || DAction();;
    }
    private bool SMerge()
    {
        bool isMerged = false;
        for (int i = 12; i <= 15; i++)
        {
            for (int j = i; j >= i-8 ; j-=4)
            {
                if (_gameModel[j] == _gameModel[j - 4] && _gameModel[j] != 0)
                {
                    _gameModel[j] *= 2;
                    _gameModel[j - 4] = 0;
                    if (!isMerged) isMerged = true;
                }
            }
        }
        
        return isMerged || SAction();;
    }
    private bool WMerge()
    {
        bool isMerged = false;
        for (int i = 0; i <= 3; i++)
        {
            for (int j = i; j <= i+8 ; j+=4)
            {
                if (_gameModel[j] == _gameModel[j+4] && _gameModel[j] != 0)
                {
                    _gameModel[j] *= 2;
                    _gameModel[j + 4] = 0;
                    if (!isMerged) isMerged = true;
                }
            }
        }
        return isMerged || WAction();
    }
    private bool AAction()
    {
        bool isMoved = false;
        for (int i = 3; i <= 15; i+=4)
        {
            for (int j = i-2; j <= i; j++)
            {
                for (int k = i-3; k < j; k++)
                {
                    if (_gameModel[k] == 0)
                    {
                        (_gameModel[k], _gameModel[j]) = (_gameModel[j], _gameModel[k]);
                        if (!isMoved) isMoved = true;
                    }
                }
            }
        }

        return isMoved;
    }
    private bool DAction()
    {
        bool isMoved = false;
        for (int i = 0; i <= 12; i+=4)
        {
            for (int j = i+2; j >= i; j--)
            {
                for (int k = i+3; k > j; k--)
                {
                    if (_gameModel[k] == 0)
                    {
                        (_gameModel[k], _gameModel[j]) = (_gameModel[j], _gameModel[k]);
                        if (!isMoved) isMoved = true;
                    }
                }
            }
        }

        return isMoved;
    }
    private bool SAction()
    {
        bool isMoved = false;
        for (int i = 0; i <= 3; i++)
        {
            for (int j = i+8; j >= i; j-=4)
            {
                for (int k = i+12; k > j; k-=4)
                {
                    if (_gameModel[k] == 0)
                    {
                        (_gameModel[k], _gameModel[j]) = (_gameModel[j], _gameModel[k]);
                        if (!isMoved) isMoved = true;
                    }
                }
            }
        }

        return isMoved;
    }
    private bool WAction()
    {
        bool isMoved = false;
        for (int i = 12; i <= 15; i++)
        {
            for (int j = i-8; j <= i; j+=4)
            {
                for (int k = i-12; k < j; k+=4)
                {
                    if (_gameModel[k] == 0)
                    {
                        (_gameModel[k], _gameModel[j]) = (_gameModel[j], _gameModel[k]);
                        if (!isMoved) isMoved = true;
                    }
                }
            }
        }

        return isMoved;
    }
}