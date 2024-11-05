// Setze die URL des Backends auf den richtigen Port
const apiUrl = "http://localhost:5000/api/test"; // Korrekte API-URL

// Alle Items laden und anzeigen
async function loadItems() {
  try {
    const response = await fetch(apiUrl);
    const items = await response.json();
    displayItems(items);
  } catch (error) {
    console.error("Fehler beim Laden der Items:", error);
  }
}

// Items im DOM anzeigen
function displayItems(items) {
  const itemList = document.getElementById("itemList");
  itemList.innerHTML = "";
  items.forEach((item, index) => {
    itemList.innerHTML += `
            <div class="item">
                <span>${item}</span>
                <button onclick="updateItem(${index})">Bearbeiten</button>
                <button onclick="deleteItem(${index})">Löschen</button>
            </div>
        `;
  });
}

// Neues Item erstellen
async function createItem() {
  const newItem = document.getElementById("newItem").value;
  if (!newItem) return alert("Bitte ein Item eingeben.");

  try {
    const response = await fetch(apiUrl, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(newItem),
    });
    if (response.ok) {
      document.getElementById("newItem").value = "";
      loadItems();
    }
  } catch (error) {
    console.error("Fehler beim Erstellen des Items:", error);
  }
}

// Item aktualisieren
async function updateItem(id) {
  const updatedItem = prompt("Neuen Wert für das Item eingeben:");
  if (!updatedItem) return;

  try {
    const response = await fetch(`${apiUrl}/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(updatedItem),
    });
    if (response.ok) loadItems();
  } catch (error) {
    console.error("Fehler beim Aktualisieren des Items:", error);
  }
}

// Item löschen
async function deleteItem(id) {
  try {
    const response = await fetch(`${apiUrl}/${id}`, { method: "DELETE" });
    if (response.ok) loadItems();
  } catch (error) {
    console.error("Fehler beim Löschen des Items:", error);
  }
}

// Items beim Laden der Seite anzeigen
window.onload = loadItems;
