using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour {

    public Vector3 Half;
    public Vector3 target;
    public GameObject Particle;
	// Use this for initialization

	void Start () {
        Half = (target - transform.position) / 2;
        Half.y += 2f;
        StartCoroutine(S());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator S()
    {
        transform.DOMove(Half,0.3f).SetEase(Ease.InQuint);
        yield return new WaitForSecondsRealtime(0.3f);
        StartCoroutine(H());
        yield return 0;
    }
    IEnumerator H()
    {
        transform.DOMove(target, 0.3f).SetEase(Ease.OutQuint);
        yield return new WaitForSecondsRealtime(0.3f);
        StartCoroutine(D());
        yield return 0;
    }
    IEnumerator D()
    {
        Instantiate(Particle, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        yield return 0;
    }
}
