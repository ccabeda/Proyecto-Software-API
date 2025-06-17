import { getFilteredProposals } from "./api/index.js";
import { traducirEstado, renderMensajeError, obtenerFiltrosProyecto, verificarUsuario, renderCargando, renderTablaVacia } from "../js/utils.js";

document.addEventListener("DOMContentLoaded", () => {
  const tabla = document.getElementById("tabla-mis-proyectos");
  const form = document.getElementById("form-filtro");
  const contador = document.getElementById("total-proyectos");
  const user = verificarUsuario();

  if (!user) {
    renderMensajeError(tabla, "Usuario no autenticado.");
    contador.textContent = "Error de autenticación.";
    return;
  }

  async function cargarMisProyectos(filtros = {}) {
    renderCargando(tabla, "Cargando proyectos...");
    filtros.createBy = user.id;

    try {
      const data = await getFilteredProposals(filtros);
      tabla.innerHTML = "";

      if (!data || data.length === 0) {
        renderTablaVacia(tabla);
        contador.textContent = "Proyectos creados: 0";
        return;
      }

      data.forEach(p => {
        const estadoTexto = traducirEstado(p.status);

        const btnVer = `<a href="detalles.html?id=${p.id}" class="bg-green-600 hover:bg-green-700 text-white px-3 py-1 rounded text-sm font-medium">Ver</a>`;
        const btnEditar = p.status === "Observed"
          ? `<a href="editar_observado.html?id=${p.id}" class="bg-yellow-300 hover:bg-yellow-400 text-black px-3 py-1 rounded text-sm font-medium ml-2">Editar</a>`
          : "";

        const fila = document.createElement("tr");
        fila.className = "border-b border-gray-200 dark:border-gray-700";

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

      contador.textContent = `Proyectos creados: ${data.length}`;
    } catch (err) {
      console.error("Error al cargar proyectos:", err);
      renderMensajeError(tabla, "No se pudieron cargar tus proyectos.");
      contador.textContent = "Error al contar proyectos";
    }
  }

  // Filtro manual por botón
  form.addEventListener("submit", e => {
    e.preventDefault();
    const filtros = obtenerFiltrosProyecto();
    cargarMisProyectos(filtros);
  });

  // Recargar al borrar filtros
  ["filtro-titulo", "filtro-estado", "filtro-aprobador"].forEach(id => {
    const campo = document.getElementById(id);
    campo.addEventListener("input", () => {
      if (campo.value === "") {
        const filtros = obtenerFiltrosProyecto();
        cargarMisProyectos(filtros);
      }
    });
  });

  cargarMisProyectos();
});

