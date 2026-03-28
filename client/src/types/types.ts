export interface Entry {
  id: number,
  type: EntryType | null,
  category: EntryCategory | null,
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
