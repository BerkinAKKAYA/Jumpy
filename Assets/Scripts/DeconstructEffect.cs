using UnityEngine;

public class DeconstructEffect : MonoBehaviour
{
    [SerializeField] GameObject prefab = null;

    static GameObject[] pool;

    void Awake() => PopulatePool();

    void PopulatePool(int size=5)
    {
        pool = new GameObject[size];

        for (int i=0; i<size; i++)
        {
            pool[i] = Instantiate(prefab, transform);
            pool[i].SetActive(false);
        }
    }

    public static void Generate(Vector2 position)
    {
        foreach (GameObject effect in pool)
        {
            if(!effect.activeSelf)
            {
                effect.transform.position = position;
                effect.SetActive(true);
                return;
            }
        }

        pool[0].SetActive(false);
        pool[0].transform.position = position;
        pool[0].SetActive(true);
    }
}