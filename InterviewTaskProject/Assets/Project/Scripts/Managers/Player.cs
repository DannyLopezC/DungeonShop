using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D _collider;
    private Vector3 _moveDelta;

    public int money;

    public List<Clothes> clothes;

    private int _clothesId;

    public int clothesId
    {
        get { return _clothesId; }
        set
        {
            _clothesId = value;
            ChangeClothing(_clothesId);
        }
    }

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        _moveDelta = Vector3.zero;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //set delta
        _moveDelta = new Vector3(x, y);

        //change facing direction
        if (_moveDelta.x > 0) transform.localScale = Vector3.one;
        else if (_moveDelta.x < 0) transform.localScale = new Vector3(-1, 1, 1);

        //movement
        transform.Translate(_moveDelta * Time.deltaTime);
    }

    public void ChangeClothing(int id) => GetComponent<SpriteRenderer>().sprite = clothes.Find(w => w.id == Mathf.Clamp(id, 0, clothes.Count - 1)).sprite;
}
