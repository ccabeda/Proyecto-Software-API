import { getProjectProposalById  } from "./api/index.js";
import { traducirEstado, renderMensajeError } from "../js/utils.js";

document.addEventListener("DOMContentLoaded", async () => {
    const params = new URLSearchParams(window.location.search);
    const id = params.get("id");
    const mensaje = document.getElementById("mensaje");

    if (!id) {
        renderMensajeError(mensaje, "ID de proyecto no especificado.");
        return;
    }

    try {
        const p = await getProjectProposalById(id);

        if (p.notFound) {
            renderMensajeError(mensaje, "Proyecto no encontrado.");
            return;
        }

        document.getElementById("title").value = p.title;
        document.getElementById("description").value = p.description;
        document.getElementById("status").value = traducirEstado(p.approvalStatus?.name);
        document.getElementById("area").value = p.area?.name || "Desconocido";
        document.getElementById("type").value = p.projectType?.name || "Desconocido";
        document.getElementById("estimatedAmount").value = "$" + p.amount;
        document.getElementById("estimatedDuration").value = p.duration;
        document.getElementById("createBy").value = p.user?.name || "Sin usuario";

        const stepsBody = document.getElementById("steps-body");
        stepsBody.innerHTML = "";

        p.steps.forEach((paso, index) => {
            const fila = document.createElement("tr");

            fila.innerHTML = `
                <td>${index + 1}</td>
                <td>${paso.approverRole?.name || "-"}</td>
                <td>${paso.approverUser?.name || "-"}</td>
                <td>${paso.observations || "-"}</td>
                <td>${traducirEstado(paso.status?.name) || "-"}</td>
            `;

            stepsBody.appendChild(fila);
        });
    } catch (err) {
        renderMensajeError(mensaje, err.message || "Error al cargar el proyecto.");
    }
});
