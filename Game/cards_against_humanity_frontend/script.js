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
  items.forEach((item) => {
    itemList.innerHTML += `
      <div class="item">
        <span>${item.name}</span> <!-- Ändere ${item} zu ${item.name}, um den Namen anzuzeigen -->
        <button onclick="updateItem('${item.id}')">Bearbeiten</button>
        <button onclick="deleteItem('${item.id}')">Löschen</button>
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
      body: JSON.stringify({ name: newItem }), // Nur "name" senden, kein "Id"
    });
    if (response.ok) {
      document.getElementById("newItem").value = "";
      loadItems(); // Liste neu laden
    } else {
      console.error("Fehler beim Erstellen des Items:", await response.text());
    }
  } catch (error) {
    console.error("Fehler beim Erstellen des Items:", error);
  }
}

// Item aktualisieren
async function updateItem(id) {
  const updatedName = prompt("Neuen Wert für das Item eingeben:");
  if (!updatedName) return;

  try {
    const response = await fetch(`${apiUrl}/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ name: updatedName }), // Nur das "name"-Feld senden
    });
    if (response.ok)
      loadItems(); // Liste neu laden, falls erfolgreich
    else {
      console.error(
        "Fehler beim Aktualisieren des Items:",
        await response.text()
      );
    }
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
