using UnityEngine;
using UnityEngine.UI;

public class CloseUi : MonoBehaviour

{
    [SerializeField] private GameObject _targetObject;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (_targetObject != null)
            {
                if (_targetObject.activeSelf != true)
                {
                    _targetObject.SetActive(true);
                }
                else
                {
                    _targetObject.SetActive(false);
                }
            }
        });
    }

}