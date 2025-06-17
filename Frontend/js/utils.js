export function traducirEstado(estado) {
    const traducciones = {
        1: "Pendiente",
        2: "Aprobado",
        3: "Rechazado",
        4: "Observado",
        Approved: "Aprobado",
        Rejected: "Rechazado",
        Observed: "Observado",
        Pending: "Pendiente"
    };
    return traducciones[estado] || "Desconocido";
}

// Limpiar cualquier input con botón X
document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll(".btn-clear-input").forEach(btn => {
        btn.addEventListener("click", () => {
            const targetId = btn.getAttribute("data-target");
            const input = document.getElementById(targetId);

            if (input) {
                input.value = "";
                input.focus();
                // Encontrar y ejecutar el submit automático del formulario
                const form = btn.closest("form");
                if (form) {
                    form.dispatchEvent(new Event("submit", { cancelable: true, bubbles: true }));
                }
            }
        });
    });
});

export function renderMensajeError(contenedor, mensaje) {
    contenedor.innerHTML = `
        <tr>
            <td colspan="5">
                <div class="flex justify-center items-center gap-2 py-8 text-red-700 text-sm font-medium">
                    <svg class="w-5 h-5 text-red-600" fill="none" stroke="currentColor" stroke-width="2" viewBox="0 0 24 24">
                        <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12" />
                    </svg>
                    ${mensaje}
                </div>
            </td>
        </tr>`;
}

export function obtenerFiltrosProyecto() {
    const filtros = {};
    const titulo = document.getElementById("filtro-titulo")?.value;
    const estado = document.getElementById("filtro-estado")?.value;
    const applicant = document.getElementById("filtro-applicant")?.value;
    const aprobador = document.getElementById("filtro-aprobador")?.value;

    if (titulo) filtros.title = titulo;
    if (estado) filtros.status = parseInt(estado);
    if (applicant && !isNaN(applicant)) filtros.applicant = parseInt(applicant);
    if (aprobador && !isNaN(aprobador)) filtros.approvalUser = parseInt(aprobador);

    return filtros;
}

export function verificarUsuario() {
    const user = JSON.parse(localStorage.getItem("user"));
    if (!user) {
        alert("Usuario no autenticado.");
        window.location.href = "login.html";
        return null;
    }
    return user;
}

export function renderCargando(contenedor, mensaje = "Cargando...") {
    contenedor.innerHTML = `
        <tr>
            <td colspan="5">
                <div class="flex justify-center items-center gap-2 py-8 text-gray-500 dark:text-gray-400 text-sm font-medium">
                    <svg class="animate-spin h-5 w-5 text-verde-fuerte" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                        <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
                        <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8v8H4z"></path>
                    </svg>
                    ${mensaje}
                </div>
            </td>
        </tr>
    `;
}

export function renderTablaVacia(contenedor, mensaje = "No se encontraron proyectos.", columnas = 5) {
    contenedor.innerHTML = `
        <tr>
            <td colspan="${columnas}">
                <div class="flex justify-center items-center gap-2 py-4 text-gray-500 dark:text-gray-400 text-sm font-medium">
                    <svg class="w-5 h-5 text-gray-400" fill="none" stroke="currentColor" stroke-width="2" viewBox="0 0 24 24">
                        <path stroke-linecap="round" stroke-linejoin="round" d="M9 17v-6h6v6m2 4H7a2 2 0 01-2-2V7a2 2 0 012-2h3l2-2h4l2 2h3a2 2 0 012 2v12a2 2 0 01-2 2z" />
                    </svg>
                    ${mensaje}
                </div>
            </td>
        </tr>
    `;
}
