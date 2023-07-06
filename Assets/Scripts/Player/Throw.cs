using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Throw : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform handPos;
    [SerializeField] private float force = 100f;

    private PlayerControls controls;
    private Animator animator;

    private int poolSize = 10;
    private List<GameObject> pooledObjects;
    public UnityEvent updateText;
    private void Awake()
    {

        animator = GetComponent<Animator>();
        controls = new PlayerControls();
        controls.Enable();
        
    }
    private void Start()
    {
        InitializePool();
        controls.Land.Throw.performed += ThrowObject;

    }
   
    private void InitializePool()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    private GameObject GetPooledObject()
    {
        foreach (GameObject obj in pooledObjects)
        {
            if(!obj.activeInHierarchy) return obj;
        }
        GameObject newObj = Instantiate(prefab);
        newObj.SetActive(false);
        pooledObjects.Add(newObj);
        return newObj;
    }
    private void ThrowObject(InputAction.CallbackContext cotx)
    {
        if(GameManager.Instance.kiwis <= 0) return;

        GameManager.Instance.kiwis--;
        GameObject obj = GetPooledObject();
        animator.SetTrigger("shoot");
        obj.transform.position = handPos.transform.position;
        obj.SetActive(true);
        updateText?.Invoke();

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        if (GetComponent<PlayerMovementScript>().isFacingRight) rb.AddForce(Vector2.right * force );
        else rb.AddForce(Vector2.left * force );

        StartCoroutine(DisableObj(obj, 4));
    }
    
    private IEnumerator DisableObj(GameObject obj,float timeDelay)
    {
       yield return new WaitForSeconds(timeDelay);
       obj.SetActive(false);
    }
    private void OnDestroy()
    {
        controls.Land.Throw.performed -= ThrowObject;
    }
}
