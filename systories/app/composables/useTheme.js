import { ref, onMounted } from "vue"

// Dark mode par défaut
const isDark = ref(true)

export function useTheme() {
  const toggleTheme = () => {
    isDark.value = !isDark.value
    if (process.client) {
      if (isDark.value) {
        document.documentElement.classList.add("dark")
      } else {
        document.documentElement.classList.remove("dark")
      }
    }
  }

  const initTheme = () => {
    if (process.client) {
      // Appliquer le dark mode par défaut
      document.documentElement.classList.add("dark")
    }
  }

  // Initialiser au montage
  onMounted(() => {
    initTheme()
  })

  // Initialiser immédiatement côté client
  if (process.client) {
    initTheme()
  }

  return { isDark, toggleTheme }
}
