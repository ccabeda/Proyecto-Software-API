const BASE_URL = "https://localhost:7298/api";

export async function apiGet(endpoint) {
    const res = await fetch(`${BASE_URL}${endpoint}`, {
        method: "GET",
        headers: getAuthHeaders()
    });
    if (!res.ok) throw new Error(await res.text());
    return await res.json();
}

export async function apiPost(endpoint, data) {
    const res = await fetch(`${BASE_URL}${endpoint}`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            ...getAuthHeaders()
        },
        body: JSON.stringify(data)
    });
    if (!res.ok) throw new Error(await res.text());
    return await res.json();
}

export async function apiPut(endpoint, data) {
    const res = await fetch(`${BASE_URL}${endpoint}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            ...getAuthHeaders()
        },
        body: JSON.stringify(data)
    });
    if (!res.ok) throw new Error(await res.text());
    return await res.json();
}

/*Genera headers con autorización si hay un usuario logueado.*/
function getAuthHeaders() {
    const user = JSON.parse(localStorage.getItem("user"));
    return user?.token ? { Authorization: `Bearer ${user.token}` } : {};
}