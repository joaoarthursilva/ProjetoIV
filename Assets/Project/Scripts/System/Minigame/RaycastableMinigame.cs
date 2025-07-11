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
        public bool SetObjectOutline(Minigame p_minigame)
        {
            bool l_return = false;
            outlinedOBJ = null;
            for (int i = 0; i < list.Length; i++)
            {
                bool l_outline = p_minigame == list[i].minigame;
                if (list[i].outlinedGO == outlinedOBJ && !l_outline) continue;

                list[i].SetOutline(l_outline);
                if (l_outline) outlinedOBJ = list[i].outlinedGO;

                if(!l_return && l_outline) l_return = true;
            }

            return l_return;
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

    bool m_canInteract = false;
    public void OnSetNextMinigame(Minigame p_minigame)
    {
        m_canInteract = objectList.SetObjectOutline(p_minigame);
    }

    public override bool CanInteract()
    {
        return m_canInteract;
    }
}
