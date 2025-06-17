import { traducirEstado, renderMensajeError, obtenerFiltrosProyecto, renderCargando, renderTablaVacia } from "../js/utils.js";
import { getFilteredProposals } from "./api/index.js";

document.addEventListener("DOMContentLoaded", () => {
    const tabla = document.getElementById("tabla-proyectos");
    const formFiltro = document.getElementById("form-filtro");

    async function cargarProyectos(filtros = {}) {
        renderCargando(tabla, "Cargando proyectos...");
        try {
            const data = await getFilteredProposals(filtros);
            tabla.innerHTML = "";

            if (!data || data.length === 0) {
                renderTablaVacia(tabla, "No se encontraron proyectos.");
                const contador = document.getElementById("total-proyectos");
                if (contador) contador.textContent = "No hay proyectos disponibles.";
                return;
            }

            data.forEach(p => {
                const fila = document.createElement("tr");
                fila.className = "border-b border-gray-200 dark:border-gray-700";

                const estadoTexto = traducirEstado(p.status);
                const btnVer = `<a href="detalles.html?id=${p.id}" class="bg-green-600 hover:bg-green-700 text-white px-3 py-1 rounded text-sm font-medium">Ver</a>`;
                const btnEditar = p.status === "Observed"
                    ? `<a href="editar_observado.html?id=${p.id}" class="bg-yellow-300 hover:bg-yellow-400 text-black px-3 py-1 rounded text-sm font-medium ml-2">Editar</a>`
                    : "";

                fila.innerHTML = `
                    <td class="truncate max-w-xs whitespace-nowrap px-4 py-2" title="${p.title}">${p.title}</td>
                    <td class="px-4 py-2">${p.type || "Sin tipo"}</td>
                    <td class="px-4 py-2">${estadoTexto}</td>
                    <td class="px-4 py-2">${p.area || "Sin área"}</td>
                    <td class="px-4 py-2">
                        ${btnVer}
                        ${btnEditar}
                    </td>
                `;

                tabla.appendChild(fila);
            });

            const contador = document.getElementById("total-proyectos");
            if (contador) contador.textContent = `Proyectos totales: ${data.length}`;
        } catch (err) {
            const contador = document.getElementById("total-proyectos");
            if (contador) contador.textContent = "Error al obtener proyectos.";
            renderMensajeError(tabla, "No se pudieron cargar los proyectos.");
        }
    }

    // Buscar al enviar el formulario
    formFiltro.addEventListener("submit", e => {
        e.preventDefault();
        const filtros = obtenerFiltrosProyecto();
        cargarProyectos(filtros);
    });

    // Recargar si se borra algún filtro
    ["filtro-titulo", "filtro-estado", "filtro-applicant", "filtro-aprobador"].forEach(id => {
        const campo = document.getElementById(id);
        campo?.addEventListener("input", () => {
            if (campo.value === "") {
                const filtros = obtenerFiltrosProyecto();
                cargarProyectos(filtros);
            }
        });
    });

    // Cargar al inicio
    cargarProyectos();
});
