import type { CreateEntry, EntriesParams, Entry, PaginatedResponse } from "@/types/types";
import { shallowRef } from "vue";
import { useFetch } from "./useFetch";

export function useEntries() {
  const api = useFetch<PaginatedResponse<Entry>>()

  const entries = shallowRef<PaginatedResponse<Entry>>({
    data: [],
    metadata: {
      hasNextPage: false,
      hasPreviousPage: false,
      pageNumber: 1,
      pageSize: 10,
      totalPages: 1,
      totalRecords: 1
    }
  })

  const fetchAll = async (params: EntriesParams) => {
    console.log('fetchAllEntries', params);

    const paramsObj = { pageNumber: `${params.page}`, pageSize: `${params.itemsPerPage}`, period: `${params.period}` }
    const searchParams = new URLSearchParams(paramsObj);

    const endpoint = `api/entries?${searchParams}`

    const result = await api.execute(endpoint, {
      method: 'GET',
    })

    entries.value = result ?? []
    return entries.value
  }

  const create = async (payload: CreateEntry) => {
    await api.execute('api/entries', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(payload)
    })
  }

  return {
    entries,
    error: api.error,
    loading: api.loading,
    fetchAll,
    create
  }
}
