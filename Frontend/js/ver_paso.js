import { apiGet, apiPut } from "./api/api.js";
import { verificarUsuario } from "../js/utils.js";

document.addEventListener("DOMContentLoaded", async () => {
  const params = new URLSearchParams(window.location.search);
  const proyectoId = params.get("id");

  const mensaje = document.getElementById("mensaje");
  const detalle = document.getElementById("detalle-proyecto");
  const form = document.getElementById("form-evaluacion");

  const titulo = localStorage.getItem("tituloProyecto");
  if (titulo) {
    document.getElementById("titulo-proyecto").textContent = titulo;
  }

  if (!proyectoId) {
    mensaje.innerHTML = '<div class="alert alert-danger">Falta el parámetro "id" en la URL.</div>';
    form.style.display = "none";
    return;
  }

  const user = verificarUsuario();
  if (!user) {
    renderMensajeError(tabla, "Usuario no autenticado.");
    contador.textContent = "Error de autenticación.";
    return;
  }

  try {
    const data = await apiGet(`/Project/${proyectoId}`);

    const pasoEvaluable = data.steps.find((s, i, arr) =>
      (s.status?.id === 1 || s.status?.id === 4) && // Pendiente u observado
      s.approverRole?.id === user.role.id &&
      arr.filter(p => p.stepOrder < s.stepOrder).every(p => p.status?.id === 2)
    );

    if (!pasoEvaluable) {
      mensaje.innerHTML = '<div class="alert alert-warning">No tenés pasos para evaluar en este proyecto.</div>';
      form.style.display = "none";
      return;
    }

    detalle.innerHTML = `
      <div class="alert alert-info">
        Evaluando paso n.º <strong>${pasoEvaluable.stepOrder}</strong>
      </div>
    `;

    form.dataset.pasoId = pasoEvaluable.id;
  } catch (err) {
    mensaje.innerHTML = '<div class="alert alert-danger">Error al cargar el proyecto.</div>';
    form.style.display = "none";
  }
});

// Limpiar título al salir
window.addEventListener("beforeunload", () => {
  localStorage.removeItem("tituloProyecto");
});

// Enviar decisión del evaluador
document.querySelectorAll("button[data-accion]").forEach(btn => {
  btn.addEventListener("click", async () => {
    const accion = parseInt(btn.dataset.accion);
    const observacion = document.getElementById("observacion").value.trim();
    const proyectoId = new URLSearchParams(window.location.search).get("id");
    const user = JSON.parse(localStorage.getItem("user"));
    const pasoId = document.getElementById("form-evaluacion").dataset.pasoId;

    if (!observacion) {
      alert("Por favor escribí una observación.");
      return;
    }

    if (!pasoId) {
      alert("No se encontró el paso a evaluar.");
      return;
    }

    const payload = {
      id: parseInt(pasoId),
      user: user.id,
      status: accion,
      observation: observacion
    };

    try {
      await apiPut(`/Project/${proyectoId}/decision`, payload);
      alert("Decisión enviada correctamente.");
      window.location.href = "../pages/aprobar_pasos.html";
    } catch (err) {
      alert("Error: " + err.message);
    }
  });
});
