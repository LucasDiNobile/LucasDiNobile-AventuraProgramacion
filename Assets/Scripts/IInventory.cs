using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInventory : MonoBehaviour
{
    public GameObject craftingObject;
    public GameObject inventoryObject;
    public List<ISlot> inventory;
    public List<string> crafteos;
    public Transform inventoryUI, craftingUI;
    public ISlotUI slotUIPrefab, craftingPrefab;
    public Camera camaraPrimeraPersona;
    public GameObject ironSword, ironPicaxe, goldPicaxe, goldSword;
    public Transform spawnItem;
    public GameObject building;
    public Transform buildPosition;
    

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown("f")) Instantiate(building, buildPosition.position, Quaternion.identity);

        if (Input.GetKeyDown("e"))
        {
            Ray ray = camaraPrimeraPersona.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.TryGetComponent(out IObject obj))
            {
                Add(obj);
            }
        }
        if (Input.GetKeyDown("e"))
           
            if (Physics.Raycast(camaraPrimeraPersona.transform.position, camaraPrimeraPersona.transform.forward, out RaycastHit hit, 5f) && hit.collider.CompareTag("CraftingTable"))
            {
                craftingObject.SetActive(!craftingObject.activeSelf);

                 if (craftingObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Time.timeScale = 0;
            }
                else
                {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
                }
            }
        if (Input.GetKeyDown("i"))
        {
            inventoryObject.SetActive(!inventoryObject.activeSelf);

            if (inventoryObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Time.timeScale = 0;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
        }


    }

    public void Add(IObject item)
    {
        var newSlot = new ISlot(item.name, item.quantity, item.stackable);
        if (Find(item.name) == null || !Find(item.name).stackable)
        {
            inventory.Add(newSlot);
        }
        else
        {
            Find(item.name).quantity += item.quantity;
        }
        item.gameObject.SetActive(false);
        UpdateUI();
    }

    public ISlot Find(string name) => inventory.Find((item) => item.name == name);

    public void Remove(string name, int quantity)
    {
        var slot = Find(name);

        if (slot.quantity - quantity <= 0)
            inventory.Remove(Find(name));
        else
            slot.quantity -= quantity;

        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (Transform child in inventoryUI) if (child.gameObject != slotUIPrefab.gameObject) Destroy(child.gameObject);
        foreach (var item in inventory)
        {
            ISlotUI slot = Instantiate(slotUIPrefab.gameObject, inventoryUI).GetComponent<ISlotUI>();
            slot.itemName.text = item.name + " x " + item.quantity;
            slot.delete.onClick.AddListener(() => Remove(item.name, item.quantity));
            slot.removeOne.onClick.AddListener(() => Remove(item.name, 1));
            slot.gameObject.SetActive(true);
        }
        foreach (Transform child in craftingUI) if (child.gameObject != craftingPrefab.gameObject) Destroy(child.gameObject);
        AsignarCrafteos();
        foreach (var item in crafteos)
        {
            ISlotUI slot = Instantiate(craftingPrefab.gameObject, craftingUI).GetComponent<ISlotUI>();
            slot.itemName.text = item;
            slot.crafting.onClick.AddListener(() => Craftear(item));
            slot.gameObject.SetActive(true);
        }
    }

    void AsignarCrafteos()
    {
        bool Palo = false;
        bool Iron = false;
        bool Gold = false;
        bool Wood = false;
        crafteos.Clear();

        foreach (var i in inventory)
        {
            if (i.name == "Palo") Palo = true;
            if (i.name == "Iron") Iron = true;
            if (i.name == "Gold") Gold = true;
            if (i.name == "Wood") Wood = true;
        }

        if (Palo && Iron) crafteos.Add("Iron Picaxe");
        if (Gold && Wood) crafteos.Add("Gold Sword");
        if (Palo && Gold) crafteos.Add("Gold Picaxe");
        if (Wood && Iron) crafteos.Add("Iron Sword");
    }

    void Craftear(string craft)
    {
        if(craft == "Iron Picaxe")
        {
            for(int i = 0; i < inventory.Count; ++i)
                if (inventory[i].name == "Palo") Remove(inventory[i].name, inventory[i].quantity);
            for (int i = 0; i < inventory.Count; ++i)
                if (inventory[i].name == "Iron") Remove(inventory[i].name, inventory[i].quantity);

            Instantiate(ironPicaxe, spawnItem.position, Quaternion.identity);
        }
        if (craft == "Gold Sword")
        {
            for (int i = 0; i < inventory.Count; ++i)
                if (inventory[i].name == "Gold") Remove(inventory[i].name, inventory[i].quantity);
            for (int i = 0; i < inventory.Count; ++i)
                if (inventory[i].name == "Wood") Remove(inventory[i].name, inventory[i].quantity);

            Instantiate(goldSword, spawnItem.position, Quaternion.identity);
        }
        if (craft == "Gold Picaxe")
        {
            for (int i = 0; i < inventory.Count; ++i)
                if (inventory[i].name == "Palo") Remove(inventory[i].name, inventory[i].quantity);
            for (int i = 0; i < inventory.Count; ++i)
                if (inventory[i].name == "Gold") Remove(inventory[i].name, inventory[i].quantity);

            Instantiate(goldPicaxe, spawnItem.position, Quaternion.identity);
        }
        if (craft == "Iron Sword")
        {
            for (int i = 0; i < inventory.Count; ++i)
                if (inventory[i].name == "Wood") Remove(inventory[i].name, inventory[i].quantity);
            for (int i = 0; i < inventory.Count; ++i)
                if (inventory[i].name == "Iron") Remove(inventory[i].name, inventory[i].quantity);

            Instantiate(ironSword, spawnItem.position, Quaternion.identity);
        }
    }

    [System.Serializable]
    public class ISlot
    {
        public string name;
        public int quantity;
        public bool stackable;

        public ISlot(string name, int quantity, bool stackable)
        {
            this.name = name;
            this.quantity = quantity;
            this.stackable = stackable;
        }
    }

}


