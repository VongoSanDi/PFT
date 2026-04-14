import type { EntryCategory } from "@/types/types";
import { shallowRef } from "vue";
import { useFetch } from "./useFetch";

export function useCategories() {
  const api = useFetch<EntryCategory | EntryCategory[]>()
  const categories = shallowRef<EntryCategory[]>([])

  const fetchAll = async () => {
    const result = await api.execute("api/categories", {
      method: 'GET',
    })

    categories.value = result ?? []
    return categories.value
  }

  return {
    categories,
    error: api.error,
    loading: api.loading,
    fetchAll,
  }
}
