using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AnimationClip anim;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed * Time.deltaTime);
        if (transform.position.z <= -4) Destroy(gameObject);
    }

    public void SetDimensions(float f)
    {
        Animator a = GetComponent<Animator>();
        a.speed = 0;
        a.Play(anim.name, 0, f);
    }

}
