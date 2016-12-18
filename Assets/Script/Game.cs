using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour{
    public GameObject m_prefab_pine_up;
    public GameObject m_prefab_pine_down;

    public GameObject m_object_pipe_contain;
    public GameObject m_object_bird;
    public GameObject m_object_floor;
    public GameObject m_object_background;

    private BirdControl _bird_control;
    private int _score = 0;

    enum GameState
    {
        InValid = 0,
        Init = 1,
        Runing = 2,
        Stop = 3,
        End = 4,
    }

    private GameState _game_state = GameState.InValid;

    // 底边滚动
    public float m_floor_speed = 10;
    private float _floor_width = 1;
    private void _UpdateFloor()
    {
        Vector3 position = m_object_floor.transform.localPosition;
        float x = position.x;
        x -= m_floor_speed / 100;
        if(x <= - _floor_width/2)
        {
            x = 0;
        }
        position.x = x;
        m_object_floor.transform.localPosition = position;
    }

    // 管子出生，删除，移动
    public float m_pine_speed = 10;
    public float m_pine_start_delta = 0.9f;
    private float _map_width = 10;
    private List<GameObject> _pipes = new List<GameObject>();

    public float m_pine_space = 1.6f;
    private float _NextPipeHigh()
    {
        float high = Random.Range(1.5f,4.5f);
        return high;
    }

    private void _UpdatePipe()
    {
        float delta = m_pine_speed / 100;
        float max_x = 0;
        // 移动现有的管子
        foreach(GameObject obj in _pipes)
        {
            Vector3 position = obj.transform.localPosition;
            // 积分判断
            if (position.x > 0 && position.x - delta <= 0)
            {
                _score++;
            }

            position.x -= delta;
            max_x = position.x;
            obj.transform.localPosition = position;
        }
        // 删除过界的管子
        if(_pipes.Count > 0)
        {
            var obj = _pipes[0];
            if (obj.transform.localPosition.x < - _map_width)
            {
                _pipes.RemoveAt(0);
                GameObject.Destroy(obj);
            }
        }
        // 增加新管子，随机
        if(max_x < _map_width - m_pine_start_delta)
        {
            float next_high = _NextPipeHigh();

            {
                var obj = GameObject.Instantiate<GameObject>(m_prefab_pine_down);
                obj.name = "pipe";
                obj.transform.SetParent(m_object_pipe_contain.transform, false);
                var position = obj.transform.localPosition;
                position.x = _map_width;
                position.y = next_high;
                obj.transform.localPosition = position;
                _pipes.Add(obj);
            }
            {
                var obj = GameObject.Instantiate<GameObject>(m_prefab_pine_up);
                obj.name = "pipe";
                obj.transform.SetParent(m_object_pipe_contain.transform, false);
                var position = obj.transform.localPosition;
                position.x = _map_width;
                position.y = next_high + m_pine_space; 
                obj.transform.localPosition = position;
                _pipes.Add(obj);
            }
        }
    }


    void Start()
    {
        _floor_width = m_object_floor.GetComponent<BoxCollider2D>().size.x;
        _map_width = m_object_background.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        _bird_control = m_object_bird.GetComponent<BirdControl>();

        m_object_pipe_contain.transform.DestroyChildren();
        _game_state = GameState.Init;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Reset();
            return;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartGame();
            return;
        }

        if (_game_state != GameState.Runing)
        {
            return;
        }

        if(!_bird_control.IsDead)
        {
            _UpdateFloor();
            _UpdatePipe();
        }
    }

    public void StartGame()
    {
        // reset 
        Reset();

        _game_state = GameState.Runing;
        _bird_control.StartGame();

        //var root = GameObject.Find("UI Root").transform;
        //var begin_ui = root.Find("BeginUI").gameObject;
        //if (begin_ui) begin_ui.SetActive(false);
        //var end_ui = root.Find("ResultUI").gameObject;
        //if (end_ui) end_ui.SetActive(true);
    }
	

    public void Reset()
    {
        m_object_pipe_contain.transform.DestroyChildren();
        _pipes.Clear();
        _bird_control.Reset();
        _game_state = GameState.Init;
        _score = 0;

        //var root = GameObject.Find("UI Root").transform;
        //var begin_ui = root.Find("BeginUI").gameObject;
        //if(begin_ui) begin_ui.SetActive(true);
        //var end_ui = root.Find("ResultUI").gameObject;
        //if(end_ui) end_ui.SetActive(false);
    }


    public int GetScore()
    {
        return _score/2;
    }
}
