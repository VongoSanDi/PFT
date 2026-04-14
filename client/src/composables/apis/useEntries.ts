import type { CreateEntry, DataTableOptions, Entry, PaginatedResponse } from "@/types/types";
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

  const fetchAll = async (pagination: DataTableOptions) => {
    console.log('fetchAllEntries', pagination);

    const paramsObj = { pageNumber: `${pagination.page}`, pageSize: `${pagination.itemsPerPage}` }
    const params = new URLSearchParams(paramsObj);

    const endpoint = `api/entries?${params}`
    console.log('e', endpoint);


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
