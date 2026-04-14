<script setup lang="ts">
import type { DataTableOptions, Entry, PaginatedResponse } from '@/types/types';
import { ref } from 'vue';

const props = defineProps<{
  entries: PaginatedResponse<Entry>,
  loading: boolean,
  options: DataTableOptions
}>()

const emit = defineEmits(['fetch', 'update:options'])
const headers = [
  {
    title: "Type",
    key: "type.name"
  },
  {
    title: "Category",
    key: "category.name"
  },
  {
    title: "Description",
    key: "description"
  },
  {
    title: "Date",
    key: "date"
  },
  {
    title: "Amount",
    key: "amount"
  },
]

const itemsPerPageOptions = ref([
  { value: 10, title: '10' },
  { value: 25, title: '25' },
  { value: 50, title: '50' },
  { value: 100, title: '100' },
])

</script>
<template>
  <v-data-table-server :options="options" @update:options="$emit('update:options', $event)" :headers="headers"
    :items="entries.data" :items-length="entries.metadata.totalRecords" :page="entries.metadata.pageNumber"
    :loading="loading" item-value="id" :items-per-page-options="itemsPerPageOptions" return-object
    show-current-page></v-data-table-server>
</template>
