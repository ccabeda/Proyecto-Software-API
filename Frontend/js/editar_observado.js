import { getProjectProposalById, updateProjectProposal } from "./api/index.js";

document.addEventListener("DOMContentLoaded", () => {
  const params = new URLSearchParams(window.location.search);
  const id = params.get("id");
  const mensaje = document.getElementById("mensaje");
  const form = document.getElementById("form-editar");

  const inputTitulo = document.getElementById("titulo");
  const inputDescripcion = document.getElementById("descripcion");
  const inputDuracion = document.getElementById("duracion");

  if (!id) {
    mensaje.innerHTML = `<div class="text-red-700">ID no especificado en la URL.</div>`;
    return;
  }

  // Función para cargar el proyecto
  async function cargarProyecto() {
    try {
      const data = await getProjectProposalById(id);

      if (data.notFound) {
        mensaje.innerHTML = `<div class="text-red-700">Proyecto no encontrado.</div>`;
        return;
      }

      inputTitulo.value = data.title || "";
      inputDescripcion.value = data.description ?? "";
      inputDuracion.value = data.duration ?? "";
    } catch (err) {
      mensaje.innerHTML = `<div class="text-red-700">Error al cargar proyecto: ${err.message}</div>`;
    }
  }

  // ✅ Guardar cambios
  form.addEventListener("submit", async (e) => {
    e.preventDefault();

    const payload = {
      title: inputTitulo.value,
      description: inputDescripcion.value,
      duration: parseInt(inputDuracion.value)
    };

    try {
      await updateProjectProposal(id, payload);
      mensaje.innerHTML = `<div class="text-green-700">Proyecto actualizado correctamente.</div>`;
    } catch (err) {
      mensaje.innerHTML = `<div class="text-red-700">Error al guardar: ${err.message}</div>`;
    }
  });

  cargarProyecto();
});
