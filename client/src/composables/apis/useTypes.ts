import type { EntryType } from "@/types/types";
import { useApi } from "./useApi";
import { ref, shallowRef } from "vue";

export function useTypes() {
  const api = useApi<EntryType | EntryType[]>('api/types')

  const types = shallowRef<EntryType[]>([])
  const loading = ref(false)

  const fetchAll = async () => {
    loading.value = true
    const { data, execute } = api.retrieve()
    await execute()
    types.value = data.value ?? []
    loading.value = false
  }

  return {
    fetchAll,
    types,
    loading
  }
}
