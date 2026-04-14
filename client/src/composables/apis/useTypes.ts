import { shallowRef } from 'vue'
import type { EntryType } from '@/types/types'
import { useFetch } from './useFetch'

export function useTypes() {
  const api = useFetch<EntryType[]>()
  const types = shallowRef<EntryType[]>([])

  const fetchAll = async () => {
    const result = await api.execute('api/types', {
      method: 'GET',
    })

    types.value = result ?? []
    return types.value
  }

  return {
    types,
    error: api.error,
    loading: api.loading,
    fetchAll,
  }
}
