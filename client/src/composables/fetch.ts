import { ref, toValue, watchEffect } from "vue"

export function useFetch(uri: string) {
  const url = `${import.meta.env.VITE_SERVER_URL}${uri}`

  const data = ref(null)
  const error = ref(null)
  const loading = ref(false)

  const fetchData = async () => {
    data.value = null
    error.value = null

    try {
      loading.value = true
      const response = await fetch(toValue(url))
      if (!response.ok) {
        throw new Error(`HTTP ${response.status}`);
      }
      data.value = await response.json()
      loading.value = false
    } catch (error) {
      if (error instanceof Error) {
        console.error('Failed to fetch user:', error.message);
      }
      throw error;
    }
  }

  watchEffect(() => {
    fetchData()
  })

  return { data, error, loading }
}
