import type { EntryCategory } from "@/types/types";
import { useApi } from "./useApi";
import { ref, shallowRef } from "vue";

export function useCategories() {
  const api = useApi<EntryCategory | EntryCategory[]>('api/categories')

  const categories = shallowRef<EntryCategory[]>([])
  const loading = ref(false)

  const fetchAll = async () => {
    loading.value = true
    const { data, execute } = api.retrieve()
    await execute()
    categories.value = data.value ?? []
    loading.value = false
  }

  return {
    fetchAll,
    categories,
    loading
  }
}
