export interface Entry {
  id: number,
  type: EntryType | null,
  typeId: number,
  category: EntryCategory | null,
  categoryId: number,
  amount: number,
  date: Date,
  description: string
}

export interface CreateEntry {
  typeId: number,
  categoryId: number,
  amount: number,
  date: Date,
  description: string
}

export interface EntryType {
  id: number,
  name: string,
  description: string
}

export interface EntryCategory {
  id: number,
  name: string,
  description: string
}

export interface PaginatedResponse<T> {
  data: T[],
  metadata: Metadata
}

export interface Metadata {
  pageNumber: number,
  pageSize: number
  totalRecords: number,
  totalPages: number,
  hasNextPage: boolean,
  hasPreviousPage: boolean
}

export interface PaginationOptions {
  pageNumber: number,
  pageSize: number
}

export interface DataTableOptions {
  page: number,
  itemsPerPage: number,
  sortBy: unknown[],
  search: undefined
}

export interface EntriesParams extends DataTableOptions {
  period: string
}
