import type { CreateEntry, Entry } from "@/types/types";
import { useApi } from "./useApi";
import { ref, shallowRef } from "vue";

export function useEntries() {
  const api = useApi<Entry | Entry[]>('api/entries')

  const entries = shallowRef<Entry[]>([])
  const loading = ref(false)

  const fetchAll = async () => {
    loading.value = true
    const { data, execute } = api.retrieve()
    await execute()
    entries.value = data.value ?? []
    loading.value = false
  }

  const create = async (payload: CreateEntry) => {
    loading.value = true
    const { execute } = api.create(payload);
    await execute()

  }

  return {
    fetchAll,
    create,
    entries,
    loading
  }
}
