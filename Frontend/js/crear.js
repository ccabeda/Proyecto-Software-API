import { getAreas, getProjectTypes, createProjectProposal  } from "./api/index.js";
import { verificarUsuario } from "../js/utils.js";

document.addEventListener("DOMContentLoaded", () => {
    const areaSelect = document.getElementById("area");
    const tipoSelect = document.getElementById("tipo");
    const form = document.getElementById("form-proyecto");
    const mensaje = document.getElementById("mensaje");
    const user = verificarUsuario();

    if (!user) {
        mensaje.innerHTML = `<div class="text-red-700">Debe iniciar sesión.</div>`;
        return;
    }

    async function cargarAreas() {
        try {
            const data = await getAreas();
            const defaultOption = new Option("Seleccione un área...", "", true, true);
            defaultOption.disabled = true;
            areaSelect.appendChild(defaultOption);

            data.forEach(a => {
                areaSelect.appendChild(new Option(a.name, a.id));
            });
        } catch (err) {
            console.error("Error al cargar áreas:", err);
        }
    }

    async function cargarTipos() {
        try {
            const data = await getProjectTypes();
            const defaultOption = new Option("Seleccione un tipo...", "", true, true);
            defaultOption.disabled = true;
            tipoSelect.appendChild(defaultOption);

            data.forEach(t => {
                tipoSelect.appendChild(new Option(t.name, t.id));
            });
        } catch (err) {
            console.error("Error al cargar tipos:", err);
        }
    }

    form.addEventListener("submit", async (e) => {
        e.preventDefault();

        const proyecto = {
            title: document.getElementById("titulo").value,
            description: document.getElementById("descripcion").value,
            amount: parseFloat(document.getElementById("monto").value),
            duration: parseInt(document.getElementById("duracion").value),
            area: parseInt(areaSelect.value),
            type: parseInt(tipoSelect.value),
            user: user.id
        };

        try {
            const data = await createProjectProposal(proyecto);
            mensaje.innerHTML = `<div class="text-green-700">Proyecto creado con éxito (ID: ${data.id})</div>`;
            form.reset();
        } catch (err) {
            console.error("Error al crear proyecto:", err);
            mensaje.innerHTML = `<div class="text-red-700">Error al crear proyecto</div>`;
        }
    });

    cargarAreas();
    cargarTipos();
});
