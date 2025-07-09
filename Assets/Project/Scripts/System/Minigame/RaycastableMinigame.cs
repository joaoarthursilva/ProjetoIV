using UnityEngine;
using UnityEngine.Events;

public class RaycastableMinigame : RaycastableObject
{
    public UnityEvent<RaycastableMinigame> OnInteractMinigame;
    public Minigame[] minigames;
    [System.Serializable]
    private struct MinigameObject
    {
        public GameObject outlinedGO;
        public Minigame minigame;
        [NaughtyAttributes.AllowNesting, NaughtyAttributes.ReadOnly] public int initialLayer;

        public void SetOutline(bool p_on)
        {
            if (p_on) Debug.Log("BAAA");
            outlinedGO.layer = p_on ? outlineLayer : initialLayer;
        }

        public void SetInitialLayer()
        {
            initialLayer = outlinedGO.layer;
        }
    }
    [System.Serializable]
    private struct MinigameObjectList
    {
        private GameObject outlinedOBJ;
        public MinigameObject[] list;
        public void SetObjectOutline(Minigame p_minigame)
        {
            outlinedOBJ = null;
            for (int i = 0; i < list.Length; i++)
            {
                bool l_outline = p_minigame == list[i].minigame;
                if (list[i].outlinedGO == outlinedOBJ && !l_outline) continue;

                list[i].SetOutline(l_outline);
                if (l_outline) outlinedOBJ = list[i].outlinedGO;
            }
        }
        public void SetInitial()
        {
            for (int i = 0; i < list.Length; i++)
            {
                list[i].SetInitialLayer();
            }
        }
    }

    [SerializeField] private MinigameObjectList objectList;
    private const int outlineLayer = 6;

    protected override void Start()
    {
        base.Start();
        objectList.SetInitial();
        if (RecipeManager.Instance != null)
            RecipeManager.Instance.SetNextMinigame += OnSetNextMinigame;
    }

    private void OnDestroy()
    {
        if (RecipeManager.Instance != null)
            RecipeManager.Instance.SetNextMinigame -= OnSetNextMinigame;
    }

    public override void Interact()
    {
        base.Interact();

        OnInteractMinigame?.Invoke(this);
    }

    public void OnSetNextMinigame(Minigame p_minigame)
    {
        objectList.SetObjectOutline(p_minigame);
    }
}
