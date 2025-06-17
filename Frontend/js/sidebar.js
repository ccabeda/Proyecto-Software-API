document.addEventListener("DOMContentLoaded", () => {
  if (!location.pathname.includes("login.html")) {
    const sidebar = document.createElement("div");
    sidebar.id = "sidebar-container";

    const rutaSidebar = location.pathname.includes("/pages/")
      ? "sidebar.html"
      : "pages/sidebar.html";

    fetch(rutaSidebar)
      .then(res => res.text())
      .then(html => {
        sidebar.innerHTML = html;
        document.body.prepend(sidebar);
        inicializarEventosSidebar();

        //ACTIVA LOS ICONOS LUCIDE LUEGO DE INSERTAR EL HTML
        if (window.lucide) {
          lucide.createIcons();
        } else {
          const script = document.createElement("script");
          script.src = "https://unpkg.com/lucide@latest";
          script.onload = () => lucide.createIcons();
          document.body.appendChild(script);
        }
      });
  }

  function inicializarEventosSidebar() {
    const user = JSON.parse(localStorage.getItem("user"));
    const nombre = user?.name || "Usuario";
    const email = user?.email || "No disponible";
    const role = user?.role?.name || "No disponible";

    // Modal usuario
    const userModal = document.getElementById("user-info-modal");
    const btnAbrir = document.getElementById("user-info-btn");
    const btnCerrar = document.getElementById("close-user-info");

    btnAbrir?.addEventListener("click", () => {
      document.getElementById("user-name").textContent = nombre;
      document.getElementById("user-email").textContent = email;
      document.getElementById("user-role").textContent = role;
      userModal.classList.remove("hidden");
      userModal.classList.add("flex");
    });

    btnCerrar?.addEventListener("click", () => {
      userModal.classList.add("hidden");
      userModal.classList.remove("flex");
    });

    // Modal cerrar sesión
    const cerrarSesion = document.getElementById("cerrar-sesion");
    const modalCerrar = document.getElementById("modal-cerrar-sesion");
    const confirmarBtn = document.getElementById("confirmar-salida");
    const cancelarBtn = document.getElementById("cancelar-salida");

    cerrarSesion?.addEventListener("click", (e) => {
      e.preventDefault();
      modalCerrar.classList.remove("hidden");
      modalCerrar.classList.add("flex");
    });

    cancelarBtn?.addEventListener("click", () => {
      modalCerrar.classList.add("hidden");
      modalCerrar.classList.remove("flex");
    });

    confirmarBtn?.addEventListener("click", () => {
      localStorage.clear();
      const rutaLogin = location.pathname.includes("/pages/")
        ? "login.html"
        : "pages/login.html";
      window.location.href = rutaLogin;
    });
  }
});