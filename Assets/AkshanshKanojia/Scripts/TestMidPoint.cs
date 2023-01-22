using UnityEngine;
using LevelEditor;

public class TestMidPoint : MonoBehaviour
{
    GridManager mang;
    int assignIndex = 0;
    private void Start()
    {
        mang = FindObjectOfType<GridManager>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = mang.cells[assignIndex].midPos;
            assignIndex++;
        }
    }
}
