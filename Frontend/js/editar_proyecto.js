import { getFilteredProposals } from "./api/index.js";
import { traducirEstado, renderMensajeError, obtenerFiltrosProyecto, renderCargando, renderTablaVacia } from "../js/utils.js";

document.addEventListener("DOMContentLoaded", () => {
  const tabla = document.getElementById("tabla-observados");
  const contador = document.getElementById("total-proyectos");
  const form = document.getElementById("form-filtro");

  async function cargarProyectos(filtros = {}) {
    renderCargando(tabla, "Cargando proyectos...");
    filtros.status = 4; // Observado

    try {
      const data = await getFilteredProposals(filtros);
      tabla.innerHTML = "";

      if (!data || data.length === 0) {
        renderTablaVacia(tabla, "No se encontraron proyectos.", 3);
        contador.textContent = "Proyectos disponibles: 0";
        return;
      }

      data.forEach(p => {
        const fila = document.createElement("tr");
        fila.innerHTML = `
          <td>${p.title}</td>
          <td>${traducirEstado(p.status)}</td>
          <td class="px-4 py-2">
            <a href="editar_observado.html?id=${p.id}" class="bg-yellow-300 hover:bg-yellow-400 text-black px-3 py-1 rounded text-sm font-medium">
              Editar
            </a>
          </td>
        `;
        tabla.appendChild(fila);
      });

      contador.textContent = `Proyectos disponibles: ${data.length}`;
    } catch (err) {
      renderMensajeError(tabla, "No se pudieron cargar los proyectos observados.");
      contador.textContent = "Error al contar proyectos";
    }
  }

  // Buscar con el botón
  form.addEventListener("submit", (e) => {
    e.preventDefault();
    const filtros = obtenerFiltrosProyecto();
    cargarProyectos(filtros);
  });

  // Recargar si se borra el filtro
  const inputTitulo = document.getElementById("filtro-titulo");
  inputTitulo.addEventListener("input", () => {
    if (inputTitulo.value === "") {
      const filtros = obtenerFiltrosProyecto();
      cargarProyectos(filtros);
    }
  });

  cargarProyectos();
});
