import { apiGet } from "../js/api/api.js";
import { traducirEstado, renderMensajeError, obtenerFiltrosProyecto, verificarUsuario, renderCargando, renderTablaVacia } from "../js/utils.js";

document.addEventListener("DOMContentLoaded", async () => {
    const tabla = document.getElementById("tabla-proyectos");
    const contador = document.getElementById("total-proyectos");
    const formFiltro = document.getElementById("form-filtro");
    const user = verificarUsuario();

    if (!user) {
        renderMensajeError(tabla, "Usuario no autenticado.");
        contador.textContent = "Error de autenticación.";
        ;
    }

    async function cargarProyectos() {
        renderCargando(tabla, "Cargando proyectos...");
        const filtros = obtenerFiltrosProyecto();
        filtros.approvalUser = user.id;

        const params = new URLSearchParams(filtros).toString();

        try {
            const data = await apiGet(`/Project?${params}`);
            tabla.innerHTML = "";

            if (!data || data.length === 0) {
                renderTablaVacia(tabla);
                contador.textContent = "Pasos pendientes: 0";
                return;
            }

            data.forEach(p => {
                const fila = document.createElement("tr");

                fila.innerHTML = `
                    <td>${p.title}</td>
                    <td>${p.type || "Sin tipo"}</td>
                    <td>${traducirEstado(p.status)}</td>
                    <td>${p.area || "Sin área"}</td>
                    <td>
                        <button onclick="verPaso('${p.id}', '${p.title.replace(/'/g, "\\'")}')"
                            class="bg-green-600 hover:bg-green-700 text-white px-3 py-1 rounded text-sm font-medium">Ver paso</button>
                    </td>
                `;

                tabla.appendChild(fila);
            });

            contador.textContent = `Pasos pendientes: ${data.length}`;
        } catch (err) {
            renderMensajeError(tabla, "No se pudieron cargar los proyectos.");
            contador.textContent = "Error al contar pasos.";
        }
    }

    formFiltro.addEventListener("submit", e => {
        e.preventDefault();
        cargarProyectos();
    });

    // Actualizar cuando se borre el filtro de título
    const inputTitulo = document.getElementById("filtro-titulo");
    if (inputTitulo) {
        inputTitulo.addEventListener("input", () => {
            if (inputTitulo.value === "") {
                cargarProyectos();
            }
        });
    }

    cargarProyectos();
});

window.verPaso = function (id, titulo) {
    localStorage.setItem("tituloProyecto", titulo);
    window.location.href = `ver_paso.html?id=${id}`;
};
