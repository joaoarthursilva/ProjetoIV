using System;
using System.Collections;
using System.Collections.Generic;
using ProjetoIV.RatInput;
using ProjetoIV.Util;
using RatSpeak;
using UnityEngine;

public class CustomerBehaviour : MonoBehaviour
{
    public Action<bool> OnOrderDelivered;
    public Collider Collider;

    [SerializeField] private List<GameObject> m_models;
    [SerializeField] private Vector3 m_spawnPosition;
    [SerializeField] private Customer m_customer;
    [SerializeField] private Animator m_currentAnimator;
    private DialogID m_currentDialogId;

    private bool m_isNewCustomer;

    void Start()
    {
        DialogManager.Instance.onEndDialog += OnDialogEnd;
    }

    public void SpawnCustomer(Customer p_customer)
    {

        Debug.Log("start SpawnCustomer");
        Debug.Log(p_customer.name);
        m_isNewCustomer = true;

        m_customer = p_customer;

        StartCoroutine(OnSpawnCustomer());
    }

    private IEnumerator OnSpawnCustomer()
    {
        yield return new WaitForSeconds(m_customer.timeToAppear);

        transform.position = m_spawnPosition;

        if (m_customer.character != null && m_customer.character.characterId != RatSpeak.CharacterId.NONE)
        {
            foreach (var model in m_models) model.SetActive(false);
            m_models[(int)m_customer.character.characterId - 1].SetActive(true);
            m_currentAnimator = m_models[(int)m_customer.character.characterId - 1].GetComponent<Animator>();

            Debug.Log("end SpawnCustomer");
        }
    }

    public void OnInteractWithRaycastableObject()
    {
        Debug.Log("aaa");
        Debug.Log(m_isNewCustomer);
        RatInput.Instance.SetMap(Map.DIALOG);
        RatInput.ShowInputUIElement(InputID.NONE);
        if (m_isNewCustomer)
        {
            if (m_customer.dialogs.Count > 1)
            {
                DialogManager.Instance.ShowDialog(m_customer.dialogs.Find((x) => x.id == DialogID.PEDIDO).key);
                m_currentDialogId = DialogID.PEDIDO;
                m_isNewCustomer = false;
            }
            else
            {
                DialogManager.Instance.ShowDialog(m_customer.dialogs.Find((x) => x.id == DialogID.UNICO).key);
                m_currentDialogId = DialogID.UNICO;
                m_isNewCustomer = false;
            }
        }
        else if (PlayerInventory.Instance.CurrentIngredient != null)
        {
            DialogManager.Instance.ShowDialog(m_customer.dialogs.Find((x) => x.id == DialogID.ENTREGA).key);
            m_currentDialogId = DialogID.ENTREGA;
        }
        else
        {
            DialogManager.Instance.ShowDialog(m_customer.dialogs.Find((x) => x.id == DialogID.ESPERA).key);
            m_currentDialogId = DialogID.ESPERA;
        }
    }

    public void CheckOrder(Ingredient p_ingredient)
    {
        if (p_ingredient == m_customer.ingredient || m_customer.ingredient == null)
        {
            PlayerInventory.Instance.SetCurrentInventory(null);
            DialogManager.Instance.ShowDialog(m_customer.dialogs.Find((x) => x.id == DialogID.RESULTADO_BOM).key);
            m_currentDialogId = DialogID.RESULTADO_BOM;
            // OnOrderDelivered(true);
        }
        else
        {
            PlayerInventory.Instance.SetCurrentInventory(null);
            DialogManager.Instance.ShowDialog(m_customer.dialogs.Find((x) => x.id == DialogID.RESULTADO_RUIM).key);
            m_currentDialogId = DialogID.RESULTADO_RUIM;
            // OnOrderDelivered(false);
        }
    }

    private void OnDialogEnd()
    {
        RatInput.Instance.SetMap(Map.KITCHEN);
        if (m_currentDialogId == DialogID.ENTREGA) CheckOrder(PlayerInventory.Instance.CurrentIngredient);
        else if (m_currentDialogId == DialogID.PEDIDO)
        {
            if (ArrowIndicator.Instance != null)
                ArrowIndicator.Instance.Show(new Vector3(1.329f, 1.5f, -9.885f));

            RecipeManager.Instance.SetInstructionToBook();
        }
        else if (m_currentDialogId == DialogID.RESULTADO_BOM || m_currentDialogId == DialogID.UNICO)
        {
            Debug.Log("fim cabo vai embora");
            m_currentAnimator.Play("Anim_Exit");
        }
    }
}