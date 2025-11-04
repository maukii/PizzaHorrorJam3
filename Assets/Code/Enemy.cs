using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool running = false;


    public void ToggleRunning()
    {
        var animator = GetComponent<Animator>();
        running = !running;
        animator.SetBool("run", running);
    }
}
