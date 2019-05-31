using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    enum State {
        Change,
        Origin,
        PingPong
    };

    State state = State.Origin;

    Color origin;

    Renderer renderer;

    float time;
    float elapsedTime = 0.0f;
    bool isEnd = false;
    bool pingpong = false;

    System.Func<Color, Color, float> function;
    System.Action action;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        origin = renderer.material.color;
    }

    private void Update()
    {
        if (!isEnd && action != null)
        {
            time += Time.deltaTime;
            time = Mathf.Clamp01(time);
            if (time >= 1.0f)
                isEnd = true;
            Debug.Log(time);

            action();
        }
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(100, 100, 200, 200), "Change"))
        {
            ChangeState(State.Change);
        }
        if (GUI.Button(new Rect(300, 100, 200, 200), "Origin"))
        {
            ChangeState(State.Origin);
        }
        if (GUI.Button(new Rect(500, 100, 200, 200), "PingPong"))
        {
            ChangeState(State.PingPong);
        }
    }    

    void ChangeState(State state)
    {
        Setting();
        switch (state)
        {
            case State.Change:
                action = ChangeToWhite;
                break;

            case State.Origin:
                action = ChangeToOrigin;
                break;

            case State.PingPong:
                action = PingPong;
                break;
        }
    }

    void Setting()
    {
        time = 0;
        isEnd = false;
    }

    void ChangeToWhite()
    {
        renderer.material.color = Color.Lerp(origin, Color.white, time);
    }

    void ChangeToOrigin()
    {
        renderer.material.color = Color.Lerp(Color.white, origin, time);
    }

    void PingPong()
    {
        renderer.material.color = Color.Lerp(origin, Color.white, time);
        if (isEnd)
            ChangeState(State.Origin);
    }
}
