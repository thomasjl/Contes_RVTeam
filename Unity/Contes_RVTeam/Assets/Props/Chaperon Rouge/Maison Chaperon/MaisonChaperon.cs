using UnityEngine;

public class MaisonChaperon : MonoBehaviour {

    [SerializeField]
    Animator doorAnimator, objectAnimator;

    public static MaisonChaperon instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        objectAnimator.gameObject.SetActive(false);
        DropItem();
    }

    public void DropItem()
    {
        doorAnimator.SetTrigger("open");
        objectAnimator.gameObject.SetActive(true);
        objectAnimator.SetTrigger("drop");
    }
}
