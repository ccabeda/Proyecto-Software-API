import { getUsers } from "./api/index.js";

document.addEventListener("DOMContentLoaded", () => {
  const form = document.getElementById("login-form");
  const inputEmail = document.getElementById("email");
  const mensaje = document.getElementById("mensaje-error");

  // Si ya está logueado, redirigir automáticamente
  const yaLogueado = JSON.parse(localStorage.getItem("user"));
  if (yaLogueado) {
    window.location.href = "index.html";
    return;
  }

  form.addEventListener("submit", async (e) => {
    e.preventDefault();
    mensaje.textContent = ""; // limpiar mensaje anterior

    const email = inputEmail.value.trim();
    if (!email) {
      mensaje.textContent = "Ingresá un email.";
      return;
    }

    try {
      const users = await getUsers();
      const user = users.find(u => u.email === email);

      if (!user) {
        mensaje.textContent = "Usuario no encontrado. Intente nuevamente.";
        form.reset();
        return;
      }

      localStorage.setItem("user", JSON.stringify(user));
      localStorage.setItem("nombreUsuario", user.name);
      window.location.href = "index.html";
    } catch (err) {
      mensaje.textContent = "Error al iniciar sesión: " + err.message;
    }
  });

  // Limpiar mensaje de error si empieza a tipear de nuevo
  inputEmail.addEventListener("input", () => {
    mensaje.textContent = "";
  });
});
