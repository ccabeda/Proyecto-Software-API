document.addEventListener("DOMContentLoaded", () => {
  // Insertar el switch en el div con id "darkmode-toggle"
  const html = `
    <div class="fixed top-4 right-4 z-50">
      <button id="toggleDarkMode" class="relative w-14 h-8 rounded-full bg-gray-300 dark:bg-gray-700 transition-colors">
        <span id="toggleCircle"
          class="absolute left-1 top-1 w-6 h-6 rounded-full bg-yellow-400 dark:bg-gray-200 transition-all duration-300 flex items-center justify-center text-white text-sm">
          ☀️
        </span>
      </button>
    </div>
  `;
  document.getElementById("darkmode-toggle").innerHTML = html;

  const toggleBtn = document.getElementById("toggleDarkMode");
  const toggleCircle = document.getElementById("toggleCircle");

  // Aplicar modo guardado en localStorage
  const modoGuardado = localStorage.getItem("modo");
  if (modoGuardado === "oscuro") {
    document.documentElement.classList.add("dark");
    toggleCircle.classList.add("translate-x-6");
    toggleCircle.textContent = "🌙";
  }

  toggleBtn.addEventListener("click", () => {
    document.documentElement.classList.toggle("dark");
    const isDark = document.documentElement.classList.contains("dark");

    toggleCircle.classList.toggle("translate-x-6", isDark);
    toggleCircle.textContent = isDark ? "🌙" : "☀️";

    localStorage.setItem("modo", isDark ? "oscuro" : "claro");
  });
});