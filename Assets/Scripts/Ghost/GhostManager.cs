using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public static GhostManager Instance;

    [Header("Префабы призраков")]
    public List<GameObject> ghostPrefabs;
    [Header("Объекты экзорцизма")]
    public List<ExorcismObject> exorcismObjects;
    [Header("Точка спавна")]
    public Transform spawnPoint;

    [Header("Все объекты признаков на сцене")]
    public List<GhostTraitObject> traitObjects;

    private GhostBehaviour currentGhost;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // выключаем всё перед запуском
        foreach (var obj in traitObjects)
        {
            if (obj != null)
                obj.gameObject.SetActive(false);
        }

        SpawnRandomGhost();
    }

    public void SpawnRandomGhost()
    {
        if (ghostPrefabs.Count == 0)
        {
            Debug.LogError("Нет префабов призраков!");
            return;
        }

        int index = Random.Range(0, ghostPrefabs.Count);
        GameObject ghostGO = Instantiate(ghostPrefabs[index], spawnPoint.position, Quaternion.identity);

        currentGhost = ghostGO.GetComponent<GhostBehaviour>();
        
        spriteRenderer = ghostGO.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        if (currentGhost == null)
        {
            Debug.LogError("У префаба нет GhostBehaviour!");
            return;
        }

        ActivateTraits(currentGhost.config);
    }

    private void ActivateTraits(GhostConfig config)
    {
        foreach (var obj in traitObjects)
        {
            if (obj == null) continue;

            bool shouldBeActive = config.traits.Contains(obj.trait);
            obj.gameObject.SetActive(shouldBeActive);
            Debug.Log(obj.gameObject.name + " : " + obj.trait);
            //Debug.Log(obj.gameObject.name + " parent active: " + obj.transform.parent.gameObject.activeSelf);
            StartCoroutine(CheckActive(obj.gameObject));
        }
        
    }
    public void ActivateExorcismObject(GhostConfig config)
    {
        foreach (var obj in exorcismObjects)
        {
            if (obj == null) continue;

            bool shouldBeActive = obj.type == config.correctExorcism;
            obj.gameObject.SetActive(shouldBeActive);

            Debug.Log(obj.gameObject.name + " EXORCISM: " + obj.type);
        }
    }
    private IEnumerator CheckActive(GameObject go)
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log(go.name + " FINAL STATE: " + go.activeSelf);
    }
}