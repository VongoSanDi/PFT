import { ref } from 'vue'

export function useFetch<T>() {
  const data = ref<T | null>(null)
  const error = ref<Error | null>(null)
  const loading = ref(false)

  const execute = async (endpoint: string, init?: RequestInit) => {
    data.value = null
    error.value = null
    loading.value = true

    try {
      const url = import.meta.env.VITE_SERVER_URL + endpoint
      const response = await fetch(url, init)

      if (!response.ok) {
        throw new Error(`Response status: ${response.status}`)
      }

      if (init?.method == 'POST') {
        return null
      }

      const json = await response.json()
      data.value = json
      return data.value
    } catch (err: unknown) {
      error.value = err instanceof Error ? err : new Error(String(err))
      return null
    } finally {
      loading.value = false
    }
  }

  return {
    data,
    error,
    loading,
    execute,
  }
}
